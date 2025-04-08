namespace KeyGeneralPurposeLibrary.PowersLib.Powers {
  public class AddZoneToCity : KeyGenLibPower {
    public AddZoneToCity() : base(new GodPower() {
      id = "add_zone_to_city_keygui",
      name = "Add Zone To City",
      force_map_mode = MetaType.City,
      select_button_action = AddZoneToCityPowerButtonPress,
      click_special_action = ClickWithAddZoneToCity,
    }) {
    }
    private static City _cityToAddZoneTo;
    
    private static bool AddZoneToCityPowerButtonPress(string _) {
      _cityToAddZoneTo = null;
      WorldTip.showNow("KGPLL_CityZoneAddition_SelectCity", true, "top");
      return false;
    }
    
    private static bool ClickWithAddZoneToCity(WorldTile pTile, string pPowerID) {
      if (_cityToAddZoneTo == null) {
        _cityToAddZoneTo = pTile.zone.city;
        if (_cityToAddZoneTo != null) {
          WorldTip.showNow("KGPLL_CityZoneAddition_SelectZones", true, "top");
          return false;
        }
        WorldTip.showNow("KGPLL_CityZoneAddition_NoCitySelectedError", true, "top");
        return false;
      }
      if (pTile.zone.city == null) {
        _cityToAddZoneTo.addZone(pTile.zone);
        return true;
      }
      WorldTip.showNow("KGPLL_CityZoneAddition_ZoneOwnershipConflict", true, "top");
      return false;
    }
  }
}
