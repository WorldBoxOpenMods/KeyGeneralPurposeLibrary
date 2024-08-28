using System.Globalization;

namespace KeyGeneralPurposeLibrary.Powers {
  public class KeyGenLibPowerButtonClickActionLibrary : KLibAssetLibrary<PowerButtonClickAction> {
    public KeyGenLibPowerButtonClickActionLibrary() {
      AddAsset(WhisperOfAlliancePowerButtonPress, out _whisperOfAlliancePowerButtonPressIndex);
      AddAsset(CultureDeletionPowerButtonPress, out _cultureDeletionPowerButtonPressIndex);
      AddAsset(CultureResetPowerButtonPress, out _cultureResetPowerButtonPressIndex);
      AddAsset(CultureTechResetPowerButtonPress, out _cultureTechResetPowerButtonPressIndex);
      AddAsset(CultureKnowledgeGainModificationPowerButtonPress, out _cultureKnowledgeGainModificationPowerButtonPressIndex);
      AddAsset(CultureForceSelectCulturePowerButtonPress, out _cultureForceSelectCulturePowerButtonPressIndex);
      AddAsset(CultureForceSelectCityPowerButtonPress, out _cultureForceSelectCityPowerButtonPressIndex);
      AddAsset(CreateNewCulturePowerButtonPress, out _createNewCulturePowerButtonPressIndex);
      AddAsset(AddZoneToCityPowerButtonPress, out _addZoneToCityPowerButtonPressIndex);
      AddAsset(RemoveZoneFromCityPowerButtonPress, out _removeZoneFromCityPowerButtonPressIndex);
      AddAsset(AddZoneToCulturePowerButtonPress, out _addZoneToCulturePowerButtonPressIndex);
      AddAsset(RemoveZoneFromCulturePowerButtonPress, out _removeZoneFromCulturePowerButtonPressIndex);
      AddAsset(ForceCityAsCapitalCityPowerButtonPress, out _forceCityAsCapitalCityPowerButtonPressIndex);
      AddAsset(ForceCityIntoOtherKingdomPowerButtonPress, out _forceCityIntoOtherKingdomPowerButtonPressIndex);
      AddAsset(MakeActorKingPowerButtonPress, out _makeActorKingPowerButtonPressIndex);
      AddAsset(PlaceBuildingPowerButtonPress, out _placeBuildingPowerButtonPressIndex);
    }
    private static int _whisperOfAlliancePowerButtonPressIndex;
    private static int _cultureDeletionPowerButtonPressIndex;
    private static int _cultureResetPowerButtonPressIndex;
    private static int _cultureTechResetPowerButtonPressIndex;
    private static int _cultureKnowledgeGainModificationPowerButtonPressIndex;
    private static int _cultureForceSelectCulturePowerButtonPressIndex;
    private static int _cultureForceSelectCityPowerButtonPressIndex;
    private static int _createNewCulturePowerButtonPressIndex;
    private static int _addZoneToCityPowerButtonPressIndex;
    private static int _removeZoneFromCityPowerButtonPressIndex;
    private static int _addZoneToCulturePowerButtonPressIndex;
    private static int _removeZoneFromCulturePowerButtonPressIndex;
    private static int _forceCityAsCapitalCityPowerButtonPressIndex;
    private static int _forceCityIntoOtherKingdomPowerButtonPressIndex;
    private static int _makeActorKingPowerButtonPressIndex;
    private static int _placeBuildingPowerButtonPressIndex;
    public static int WhisperOfAlliancePowerButtonPressIndex => _whisperOfAlliancePowerButtonPressIndex;
    public static int CultureDeletionPowerButtonPressIndex => _cultureDeletionPowerButtonPressIndex;
    public static int CultureResetPowerButtonPressIndex => _cultureResetPowerButtonPressIndex;
    public static int CultureTechResetPowerButtonPressIndex => _cultureTechResetPowerButtonPressIndex;
    public static int CultureKnowledgeGainModificationPowerButtonPressIndex => _cultureKnowledgeGainModificationPowerButtonPressIndex;
    public static int CultureForceSelectCulturePowerButtonPressIndex => _cultureForceSelectCulturePowerButtonPressIndex;
    public static int CultureForceSelectCityPowerButtonPressIndex => _cultureForceSelectCityPowerButtonPressIndex;
    public static int CreateNewCulturePowerButtonPressIndex => _createNewCulturePowerButtonPressIndex;
    public static int AddZoneToCityPowerButtonPressIndex => _addZoneToCityPowerButtonPressIndex;
    public static int RemoveZoneFromCityPowerButtonPressIndex => _removeZoneFromCityPowerButtonPressIndex;
    public static int AddZoneToCulturePowerButtonPressIndex => _addZoneToCulturePowerButtonPressIndex;
    public static int RemoveZoneFromCulturePowerButtonPressIndex => _removeZoneFromCulturePowerButtonPressIndex;
    public static int ForceCityAsCapitalCityPowerButtonPressIndex => _forceCityAsCapitalCityPowerButtonPressIndex;
    public static int ForceCityIntoOtherKingdomPowerButtonPressIndex => _forceCityIntoOtherKingdomPowerButtonPressIndex;
    public static int MakeActorKingPowerButtonPressIndex => _makeActorKingPowerButtonPressIndex;
    public static int PlaceBuildingPowerButtonPressIndex => _placeBuildingPowerButtonPressIndex;

    private static bool WhisperOfAlliancePowerButtonPress(string _) {
      WorldTip.showNow("KGPLL_AllianceCreation_SelectFirstKingdom", true, "top");
      Config.whisperA = null;
      Config.whisperB = null;
      return false;
    }

    private static bool CultureDeletionPowerButtonPress(string _) {
      WorldTip.showNow("KGPLL_CultureDeletion_SelectCulture", true, "top");
      return false;
    }
    
    private static bool CultureResetPowerButtonPress(string _) {
      WorldTip.showNow("KGPLL_CultureFullReset_SelectCulture", true, "top");
      return false;
    }
    
    private static bool CultureTechResetPowerButtonPress(string _) {
      WorldTip.showNow("KGPLL_CultureTechReset_SelectCulture", true, "top");
      return false;
    }
    
    private static bool CultureKnowledgeGainModificationPowerButtonPress(string pPowerID) {
      string modifierString = AssetManager.powers.get(pPowerID).dropID;
      if (int.TryParse(modifierString, NumberStyles.Integer, CultureInfo.InvariantCulture, out int modifier)) {
        WorldTip.showNow("KGPLL_CultureKnowledgeGainModification_Select" + (modifier > 0 ? "Increase" : "Decrease") + "Culture", true, "top");
      }
      return false;
    }

    private static bool CultureForceSelectCulturePowerButtonPress(string _) {
      WorldTip.showNow("KGPLL_CultureForceConversion_SelectCulture", true, "top");
      return false;
    }
    
    private static bool CultureForceSelectCityPowerButtonPress(string _) {
      KeyGenLibPowerActionWithIdLibrary.CultureToForceUponCity = null;
      WorldTip.showNow("KGPLL_CultureForceConversion_SelectCity", true, "top");
      return false;
    }
    
    private static bool CreateNewCulturePowerButtonPress(string _) {
      WorldTip.showNow("KGPLL_CultureCreation_SelectCity", true, "top");
      return false;
    }
    
    private static bool AddZoneToCityPowerButtonPress(string _) {
      KeyGenLibPowerActionWithIdLibrary.CityToAddZoneTo = null;
      WorldTip.showNow("KGPLL_CityZoneAddition_SelectCity", true, "top");
      return false;
    }

    private static bool RemoveZoneFromCityPowerButtonPress(string _) {
      KeyGenLibPowerActionWithIdLibrary.CityToRemoveZoneFrom = null;
      WorldTip.showNow("KGPLL_CityZoneRemoval_SelectCity", true, "top");
      return false;
    }

    private static bool AddZoneToCulturePowerButtonPress(string _) {
      KeyGenLibPowerActionWithIdLibrary.CultureToAddZoneTo = null;
      WorldTip.showNow("KGPLL_CultureZoneAddition_SelectCulture", true, "top");
      return false;
    }
    
    private static bool RemoveZoneFromCulturePowerButtonPress(string _) {
      KeyGenLibPowerActionWithIdLibrary.CultureToRemoveZoneFrom = null;
      WorldTip.showNow("KGPLL_CultureZoneRemoval_SelectCulture", true, "top");
      return false;
    }
    
    private static bool ForceCityAsCapitalCityPowerButtonPress(string _) {
      WorldTip.showNow("KGPLL_ForceCapital_SelectCity", true, "top");
      return false;
    }
    
    private static bool ForceCityIntoOtherKingdomPowerButtonPress(string _) {
      KeyGenLibPowerActionWithIdLibrary.CityToForceIntoOtherKingdom = null;
      WorldTip.showNow("KGPLL_ChangeCityKingdom_SelectCity", true, "top");
      return false;
    }
    
    private static bool MakeActorKingPowerButtonPress(string _) {
      WorldTip.showNow("KGPLL_SetKing_SelectActor", true, "top");
      return false;
    }
    
    private static bool PlaceBuildingPowerButtonPress(string _) {
      WorldTip.showNow("KGPLL_PlaceBuilding_SelectCity", true, "top");
      return false;
    }
  }
}
