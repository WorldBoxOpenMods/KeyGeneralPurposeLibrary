namespace KeyGeneralPurposeLibrary.PowersLib.Powers {
  public class ForceCityAsCapitalCity : KeyGenLibPower {
    public ForceCityAsCapitalCity() : base(new GodPower() {
      id = "force_city_as_capital_city_keygui",
      name = "Force City As Capital City",
      force_map_mode = MetaType.City,
      select_button_action = ForceCityAsCapitalCityPowerButtonPress,
      click_special_action = ClickWithForceCityAsCapitalCity,
    }) {
    }
        
    private static bool ForceCityAsCapitalCityPowerButtonPress(string _) {
      WorldTip.showNow("KGPLL_ForceCapital_SelectCity", true, "top");
      return false;
    }
    
    private static bool ClickWithForceCityAsCapitalCity(WorldTile pTile, string pPowerID) {
      City cityToForceAsCapitalCity = pTile.zone.city;
      if (cityToForceAsCapitalCity != null) {
        cityToForceAsCapitalCity.kingdom.capital = cityToForceAsCapitalCity;
        cityToForceAsCapitalCity.kingdom.data.capitalID = cityToForceAsCapitalCity.kingdom.capital.data.id;
        cityToForceAsCapitalCity.kingdom.location = cityToForceAsCapitalCity.kingdom.capital.city_center;
        WorldTip.showNow("KGPLL_ForceCapital_Success", true, "top");
        return true;
      }
      WorldTip.showNow("KGPLL_ForceCapital_NoCitySelectedError", true, "top");
      return false;
    }
  }
}
