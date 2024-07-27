using System.Collections.Generic;
using System.Linq;

namespace KeyGeneralPurposeLibrary.BehaviourManipulation {
  public class KeyGenLibCultureManipulationMethodCollection : KLibComponent {
    public void DeleteCulture(Culture targetCulture) {
      foreach (City city in targetCulture._list_cities) {
        city.data.culture = "";
      }

      targetCulture._list_cities.Clear();
      targetCulture._list_tech.Clear();
      for (int i = 0; i < World.world.units.ToList().Count; ++i) {
        if (World.world.units.ToList()[i].getCulture() == targetCulture) {
          World.world.units.ToList()[i].data.culture = "";
        }
      }

      targetCulture.clearZones();
      targetCulture.reset();
      World.world.cultures.removeObject(targetCulture);
    }

    public void ResetCultureTech(Culture targetCulture) {
      targetCulture._list_tech.Clear();
      targetCulture._maximum_level_reached = false;
      targetCulture.data.list_tech_ids.Clear();
      targetCulture.data.research_progress = 0;
    }

    public void ModifyKnowledgeGain(Culture targetCulture, float modifier) {
      targetCulture.stats.knowledge_gain.value += modifier;
    }

    public void ForceCultureOnCity(Culture citySelectionTargetCulture, City city) {
      Culture currentCityCulture = city.getCulture();
      if (currentCityCulture != null) {
        currentCityCulture._list_cities.Remove(city);
        foreach (TileZone zone in city.zones) {
          currentCityCulture.removeZone(zone);
        }
      }

      city.setCulture(citySelectionTargetCulture);
      citySelectionTargetCulture._list_cities.Add(city);
      foreach (TileZone zone in city.zones) {
        citySelectionTargetCulture.addZone(zone);
      }

      foreach (Actor actor in city.units._simpleList) {
        actor.setCulture(citySelectionTargetCulture);
      }
    }

    public void ResetCulture(Culture culture) {
      List<City> cities = culture._list_cities.ToList();
      DeleteCulture(culture);
      Culture newCulture = World.world.cultures.newCulture(AssetManager.raceLibrary.get(culture.data.race), cities[0]);
      foreach (City t in cities) {
        t.setCulture(newCulture);
      }
    }
  }
}