namespace KeyGeneralPurposeLibrary.PowersLib.Powers {
  public class RemoveZoneFromCity : KeyGenLibPower {
    public RemoveZoneFromCity() : base(new GodPower() {
      id = "remove_zone_from_city_keygui",
      name = "Remove Zone From City",
      force_map_mode = MetaType.City,
      select_button_action = RemoveZoneFromCityPowerButtonPress,
      click_special_action = ClickWithRemoveZoneFromCity,
    }) {
    }
    private static City _cityToRemoveZoneFrom;
    
    private static bool RemoveZoneFromCityPowerButtonPress(string _) {
      _cityToRemoveZoneFrom = null;
      WorldTip.showNow("KGPLL_CityZoneRemoval_SelectCity", true, "top");
      return false;
    }
    
    private static bool ClickWithRemoveZoneFromCity(WorldTile pTile, string pPowerID) {
      if (_cityToRemoveZoneFrom == null) {
        _cityToRemoveZoneFrom = pTile.zone.city;
        if (_cityToRemoveZoneFrom != null) {
          WorldTip.showNow("KGPLL_CityZoneRemoval_SelectZones", true, "top");
          return false;
        }
        WorldTip.showNow("KGPLL_CityZoneRemoval_NoCitySelectedError", true, "top");
        return false;
      }
      if (pTile.zone.city == _cityToRemoveZoneFrom) {
        _cityToRemoveZoneFrom.removeZone(pTile.zone);
        return true;
      }
      WorldTip.showNow("KGPLL_CityZoneRemoval_ZoneOwnershipConflict", true, "top");
      return false;
    }
  }
}
