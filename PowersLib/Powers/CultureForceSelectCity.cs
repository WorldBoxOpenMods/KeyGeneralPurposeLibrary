using KeyGeneralPurposeLibrary.BehaviourManipulation;
namespace KeyGeneralPurposeLibrary.PowersLib.Powers {
  public class CultureForceSelectCity : KeyGenLibPower {
    public CultureForceSelectCity() : base(new GodPower() {
      id = "culture_force_select_city_keygui",
      name = "Culture Force Select City",
      force_map_mode = MetaType.City,
      select_button_action = CultureForceSelectCityPowerButtonPress,
      click_special_action = ClickWithCultureForceSelectCity,
    }) {
    }
        
    private static bool CultureForceSelectCityPowerButtonPress(string _) {
      WorldTip.showNow("KGPLL_CultureForceConversion_SelectCity", true, "top");
      return false;
    }
    
    private static bool ClickWithCultureForceSelectCity(WorldTile pTile, string pPowerID) {
      City cityToForceCultureUpon = pTile.zone.city;
      if (cityToForceCultureUpon != null) {
        if (CultureForceSelectCulture.CultureToForceUponCity != null) {
          KeyLib.Get<KeyGenLibCultureManipulationMethodCollection>().ForceCultureOnCity(CultureForceSelectCulture.CultureToForceUponCity, cityToForceCultureUpon);
          WorldTip.showNow("KGPLL_CultureForceConversion_Success", true, "top");
          return true;
        }
        WorldTip.showNow("KGPLL_CultureForceConversion_NoCultureSelectedError", true, "top");
        return false;
      }
      WorldTip.showNow("KGPLL_CultureForceConversion_NoCitySelectedError", true, "top");
      return false;
    }
  }
}
