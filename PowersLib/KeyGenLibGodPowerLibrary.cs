using System;
using System.Collections.Generic;
using System.Linq;
using KeyGeneralPurposeLibrary.PowersLib.Powers;
using UnityEngine;
namespace KeyGeneralPurposeLibrary.PowersLib {
  public class KeyGenLibGodPowerLibrary : KLibComponent {
    private static readonly List<KeyGenLibPower> Components = new List<KeyGenLibPower>();

    public KeyGenLibGodPowerLibrary() {
      LoadComponent<AddZoneToCity>();
      LoadComponent<CreateNewCulture>();
      LoadComponent<CultureDeletion>();
      LoadComponent<CultureForceSelectCity>();
      LoadComponent<CultureForceSelectCulture>();
      LoadComponent<ForceCityAsCapitalCity>();
      LoadComponent<ForceCityIntoOtherKingdom>();
      LoadComponent<MakeActorKing>();
      LoadComponent<MassItemAdditionRain>();
      LoadComponent<MassTraitRemovalRain>();
      LoadComponent<PlaceBuilding>();
      LoadComponent<RemoveZoneFromCity>();
      LoadComponent<WhisperOfAlliance>();
    }
    
    private static void LoadComponent<T>() where T : KeyGenLibPower, new() {
      Debug.Log($"Loading {typeof(T).FullName}...");
      try {
        Components.Add(new T());
      } catch (Exception e) {
        Debug.LogError($"Failed to load {typeof(T).FullName}!");
        Debug.LogError(e);
        return;
      }
      Debug.Log($"Loaded {typeof(T).FullName}!");
    }

    internal override void Update() {
      foreach (KeyGenLibPower component in Components.Where(component => component.IsInitialized == false).Where(_ => Config.game_loaded)) {
        component.Initialize();
      }

      foreach (KeyGenLibPower component in Components) {
        component.Update();
      }
    }
    
    public T Get<T>() where T : KeyGenLibPower, new() {
      return Components.Where(component => component is T).Cast<T>().FirstOrDefault() ?? throw new ApplicationException($"Component {typeof(T).FullName} not found!");
    }

    public (GodPower power, PowerButton button) GetPower<T>() where T : KeyGenLibPower, new() {
      T powerComponent = Get<T>();
      if (powerComponent != null) {
        return (powerComponent.Power, powerComponent.Button);
      }
      throw new ApplicationException($"Power {typeof(T).FullName} not found!");
    }
  }
}
