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
        WorldTip.showNow("Select the second kingdom to create an alliance with.", false, "top");
        return false;
      }

      if (Config.whisperB == null && Config.whisperA == kingdom) {
        WorldTip.showNow("Selected the same kingdom twice, try again.", false, "top");
        return false;
      }

      if (Config.whisperB == null) {
        Config.whisperB = kingdom;
      }

      if (Config.whisperB != Config.whisperA) {
        if (Alliance.isSame(Config.whisperA.getAlliance(), Config.whisperB.getAlliance())) {
          WorldTip.showNow("The selected kingdoms are already allied, try again.", false, "top");
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
        WorldTip.showNow("The kingdom " + Config.whisperA.name + " has successfully entered an alliance with kingdom " + Config.whisperB.name + ".", false, "top");
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
        WorldTip.showNow("Culture deleted!", false, "top");
        return true;
      }

      WorldTip.showNow("No culture to delete here, try again!", false, "top");
      return false;
    }

    private static bool ClickWithCultureReset(WorldTile pTile, string pPowerID) {
      Culture cultureToReset = pTile.zone.culture;
      if (cultureToReset != null) {
        KeyLib.Get<KeyGenLibCultureManipulationMethodCollection>().ResetCulture(cultureToReset);
        WorldTip.showNow("Culture reset!", false, "top");
        return true;
      }

      WorldTip.showNow("No culture to reset here, try again!", false, "top");
      return false;
    }

    private static bool ClickWithCultureTechReset(WorldTile pTile, string pPowerID) {
      Culture cultureToReset = pTile.zone.culture;
      if (cultureToReset != null) {
        KeyLib.Get<KeyGenLibCultureManipulationMethodCollection>().ResetCultureTech(cultureToReset);
        WorldTip.showNow("Culture tech reset!", false, "top");
        return true;
      }

      WorldTip.showNow("No culture to reset the tech of here, try again!", false, "top");
      return false;
    }

    private static bool ClickWithCultureKnowledgeGainModification(WorldTile pTile, string pPowerID) {
      Culture cultureToIncrease = pTile.zone.culture;
      if (cultureToIncrease != null) {
        string modifierString = AssetManager.powers.get(pPowerID).dropID;
        if (int.TryParse(modifierString, NumberStyles.Integer, CultureInfo.InvariantCulture, out int modifier)) {
          KeyLib.Get<KeyGenLibCultureManipulationMethodCollection>().ModifyKnowledgeGain(cultureToIncrease, modifier);
          WorldTip.showNow("Culture knowledge gain " + (modifier > 0 ? "increased" : "decreased") + "!", false, "top");
        } else {
          WorldTip.showNow("Invalid value for knowledge gain provided, try again!", false, "top");
          return false;
        }
        return true;
      }
      WorldTip.showNow("No culture to change the knowledge gain of here, try again!", false, "top");
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
          WorldTip.showNow("Select the city to force the culture upon.", false, "top");
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
          return true;
        }
        Debug.LogError("Something went wrong with the Culture Conversion! Please report this to the mod author!");
        return false;
      }
      WorldTip.showNow("No culture to force upon city here, try again!", false, "top");
      return false;
    }

    private static bool ClickWithCultureForceSelectCity(WorldTile pTile, string pPowerID) {
      City cityToForceCultureUpon = pTile.zone.city;
      if (cityToForceCultureUpon != null) {
        if (CultureToForceUponCity != null) {
          KeyLib.Get<KeyGenLibCultureManipulationMethodCollection>().ForceCultureOnCity(CultureToForceUponCity, cityToForceCultureUpon);
          WorldTip.showNow("Culture forced upon city!", false, "top");
          return true;
        }
        WorldTip.showNow("No culture to force upon city here, try again!", false, "top");
        return false;
      }
      WorldTip.showNow("No city to force culture upon here, try again!", false, "top");
      return false;
    }

    private static bool ClickWithCreateNewCulture(WorldTile pTile, string pPowerID) {
      City cityToCreateCultureFor = pTile.zone.city;
      if (cityToCreateCultureFor != null) {
        Culture newCulture = World.world.cultures.newCulture(cityToCreateCultureFor.race, cityToCreateCultureFor);
        KeyLib.Get<KeyGenLibCultureManipulationMethodCollection>().ForceCultureOnCity(newCulture, cityToCreateCultureFor);
        WorldTip.showNow("New culture created!", false, "top");
        return true;
      }
      WorldTip.showNow("No city to create new culture for here, try again!", false, "top");
      return false;
    }
    internal static City CityToAddZoneTo;
    private static bool ClickWithAddZoneToCity(WorldTile pTile, string pPowerID) {
      if (CityToAddZoneTo == null) {
        CityToAddZoneTo = pTile.zone.city;
        if (CityToAddZoneTo != null) {
          WorldTip.showNow("Select zones to add to the city.", false, "top");
          return false;
        }
        WorldTip.showNow("No city to add zone to here, try again!", false, "top");
        return false;
      }
      if (pTile.zone.city == null) {
        CityToAddZoneTo.addZone(pTile.zone);
        WorldTip.showNow("Zone added to city!", false, "top");
        return true;
      }
      WorldTip.showNow("The zone you're trying to add already belongs to another city, please remove it from that city first!", false, "top");
      return false;
    }
    internal static City CityToRemoveZoneFrom;
    private static bool ClickWithRemoveZoneFromCity(WorldTile pTile, string pPowerID) {
      if (CityToRemoveZoneFrom == null) {
        CityToRemoveZoneFrom = pTile.zone.city;
        if (CityToRemoveZoneFrom != null) {
          WorldTip.showNow("Select zones to remove.", false, "top");
          return false;
        }
        WorldTip.showNow("No city to remove zone from here, try again!", false, "top");
        return false;
      }
      if (pTile.zone.city == CityToRemoveZoneFrom) {
        CityToRemoveZoneFrom.removeZone(pTile.zone);
        WorldTip.showNow("Zone removed from city!", false, "top");
        return true;
      }
      WorldTip.showNow("The zone you're trying to remove doesn't belong to the city you selected, please select the correct city!", false, "top");
      return false;
    }
    internal static Culture CultureToAddZoneTo;
    private static bool ClickWithAddZoneToCulture(WorldTile pTile, string pPowerID) {
      if (CultureToAddZoneTo == null) {
        CultureToAddZoneTo = pTile.zone.culture;
        if (CultureToAddZoneTo != null) {
          WorldTip.showNow("Select zones to add to the culture.", false, "top");
          return false;
        }
        WorldTip.showNow("No culture to add zone to here, try again!", false, "top");
        return false;
      }
      if (pTile.zone.culture == null) {
        CultureToAddZoneTo.addZone(pTile.zone);
        WorldTip.showNow("Zone added to culture!", false, "top");
        return true;
      }
      WorldTip.showNow("The zone you're trying to add already belongs to another culture, please remove it from that culture first!", false, "top");
      return false;
    }
    internal static Culture CultureToRemoveZoneFrom;
    private static bool ClickWithRemoveZoneFromCulture(WorldTile pTile, string pPowerID) {
      if (CultureToRemoveZoneFrom == null) {
        CultureToRemoveZoneFrom = pTile.zone.culture;
        if (CultureToRemoveZoneFrom != null) {
          WorldTip.showNow("Select zones to remove.", false, "top");
          return false;
        }
        WorldTip.showNow("No culture to remove zone from here, try again!", false, "top");
        return false;
      }
      if (pTile.zone.culture == CultureToRemoveZoneFrom) {
        CultureToRemoveZoneFrom.removeZone(pTile.zone);
        WorldTip.showNow("Zone removed from culture!", false, "top");
        return true;
      }
      WorldTip.showNow("The zone you're trying to remove doesn't belong to the culture you selected, please select the correct culture!", false, "top");
      return false;
    }

    private static bool ClickWithForceCityAsCapitalCity(WorldTile pTile, string pPowerID) {
      City cityToForceAsCapitalCity = pTile.zone.city;
      if (cityToForceAsCapitalCity != null) {
        cityToForceAsCapitalCity.kingdom.capital = cityToForceAsCapitalCity;
        cityToForceAsCapitalCity.kingdom.data.capitalID = cityToForceAsCapitalCity.kingdom.capital.data.id;
        cityToForceAsCapitalCity.kingdom.location = cityToForceAsCapitalCity.kingdom.capital.cityCenter;
        WorldTip.showNow("City forced as capital city!", false, "top");
        return true;
      }
      WorldTip.showNow("No city to force as capital city here, try again!", false, "top");
      return false;
    }
    internal static City CityToForceIntoOtherKingdom;
    private static bool ClickWithForceCityIntoOtherKingdom(WorldTile pTile, string pPowerID) {
      if (CityToForceIntoOtherKingdom == null) {
        CityToForceIntoOtherKingdom = pTile.zone.city;
        if (CityToForceIntoOtherKingdom != null) {
          WorldTip.showNow("Select the kingdom to force the city into.", false, "top");
          return false;
        }
        WorldTip.showNow("No city to force into other kingdom here, try again!", false, "top");
        return false;
      }
      if (pTile.zone.city?.kingdom != null) {
        CityToForceIntoOtherKingdom.kingdom.removeCity(CityToForceIntoOtherKingdom);
        CityToForceIntoOtherKingdom.kingdom = pTile.zone.city.kingdom;
        CityToForceIntoOtherKingdom.kingdom.addCity(CityToForceIntoOtherKingdom);
        CityToForceIntoOtherKingdom.units.ToList().ForEach(a => CityToForceIntoOtherKingdom.kingdom.addUnit(a));
        WorldTip.showNow("City forced into other kingdom!", false, "top");
        return true;
      }
      WorldTip.showNow("The tile you selected isn't owned by any kingdom!", false, "top");
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
          WorldTip.showNow("Actor made king!", false, "top");
          return true;
        }
        WorldTip.showNow("The actor you selected doesn't have a kingdom!", false, "top");
      }
      WorldTip.showNow("No actor to make king here, try again!", false, "top");
      return false;
    }

    private static bool ClickWithPlaceBuilding(WorldTile pTile, string pPowerID) {
      BuildingAsset buildingToPlace = AssetManager.buildings.get(AssetManager.powers.get(pPowerID).dropID);
      if (buildingToPlace != null) {
        Building newBuilding = World.world.buildings.addBuilding(buildingToPlace.id, pTile);
        if (newBuilding == null)
        {
          EffectsLibrary.spawnAtTile("fx_bad_place", pTile, 0.25f);
          WorldTip.showNow("Can't place building here, try again!", false, "top");
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
        WorldTip.showNow("Building placed!", false, "top");
        return true;
      }
      WorldTip.showNow("No building to place here, try again!", false, "top");
      return false;
    }
  }
}