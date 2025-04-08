using System.Linq;
namespace KeyGeneralPurposeLibrary.PowersLib.Powers {
  public class ForceCityIntoOtherKingdom : KeyGenLibPower {
    public ForceCityIntoOtherKingdom() : base(new GodPower() {
      id = "force_city_into_other_kingdom_keygui",
      name = "Force City Into Other Kingdom",
      force_map_mode = MetaType.City,
      select_button_action = ForceCityIntoOtherKingdomPowerButtonPress,
      click_special_action = ClickWithForceCityIntoOtherKingdom
    }) {
    }
    internal static City CityToForceIntoOtherKingdom;
        
    private static bool ForceCityIntoOtherKingdomPowerButtonPress(string _) {
      CityToForceIntoOtherKingdom = null;
      WorldTip.showNow("KGPLL_ChangeCityKingdom_SelectCity", true, "top");
      return false;
    }
    
    private static bool ClickWithForceCityIntoOtherKingdom(WorldTile pTile, string pPowerID) {
      if (CityToForceIntoOtherKingdom == null) {
        CityToForceIntoOtherKingdom = pTile.zone.city;
        if (CityToForceIntoOtherKingdom != null) {
          WorldTip.showNow("KGPLL_ChangeCityKingdom_SelectKingdom", true, "top");
          return false;
        }
        WorldTip.showNow("KGPLL_ChangeCityKingdom_NoCitySelectedError", true, "top");
        return false;
      }
      if (pTile.zone.city?.kingdom != null) {
        CityToForceIntoOtherKingdom.kingdom.cities.Remove(CityToForceIntoOtherKingdom);
        CityToForceIntoOtherKingdom.kingdom = pTile.zone.city.kingdom;
        CityToForceIntoOtherKingdom.kingdom.cities.Add(CityToForceIntoOtherKingdom);
        CityToForceIntoOtherKingdom.units.ToList().ForEach(a => a.setKingdom(CityToForceIntoOtherKingdom.kingdom));
        WorldTip.showNow("KGPLL_ChangeCityKingdom_Success", true, "top");
        return true;
      }
      WorldTip.showNow("KGPLL_ChangeCityKingdom_NoKingdomSelectedError", true, "top");
      return false;
    }
  }
}
