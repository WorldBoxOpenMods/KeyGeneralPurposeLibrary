using System.Linq;

namespace KeyGeneralPurposeLibrary.BehaviourManipulation {
  public class KeyGenLibRaceManipulationMethodCollection : KLibComponent {
    public void ModifySpecificRaceStat(string stat, Race race, int statModifier) {
      foreach (ActorAsset actorAsset in race.units.Select(a => a.asset).Distinct()) {
        actorAsset.base_stats[stat] += statModifier;
      }
      foreach (Actor unit in race.units) {
        unit.setStatsDirty();
      }
    }

    public void KillRace(Race race) {
      foreach (Actor a in race.units) {
        a.killHimself();
      }
    }
  }
}