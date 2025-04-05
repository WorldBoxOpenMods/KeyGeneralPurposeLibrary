using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx;
using KeyGeneralPurposeLibrary.Assets;
using KeyGeneralPurposeLibrary.BehaviourManipulation;
using KeyGeneralPurposeLibrary.Powers;

namespace KeyGeneralPurposeLibrary {
  [BepInPlugin(KeyGeneralPurposeLibraryConfig.PluginGuid, KeyGeneralPurposeLibraryConfig.PluginName, KeyGeneralPurposeLibraryConfig.PluginVersion)]
  public class KeyLib : BaseUnityPlugin {
    private static readonly List<KLibComponent> Components = new List<KLibComponent>();
    
    public void Awake() {
      Logger.LogInfo("Started loading KeyGeneralPurposeLibrary...");
      LoadComponent<KeyGenLibCustomWarTypeLibrary>();
      LoadComponent<KeyGenLibWorldTileManipulationMethodCollection>();
      LoadComponent<KeyGenLibCultureManipulationMethodCollection>();
      LoadComponent<KeyGenLibHarmonyPatchCollection>();
      LoadComponent<KeyGenLibFileAssetManager>();
      LoadComponent<KeyGenLibCustomTraitManager>();
      LoadComponent<KeyGenLibCustomItemManager>();
      LoadComponent<KeyGenLibPowerActionWithIdLibrary>();
      LoadComponent<KeyGenLibPowerButtonClickActionLibrary>();
      LoadComponent<KeyGenLibGodPowerLibrary>();
      LoadComponent<KeyGenLibGodPowerButtonGenerator>();
      LoadComponent<KeyGenLibGodPowerButtonLibrary>();
      Logger.LogInfo("KeyGeneralPurposeLibrary finished loading successfully!");
    }
    
    private void LoadComponent<T>() where T : KLibComponent, new() {
      Logger.LogInfo("Loading " + typeof(T).FullName + "...");
      try {
        Components.Add(new T());
      } catch (Exception e) {
        Logger.LogError("Failed to load " + typeof(T).FullName + "!");
        Logger.LogError(e);
        return;
      }
      Logger.LogInfo("Loaded " + typeof(T).FullName + "!");
    }

    private void Update() {
      foreach (KLibComponent component in Components.Where(component => component.IsInitialized == false).Where(_ => global::Config.game_loaded)) {
        component.Initialize();
      }

      foreach (KLibComponent component in Components) {
        component.Update();
      }
    }

    public static T Get<T>() where T : KLibComponent, new() {
      return Components.Where(component => component is T).Cast<T>().FirstOrDefault() ?? throw new ApplicationException("Component " + typeof(T).FullName + " not found!");
    }
  }
}
