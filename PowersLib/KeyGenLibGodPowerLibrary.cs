using System;
using System.Collections.Generic;
using System.Linq;
using KeyGeneralPurposeLibrary.PowersLib.Powers;
using UnityEngine;
namespace KeyGeneralPurposeLibrary.PowersLib {
  public class KeyGenLibGodPowerLibrary : KLibComponent {
    private static readonly List<KeyGenLibPower> Powers = new List<KeyGenLibPower>();

    public KeyGenLibGodPowerLibrary() {
      LoadPower<AddZoneToCity>();
      LoadPower<CreateNewCulture>();
      LoadPower<CultureDeletion>();
      LoadPower<CultureForceSelectCity>();
      LoadPower<CultureForceSelectCulture>();
      LoadPower<ForceCityAsCapitalCity>();
      LoadPower<ForceCityIntoOtherKingdom>();
      LoadPower<MakeActorKing>();
      LoadPower<MassItemAdditionRain>();
      LoadPower<MassTraitRemovalRain>();
      LoadPower<PlaceBuilding>();
      LoadPower<RemoveZoneFromCity>();
      LoadPower<WhisperOfAlliance>();
    }
    
    private static void LoadPower<T>() where T : KeyGenLibPower, new() {
      Debug.Log($"Loading {typeof(T).FullName}...");
      try {
        Powers.Add(new T());
      } catch (Exception e) {
        Debug.LogError($"Failed to load {typeof(T).FullName}!");
        Debug.LogError(e);
        return;
      }
      Debug.Log($"Loaded {typeof(T).FullName}!");
    }

    internal override void Update() {
      foreach (KeyGenLibPower power in Powers.Where(power => power.IsInitialized == false).Where(_ => Config.game_loaded)) {
        power.Initialize();
      }

      foreach (KeyGenLibPower power in Powers) {
        power.Update();
      }
    }
    
    public T Get<T>() where T : KeyGenLibPower, new() {
      return Powers.Where(power => power is T).Cast<T>().FirstOrDefault() ?? throw new ApplicationException($"Power {typeof(T).FullName} not found!");
    }

    public (GodPower power, PowerButton button) GetPower<T>() where T : KeyGenLibPower, new() {
      T powerComponent = Get<T>();
      return (powerComponent.Power, powerComponent.Button);
    }
  }
}
