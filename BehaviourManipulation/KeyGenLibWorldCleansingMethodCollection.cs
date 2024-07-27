using System.Linq;

namespace KeyGeneralPurposeLibrary.BehaviourManipulation {
  public class KeyGenLibWorldCleansingMethodCollection : KLibComponent {
    public void RemoveMushSpores() {
      for (int i = 0; i < World.world.mapStats.id_unit; ++i) {
        Actor unit = World.world.units.get("u_" + i);
        if (unit != null) {
          unit.removeTrait("mushSpores");
          if (unit.asset.race == "mush") {
            unit.killHimself();
          }
        }
      }
    }

    public void RemoveThePlague() {
      for (int i = 0; i < World.world.mapStats.id_unit; ++i) {
        Actor unit = World.world.units.get("u_" + i);
        if (unit != null) {
          unit.removeTrait("plague");
        }
      }
    }

    public void RemoveTumors() {
      for (int i = 0; i < World.world.mapStats.id_unit; ++i) {
        Actor unit = World.world.units.get("u_" + i);
        if (unit != null) {
          unit.removeTrait("tumorInfection");
          if (unit.asset.race == "tumor") {
            unit.killHimself();
          }
        }
      }

      for (int i = 0; i < World.world.buildings.ToList().Count; ++i) {
        if (World.world.buildings.ToList()[i].data.asset_id == SB.tumor) {
          World.world.buildings.ToList()[i].startDestroyBuilding();
        }
      }
    }

    public void RemoveZombies() {
      for (int i = 0; i < World.world.mapStats.id_unit; ++i) {
        Actor unit = World.world.units.get("u_" + i);
        if (unit != null) {
          unit.removeTrait("infected");
          if (unit.asset.race == "undead") {
            unit.killHimself();
          }
        }
      }
    }
    
    public void RemoveRoads() {
      foreach (WorldTile tile in World.world.tilesList.Where(tile => tile.Type.road)) {
        tile.setTopTileType(TopTileLibrary.grass_high);
        MapAction.removeGreens(tile);
      }

      World.world.roadsCalculator.clear();
    }

    public void LowerMountains() {
      foreach (WorldTile tile in World.world.tilesList.Where(tile => tile.Type.mountains)) {
        tile.setTileType(TileLibrary.hills);
        tile.unfreeze();
      }
    }
    
    public void LowerHills() {
      foreach (WorldTile tile in World.world.tilesList) {
        if (tile.top_type == TopTileLibrary.snow_hills) {
          tile.setTopTileType(TopTileLibrary.grass_high);
        }
        if (tile.Type == TileLibrary.hills) {
          tile.setTileType(TileLibrary.soil_high);
        }
      }
    }

    public void RemoveBrokenBuildings() {
      foreach (Building building in World.world.buildings.Where(building => building.data.state == BuildingState.Ruins || building.data.state == BuildingState.CivAbandoned)) {
        building.startRemove();
      }
    }
  }
}