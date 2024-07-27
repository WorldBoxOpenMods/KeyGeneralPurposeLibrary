namespace KeyGeneralPurposeLibrary.Assets {
  public class KeyGenLibDisasterAssetLibrary : KLibAssetLibrary<DisasterAsset> {
    public KeyGenLibDisasterAssetLibrary() {
      AddAsset(_heatwave, out _heatwaveIndex);
      AddAsset(_earthquake, out _earthquakeIndex);
      AddAsset(_tornado, out _tornadoIndex);
      AddAsset(_madThought, out _madThoughtIndex);
      AddAsset(_meteorite, out _meteoriteIndex);
      AddAsset(_mage, out _mageIndex);
      AddAsset(_snowman, out _snowmanIndex);
      AddAsset(_demon, out _demonIndex);
      AddAsset(_bandit, out _banditIndex);
      AddAsset(_frozenOne, out _frozenOneIndex);
      AddAsset(_necromancer, out _necromancerIndex);
      AddAsset(_dragon, out _dragonIndex);
      AddAsset(_ufo, out _ufoIndex);
      AddAsset(_biomass, out _biomassIndex);
      AddAsset(_tumor, out _tumorIndex);
      AddAsset(_gardenSurprise, out _gardenSurpriseIndex);
      AddAsset(_greg, out _gregIndex);
    }

    internal override void Initialize() {
      base.Initialize();
      _heatwave.action = AssetManager.disasters.spawnHeatwave;
      _earthquake.action = AssetManager.disasters.spawnSmallEarthquake;
      _tornado.action = AssetManager.disasters.spawnTornado;
      _madThought.action = AssetManager.disasters.spawnMadThought;
      _meteorite.action = AssetManager.disasters.spawnMeteorite;
      _mage.action = AssetManager.disasters.spawnEvilMage;
      _snowman.action = AssetManager.disasters.simpleUnitAssetSpawnUsingIslands;
      _demon.action = AssetManager.disasters.simpleUnitAssetSpawnUsingIslands;
      _bandit.action = AssetManager.disasters.simpleUnitAssetSpawnUsingIslands;
      _frozenOne.action = AssetManager.disasters.simpleUnitAssetSpawnUsingIslands;
      _necromancer.action = AssetManager.disasters.spawnNecromancer;
      _dragon.action = AssetManager.disasters.spawnDragon;
      _ufo.action = AssetManager.disasters.startAlienInvasion;
      _biomass.action = AssetManager.disasters.spawnBiomass;
      _tumor.action = AssetManager.disasters.spawnTumor;
      _gardenSurprise.action = AssetManager.disasters.gardenSurprise;
      _greg.action = AssetManager.disasters.spawnGreg;
    }

    private static int _heatwaveIndex;
    private static int _earthquakeIndex ;
    private static int _tornadoIndex;
    private static int _madThoughtIndex;
    private static int _meteoriteIndex;
    private static int _mageIndex;
    private static int _snowmanIndex;
    private static int _demonIndex;
    private static int _banditIndex;
    private static int _frozenOneIndex;
    private static int _necromancerIndex;
    private static int _dragonIndex;
    private static int _ufoIndex;
    private static int _biomassIndex;
    private static int _tumorIndex;
    private static int _gardenSurpriseIndex;
    private static int _gregIndex;
    

    private readonly DisasterAsset _heatwave = new DisasterAsset {
      id = "heatwave",
      rate = 1,
      chance = 1,
      world_log = "worldlog_disaster_heatwave",
      world_log_icon = "iconFire",
      premium_only = false,
      min_world_cities = 0,
      min_world_population = 0,
      type = DisasterType.Nature
    };
    
    private readonly DisasterAsset _earthquake = new DisasterAsset {
      id = "small_earthquake",
      rate = 1,
      chance = 1,
      world_log = "worldlog_disaster_earthquake",
      world_log_icon = "iconEarthquake",
      min_world_population = 0,
      min_world_cities = 0,
      type = DisasterType.Nature
    };
    
    private readonly DisasterAsset _tornado = new DisasterAsset {
      id = "tornado",
      rate = 1,
      chance = 1,
      world_log = "worldlog_disaster_tornado",
      world_log_icon = "iconTornado",
      min_world_population = 0,
      min_world_cities = 0,
      type = DisasterType.Nature
    };
    
    private readonly DisasterAsset _madThought = new DisasterAsset {
      id = "mad_thoughts",
      rate = 1,
      chance = 1,
      world_log = "worldlog_disaster_mad_thoughts",
      world_log_icon = "iconMadness",
      min_world_population = 1,
      min_world_cities = 1
    };
    
    private readonly DisasterAsset _meteorite = new DisasterAsset {
      id = "small_meteorite",
      rate = 1,
      chance = 1,
      world_log = "worldlog_disaster_meteorite",
      world_log_icon = "iconMeteorite",
      min_world_population = 0,
      min_world_cities = 0,
      premium_only = true
    };
    
    private readonly DisasterAsset _mage = new DisasterAsset {
      id = "wild_mage",
      rate = 1,
      chance = 1,
      world_log = "worldlog_disaster_evil_mage",
      world_log_icon = "iconEvilMage",
      min_world_population = 0,
      min_world_cities = 0,
      premium_only = true,
      spawn_asset_unit = "evilMage",
      max_existing_units = 9999,
      units_min = 1,
      units_max = 1
    };
    
    private readonly DisasterAsset _snowman = new DisasterAsset {
      id = "sudden_snowman",
      rate = 1,
      chance = 1,
      world_log = "worldlog_disaster_sudden_snowman",
      world_log_icon = "iconSnowMan",
      min_world_population = 0,
      min_world_cities = 0,
      spawn_asset_unit = "snowman",
      max_existing_units = 9999,
      units_min = 20,
      units_max = 40
    };
    
    private readonly DisasterAsset _demon = new DisasterAsset {
      id = "hellspawn",
      rate = 1,
      chance = 1,
      world_log = "worldlog_disaster_hellspawn",
      world_log_icon = "iconDemon",
      min_world_population = 0,
      min_world_cities = 0,
      premium_only = true,
      spawn_asset_unit = "demon",
      max_existing_units = 9999,
      units_min = 2,
      units_max = 5
    };
    
    private readonly DisasterAsset _bandit = new DisasterAsset {
      id = "ash_bandits",
      rate = 1,
      chance = 1,
      world_log = "worldlog_disaster_bandits",
      world_log_icon = "iconBandit",
      min_world_population = 0,
      min_world_cities = 0,
      premium_only = true,
      spawn_asset_unit = "bandit",
      max_existing_units = 9999,
      units_min = 15,
      units_max = 30
    };
    
    private readonly DisasterAsset _frozenOne = new DisasterAsset {
      id = "ice_ones_awoken",
      rate = 1,
      chance = 1,
      world_log = "worldlog_disaster_ice_ones",
      world_log_icon = "iconWalker",
      min_world_population = 0,
      min_world_cities = 0,
      premium_only = true,
      spawn_asset_unit = "walker",
      max_existing_units = 9999,
      units_min = 10,
      units_max = 20
    };
    
    private readonly DisasterAsset _necromancer = new DisasterAsset {
      id = "underground_necromancer",
      rate = 1,
      chance = 1,
      world_log = "worldlog_disaster_underground_necromancer",
      world_log_icon = "iconNecromancer",
      min_world_population = 1,
      min_world_cities = 1,
      premium_only = true,
      spawn_asset_unit = "necromancer",
      max_existing_units = 9999,
      units_min = 1,
      units_max = 1
    };
    
    private readonly DisasterAsset _dragon = new DisasterAsset {
      id = "dragon_from_farlands",
      rate = 1,
      chance = 1,
      world_log = "worldlog_disaster_dragon_from_farlands",
      world_log_icon = "iconDragon",
      min_world_population = 0,
      min_world_cities = 0,
      spawn_asset_unit = "dragon",
      max_existing_units = 9999,
      units_min = 1,
      units_max = 1
    };
    
    private readonly DisasterAsset _ufo = new DisasterAsset {
      id = "alien_invasion",
      rate = 1,
      chance = 1,
      world_log = "worldlog_disaster_alien_invasion",
      world_log_icon = "iconUfo",
      min_world_population = 0,
      min_world_cities = 0,
      spawn_asset_building = "tumor",
      spawn_asset_unit = "UFO",
      max_existing_units = 9999,
      units_min = 5,
      units_max = 10
    };
    
    private readonly DisasterAsset _biomass = new DisasterAsset {
      id = "biomass",
      rate = 1,
      chance = 1,
      world_log = "worldlog_disaster_biomass",
      world_log_icon = "iconBiomass",
      min_world_population = 0,
      min_world_cities = 0,
      spawn_asset_building = "biomass",
      spawn_asset_unit = "bioblob",
      max_existing_units = 9999,
      units_min = 20,
      units_max = 30
    };
    
    private readonly DisasterAsset _tumor = new DisasterAsset {
      id = "tumor",
      rate = 1,
      chance = 1,
      world_log = "worldlog_disaster_tumor",
      world_log_icon = "iconTumor",
      min_world_population = 0,
      min_world_cities = 0,
      spawn_asset_building = "tumor",
      spawn_asset_unit = "tumor_monster_unit",
      max_existing_units = 9999,
      units_min = 20,
      units_max = 30
    };
    
    private readonly DisasterAsset _gardenSurprise = new DisasterAsset {
      id = "garden_surprise",
      rate = 1,
      chance = 1,
      world_log = "worldlog_disaster_garden_surprise",
      world_log_icon = "iconSuperPumpkin",
      min_world_population = 1,
      min_world_cities = 1,
      spawn_asset_building = SB.super_pumpkin,
      spawn_asset_unit = "lil_pumpkin",
      max_existing_units = 9999,
      units_min = 50,
      units_max = 100
    };
    
    private readonly DisasterAsset _greg = new DisasterAsset {
      id = "greg_abominations",
      rate = 1,
      chance = 1,
      world_log = "worldlog_disaster_greg_abominations",
      world_log_icon = "iconGreg",
      min_world_population = 1,
      min_world_cities = 1,
      spawn_asset_unit = "greg",
      max_existing_units = 9999,
      units_min = 30,
      units_max = 55
    };

    public static int HeatwaveIndex => _heatwaveIndex;

    public static int EarthquakeIndex => _earthquakeIndex;

    public static int TornadoIndex => _tornadoIndex;

    public static int MadThoughtIndex => _madThoughtIndex;

    public static int MeteoriteIndex => _meteoriteIndex;

    public static int MageIndex => _mageIndex;

    public static int SnowmanIndex => _snowmanIndex;

    public static int DemonIndex => _demonIndex;

    public static int BanditIndex => _banditIndex;

    public static int FrozenOneIndex => _frozenOneIndex;

    public static int NecromancerIndex => _necromancerIndex;

    public static int DragonIndex => _dragonIndex;

    public static int UfoIndex => _ufoIndex;

    public static int BiomassIndex => _biomassIndex;

    public static int TumorIndex => _tumorIndex;

    public static int GardenSurpriseIndex => _gardenSurpriseIndex;

    public static int GregIndex => _gregIndex;
  }
}