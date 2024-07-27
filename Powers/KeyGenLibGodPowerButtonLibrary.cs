using KeyGeneralPurposeLibrary.Assets;

namespace KeyGeneralPurposeLibrary.Powers {
  public class KeyGenLibGodPowerButtonLibrary : KLibAssetLibrary<PowerButton> {
    internal override void Initialize() {
      base.Initialize();
      _whisperOfAllianceButton = KeyLib.Get<KeyGenLibGodPowerButtonGenerator>().
        CreateHiddenPowerButton(
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite"),
          KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.WhisperOfAllianceIndex],
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite")
        );
      AddAsset(_whisperOfAllianceButton, out _whisperOfAllianceButtonIndex);
      _massTraitRemovalRainButton = KeyLib.Get<KeyGenLibGodPowerButtonGenerator>().
        CreateHiddenPowerButton(
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite"),
          KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.MassTraitRemovalRainIndex],
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite")
        );
      AddAsset(_massTraitRemovalRainButton, out _massTraitRemovalRainButtonIndex);
      _cultureDeletionButton = KeyLib.Get<KeyGenLibGodPowerButtonGenerator>().
        CreateHiddenPowerButton(
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite"),
          KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.CultureDeletionIndex],
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite")
        );
      AddAsset(_cultureDeletionButton, out _cultureDeletionButtonIndex);
      _cultureResetButton = KeyLib.Get<KeyGenLibGodPowerButtonGenerator>().
        CreateHiddenPowerButton(
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite"),
          KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.CultureResetIndex],
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite")
        );
      AddAsset(_cultureResetButton, out _cultureResetButtonIndex);
      _cultureTechResetButton = KeyLib.Get<KeyGenLibGodPowerButtonGenerator>().
        CreateHiddenPowerButton(
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite"),
          KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.CultureTechResetIndex],
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite")
        );
      AddAsset(_cultureTechResetButton, out _cultureTechResetButtonIndex);
      _cultureKnowledgeGainModificationButton = KeyLib.Get<KeyGenLibGodPowerButtonGenerator>().
        CreateHiddenPowerButton(
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite"),
          KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.CultureKnowledgeGainModificationIndex],
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite")
        );
      AddAsset(_cultureKnowledgeGainModificationButton, out _cultureKnowledgeGainIncreaseButtonIndex);
      _cultureForceSelectCultureButton = KeyLib.Get<KeyGenLibGodPowerButtonGenerator>().
        CreateHiddenPowerButton(
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite"),
          KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.CultureForceSelectCultureIndex],
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite")
        );
      AddAsset(_cultureForceSelectCultureButton, out _cultureForceSelectCultureButtonIndex);
      _cultureForceSelectCityButton = KeyLib.Get<KeyGenLibGodPowerButtonGenerator>().
        CreateHiddenPowerButton(
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite"),
          KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.CultureForceSelectCityIndex],
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite")
        );
      AddAsset(_cultureForceSelectCityButton, out _cultureForceSelectCityButtonIndex);
      _createNewCultureButton = KeyLib.Get<KeyGenLibGodPowerButtonGenerator>().
        CreateHiddenPowerButton(
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite"),
          KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.CreateNewCultureIndex],
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite")
        );
      AddAsset(_createNewCultureButton, out _createNewCultureButtonIndex);
      _addZoneToCityButton = KeyLib.Get<KeyGenLibGodPowerButtonGenerator>().
        CreateHiddenPowerButton(
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite"),
          KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.AddZoneToCityIndex],
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite")
        );
      AddAsset(_addZoneToCityButton, out _addZoneToCityButtonIndex);
      _removeZoneFromCityButton = KeyLib.Get<KeyGenLibGodPowerButtonGenerator>().
        CreateHiddenPowerButton(
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite"),
          KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.RemoveZoneFromCityIndex],
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite")
        );
      AddAsset(_removeZoneFromCityButton, out _removeZoneFromCityButtonIndex);
      _addZoneToCultureButton = KeyLib.Get<KeyGenLibGodPowerButtonGenerator>().
        CreateHiddenPowerButton(
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite"),
          KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.AddZoneToCultureIndex],
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite")
        );
      AddAsset(_addZoneToCultureButton, out _addZoneToCultureButtonIndex);
      _removeZoneFromCultureButton = KeyLib.Get<KeyGenLibGodPowerButtonGenerator>().
        CreateHiddenPowerButton(
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite"),
          KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.RemoveZoneFromCultureIndex],
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite")
        );
      AddAsset(_removeZoneFromCultureButton, out _removeZoneFromCultureButtonIndex);
      _forceCityAsCapitalCityButton = KeyLib.Get<KeyGenLibGodPowerButtonGenerator>().
        CreateHiddenPowerButton(
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite"),
          KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.ForceCityAsCapitalCityIndex],
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite")
        );
      AddAsset(_forceCityAsCapitalCityButton, out _forceCityAsCapitalCityButtonIndex);
      _forceCityIntoOtherKingdomButton = KeyLib.Get<KeyGenLibGodPowerButtonGenerator>().
        CreateHiddenPowerButton(
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite"),
          KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.ForceCityIntoOtherKingdomIndex],
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite")
        );
      AddAsset(_forceCityIntoOtherKingdomButton, out _forceCityIntoOtherKingdomButtonIndex);
      _massItemAdditionRainButton = KeyLib.Get<KeyGenLibGodPowerButtonGenerator>().
        CreateHiddenPowerButton(
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite"),
          KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.MassItemAdditionRainIndex],
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite")
        );
      AddAsset(_massItemAdditionRainButton, out _massItemAdditionRainButtonIndex);
      _makeActorKingButton = KeyLib.Get<KeyGenLibGodPowerButtonGenerator>().
        CreateHiddenPowerButton(
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite"),
          KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.MakeActorKingIndex],
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite")
        );
      AddAsset(_makeActorKingButton, out _makeActorKingButtonIndex);
      _placeBuildingButton = KeyLib.Get<KeyGenLibGodPowerButtonGenerator>().
        CreateHiddenPowerButton(
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite"),
          KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.PlaceBuildingIndex],
          KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite")
        );
      AddAsset(_placeBuildingButton, out _placeBuildingButtonIndex);
    }

    private static int _whisperOfAllianceButtonIndex;
    private static int _massTraitRemovalRainButtonIndex;
    private static int _cultureDeletionButtonIndex;
    private static int _cultureResetButtonIndex;
    private static int _cultureTechResetButtonIndex;
    private static int _cultureKnowledgeGainIncreaseButtonIndex;
    private static int _cultureForceSelectCultureButtonIndex;
    private static int _cultureForceSelectCityButtonIndex;
    private static int _createNewCultureButtonIndex;
    private static int _addZoneToCityButtonIndex;
    private static int _removeZoneFromCityButtonIndex;
    private static int _addZoneToCultureButtonIndex;
    private static int _removeZoneFromCultureButtonIndex;
    private static int _forceCityAsCapitalCityButtonIndex;
    private static int _forceCityIntoOtherKingdomButtonIndex;
    private static int _massItemAdditionRainButtonIndex;
    private static int _makeActorKingButtonIndex;
    private static int _placeBuildingButtonIndex;
    public static int WhisperOfAllianceButtonIndex => _whisperOfAllianceButtonIndex;
    public static int MassTraitRemovalRainButtonIndex => _massTraitRemovalRainButtonIndex;
    public static int CultureDeletionButtonIndex => _cultureDeletionButtonIndex;
    public static int CultureResetButtonIndex => _cultureResetButtonIndex;
    public static int CultureTechResetButtonIndex => _cultureTechResetButtonIndex;
    public static int CultureKnowledgeGainIncreaseButtonIndex => _cultureKnowledgeGainIncreaseButtonIndex;
    public static int CultureForceSelectCultureButtonIndex => _cultureForceSelectCultureButtonIndex;
    public static int CultureForceSelectCityButtonIndex => _cultureForceSelectCityButtonIndex;
    public static int CreateNewCultureButtonIndex => _createNewCultureButtonIndex;
    public static int AddZoneToCityButtonIndex => _addZoneToCityButtonIndex;
    public static int RemoveZoneFromCityButtonIndex => _removeZoneFromCityButtonIndex;
    public static int AddZoneToCultureButtonIndex => _addZoneToCultureButtonIndex;
    public static int RemoveZoneFromCultureButtonIndex => _removeZoneFromCultureButtonIndex;
    public static int ForceCityAsCapitalCityButtonIndex => _forceCityAsCapitalCityButtonIndex;
    public static int ForceCityIntoOtherKingdomButtonIndex => _forceCityIntoOtherKingdomButtonIndex;
    public static int MassItemAdditionRainButtonIndex => _massItemAdditionRainButtonIndex;
    public static int MakeActorKingButtonIndex => _makeActorKingButtonIndex;
    public static int PlaceBuildingButtonIndex => _placeBuildingButtonIndex;

    private static PowerButton _whisperOfAllianceButton;
    private static PowerButton _massTraitRemovalRainButton;
    private static PowerButton _cultureDeletionButton;
    private static PowerButton _cultureResetButton;
    private static PowerButton _cultureTechResetButton;
    private static PowerButton _cultureKnowledgeGainModificationButton;
    private static PowerButton _cultureForceSelectCultureButton;
    private static PowerButton _cultureForceSelectCityButton;
    private static PowerButton _createNewCultureButton;
    private static PowerButton _addZoneToCityButton;
    private static PowerButton _removeZoneFromCityButton;
    private static PowerButton _addZoneToCultureButton;
    private static PowerButton _removeZoneFromCultureButton;
    private static PowerButton _forceCityAsCapitalCityButton;
    private static PowerButton _forceCityIntoOtherKingdomButton;
    private static PowerButton _massItemAdditionRainButton;
    private static PowerButton _makeActorKingButton;
    private static PowerButton _placeBuildingButton;
  }
}