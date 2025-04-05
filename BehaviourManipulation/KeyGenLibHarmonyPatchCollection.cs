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
    public static bool CrabzillaIsSpawned { get; private set; }
    public static int CrabzillaArmExplosionRadius { private get; set; } = 4;

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
      MethodInfo original = AccessTools.Method(typeof(Crabzilla), nameof(Crabzilla.update));
      MethodInfo postfix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(update_Giantzilla_Postfix));
      Harmony.Patch(original, null, new HarmonyMethod(postfix));
    }

    public void PatchDamageWorld_CrabArm() {
      MethodInfo original = AccessTools.Method(typeof(CrabArm), nameof(CrabArm.damageWorld));
      MethodInfo transpiler = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(CrabArm_damageWorld_Transpiler));
      Harmony.Patch(original, null, null, new HarmonyMethod(transpiler));
    }

    public void PatchPartnerTraitAdditions() {
      MethodInfo original = AccessTools.Method(typeof(Actor), nameof(Actor.addTrait), new []{typeof(ActorTrait), typeof(bool)});
      MethodInfo postfix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(Actor_addTrait_Postfix));
      Harmony.Patch(original, null, new HarmonyMethod(postfix));
      original = AccessTools.Method(typeof(Actor), nameof(Actor.removeTrait), new []{typeof(ActorTrait), typeof(bool)});
      postfix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(Actor_removeTrait_Postfix));
      Harmony.Patch(original, null, new HarmonyMethod(postfix));
      original = AccessTools.Method(typeof(Actor), nameof(Actor.removeTraits));
      postfix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(Actor_removeTraits_Postfix));
      Harmony.Patch(original, null, new HarmonyMethod(postfix));
      original = AccessTools.Method(typeof(Actor), nameof(Actor.die));
      MethodInfo prefix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(Actor_die_prefix));
      Harmony.Patch(original, new HarmonyMethod(prefix));
    }

    public void PatchClanTraitAdditions() {
      MethodInfo original = AccessTools.Method(typeof(Actor), nameof(Actor.setClan));
      MethodInfo prefix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(Actor_setClan_Prefix));
      Harmony.Patch(original, new HarmonyMethod(prefix));
    }
    
    public void PatchCultureTraitAdditions() {
      MethodInfo original = AccessTools.Method(typeof(Actor), nameof(Actor.setCulture));
      MethodInfo prefix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(setCulture_Prefix));
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

    public static int GetCrabzillaArmExplosionRadius() {
      return CrabzillaArmExplosionRadius;
    }

    public void ToggleBoatIceMovement() {
      DisableBoatMovementOnIce = !DisableBoatMovementOnIce;
    }

    public void StopCrabzillaFromDying() {
      MethodInfo original = AccessTools.Method(typeof(Actor), nameof(Actor.die));
      MethodInfo prefix = AccessTools.Method(typeof(KeyGenLibHarmonyPatchCollection), nameof(temp_Actor_die_Prefix));
      Harmony.Patch(original, new HarmonyMethod(prefix));
    }

    public void LetCrabzillaDieAgain() {
      MethodInfo original = AccessTools.Method(typeof(Actor), nameof(Actor.die));
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

    private static bool temp_Actor_die_Prefix(Actor __instance) {
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

    private static bool updateMouseCameraDrag_Prefix() {
      return AllowMouseDrag;
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

    private static void Actor_addTrait_Postfix(Actor __instance, ActorTrait pTrait) {
      if (pTrait is CustomTrait customTrait) {
        foreach (ActorTrait partnerTrait in from partnerTraitId in customTrait.PartnerTraits let partnerTrait = AssetManager.traits.get(partnerTraitId) where partnerTrait != null where !__instance.hasTrait(partnerTraitId) select partnerTrait) {
          __instance.removeOppositeTraits(partnerTrait);
          __instance.data.saved_traits.Add(partnerTrait.id);
          __instance.setStatsDirty();
          if (!customTrait.PartnerTraitCache.ContainsKey(__instance.data)) {
            customTrait.PartnerTraitCache.Add(__instance.data, new List<string>());
          }
          customTrait.PartnerTraitCache[__instance.data].Add(partnerTrait.id);
        }
      }
    }
    
    private static void Actor_removeTrait_Postfix(Actor __instance, ActorTrait pTrait) {
      if (pTrait is CustomTrait customTrait) {
        if (customTrait.PartnerTraitCache.ContainsKey(__instance.data)) {
          foreach (string partnerTraitId in customTrait.PartnerTraitCache[__instance.data]) {
            __instance.removeTrait(partnerTraitId);
          }
          customTrait.PartnerTraitCache.Remove(__instance.data);
        }
      }
    }

    private static void Actor_removeTraits_Postfix(Actor __instance, ICollection<ActorTrait> pTraits) {
      foreach (ActorTrait trait in pTraits) {
        Actor_removeTrait_Postfix(__instance, trait);
      }
    }

    private static void Actor_die_prefix(Actor __instance) {
      foreach (CustomTrait trait in __instance.data.saved_traits.Select(traitId => AssetManager.traits.get(traitId)).Where(trait => trait.GetType() == typeof(CustomTrait)).Cast<CustomTrait>().Where(trait => trait.PartnerTraitCache.ContainsKey(__instance.data))) {
        trait.PartnerTraitCache.Remove(__instance.data);
      }
    }

    private static void Actor_setClan_Prefix(Actor __instance, Clan pClan) {
      if (pClan.units.Count >= pClan.getMaxMembers()) return;
      if (pClan.data.custom_data_string != null) {
        bool clanTraitsSet = pClan.data.custom_data_string.TryGetValue("ClanTraits", out string clanTraitsJson);
        if (clanTraitsSet) {
          JToken[] clanTraitsArray = JsonConvert.DeserializeObject<JArray>(clanTraitsJson).ToArray();
          clanTraitsArray.Shuffle();
          foreach (string traitId in clanTraitsArray.Select(token => token.Value<string>()).ToArray()) {
            __instance.addTrait(traitId);
          }
        }
      }
    }
    
    private static void setCulture_Prefix(Actor __instance, Culture pCulture) {
      if (pCulture.data.custom_data_string != null) {
        bool cultureTraitsSet = pCulture.data.custom_data_string.TryGetValue("CultureTraits", out string cultureTraitsJson);
        if (cultureTraitsSet) {
          JToken[] cultureTraitsArray = JsonConvert.DeserializeObject<JArray>(cultureTraitsJson).ToArray();
          cultureTraitsArray.Shuffle();
          foreach (string traitId in cultureTraitsArray.Select(token => token.Value<string>()).ToArray()) {
            __instance.addTrait(traitId);
          }
        }
      }
    }
  }
}
