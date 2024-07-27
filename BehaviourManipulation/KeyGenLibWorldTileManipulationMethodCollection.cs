using System;
using System.Linq;

namespace KeyGeneralPurposeLibrary.BehaviourManipulation {
  public class KeyGenLibWorldTileManipulationMethodCollection : KLibComponent {
    public void ChangeEveryWorldTile(Action<WorldTile, string> action) {
      foreach (WorldTile tile in World.world.tilesList.Where(tile => tile != null)) {
        action(tile, null);
      }
    }

    public void ChangeSpecificWorldTiles(Action<WorldTile, string> action, Func<WorldTile, bool> requirement) {
      foreach (WorldTile tile in World.world.tilesList.Where(tile => tile != null && requirement(tile))) {
        action(tile, null);
      }
    }
    public void ChangeSpecificWorldTiles(Action<WorldTile> action, Func<WorldTile, bool> requirement) {
      foreach (WorldTile tile in World.world.tilesList.Where(tile => tile != null && requirement(tile))) {
        action(tile);
      }
    }
  }
}