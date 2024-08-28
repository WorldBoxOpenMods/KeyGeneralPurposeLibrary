using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using KeyGeneralPurposeLibrary.BehaviourManipulation;
using UnityEngine;

namespace KeyGeneralPurposeLibrary.Powers {
  public class KeyGenLibPowerActionWithIdLibrary : KLibAssetLibrary<PowerActionWithID> {
    public KeyGenLibPowerActionWithIdLibrary() {
      AddAsset(ClickWithWhisperOfAlliance, out _clickWithWhisperOfAllianceIndex);
      AddAsset(ClickWithCultureDeletion, out _clickWithCultureDeletionIndex);
      AddAsset(ClickWithCultureReset, out _clickWithCultureResetIndex);
      AddAsset(ClickWithCultureTechReset, out _clickWithCultureTechResetIndex);
      AddAsset(ClickWithCultureKnowledgeGainModification, out _clickWithCultureKnowledgeGainModificationIndex);
      AddAsset(ClickWithCultureForceSelectCulture, out _clickWithCultureForceSelectCultureIndex);
      AddAsset(ClickWithCultureForceSelectCity, out _clickWithCultureForceSelectCityIndex);
      AddAsset(ClickWithCreateNewCulture, out _clickWithCreateNewCultureIndex);
      AddAsset(ClickWithAddZoneToCity, out _clickWithAddZoneToCityIndex);
      AddAsset(ClickWithRemoveZoneFromCity, out _clickWithRemoveZoneFromCityIndex);
      AddAsset(ClickWithAddZoneToCulture, out _clickWithAddZoneToCultureIndex);
      AddAsset(ClickWithRemoveZoneFromCulture, out _clickWithRemoveZoneFromCultureIndex);
      AddAsset(ClickWithForceCityAsCapitalCity, out _clickWithForceCityAsCapitalCityIndex);
      AddAsset(ClickWithForceCityIntoOtherKingdom, out _clickWithForceCityIntoOtherKingdomIndex);
      AddAsset(ClickWithMakeActorKing, out _clickWithMakeActorKingIndex);
      AddAsset(ClickWithPlaceBuilding, out _clickWithPlaceBuildingIndex);
    }
    private static int _clickWithWhisperOfAllianceIndex;
    private static int _clickWithCultureDeletionIndex;
    private static int _clickWithCultureResetIndex;
    private static int _clickWithCultureTechResetIndex;
    private static int _clickWithCultureKnowledgeGainModificationIndex;
    private static int _clickWithCultureForceSelectCultureIndex;
    private static int _clickWithCultureForceSelectCityIndex;
    private static int _clickWithCreateNewCultureIndex;
    private static int _clickWithAddZoneToCityIndex;
    private static int _clickWithRemoveZoneFromCityIndex;
    private static int _clickWithAddZoneToCultureIndex;
    private static int _clickWithRemoveZoneFromCultureIndex;
    private static int _clickWithForceCityAsCapitalCityIndex;
    private static int _clickWithForceCityIntoOtherKingdomIndex;
    private static int _clickWithMakeActorKingIndex;
    private static int _clickWithPlaceBuildingIndex;
    public static int ClickWithWhisperOfAllianceIndex => _clickWithWhisperOfAllianceIndex;
    public static int ClickWithCultureDeletionIndex => _clickWithCultureDeletionIndex;
    public static int ClickWithCultureResetIndex => _clickWithCultureResetIndex;
    public static int ClickWithCultureTechResetIndex => _clickWithCultureTechResetIndex;
    public static int ClickWithCultureKnowledgeGainModificationIndex => _clickWithCultureKnowledgeGainModificationIndex;
    public static int ClickWithCultureForceSelectCultureIndex => _clickWithCultureForceSelectCultureIndex;
    public static int ClickWithCultureForceSelectCityIndex => _clickWithCultureForceSelectCityIndex;
    public static int ClickWithCreateNewCultureIndex => _clickWithCreateNewCultureIndex;
    public static int ClickWithAddZoneToCityIndex => _clickWithAddZoneToCityIndex;
    public static int ClickWithRemoveZoneFromCityIndex => _clickWithRemoveZoneFromCityIndex;
    public static int ClickWithAddZoneToCultureIndex => _clickWithAddZoneToCultureIndex;
    public static int ClickWithRemoveZoneFromCultureIndex => _clickWithRemoveZoneFromCultureIndex;
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
      if (Config.whisperA == null) {
        Config.whisperA = kingdom;
        WorldTip.showNow("KGPLL_AllianceCreation_SelectSecondKingdom", true, "top");
        return false;
      }

      if (Config.whisperB == null && Config.whisperA == kingdom) {
        WorldTip.showNow("KGPLL_AllianceCreation_SameKingdomTwiceError", true, "top");
        return false;
      }

      if (Config.whisperB == null) {
        Config.whisperB = kingdom;
      }

      if (Config.whisperB != Config.whisperA) {
        if (Alliance.isSame(Config.whisperA.getAlliance(), Config.whisperB.getAlliance())) {
          WorldTip.showNow("KGPLL_AllianceCreation_KingdomsAlreadyAlliedError", true, "top");
          Config.whisperB = null;
          return false;
        }

        foreach (War war in World.world.wars.getWars(Config.whisperA).Where(war => war.isInWarWith(Config.whisperA, Config.whisperB))) {
          war.removeFromWar(Config.whisperA);
          war.removeFromWar(Config.whisperB);
        }

        Alliance allianceA = Config.whisperA.getAlliance();
        Alliance allianceB = Config.whisperB.getAlliance();
        if (allianceA != null) {
          if (allianceB != null) {
            IEnumerable<Kingdom> kingdoms = allianceB.kingdoms_list;
            World.world.alliances.dissolveAlliance(allianceB);
            foreach (Kingdom ally in kingdoms) {
              ForceIntoAlliance(allianceA, ally);
            }
          } else {
            ForceIntoAlliance(allianceA, Config.whisperB);
          }
        } else {
          if (allianceB == null) {
            ForceNewAlliance(Config.whisperA, Config.whisperB);
          } else {
            ForceIntoAlliance(allianceB, Config.whisperA);
          }
        }
        WorldTip.showNow(string.Format(LocalizedTextManager.getText("KGPLL_AllianceCreation_CreationSuccess"), Config.whisperA.name, Config.whisperB.name), false, "top");
        Config.whisperA = null;
        Config.whisperB = null;
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
      alliance.createAlliance();
      alliance.data.founder_kingdom_1 = kingdomA.data.name;
      if (kingdomA.king != null) {
        alliance.data.founder_name_1 = kingdomA.king.getName();
      }
      ForceIntoAlliance(alliance, kingdomA);
      ForceIntoAlliance(alliance, kingdomB);
      WorldLog.logAllianceCreated(alliance);
    }

    private static bool ClickWithCultureDeletion(WorldTile pTile, string pPowerID) {
      Culture cultureToWipe = pTile.zone.culture;
      if (cultureToWipe != null) {
        KeyLib.Get<KeyGenLibCultureManipulationMethodCollection>().DeleteCulture(cultureToWipe);
        WorldTip.showNow("KGPLL_CultureDeletion_Success", true, "top");
        return true;
      }

      WorldTip.showNow("KGPLL_CultureDeletion_NoCultureSelectedError", true, "top");
      return false;
    }

    private static bool ClickWithCultureReset(WorldTile pTile, string pPowerID) {
      Culture cultureToReset = pTile.zone.culture;
      if (cultureToReset != null) {
        KeyLib.Get<KeyGenLibCultureManipulationMethodCollection>().ResetCulture(cultureToReset);
        WorldTip.showNow("KGPLL_CultureFullReset_Success", true, "top");
        return true;
      }

      WorldTip.showNow("KGPLL_CultureFullReset_NoCultureSelectedError", true, "top");
      return false;
    }

    private static bool ClickWithCultureTechReset(WorldTile pTile, string pPowerID) {
      Culture cultureToReset = pTile.zone.culture;
      if (cultureToReset != null) {
        KeyLib.Get<KeyGenLibCultureManipulationMethodCollection>().ResetCultureTech(cultureToReset);
        WorldTip.showNow("KGPLL_CultureTechReset_Success", true, "top");
        return true;
      }

      WorldTip.showNow("KGPLL_CultureTechReset_NoCultureSelectedError", false, "top");
      return false;
    }

    private static bool ClickWithCultureKnowledgeGainModification(WorldTile pTile, string pPowerID) {
      Culture cultureToIncrease = pTile.zone.culture;
      if (cultureToIncrease != null) {
        string modifierString = AssetManager.powers.get(pPowerID).dropID;
        if (int.TryParse(modifierString, NumberStyles.Integer, CultureInfo.InvariantCulture, out int modifier)) {
          KeyLib.Get<KeyGenLibCultureManipulationMethodCollection>().ModifyKnowledgeGain(cultureToIncrease, modifier);
          WorldTip.showNow("KGPLL_CultureKnowledgeGainModification_" + (modifier > 0 ? "Increase" : "Decrease") + "Success", true, "top");
        } else {
          WorldTip.showNow("KGPLL_CultureKnowledgeGainModification_InvalidGainValueError", true, "top");
          return false;
        }
        return true;
      }
      WorldTip.showNow("KGPLL_CultureKnowledgeGainModification_NoCultureSelectedError", true, "top");
      return false;
    }

    internal static Culture CultureToForceUponCity;
    private static bool ClickWithCultureForceSelectCulture(WorldTile pTile, string pPowerID) {
      Culture cultureToForce = pTile.zone.culture;
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
        Culture newCulture = World.world.cultures.newCulture(cityToCreateCultureFor.race, cityToCreateCultureFor);
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
    internal static Culture CultureToAddZoneTo;
    private static bool ClickWithAddZoneToCulture(WorldTile pTile, string pPowerID) {
      if (CultureToAddZoneTo == null) {
        CultureToAddZoneTo = pTile.zone.culture;
        if (CultureToAddZoneTo != null) {
          WorldTip.showNow("KGPLL_CultureZoneAddition_SelectZones", true, "top");
          return false;
        }
        WorldTip.showNow("KGPLL_CultureZoneAddition_NoCultureSelected", true, "top");
        return false;
      }
      if (pTile.zone.culture == null) {
        CultureToAddZoneTo.addZone(pTile.zone);
        return true;
      }
      WorldTip.showNow("KGPLL_CultureZoneAddition_ZoneOwnershipConflict", true, "top");
      return false;
    }
    internal static Culture CultureToRemoveZoneFrom;
    private static bool ClickWithRemoveZoneFromCulture(WorldTile pTile, string pPowerID) {
      if (CultureToRemoveZoneFrom == null) {
        CultureToRemoveZoneFrom = pTile.zone.culture;
        if (CultureToRemoveZoneFrom != null) {
          WorldTip.showNow("KGPLL_CultureZoneRemoval_SelectZones", true, "top");
          return false;
        }
        WorldTip.showNow("KGPLL_CultureZoneRemoval_NoCultureSelected", true, "top");
        return false;
      }
      if (pTile.zone.culture == CultureToRemoveZoneFrom) {
        CultureToRemoveZoneFrom.removeZone(pTile.zone);
        return true;
      }
      WorldTip.showNow("KGPLL_CultureZoneRemoval_ZoneOwnershipConflict", true, "top");
      return false;
    }

    private static bool ClickWithForceCityAsCapitalCity(WorldTile pTile, string pPowerID) {
      City cityToForceAsCapitalCity = pTile.zone.city;
      if (cityToForceAsCapitalCity != null) {
        cityToForceAsCapitalCity.kingdom.capital = cityToForceAsCapitalCity;
        cityToForceAsCapitalCity.kingdom.data.capitalID = cityToForceAsCapitalCity.kingdom.capital.data.id;
        cityToForceAsCapitalCity.kingdom.location = cityToForceAsCapitalCity.kingdom.capital.cityCenter;
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
        CityToForceIntoOtherKingdom.kingdom.removeCity(CityToForceIntoOtherKingdom);
        CityToForceIntoOtherKingdom.kingdom = pTile.zone.city.kingdom;
        CityToForceIntoOtherKingdom.kingdom.addCity(CityToForceIntoOtherKingdom);
        CityToForceIntoOtherKingdom.units.ToList().ForEach(a => CityToForceIntoOtherKingdom.kingdom.addUnit(a));
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
      BuildingAsset buildingToPlace = AssetManager.buildings.get(AssetManager.powers.get(pPowerID).dropID);
      if (buildingToPlace != null) {
        Building newBuilding = World.world.buildings.addBuilding(buildingToPlace.id, pTile);
        if (newBuilding == null)
        {
          EffectsLibrary.spawnAtTile("fx_bad_place", pTile, 0.25f);
          WorldTip.showNow("KGPLL_PlaceBuilding_InvalidTileError", true, "top");
          return false;
        }
        if (newBuilding.asset.cityBuilding) {
          if( pTile.zone.city != null) {
            pTile.zone.city.addBuilding(newBuilding);
            newBuilding.retake();
          } else {
            newBuilding.makeRuins();
          }
        }
        WorldTip.showNow("KGPLL_PlaceBuilding_Success", true, "top");
        return true;
      }
      WorldTip.showNow("KGPLL_PlaceBuilding_NoBuildingSelected", true, "top");
      return false;
    }
  }
}
