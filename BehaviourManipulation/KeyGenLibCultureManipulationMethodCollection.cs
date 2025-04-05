using System.Collections.Generic;
using System.Linq;

namespace KeyGeneralPurposeLibrary.BehaviourManipulation {
  public class KeyGenLibCultureManipulationMethodCollection : KLibComponent {
    public void DeleteCulture(Culture targetCulture) {
      foreach (City city in targetCulture.cities.ToList()) {
        city.setCulture(null);
      }
      for (int i = 0; i < World.world.units.ToList().Count; ++i) {
        if (World.world.units.ToList()[i].culture == targetCulture) {
          World.world.units.ToList()[i].setCulture(null);
        }
      }
      World.world.cultures.removeObject(targetCulture);
    }

    public void ForceCultureOnCity(Culture citySelectionTargetCulture, City city) {
      Culture currentCityCulture = city.getCulture();
      currentCityCulture?.cities.Remove(city);

      city.setCulture(citySelectionTargetCulture);
      citySelectionTargetCulture.cities.Add(city);

      foreach (Actor actor in city.units) {
        actor.setCulture(citySelectionTargetCulture);
      }
    }

    public void ResetCulture(Culture culture) {
      List<City> cities = culture.cities.ToList();
      DeleteCulture(culture);
      Culture newCulture = World.world.cultures.newCulture(culture.units.First());
      foreach (City t in cities) {
        t.setCulture(newCulture);
      }
      foreach (Actor unit in culture.units) {
        unit.setCulture(newCulture);
      }
    }
  }
}
