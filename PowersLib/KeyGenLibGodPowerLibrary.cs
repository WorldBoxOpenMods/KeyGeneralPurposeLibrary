namespace KeyGeneralPurposeLibrary.PowersLib {
  public class KeyGenLibGodPowerLibrary : KLibAssetLibrary<GodPower> {
    public KeyGenLibGodPowerLibrary() {
      AddAsset(_cultureDeletion, out _cultureDeletionIndex);
      AddAsset(_cultureReset, out _cultureResetIndex);
      AddAsset(_cultureForceSelectCulture, out _cultureForceSelectCultureIndex);
      AddAsset(_cultureForceSelectCity, out _cultureForceSelectCityIndex);
      AddAsset(_createNewCulture, out _createNewCultureIndex);
      AddAsset(_addZoneToCity, out _addZoneToCityIndex);
      AddAsset(_removeZoneFromCity, out _removeZoneFromCityIndex);
      AddAsset(_forceCityAsCapitalCity, out _forceCityAsCapitalCityIndex);
      AddAsset(_forceCityIntoOtherKingdom, out _forceCityIntoOtherKingdomIndex);
      AddAsset(_makeActorKing, out _makeActorKingIndex);
      AddAsset(_placeBuilding, out _placeBuildingIndex);
    }
    private static int _whisperOfAllianceIndex;
    private static int _massTraitRemovalRainIndex;
    private static int _cultureDeletionIndex;
    private static int _cultureResetIndex;
    private static int _cultureForceSelectCultureIndex;
    private static int _cultureForceSelectCityIndex;
    private static int _createNewCultureIndex;
    private static int _addZoneToCityIndex;
    private static int _removeZoneFromCityIndex;
    private static int _forceCityAsCapitalCityIndex;
    private static int _forceCityIntoOtherKingdomIndex;
    private static int _massItemAdditionRainIndex;
    private static int _makeActorKingIndex;
    private static int _placeBuildingIndex;
    public static int WhisperOfAllianceIndex => _whisperOfAllianceIndex;
    public static int MassTraitRemovalRainIndex => _massTraitRemovalRainIndex;
    public static int CultureDeletionIndex => _cultureDeletionIndex;
    public static int CultureResetIndex => _cultureResetIndex;
    public static int CultureForceSelectCultureIndex => _cultureForceSelectCultureIndex;
    public static int CultureForceSelectCityIndex => _cultureForceSelectCityIndex;
    public static int CreateNewCultureIndex => _createNewCultureIndex;
    public static int AddZoneToCityIndex => _addZoneToCityIndex;
    public static int RemoveZoneFromCityIndex => _removeZoneFromCityIndex;
    public static int ForceCityAsCapitalCityIndex => _forceCityAsCapitalCityIndex;
    public static int ForceCityIntoOtherKingdomIndex => _forceCityIntoOtherKingdomIndex;
    public static int MassItemAdditionRainIndex => _massItemAdditionRainIndex;
    public static int MakeActorKingIndex => _makeActorKingIndex;
    public static int PlaceBuildingIndex => _placeBuildingIndex;

    internal override void Initialize() {
      base.Initialize();
      foreach (GodPower power in Assets) {
        AssetManager.powers.add(power);
      }
    }
    
    private GodPower _massTraitRemovalRain;
    
    private readonly GodPower _cultureDeletion;
    
    private readonly GodPower _cultureReset;
    
    private readonly GodPower _cultureForceSelectCulture;
    
    private readonly GodPower _cultureForceSelectCity;
    
    private readonly GodPower _createNewCulture;
    
    private readonly GodPower _addZoneToCity;
    
    private readonly GodPower _removeZoneFromCity;
    
    private readonly GodPower _forceCityAsCapitalCity;
    
    private readonly GodPower _forceCityIntoOtherKingdom;
    
    private GodPower _massItemAdditionRain;
    
    private readonly GodPower _makeActorKing;
    
    private readonly GodPower _placeBuilding;
  }
}
