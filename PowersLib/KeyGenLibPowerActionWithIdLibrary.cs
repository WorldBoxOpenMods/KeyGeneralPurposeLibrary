using System.Collections.Generic;
using System.Linq;
using KeyGeneralPurposeLibrary.BehaviourManipulation;
using UnityEngine;
namespace KeyGeneralPurposeLibrary.PowersLib {
  public class KeyGenLibPowerActionWithIdLibrary : KLibAssetLibrary<PowerActionWithID> {
    public KeyGenLibPowerActionWithIdLibrary() {
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
  }
}
