using System.Collections.Generic;
using System.Linq;
using KeyGeneralPurposeLibrary.BehaviourManipulation;
using UnityEngine;

namespace KeyGeneralPurposeLibrary.Powers {
  public class KeyGenLibPowerActionWithIdLibrary : KLibAssetLibrary<PowerActionWithID> {
    public KeyGenLibPowerActionWithIdLibrary() {
      AddAsset(ClickWithWhisperOfAlliance, out _clickWithWhisperOfAllianceIndex);
      AddAsset(ClickWithCultureDeletion, out _clickWithCultureDeletionIndex);
      AddAsset(ClickWithCultureReset, out _clickWithCultureResetIndex);
      AddAsset(ClickWithCultureForceSelectCulture, out _clickWithCultureForceSelectCultureIndex);
      AddAsset(ClickWithCultureForceSelectCity, out _clickWithCultureForceSelectCityIndex);
      AddAsset(ClickWithCreateNewCulture, out _clickWithCreateNewCultureIndex);
      AddAsset(ClickWithAddZoneToCity, out _clickWithAddZoneToCityIndex);
      AddAsset(ClickWithRemoveZoneFromCity, out _clickWithRemoveZoneFromCityIndex);
      AddAsset(ClickWithForceCityAsCapitalCity, out _clickWithForceCityAsCapitalCityIndex);
      AddAsset(ClickWithForceCityIntoOtherKingdom, out _clickWithForceCityIntoOtherKingdomIndex);
      AddAsset(ClickWithMakeActorKing, out _clickWithMakeActorKingIndex);
      AddAsset(ClickWithPlaceBuilding, out _clickWithPlaceBuildingIndex);
    }
    private static int _clickWithWhisperOfAllianceIndex;
    private static int _clickWithCultureDeletionIndex;
    private static int _clickWithCultureResetIndex;
    private static int _clickWithCultureForceSelectCultureIndex;
    private static int _clickWithCultureForceSelectCityIndex;
    private static int _clickWithCreateNewCultureIndex;
    private static int _clickWithAddZoneToCityIndex;
    private static int _clickWithRemoveZoneFromCityIndex;
    private static int _clickWithForceCityAsCapitalCityIndex;
    private static int _clickWithForceCityIntoOtherKingdomIndex;
    private static int _clickWithMakeActorKingIndex;
    private static int _clickWithPlaceBuildingIndex;
    public static int ClickWithWhisperOfAllianceIndex => _clickWithWhisperOfAllianceIndex;
    public static int ClickWithCultureDeletionIndex => _clickWithCultureDeletionIndex;
    public static int ClickWithCultureResetIndex => _clickWithCultureResetIndex;
    public static int ClickWithCultureForceSelectCultureIndex => _clickWithCultureForceSelectCultureIndex;
    public static int ClickWithCultureForceSelectCityIndex => _clickWithCultureForceSelectCityIndex;
    public static int ClickWithCreateNewCultureIndex => _clickWithCreateNewCultureIndex;
    public static int ClickWithAddZoneToCityIndex => _clickWithAddZoneToCityIndex;
    public static int ClickWithRemoveZoneFromCityIndex => _clickWithRemoveZoneFromCityIndex;
    public static int ClickWithForceCityAsCapitalCityIndex => _clickWithForceCityAsCapitalCityIndex;
    public static int ClickWithForceCityIntoOtherKingdomIndex => _clickWithForceCityIntoOtherKingdomIndex;
    public static int ClickWithMakeActorKingIndex => _clickWithMakeActorKingIndex;
    public static int ClickWithPlaceBuildingIndex => _clickWithPlaceBuildingIndex;

    private static bool ClickWithWhisperOfAlliance(WorldTile pTile, string pPowerID) {
      City city = pTile.zone.city;
      if (city == null) {
        return false;
      }

      Kingdom kingdom = city.kingdom;
      if (Config.whisper_A == null) {
        Config.whisper_A = kingdom;
        WorldTip.showNow("KGPLL_AllianceCreation_SelectSecondKingdom", true, "top");
        return false;
      }

      if (Config.whisper_B == null && Config.whisper_A == kingdom) {
        WorldTip.showNow("KGPLL_AllianceCreation_SameKingdomTwiceError", true, "top");
        return false;
      }

      if (Config.whisper_B == null) {
        Config.whisper_B = kingdom;
      }

      if (Config.whisper_B != Config.whisper_A) {
        if (Alliance.isSame(Config.whisper_A.getAlliance(), Config.whisper_B.getAlliance())) {
          WorldTip.showNow("KGPLL_AllianceCreation_KingdomsAlreadyAlliedError", true, "top");
          Config.whisper_B = null;
          return false;
        }

        foreach (War war in World.world.wars.getWars(Config.whisper_A).Where(war => war.isInWarWith(Config.whisper_A, Config.whisper_B))) {
          war.removeFromWar(Config.whisper_A, true);
          war.removeFromWar(Config.whisper_B, true);
        }

        Alliance allianceA = Config.whisper_A.getAlliance();
        Alliance allianceB = Config.whisper_B.getAlliance();
        if (allianceA != null) {
          if (allianceB != null) {
            IEnumerable<Kingdom> kingdoms = allianceB.kingdoms_list;
            World.world.alliances.dissolveAlliance(allianceB);
            foreach (Kingdom ally in kingdoms) {
              ForceIntoAlliance(allianceA, ally);
            }
          } else {
            ForceIntoAlliance(allianceA, Config.whisper_B);
          }
        } else {
          if (allianceB == null) {
            ForceNewAlliance(Config.whisper_A, Config.whisper_B);
          } else {
            ForceIntoAlliance(allianceB, Config.whisper_A);
          }
        }
        WorldTip.showNow(string.Format(LocalizedTextManager.getText("KGPLL_AllianceCreation_CreationSuccess"), Config.whisper_A.name, Config.whisper_B.name), false, "top");
        Config.whisper_A = null;
        Config.whisper_B = null;
      }

      return true;
    }

    private static void ForceIntoAlliance(Alliance alliance, Kingdom kingdom) {
      alliance.kingdoms_hashset.Add(kingdom);
      kingdom.allianceJoin(alliance);
      alliance.recalculate();
      alliance.data.timestamp_member_joined = World.world.getCurWorldTime();
    }

    private static void ForceNewAlliance(Kingdom kingdomA, Kingdom kingdomB) {
      Alliance alliance = World.world.alliances.newObject();
      alliance.createNewAlliance();
      alliance.data.founder_kingdom_id = kingdomA.data.id;
      alliance.data.founder_kingdom_name = kingdomA.data.name;
      if (kingdomA.king != null) {
        alliance.data.founder_actor_id = kingdomA.king.data.id;
        alliance.data.founder_actor_name = kingdomA.king.getName();
      }
      ForceIntoAlliance(alliance, kingdomA);
      ForceIntoAlliance(alliance, kingdomB);
      WorldLog.logAllianceCreated(alliance);
    }

    private static bool ClickWithCultureDeletion(WorldTile pTile, string pPowerID) {
      Culture cultureToWipe = pTile.zone.city?.culture;
      if (cultureToWipe != null) {
        KeyLib.Get<KeyGenLibCultureManipulationMethodCollection>().DeleteCulture(cultureToWipe);
        WorldTip.showNow("KGPLL_CultureDeletion_Success", true, "top");
        return true;
      }

      WorldTip.showNow("KGPLL_CultureDeletion_NoCultureSelectedError", true, "top");
      return false;
    }

    private static bool ClickWithCultureReset(WorldTile pTile, string pPowerID) {
      Culture cultureToReset = pTile.zone.city?.culture;
      if (cultureToReset != null) {
        KeyLib.Get<KeyGenLibCultureManipulationMethodCollection>().ResetCulture(cultureToReset);
        WorldTip.showNow("KGPLL_CultureFullReset_Success", true, "top");
        return true;
      }

      WorldTip.showNow("KGPLL_CultureFullReset_NoCultureSelectedError", true, "top");
      return false;
    }

    internal static Culture CultureToForceUponCity;
    private static bool ClickWithCultureForceSelectCulture(WorldTile pTile, string pPowerID) {
      Culture cultureToForce = pTile.zone.city?.culture;
      CultureToForceUponCity = cultureToForce;
      if (cultureToForce != null) {
        GodPower power = KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.CultureForceSelectCityIndex];
        PowerButton button = KeyLib.Get<KeyGenLibGodPowerButtonLibrary>()[KeyGenLibGodPowerButtonLibrary.CultureForceSelectCityButtonIndex];
        if (button != null) {
          WorldTip.showNow("KGPLL_CultureForceConversion_SelectCity", true, "top");
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
          return true;
        }
        Debug.LogError("Something went wrong with the Culture Conversion! Please report this to the mod author!");
        return false;
      }
      WorldTip.showNow("KGPLL_CultureForceConversion_NoCultureSelectedError", true, "top");
      return false;
    }

    private static bool ClickWithCultureForceSelectCity(WorldTile pTile, string pPowerID) {
      City cityToForceCultureUpon = pTile.zone.city;
      if (cityToForceCultureUpon != null) {
        if (CultureToForceUponCity != null) {
          KeyLib.Get<KeyGenLibCultureManipulationMethodCollection>().ForceCultureOnCity(CultureToForceUponCity, cityToForceCultureUpon);
          WorldTip.showNow("KGPLL_CultureForceConversion_Success", true, "top");
          return true;
        }
        WorldTip.showNow("KGPLL_CultureForceConversion_NoCultureSelectedError", true, "top");
        return false;
      }
      WorldTip.showNow("KGPLL_CultureForceConversion_NoCitySelectedError", true, "top");
      return false;
    }

    private static bool ClickWithCreateNewCulture(WorldTile pTile, string pPowerID) {
      City cityToCreateCultureFor = pTile.zone.city;
      if (cityToCreateCultureFor != null) {
        Culture newCulture = World.world.cultures.newCulture(cityToCreateCultureFor.leader);
        KeyLib.Get<KeyGenLibCultureManipulationMethodCollection>().ForceCultureOnCity(newCulture, cityToCreateCultureFor);
        WorldTip.showNow("KGPLL_CultureCreation_Success", true, "top");
        return true;
      }
      WorldTip.showNow("KGPLL_CultureCreation_NoCitySelected", true, "top");
      return false;
    }
    internal static City CityToAddZoneTo;
    private static bool ClickWithAddZoneToCity(WorldTile pTile, string pPowerID) {
      if (CityToAddZoneTo == null) {
        CityToAddZoneTo = pTile.zone.city;
        if (CityToAddZoneTo != null) {
          WorldTip.showNow("KGPLL_CityZoneAddition_SelectZones", true, "top");
          return false;
        }
        WorldTip.showNow("KGPLL_CityZoneAddition_NoCitySelectedError", true, "top");
        return false;
      }
      if (pTile.zone.city == null) {
        CityToAddZoneTo.addZone(pTile.zone);
        return true;
      }
      WorldTip.showNow("KGPLL_CityZoneAddition_ZoneOwnershipConflict", true, "top");
      return false;
    }
    internal static City CityToRemoveZoneFrom;
    private static bool ClickWithRemoveZoneFromCity(WorldTile pTile, string pPowerID) {
      if (CityToRemoveZoneFrom == null) {
        CityToRemoveZoneFrom = pTile.zone.city;
        if (CityToRemoveZoneFrom != null) {
          WorldTip.showNow("KGPLL_CityZoneRemoval_SelectZones", true, "top");
          return false;
        }
        WorldTip.showNow("KGPLL_CityZoneRemoval_NoCitySelectedError", true, "top");
        return false;
      }
      if (pTile.zone.city == CityToRemoveZoneFrom) {
        CityToRemoveZoneFrom.removeZone(pTile.zone);
        return true;
      }
      WorldTip.showNow("KGPLL_CityZoneRemoval_ZoneOwnershipConflict", true, "top");
      return false;
    }

    private static bool ClickWithForceCityAsCapitalCity(WorldTile pTile, string pPowerID) {
      City cityToForceAsCapitalCity = pTile.zone.city;
      if (cityToForceAsCapitalCity != null) {
        cityToForceAsCapitalCity.kingdom.capital = cityToForceAsCapitalCity;
        cityToForceAsCapitalCity.kingdom.data.capitalID = cityToForceAsCapitalCity.kingdom.capital.data.id;
        cityToForceAsCapitalCity.kingdom.location = cityToForceAsCapitalCity.kingdom.capital.city_center;
        WorldTip.showNow("KGPLL_ForceCapital_Success", true, "top");
        return true;
      }
      WorldTip.showNow("KGPLL_ForceCapital_NoCitySelectedError", true, "top");
      return false;
    }
    internal static City CityToForceIntoOtherKingdom;
    private static bool ClickWithForceCityIntoOtherKingdom(WorldTile pTile, string pPowerID) {
      if (CityToForceIntoOtherKingdom == null) {
        CityToForceIntoOtherKingdom = pTile.zone.city;
        if (CityToForceIntoOtherKingdom != null) {
          WorldTip.showNow("KGPLL_ChangeCityKingdom_SelectKingdom", true, "top");
          return false;
        }
        WorldTip.showNow("KGPLL_ChangeCityKingdom_NoCitySelectedError", true, "top");
        return false;
      }
      if (pTile.zone.city?.kingdom != null) {
        CityToForceIntoOtherKingdom.kingdom.cities.Remove(CityToForceIntoOtherKingdom);
        CityToForceIntoOtherKingdom.kingdom = pTile.zone.city.kingdom;
        CityToForceIntoOtherKingdom.kingdom.cities.Add(CityToForceIntoOtherKingdom);
        CityToForceIntoOtherKingdom.units.ToList().ForEach(a => a.setKingdom(CityToForceIntoOtherKingdom.kingdom));
        WorldTip.showNow("KGPLL_ChangeCityKingdom_Success", true, "top");
        return true;
      }
      WorldTip.showNow("KGPLL_ChangeCityKingdom_NoKingdomSelectedError", true, "top");
      return false;
    }

    private static bool ClickWithMakeActorKing(WorldTile pTile, string pPowerID) {
      Actor actorToMakeKing = pTile._units.FirstOrDefault(a => a.kingdom?.isCiv() ?? false);
      if (actorToMakeKing != null) {
        if (actorToMakeKing.kingdom != null) {
          if (actorToMakeKing.kingdom.king != null) {
            actorToMakeKing.kingdom.king.setProfession(UnitProfession.Unit);
          }
          actorToMakeKing.kingdom.setKing(actorToMakeKing);
          WorldTip.showNow("KGPLL_SetKing_Success", true, "top");
          return true;
        }
        WorldTip.showNow("KGPLL_SetKing_NoKingdomOnSelectedActorError", true, "top");
      }
      WorldTip.showNow("KGPLL_SetKing_NoActorSelectedError", true, "top");
      return false;
    }

    private static bool ClickWithPlaceBuilding(WorldTile pTile, string pPowerID) {
      BuildingAsset buildingToPlace = AssetManager.buildings.get(AssetManager.powers.get(pPowerID).drop_id);
      if (buildingToPlace != null) {
        Building newBuilding = World.world.buildings.addBuilding(buildingToPlace.id, pTile);
        if (newBuilding == null)
        {
          EffectsLibrary.spawnAtTile("fx_bad_place", pTile, 0.25f);
          WorldTip.showNow("KGPLL_PlaceBuilding_InvalidTileError", true, "top");
          return false;
        }
        WorldTip.showNow("KGPLL_PlaceBuilding_Success", true, "top");
        return true;
      }
      WorldTip.showNow("KGPLL_PlaceBuilding_NoBuildingSelected", true, "top");
      return false;
    }
  }
}
