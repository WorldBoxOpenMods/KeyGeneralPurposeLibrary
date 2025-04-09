namespace KeyGeneralPurposeLibrary.PowersLib.Powers {
  public class CultureForceSelectCity : KeyGenLibPower {
    public CultureForceSelectCity() : base(new GodPower() {
      id = "culture_force_select_city_keygui",
      name = "Culture Force Select City",
      force_map_mode = MetaType.City,
    }) {
      Power.select_button_action = CultureForceSelectCityPowerButtonPress;
      Power.click_special_action = ClickWithCultureForceSelectCity;
    }
        
    private bool CultureForceSelectCityPowerButtonPress(string _) {
      WorldTip.showNow("KGPLL_CultureForceConversion_SelectCity", true, "top");
      return false;
    }
    
    private bool ClickWithCultureForceSelectCity(WorldTile pTile, string pPowerID) {
      City cityToForceCultureUpon = pTile.zone.city;
      if (cityToForceCultureUpon != null) {
        if (CultureForceSelectCulture.CultureToForceUponCity != null) {
          ForceCultureOnCity(CultureForceSelectCulture.CultureToForceUponCity, cityToForceCultureUpon);
          WorldTip.showNow("KGPLL_CultureForceConversion_Success", true, "top");
          return true;
        }
        WorldTip.showNow("KGPLL_CultureForceConversion_NoCultureSelectedError", true, "top");
        return false;
      }
      WorldTip.showNow("KGPLL_CultureForceConversion_NoCitySelectedError", true, "top");
      return false;
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
  }
}
