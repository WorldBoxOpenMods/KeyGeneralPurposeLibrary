using System.Collections.Generic;

namespace KeyGeneralPurposeLibrary.Assets {
  public class KeyGenLibDisasterGenerator : KLibComponent {
    private readonly KeyGenLibDisasterAssetLibrary _disasterAssetLibrary = KeyLib.Get<KeyGenLibDisasterAssetLibrary>();
    
    public void SpawnHeatwave() {
      bool areDisastersActive = World.world.worldLaws.world_law_disasters_nature.boolVal;
      World.world.worldLaws.world_law_disasters_nature.boolVal = true;
      DisasterAsset heatwaveAsset = _disasterAssetLibrary[KeyGenLibDisasterAssetLibrary.HeatwaveIndex];
      heatwaveAsset.action(heatwaveAsset);
      if (!areDisastersActive) {
        World.world.worldLaws.world_law_disasters_nature.boolVal = false;
      }
    }
    
    public void SpawnEarthquake() {
      bool areDisastersActive = World.world.worldLaws.world_law_disasters_nature.boolVal;
      World.world.worldLaws.world_law_disasters_nature.boolVal = true;
      DisasterAsset earthquakeAsset = _disasterAssetLibrary[KeyGenLibDisasterAssetLibrary.EarthquakeIndex];
      earthquakeAsset.action(earthquakeAsset);
      if (!areDisastersActive) {
        World.world.worldLaws.world_law_disasters_nature.boolVal = false;
      }
    }
    
    public void SpawnTornado() {
      bool areDisastersActive = World.world.worldLaws.world_law_disasters_nature.boolVal;
      World.world.worldLaws.world_law_disasters_nature.boolVal = true;
      DisasterAsset tornadoAsset = _disasterAssetLibrary[KeyGenLibDisasterAssetLibrary.TornadoIndex];
      tornadoAsset.action(tornadoAsset);
      if (!areDisastersActive) {
        World.world.worldLaws.world_law_disasters_nature.boolVal = false;
      }
    }
    
    public void SpawnMadThought() {
      bool areDisastersActive = World.world.worldLaws.world_law_disasters_nature.boolVal;
      World.world.worldLaws.world_law_disasters_nature.boolVal = true;
      DisasterAsset madThoughtAsset = _disasterAssetLibrary[KeyGenLibDisasterAssetLibrary.MadThoughtIndex];
      madThoughtAsset.action(madThoughtAsset);
      if (!areDisastersActive) {
        World.world.worldLaws.world_law_disasters_nature.boolVal = false;
      }
    }
    
    public void SpawnMeteorite() {
      bool areDisastersActive = World.world.worldLaws.world_law_disasters_nature.boolVal;
      World.world.worldLaws.world_law_disasters_nature.boolVal = true;
      DisasterAsset meteoriteAsset = _disasterAssetLibrary[KeyGenLibDisasterAssetLibrary.MeteoriteIndex];
      meteoriteAsset.action(meteoriteAsset);
      if (!areDisastersActive) {
        World.world.worldLaws.world_law_disasters_nature.boolVal = false;
      }
    }
    
    public void SpawnEvilMage() {
      bool areDisastersActive = World.world.worldLaws.world_law_disasters_other.boolVal;
      World.world.worldLaws.world_law_disasters_other.boolVal = true;
      DisasterAsset mageAsset = _disasterAssetLibrary[KeyGenLibDisasterAssetLibrary.MageIndex];
      mageAsset.action(mageAsset);
      if (!areDisastersActive) {
        World.world.worldLaws.world_law_disasters_other.boolVal = false;
      }
    }

    public void SpawnSnowmen() {
      DisasterAsset snowmanAsset = _disasterAssetLibrary[KeyGenLibDisasterAssetLibrary.SnowmanIndex];
      snowmanAsset.action(snowmanAsset);
    }
    
    public void SpawnDemons() {
      DisasterAsset demonAsset = _disasterAssetLibrary[KeyGenLibDisasterAssetLibrary.DemonIndex];
      demonAsset.action(demonAsset);
    }
    
    public void SpawnBandits() {
      DisasterAsset banditAsset = _disasterAssetLibrary[KeyGenLibDisasterAssetLibrary.BanditIndex];
      banditAsset.action(banditAsset);
    }

    public void SpawnFrozenOnes() {
      DisasterAsset frozenOneAsset = _disasterAssetLibrary[KeyGenLibDisasterAssetLibrary.FrozenOneIndex];
      frozenOneAsset.action(frozenOneAsset);
    }

    public void SpawnNecromancer() {
      DisasterAsset necromancerAsset = _disasterAssetLibrary[KeyGenLibDisasterAssetLibrary.NecromancerIndex];
      necromancerAsset.action(necromancerAsset);
    }

    public void SpawnDragon() {
      DisasterAsset dragonAsset = _disasterAssetLibrary[KeyGenLibDisasterAssetLibrary.DragonIndex];
      dragonAsset.action(dragonAsset);
    }

    public void SpawnAliens() {
      DisasterAsset ufoAsset = _disasterAssetLibrary[KeyGenLibDisasterAssetLibrary.UfoIndex];
      ufoAsset.action(ufoAsset);
    }

    public void SpawnBiomass() {
      DisasterAsset biomassAsset = _disasterAssetLibrary[KeyGenLibDisasterAssetLibrary.BiomassIndex];
      biomassAsset.action(biomassAsset);
    }

    public void SpawnTumor() {
      DisasterAsset tumorAsset = _disasterAssetLibrary[KeyGenLibDisasterAssetLibrary.TumorIndex];
      tumorAsset.action(tumorAsset);
    }

    public void SpawnGardenSurprise() {
      DisasterAsset gardenSurpriseAsset = _disasterAssetLibrary[KeyGenLibDisasterAssetLibrary.GardenSurpriseIndex];
      List<City> list = World.world.cities.list;
      list.Shuffle();
      Building spawnBuilding = null;
      foreach (City city in list) {
        city.buildings_dict_type.TryGetValue(SB.type_windmill, out BuildingContainer container);
        if (container != null) {
          List<Building> buildingList = container.getSimpleList();
          if (buildingList.Count > 0) {
            spawnBuilding = buildingList[0];
            break;
          }
        }
      }

      if (spawnBuilding != null) {
        WorldTile buildingTile = spawnBuilding.currentTile;
        if (buildingTile != null) {
          BuildingData buildingData = new BuildingData {
            asset_id = gardenSurpriseAsset.spawn_asset_building,
            mainX = buildingTile.x,
            mainY = buildingTile.y,
            id = "b_" + World.world.mapStats.id_building
          };
          World.world.mapStats.id_building++;
          Building pumpkin = World.world.buildings.loadObject(buildingData);
          if (pumpkin != null) {
            pumpkin.currentTile = buildingTile;
            int random = Toolbox.randomInt(gardenSurpriseAsset.units_min, gardenSurpriseAsset.units_max);
            for (int i = 0; i < random; ++i) {
              World.world.units.createNewUnit(gardenSurpriseAsset.spawn_asset_unit, buildingTile);
            }

            WorldLog.logDisaster(gardenSurpriseAsset, buildingTile);
          }
        }
      }
    }

    public void SpawnGreg() {
      bool isGregActive = DebugConfig.isOn(DebugOption.Greg);
      DebugConfig.setOption(DebugOption.Greg, true);
      DisasterAsset gregAsset = _disasterAssetLibrary[KeyGenLibDisasterAssetLibrary.GregIndex];
      gregAsset.action(gregAsset);
      if (!isGregActive) {
        DebugConfig.setOption(DebugOption.Greg, false);
      }
    }
    
    #region Disaster Validity Checks

    public bool CheckForVillage() {
      bool valid = false;
      if (World.world != null) {
        if (World.world.cities != null) {
          if (World.world.cities.list != null) {
            if (World.world.cities.list.Count > 0) {
              valid = true;
            }
          }
        }
      }

      return valid;
    }

    public bool CheckForWindmill() {
      bool valid = false;
      if (World.world != null) {
        if (World.world.cities != null) {
          if (World.world.cities.list != null) {
            if (World.world.cities.list.Count > 0) {
              for (int i = 0; i < World.world.cities.Count; ++i) {
                World.world.cities.list[i].buildings_dict_type.TryGetValue(SB.type_windmill, out BuildingContainer container);
                if (container != null) {
                  valid = true;
                }
              }
            }
          }
        }
      }

      return valid;
    }

    public bool CheckForMine() {
      bool valid = false;
      if (World.world != null) {
        if (World.world.cities != null) {
          if (World.world.cities.list != null) {
            if (World.world.cities.list.Count > 0) {
              for (int i = 0; i < World.world.cities.Count; ++i) {
                World.world.cities.list[i].buildings_dict_type.TryGetValue(SB.type_mine, out BuildingContainer container);
                if (container != null) {
                  valid = true;
                }
              }
            }
          }
        }
      }

      return valid;
    }

    #endregion
  }
}