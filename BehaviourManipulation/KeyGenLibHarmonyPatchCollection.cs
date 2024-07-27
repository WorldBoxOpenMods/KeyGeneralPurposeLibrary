using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using KeyGeneralPurposeLibrary.Assets;
using KeyGeneralPurposeLibrary.Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

// ReSharper disable InconsistentNaming
// ReSharper disable RedundantAssignment

namespace KeyGeneralPurposeLibrary.BehaviourManipulation {
  public class KeyGenLibHarmonyPatchCollection : KLibComponent {
    private static readonly Harmony Harmony = new Harmony(KeyGeneralPurposeLibraryConfig.PluginGuid);
    private static int TargetFrameRate { get; set; } = 60;
    private static float DeltaTime { get; set; } = 1.0f;
    private static bool AllowMouseDrag { get; set; } = true;
    private static bool TraitsChanged { get; set; }
    public static bool DisableBoatMovementOnIce { get; private set; }
    private static int RandomSeed { get; set; }
    public static bool UseFixedRandomSeed { get; private set; }
    public static bool CrabzillaIsSpawned { get; private set; }
    public static int CrabzillaArmExplosionRadius { private get; set; } = 4;

    private static System.Random _random = new System.Random();

    private static System.Random _listExtensionsRandom = new System.Random();


    public void PatchTargetFramerateSetter() {
      TargetFrameRate = Application.targetFrameRate;
      MethodInfo original = AccessTools.PropertySetter(typeof(Application), nameof(Application.targetFrameRate));
      MethodInfo prefix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(TargetFramerateSetter_Prefix));
      Harmony.Patch(original, new HarmonyMethod(prefix));
    }

    public void PatchDeltaTimeGetter() {
      DeltaTime = 1;
      MethodInfo original = AccessTools.PropertyGetter(typeof(Time), nameof(Time.deltaTime));
      MethodInfo postfix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(DeltaTimeGetter_Postfix));
      Harmony.Patch(original, null, new HarmonyMethod(postfix));
    }

    public void PatchCheckTraitButtonCreation() {
      MethodInfo original = AccessTools.Method(typeof(TraitsWindow), nameof(TraitsWindow.checkTraitButtonCreation));
      MethodInfo prefix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(checkTraitButtonCreation_Prefix));
      Harmony.Patch(original, new HarmonyMethod(prefix));
    }

    public void PatchGetSprite_Trait() {
      MethodInfo original = AccessTools.Method(typeof(ActorTrait), nameof(ActorTrait.getSprite));
      MethodInfo prefix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(getSprite_Trait_Prefix));
      Harmony.Patch(original, new HarmonyMethod(prefix));
    }

    public void PatchGetSprite_Item() {
      MethodInfo original = AccessTools.Method(typeof(ItemAsset), nameof(ItemAsset.getSprite));
      MethodInfo prefix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(getSprite_Item_Postfix));
      Harmony.Patch(original, null, new HarmonyMethod(prefix));
    }

    public void PatchGetSprite_Mood() {
      MethodInfo original = AccessTools.Method(typeof(MoodAsset), nameof(MoodAsset.getSprite));
      MethodInfo prefix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(getSprite_Mood_Prefix));
      Harmony.Patch(original, new HarmonyMethod(prefix));
    }

    public void PatchUpdateMouseDrag() {
      MethodInfo original = AccessTools.Method(typeof(MoveCamera), nameof(MoveCamera.updateMouseCameraDrag));
      MethodInfo prefix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(updateMouseCameraDrag_Prefix));
      Harmony.Patch(original, new HarmonyMethod(prefix));
    }

    public void PatchIsGoodForBoat() {
      MethodInfo original = AccessTools.Method(typeof(WorldTile), nameof(WorldTile.isGoodForBoat));
      MethodInfo postfix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(isGoodForBoat_Postfix));
      Harmony.Patch(original, null, new HarmonyMethod(postfix));
    }

    public void PatchUpdate_Giantzilla() {
      MethodInfo original = AccessTools.Method(typeof(Giantzilla), nameof(Giantzilla.update));
      MethodInfo postfix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(update_Giantzilla_Postfix));
      Harmony.Patch(original, null, new HarmonyMethod(postfix));
    }

    public void PatchMapGeneration() {
      Debug.Log("Patching MapGenerator.schedulePerlinNoiseMap");
      MethodInfo original = AccessTools.Method(typeof(MapGenerator), nameof(MapGenerator.schedulePerlinNoiseMap));
      MethodInfo prefix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(schedulePerlinNoiseMap_Prefix));
      MethodInfo postfix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(schedulePerlinNoiseMap_Postfix));
      Harmony.Patch(original, new HarmonyMethod(prefix), new HarmonyMethod(postfix));
      // devs, why did you fix the typo in ApplyPerlinNoice? :(
      original = AccessTools.Method(typeof(GeneratorTool), nameof(GeneratorTool.ApplyPerlinNoise));
      prefix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(ApplyPerlinNoise_Prefix));
      Harmony.Patch(original, new HarmonyMethod(prefix));
      original = AccessTools.Method(typeof(Random), nameof(Random.Range), new[] { typeof(int), typeof(int) });
      postfix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(Range_Postfix));
      Harmony.Patch(original, null, new HarmonyMethod(postfix));
      original = AccessTools.Method(typeof(Toolbox), nameof(Toolbox.randomBool));
      postfix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(randomBool_Postfix));
      Harmony.Patch(original, null, new HarmonyMethod(postfix));
      original = AccessTools.Method(typeof(TextureScale), nameof(TextureScale.Bilinear));
      prefix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(Bilinear_Prefix));
      Harmony.Patch(original, new HarmonyMethod(prefix));
    }

    public void PatchDamageWorld_CrabArm() {
      MethodInfo original = AccessTools.Method(typeof(CrabArm), nameof(CrabArm.damageWorld));
      MethodInfo transpiler = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(CrabArm_damageWorld_Transpiler));
      Harmony.Patch(original, null, null, new HarmonyMethod(transpiler));
    }

    public void PatchPartnerTraitAdditions() {
      MethodInfo original = AccessTools.Method(typeof(ActorBase), nameof(ActorBase.addTrait));
      MethodInfo postfix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(ActorBase_addTrait_Postfix));
      Harmony.Patch(original, null, new HarmonyMethod(postfix));
      original = AccessTools.Method(typeof(ActorBase), nameof(ActorBase.removeTrait));
      postfix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(ActorBase_removeTrait_Postfix));
      Harmony.Patch(original, null, new HarmonyMethod(postfix));
      original = AccessTools.Method(typeof(Actor), nameof(Actor.killHimself));
      MethodInfo prefix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(killHimself_Prefix));
      Harmony.Patch(original, new HarmonyMethod(prefix));
      original = AccessTools.Method(typeof(ActorData), nameof(ActorData.addTrait));
      postfix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(ActorData_addTrait_Postfix));
      Harmony.Patch(original, null, new HarmonyMethod(postfix));
      original = AccessTools.Method(typeof(ActorData), nameof(ActorData.removeTrait));
      postfix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(ActorData_removeTrait_Postfix));
      Harmony.Patch(original, null, new HarmonyMethod(postfix));
    }

    public void PatchClanTraitAdditions() {
      MethodInfo original = AccessTools.Method(typeof(Clan), nameof(Clan.addUnit));
      MethodInfo prefix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(addUnit_Prefix));
      Harmony.Patch(original, new HarmonyMethod(prefix));
    }

    public void SetTargetFramerate(int targetFrameRate) {
      TargetFrameRate = targetFrameRate;
    }

    public void SetDeltaTime(float deltaTime) {
      DeltaTime = deltaTime;
    }

    public void ToggleMouseDrag() {
      AllowMouseDrag = !AllowMouseDrag;
    }

    public void NotifyOfNewTraits() {
      TraitsChanged = true;
    }

    internal void SetRandomSeed(int seed) {
      RandomSeed = seed;
    }

    public static int GetCrabzillaArmExplosionRadius() {
      return CrabzillaArmExplosionRadius;
    }

    public void ToggleFixedSeedUsage() {
      UseFixedRandomSeed = !UseFixedRandomSeed;
      FieldInfo field = typeof(ListExtensions).GetField("rnd", BindingFlags.NonPublic | BindingFlags.Static);
      if (field != null) {
        if (field.GetValue(null) is System.Random random) {
          if (random != _random) {
            _listExtensionsRandom = random;
            field.SetValue(null, _random);
          } else {
            field.SetValue(null, _listExtensionsRandom);
            _listExtensionsRandom = new System.Random();
          }
        }
      }
    }

    public void ToggleBoatIceMovement() {
      DisableBoatMovementOnIce = !DisableBoatMovementOnIce;
    }

    public void StopCrabzillaFromDying() {
      MethodInfo original = AccessTools.Method(typeof(Actor), nameof(Actor.killHimself));
      MethodInfo prefix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(temp_killHimself_Prefix));
      Harmony.Patch(original, new HarmonyMethod(prefix));
    }

    public void LetCrabzillaDieAgain() {
      MethodInfo original = AccessTools.Method(typeof(Actor), nameof(Actor.killHimself));
      Harmony.Unpatch(original, HarmonyPatchType.Prefix, KeyGeneralPurposeLibraryConfig.PluginGuid);
    }

    public void StopCrabzillaSpriteIssues() {
      MethodInfo original = AccessTools.Method(typeof(ActorAnimationLoader), nameof(ActorAnimationLoader.generateAnimation));
      MethodInfo prefix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(generateAnimation_Prefix));
      Harmony.Patch(original, new HarmonyMethod(prefix));
      original = AccessTools.Method(typeof(ActorAnimationLoader), nameof(ActorAnimationLoader.loadAnimationUnit));
      prefix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(loadAnimationUnit_Prefix));
      Harmony.Patch(original, new HarmonyMethod(prefix));
    }

    public void LetCrabzillaHaveSpriteIssuesAgain() {
      MethodInfo original = AccessTools.Method(typeof(ActorAnimationLoader), nameof(ActorAnimationLoader.generateAnimation));
      Harmony.Unpatch(original, HarmonyPatchType.Prefix, KeyGeneralPurposeLibraryConfig.PluginGuid);
      original = AccessTools.Method(typeof(ActorAnimationLoader), nameof(ActorAnimationLoader.loadAnimationUnit));
      Harmony.Unpatch(original, HarmonyPatchType.Prefix, KeyGeneralPurposeLibraryConfig.PluginGuid);
    }

    public void StopCrabzillaInspectionIssues() {
      MethodInfo original = AccessTools.Method(typeof(Actor), nameof(Actor.checkSpriteToRender));
      MethodInfo prefix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(checkSpriteToRender_Prefix));
      Harmony.Patch(original, new HarmonyMethod(prefix));
    }

    internal override void Update() {
      CrabzillaIsSpawned = false;
    }

    private static bool checkSpriteToRender_Prefix(ref Sprite __result, Actor __instance) {
      if (__instance.asset.id == SA.crabzilla) {
        __result = SpriteTextureLoader.getSprite("ui/icons/iconcrabzilla");
        return false;
      }

      return true;
    }

    private static bool loadAnimationUnit_Prefix(ref AnimationContainerUnit __result, string pTexturePath, ActorAsset pAsset) {
      if (pAsset.id == SA.crabzilla) {
        __result = new AnimationContainerUnit {
          sprites = new Dictionary<string, Sprite>(),
          dict_frame_data = new Dictionary<string, AnimationFrameData>(),
          id = pTexturePath
        };
        return false;
      }

      return true;
    }

    private static bool generateAnimation_Prefix(ref AnimationContainerUnit __result, string pSheetPath, ActorAsset pAsset) {
      if (pAsset.id == SA.crabzilla) {
        __result = new AnimationContainerUnit {
          sprites = new Dictionary<string, Sprite>(),
          dict_frame_data = new Dictionary<string, AnimationFrameData>(),
          id = pSheetPath
        };
        return false;
      }

      return true;
    }

    private static bool temp_killHimself_Prefix(Actor __instance) {
      return __instance.asset.id != SA.crabzilla;
    }

    private static void isGoodForBoat_Postfix(ref bool __result, WorldTile __instance) {
      if (DisableBoatMovementOnIce) {
        if (__result) {
          if (__instance.isFrozen()) {
            __result = false;
          }
        }
      }
    }

    private static void TargetFramerateSetter_Prefix(ref int value) {
      value = TargetFrameRate;
    }

    private static void DeltaTimeGetter_Postfix(ref float __result) {
      __result *= DeltaTime;
    }

    private static void checkTraitButtonCreation_Prefix(TraitsWindow __instance) {
      if (TraitsChanged) {
        __instance._all_traits_buttons.Clear();
        __instance.dict_groups.Clear();
        foreach (Transform child in __instance.transform_content.Cast<Transform>().Where(child => child.name == "TraitGroup(Clone)")) {
          Object.Destroy(child.gameObject);
        }

        __instance._listInitiated = false;
        TraitsChanged = false;
      }
    }

    private static void getSprite_Trait_Prefix(ActorTrait __instance) {
      if (__instance is CustomTrait customTrait) {
        if (customTrait.cached_sprite == null) {
          Sprite sprite = KeyGenLibFileAssetManager.CreateSprite(customTrait.Author, customTrait.Sprite);
          customTrait.cached_sprite = sprite;
        }
      }
    }

    private static void getSprite_Item_Postfix(ItemAsset __instance, ref Sprite __result) {
      if (__instance is CustomItemAsset customItemAsset) {
        Sprite sprite = KeyGenLibFileAssetManager.CreateSprite(customItemAsset.Author, customItemAsset.Sprite);
        customItemAsset.cached_sprite = sprite;
        __result = sprite;
      }
    }

    private static void getSprite_Mood_Prefix(MoodAsset __instance) {
      if (__instance is CustomMoodAsset customMoodAsset) {
        if (customMoodAsset.sprite == null) {
          Sprite sprite = KeyGenLibFileAssetManager.CreateSprite(customMoodAsset.Author, customMoodAsset.Sprite);
          customMoodAsset.sprite = sprite;
        }
      }
    }

    private static void Range_Postfix(int minInclusive, int maxExclusive, ref int __result) {
      if (UseFixedRandomSeed) {
        Debug.Log("Unrandomizing Random.range.");
        __result = _random.Next(minInclusive, maxExclusive);
      }
    }

    private static void Bilinear_Prefix(Texture tex, ref int newWidth, ref int newHeight) {
      if (UseFixedRandomSeed) {
        Debug.Log("Unrandomizing texture size.");
        newWidth = (int)(tex.width * (_random.NextDouble() + 0.3) * 1.8);
        newHeight = (int)(tex.height * (_random.NextDouble() + 0.3) * 1.8);
      }
    }

    private static bool updateMouseCameraDrag_Prefix() {
      return AllowMouseDrag;
    }

    private static void ApplyPerlinNoise_Prefix(ref float pPosX, ref float pPosY) {
      if (UseFixedRandomSeed) {
        Debug.Log("Unrandomizing perlin noice.");
        pPosX = (float)_random.NextDouble();
        pPosY = (float)_random.NextDouble();
      }
    }

    private static void schedulePerlinNoiseMap_Prefix() {
      if (UseFixedRandomSeed) {
        Debug.Log("Seeded Random instance created.");
        _random = new System.Random(RandomSeed);
        FieldInfo field = typeof(ListExtensions).GetField("rnd", BindingFlags.NonPublic | BindingFlags.Static);
        if (field != null) {
          field.SetValue(null, _random);
        }
      }
    }

    private static void schedulePerlinNoiseMap_Postfix() {
      if (UseFixedRandomSeed) {
        Debug.Log("Setup for seeded generation to not disable.");
        SmoothLoader._has_actions = true;
      }
    }

    private static void randomBool_Postfix(ref bool __result) {
      if (UseFixedRandomSeed) {
        Debug.Log("Unrandomizing random bool.");
        __result = _random.NextDouble() > 0.5;
      }
    }

    private static void update_Giantzilla_Postfix() {
      CrabzillaIsSpawned = true;
    }

    private static IEnumerable<CodeInstruction> CrabArm_damageWorld_Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator) {
      LocalBuilder builder = generator.DeclareLocal(typeof(int));
      foreach (CodeInstruction instruction in instructions) {
        if (instruction.opcode == OpCodes.Ldc_I4_4 || (instruction.opcode == OpCodes.Ldc_I4 && (int)instruction.operand == 4)) {
          yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(GetCrabzillaArmExplosionRadius)));
          yield return new CodeInstruction(OpCodes.Stloc_S, builder.LocalIndex);
          yield return new CodeInstruction(OpCodes.Ldloc_S, builder.LocalIndex);
        } else {
          yield return instruction;
        }
      }
    }

    private static void ActorBase_addTrait_Postfix(ActorBase __instance, string pTrait) {
      ActorTrait trait = AssetManager.traits.get(pTrait);
      if (trait is CustomTrait customTrait) {
        foreach (string partnerTraitId in from partnerTraitId in customTrait.PartnerTraits let partnerTrait = AssetManager.traits.get(partnerTraitId) where partnerTrait != null where !__instance.hasTrait(partnerTraitId) select partnerTraitId) {
          __instance.removeOppositeTraits(partnerTraitId);
          __instance.data.traits.Add(partnerTraitId);
          __instance.setStatsDirty();
          if (!customTrait.PartnerTraitCache.ContainsKey(__instance.data)) {
            customTrait.PartnerTraitCache.Add(__instance.data, new List<string>());
          }
          customTrait.PartnerTraitCache[__instance.data].Add(partnerTraitId);
        }
      }
    }
    
    private static void ActorBase_removeTrait_Postfix(ActorBase __instance, string pTraitID) {
      ActorTrait trait = AssetManager.traits.get(pTraitID);
      if (trait is CustomTrait customTrait) {
        if (customTrait.PartnerTraitCache.ContainsKey(__instance.data)) {
          foreach (string partnerTraitId in customTrait.PartnerTraitCache[__instance.data]) {
            __instance.removeTrait(partnerTraitId);
          }
          customTrait.PartnerTraitCache.Remove(__instance.data);
        }
      }
    }
    
    private static void ActorData_addTrait_Postfix(ActorData __instance, string pTrait) {
      ActorTrait trait = AssetManager.traits.get(pTrait);
      if (trait is CustomTrait customTrait) {
        foreach (string partnerTraitId in from partnerTraitId in customTrait.PartnerTraits let partnerTrait = AssetManager.traits.get(partnerTraitId) where partnerTrait != null where !__instance.hasTrait(partnerTraitId) select partnerTraitId) {
          __instance.traits.Add(partnerTraitId);
          if (!customTrait.PartnerTraitCache.ContainsKey(__instance)) {
            customTrait.PartnerTraitCache.Add(__instance, new List<string>());
          }
          customTrait.PartnerTraitCache[__instance].Add(partnerTraitId);
        }
      }
    }
    
    private static void ActorData_removeTrait_Postfix(ActorData __instance, string pTraitID) {
      ActorTrait trait = AssetManager.traits.get(pTraitID);
      if (trait is CustomTrait customTrait) {
        if (customTrait.PartnerTraitCache.ContainsKey(__instance)) {
          foreach (string partnerTraitId in customTrait.PartnerTraitCache[__instance]) {
            __instance.removeTrait(partnerTraitId);
          }
          customTrait.PartnerTraitCache.Remove(__instance);
        }
      }
    }

    private static void killHimself_Prefix(Actor __instance) {
      foreach (CustomTrait trait in __instance.data.traits.Select(traitId => AssetManager.traits.get(traitId)).Where(trait => trait.GetType() == typeof(CustomTrait)).Cast<CustomTrait>().Where(trait => trait.PartnerTraitCache.ContainsKey(__instance.data))) {
        trait.PartnerTraitCache.Remove(__instance.data);
      }
    }

    private static void addUnit_Prefix(Clan __instance, Actor pActor) {
      if (__instance.units.Count >= __instance.getMaxMembers()) return;
      if (__instance.data.custom_data_string != null) {
        bool clanTraitsSet = __instance.data.custom_data_string.TryGetValue("ClanTraits", out string clanTraitsJson);
        if (clanTraitsSet) {
          JToken[] clanTraitsArray = JsonConvert.DeserializeObject<JArray>(clanTraitsJson).ToArray();
          clanTraitsArray.Shuffle();
          foreach (string traitId in clanTraitsArray.Select(token => token.Value<string>()).ToArray()) {
            pActor.addTrait(traitId);
          }
        }
      }
    }
  }
}