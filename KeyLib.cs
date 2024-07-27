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
      LoadComponent<KeyGenLibCustomPlotCreator>();
      LoadComponent<KeyGenLibCustomPlotLibrary>();
      LoadComponent<KeyGenLibCustomWarTypeLibrary>();
      LoadComponent<KeyGenLibRaceManipulationMethodCollection>();
      LoadComponent<KeyGenLibWorldTileManipulationMethodCollection>();
      LoadComponent<KeyGenLibCultureManipulationMethodCollection>();
      LoadComponent<KeyGenLibDisasterAssetLibrary>();
      LoadComponent<KeyGenLibDisasterGenerator>();
      LoadComponent<KeyGenLibWorldCleansingMethodCollection>();
      LoadComponent<KeyGenLibHarmonyPatchCollection>();
      LoadComponent<KeyGenLibFileAssetManager>();
      LoadComponent<KeyGenLibCustomTraitManager>();
      LoadComponent<KeyGenLibCustomItemManager>();
      LoadComponent<KeyGenLibWorldGenerationManipulationMethodCollection>();
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
      foreach (KLibComponent component in Components.Where(component => component.IsInitialized == false).Where(_ => global::Config.gameLoaded)) {
        component.Initialize();
      }

      foreach (KLibComponent component in Components) {
        component.Update();
      }
    }

    public static T Get<T>() where T : KLibComponent, new() {
      return Components.Where(component => component is T).Cast<T>().FirstOrDefault() ?? throw new ApplicationException("Component " + typeof(T).FullName + " not found!");
    }

    #region Deprecated code
    [Obsolete("Use Get<T>() instead")]
    public KeyGenLibCustomPlotCreator GetCustomPlotCreator() {
      return Get<KeyGenLibCustomPlotCreator>();
    }

    [Obsolete("Use Get<T>() instead")]
    public KeyGenLibCustomPlotLibrary GetCustomPlotList() {
      return Get<KeyGenLibCustomPlotLibrary>();
    }

    [Obsolete("Use Get<T>() instead")]
    public KeyGenLibCustomWarTypeLibrary GetCustomWarTypeList() {
      return Get<KeyGenLibCustomWarTypeLibrary>();
    }

    [Obsolete("Use Get<T>() instead")]
    public KeyGenLibRaceManipulationMethodCollection GetRaceManipulationMethodCollection() {
      return Get<KeyGenLibRaceManipulationMethodCollection>();
    }

    [Obsolete("Use Get<T>() instead")]
    public KeyGenLibWorldTileManipulationMethodCollection GetWorldTileManipulationMethodCollection() {
      return Get<KeyGenLibWorldTileManipulationMethodCollection>();
    }

    [Obsolete("Use Get<T>() instead")]
    public KeyGenLibCultureManipulationMethodCollection GetCultureManipulationMethodCollection() {
      return Get<KeyGenLibCultureManipulationMethodCollection>();
    }

    [Obsolete("Use Get<T>() instead")]
    public KeyGenLibDisasterGenerator GetDisasterGenerator() {
      return Get<KeyGenLibDisasterGenerator>();
    }

    [Obsolete("Use Get<T>() instead")]
    public KeyGenLibWorldCleansingMethodCollection GetWorldCleansingMethodCollection() {
      return Get<KeyGenLibWorldCleansingMethodCollection>();
    }

    [Obsolete("Use Get<T>() instead")]
    public KeyGenLibHarmonyPatchCollection GetHarmonyPatchCollection() {
      return Get<KeyGenLibHarmonyPatchCollection>();
    }

    [Obsolete("Use Get<T>() instead")]
    public KeyGenLibFileAssetManager GetFileAssetManager() {
      return Get<KeyGenLibFileAssetManager>();
    }

    [Obsolete("Use Get<T>() instead")]
    public KeyGenLibCustomTraitManager GetCustomTraitManager() {
      return Get<KeyGenLibCustomTraitManager>();
    }

    [Obsolete("Use Get<T>() instead")]
    public KeyGenLibCustomItemManager GetCustomItemManager() {
      return Get<KeyGenLibCustomItemManager>();
    }

    [Obsolete("Use Get<T>() instead")]
    public KeyGenLibWorldGenerationManipulationMethodCollection GetWorldGenerationManipulationMethodCollection() {
      return Get<KeyGenLibWorldGenerationManipulationMethodCollection>();
    }
    #endregion
  }
}