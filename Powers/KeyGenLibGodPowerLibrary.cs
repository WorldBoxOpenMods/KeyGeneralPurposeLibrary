namespace KeyGeneralPurposeLibrary.Powers {
  public class KeyGenLibGodPowerLibrary : KLibAssetLibrary<GodPower> {
    public KeyGenLibGodPowerLibrary() {
      AddAsset(_whisperOfAlliance, out _whisperOfAllianceIndex);
      AddAsset(_cultureDeletion, out _cultureDeletionIndex);
      AddAsset(_cultureReset, out _cultureResetIndex);
      AddAsset(_cultureTechReset, out _cultureTechResetIndex);
      AddAsset(_cultureKnowledgeGainModification, out _cultureKnowledgeGainModificationIndex);
      AddAsset(_cultureForceSelectCulture, out _cultureForceSelectCultureIndex);
      AddAsset(_cultureForceSelectCity, out _cultureForceSelectCityIndex);
      AddAsset(_createNewCulture, out _createNewCultureIndex);
      AddAsset(_addZoneToCity, out _addZoneToCityIndex);
      AddAsset(_removeZoneFromCity, out _removeZoneFromCityIndex);
      AddAsset(_addZoneToCulture, out _addZoneToCultureIndex);
      AddAsset(_removeZoneFromCulture, out _removeZoneFromCultureIndex);
      AddAsset(_forceCityAsCapitalCity, out _forceCityAsCapitalCityIndex);
      AddAsset(_forceCityIntoOtherKingdom, out _forceCityIntoOtherKingdomIndex);
      AddAsset(_makeActorKing, out _makeActorKingIndex);
      AddAsset(_placeBuilding, out _placeBuildingIndex);
    }
    private static int _whisperOfAllianceIndex;
    private static int _massTraitRemovalRainIndex;
    private static int _cultureDeletionIndex;
    private static int _cultureResetIndex;
    private static int _cultureTechResetIndex;
    private static int _cultureKnowledgeGainModificationIndex;
    private static int _cultureForceSelectCultureIndex;
    private static int _cultureForceSelectCityIndex;
    private static int _createNewCultureIndex;
    private static int _addZoneToCityIndex;
    private static int _removeZoneFromCityIndex;
    private static int _addZoneToCultureIndex;
    private static int _removeZoneFromCultureIndex;
    private static int _forceCityAsCapitalCityIndex;
    private static int _forceCityIntoOtherKingdomIndex;
    private static int _massItemAdditionRainIndex;
    private static int _makeActorKingIndex;
    private static int _placeBuildingIndex;
    public static int WhisperOfAllianceIndex => _whisperOfAllianceIndex;
    public static int MassTraitRemovalRainIndex => _massTraitRemovalRainIndex;
    public static int CultureDeletionIndex => _cultureDeletionIndex;
    public static int CultureResetIndex => _cultureResetIndex;
    public static int CultureTechResetIndex => _cultureTechResetIndex;
    public static int CultureKnowledgeGainModificationIndex => _cultureKnowledgeGainModificationIndex;
    public static int CultureForceSelectCultureIndex => _cultureForceSelectCultureIndex;
    public static int CultureForceSelectCityIndex => _cultureForceSelectCityIndex;
    public static int CreateNewCultureIndex => _createNewCultureIndex;
    public static int AddZoneToCityIndex => _addZoneToCityIndex;
    public static int RemoveZoneFromCityIndex => _removeZoneFromCityIndex;
    public static int AddZoneToCultureIndex => _addZoneToCultureIndex;
    public static int RemoveZoneFromCultureIndex => _removeZoneFromCultureIndex;
    public static int ForceCityAsCapitalCityIndex => _forceCityAsCapitalCityIndex;
    public static int ForceCityIntoOtherKingdomIndex => _forceCityIntoOtherKingdomIndex;
    public static int MassItemAdditionRainIndex => _massItemAdditionRainIndex;
    public static int MakeActorKingIndex => _makeActorKingIndex;
    public static int PlaceBuildingIndex => _placeBuildingIndex;

    internal override void Initialize() {
      base.Initialize();
      _massTraitRemovalRain = new GodPower {
        id = "trait_mass_removal_rain_keygui",
        name = "Trait Mass Removal Rain",
        holdAction = true,
        showToolSizes = true,
        unselectWhenWindow = true,
        fallingChance = 0.05f,
        rank = PowerRank.Rank0_free,
        dropID = "trait_mass_removal_rain_keygui",
        click_power_action = (tTile, pPower) => {
          bool result = AssetManager.powers.spawnDrops(tTile, pPower);
          result = result && AssetManager.powers.flashPixel(tTile, pPower);
          result = result && AssetManager.powers.fmodDrawingSound(tTile, pPower);
          return result;
        },
        click_power_brush_action = AssetManager.powers.loopWithCurrentBrushPower,
        fmod_event_drawing = "event:/SFX/POWERS/GammaRain"
      };
      AddAsset(_massTraitRemovalRain, out _massTraitRemovalRainIndex);
      _massItemAdditionRain = new GodPower {
        id = "item_mass_addition_rain_keygui",
        name = "Item Mass Addition Rain",
        holdAction = true,
        showToolSizes = true,
        unselectWhenWindow = true,
        fallingChance = 0.05f,
        rank = PowerRank.Rank0_free,
        dropID = "item_mass_addition_rain_keygui",
        click_power_action = (tTile, pPower) => {
          bool result = AssetManager.powers.spawnDrops(tTile, pPower);
          result = result && AssetManager.powers.flashPixel(tTile, pPower);
          result = result && AssetManager.powers.fmodDrawingSound(tTile, pPower);
          return result;
        },
        click_power_brush_action = AssetManager.powers.loopWithCurrentBrushPower,
        fmod_event_drawing = "event:/SFX/POWERS/GammaRain"
      };
      AddAsset(_massItemAdditionRain, out _massItemAdditionRainIndex);
      foreach (GodPower power in Assets) {
        AssetManager.powers.add(power);
      }
    }

    private readonly GodPower _whisperOfAlliance = new GodPower() {
      id = "create_alliance_keygui",
      name = "Whisper Of Alliance",
      force_map_text = MapMode.Alliances,
      select_button_action = KeyLib.Get<KeyGenLibPowerButtonClickActionLibrary>()[KeyGenLibPowerButtonClickActionLibrary.WhisperOfAlliancePowerButtonPressIndex],
      click_special_action = KeyLib.Get<KeyGenLibPowerActionWithIdLibrary>()[KeyGenLibPowerActionWithIdLibrary.ClickWithWhisperOfAllianceIndex]
    };

    private GodPower _massTraitRemovalRain;
    
    private readonly GodPower _cultureDeletion = new GodPower() {
      id = "culture_wipe_keygui",
      name = "Culture Wipe",
      force_map_text = MapMode.Cultures,
      select_button_action = KeyLib.Get<KeyGenLibPowerButtonClickActionLibrary>()[KeyGenLibPowerButtonClickActionLibrary.CultureDeletionPowerButtonPressIndex],
      click_special_action = KeyLib.Get<KeyGenLibPowerActionWithIdLibrary>()[KeyGenLibPowerActionWithIdLibrary.ClickWithCultureDeletionIndex]
    };
    
    private readonly GodPower _cultureReset = new GodPower() {
      id = "culture_reset_keygui",
      name = "Culture Reset",
      force_map_text = MapMode.Cultures,
      select_button_action = KeyLib.Get<KeyGenLibPowerButtonClickActionLibrary>()[KeyGenLibPowerButtonClickActionLibrary.CultureResetPowerButtonPressIndex],
      click_special_action = KeyLib.Get<KeyGenLibPowerActionWithIdLibrary>()[KeyGenLibPowerActionWithIdLibrary.ClickWithCultureResetIndex]
    };

    private readonly GodPower _cultureTechReset = new GodPower() {
      id = "culture_tech_reset_keygui",
      name = "Culture Tech Reset",
      force_map_text = MapMode.Cultures,
      select_button_action = KeyLib.Get<KeyGenLibPowerButtonClickActionLibrary>()[KeyGenLibPowerButtonClickActionLibrary.CultureTechResetPowerButtonPressIndex],
      click_special_action = KeyLib.Get<KeyGenLibPowerActionWithIdLibrary>()[KeyGenLibPowerActionWithIdLibrary.ClickWithCultureTechResetIndex]
    };
    
    private readonly GodPower _cultureKnowledgeGainModification = new GodPower() {
      id = "culture_knowledge_gain_modification_keygui",
      name = "Culture Knowledge Gain Modification",
      force_map_text = MapMode.Cultures,
      select_button_action = KeyLib.Get<KeyGenLibPowerButtonClickActionLibrary>()[KeyGenLibPowerButtonClickActionLibrary.CultureKnowledgeGainModificationPowerButtonPressIndex],
      click_special_action = KeyLib.Get<KeyGenLibPowerActionWithIdLibrary>()[KeyGenLibPowerActionWithIdLibrary.ClickWithCultureKnowledgeGainModificationIndex]
    };
    
    private readonly GodPower _cultureForceSelectCulture = new GodPower() {
      id = "culture_force_select_culture_keygui",
      name = "Culture Force Select Culture",
      force_map_text = MapMode.Cultures,
      select_button_action = KeyLib.Get<KeyGenLibPowerButtonClickActionLibrary>()[KeyGenLibPowerButtonClickActionLibrary.CultureForceSelectCulturePowerButtonPressIndex],
      click_special_action = KeyLib.Get<KeyGenLibPowerActionWithIdLibrary>()[KeyGenLibPowerActionWithIdLibrary.ClickWithCultureForceSelectCultureIndex]
    };
    
    private readonly GodPower _cultureForceSelectCity = new GodPower() {
      id = "culture_force_select_city_keygui",
      name = "Culture Force Select City",
      force_map_text = MapMode.Cities,
      select_button_action = KeyLib.Get<KeyGenLibPowerButtonClickActionLibrary>()[KeyGenLibPowerButtonClickActionLibrary.CultureForceSelectCityPowerButtonPressIndex],
      click_special_action = KeyLib.Get<KeyGenLibPowerActionWithIdLibrary>()[KeyGenLibPowerActionWithIdLibrary.ClickWithCultureForceSelectCityIndex]
    };
    
    private readonly GodPower _createNewCulture = new GodPower() {
      id = "create_new_culture_keygui",
      name = "Create New Culture",
      force_map_text = MapMode.Cities,
      select_button_action = KeyLib.Get<KeyGenLibPowerButtonClickActionLibrary>()[KeyGenLibPowerButtonClickActionLibrary.CreateNewCulturePowerButtonPressIndex],
      click_special_action = KeyLib.Get<KeyGenLibPowerActionWithIdLibrary>()[KeyGenLibPowerActionWithIdLibrary.ClickWithCreateNewCultureIndex]
    };
    
    private readonly GodPower _addZoneToCity = new GodPower() {
      id = "add_zone_to_city_keygui",
      name = "Add Zone To City",
      force_map_text = MapMode.Cities,
      select_button_action = KeyLib.Get<KeyGenLibPowerButtonClickActionLibrary>()[KeyGenLibPowerButtonClickActionLibrary.AddZoneToCityPowerButtonPressIndex],
      click_special_action = KeyLib.Get<KeyGenLibPowerActionWithIdLibrary>()[KeyGenLibPowerActionWithIdLibrary.ClickWithAddZoneToCityIndex]
    };
    
    private readonly GodPower _removeZoneFromCity = new GodPower() {
      id = "remove_zone_from_city_keygui",
      name = "Remove Zone From City",
      force_map_text = MapMode.Cities,
      select_button_action = KeyLib.Get<KeyGenLibPowerButtonClickActionLibrary>()[KeyGenLibPowerButtonClickActionLibrary.RemoveZoneFromCityPowerButtonPressIndex],
      click_special_action = KeyLib.Get<KeyGenLibPowerActionWithIdLibrary>()[KeyGenLibPowerActionWithIdLibrary.ClickWithRemoveZoneFromCityIndex]
    };
    
    private readonly GodPower _addZoneToCulture = new GodPower() {
      id = "add_zone_to_culture_keygui",
      name = "Add Zone To Culture",
      force_map_text = MapMode.Cultures,
      select_button_action = KeyLib.Get<KeyGenLibPowerButtonClickActionLibrary>()[KeyGenLibPowerButtonClickActionLibrary.AddZoneToCulturePowerButtonPressIndex],
      click_special_action = KeyLib.Get<KeyGenLibPowerActionWithIdLibrary>()[KeyGenLibPowerActionWithIdLibrary.ClickWithAddZoneToCultureIndex]
    };
    
    private readonly GodPower _removeZoneFromCulture = new GodPower() {
      id = "remove_zone_from_culture_keygui",
      name = "Remove Zone From Culture",
      force_map_text = MapMode.Cultures,
      select_button_action = KeyLib.Get<KeyGenLibPowerButtonClickActionLibrary>()[KeyGenLibPowerButtonClickActionLibrary.RemoveZoneFromCulturePowerButtonPressIndex],
      click_special_action = KeyLib.Get<KeyGenLibPowerActionWithIdLibrary>()[KeyGenLibPowerActionWithIdLibrary.ClickWithRemoveZoneFromCultureIndex]
    };
    
    private readonly GodPower _forceCityAsCapitalCity = new GodPower() {
      id = "force_city_as_capital_city_keygui",
      name = "Force City As Capital City",
      force_map_text = MapMode.Cities,
      select_button_action = KeyLib.Get<KeyGenLibPowerButtonClickActionLibrary>()[KeyGenLibPowerButtonClickActionLibrary.ForceCityAsCapitalCityPowerButtonPressIndex],
      click_special_action = KeyLib.Get<KeyGenLibPowerActionWithIdLibrary>()[KeyGenLibPowerActionWithIdLibrary.ClickWithForceCityAsCapitalCityIndex]
    };
    
    private readonly GodPower _forceCityIntoOtherKingdom = new GodPower() {
      id = "force_city_into_other_kingdom_keygui",
      name = "Force City Into Other Kingdom",
      force_map_text = MapMode.Cities,
      select_button_action = KeyLib.Get<KeyGenLibPowerButtonClickActionLibrary>()[KeyGenLibPowerButtonClickActionLibrary.ForceCityIntoOtherKingdomPowerButtonPressIndex],
      click_special_action = KeyLib.Get<KeyGenLibPowerActionWithIdLibrary>()[KeyGenLibPowerActionWithIdLibrary.ClickWithForceCityIntoOtherKingdomIndex]
    };
    
    private GodPower _massItemAdditionRain;
    
    private readonly GodPower _makeActorKing = new GodPower() {
      id = "make_actor_king_keygui",
      name = "Make Actor King",
      force_map_text = MapMode.None,
      select_button_action = KeyLib.Get<KeyGenLibPowerButtonClickActionLibrary>()[KeyGenLibPowerButtonClickActionLibrary.MakeActorKingPowerButtonPressIndex],
      click_special_action = KeyLib.Get<KeyGenLibPowerActionWithIdLibrary>()[KeyGenLibPowerActionWithIdLibrary.ClickWithMakeActorKingIndex]
    };
    
    private readonly GodPower _placeBuilding = new GodPower() {
      id = "place_building_keygui",
      name = "Place Building",
      force_map_text = MapMode.None,
      select_button_action = KeyLib.Get<KeyGenLibPowerButtonClickActionLibrary>()[KeyGenLibPowerButtonClickActionLibrary.PlaceBuildingPowerButtonPressIndex],
      click_special_action = KeyLib.Get<KeyGenLibPowerActionWithIdLibrary>()[KeyGenLibPowerActionWithIdLibrary.ClickWithPlaceBuildingIndex]
    };
  }
}