using KeyGeneralPurposeLibrary.BehaviourManipulation;
namespace KeyGeneralPurposeLibrary.PowersLib.Powers {
  public class CultureReset : KeyGenLibPower {
    public CultureReset() : base(new GodPower() {
      id = "culture_reset_keygui",
      name = "Culture Reset",
      force_map_mode = MetaType.Culture,
      select_button_action = CultureResetPowerButtonPress,
      click_special_action = ClickWithCultureReset,
    }) {
    }
    
    private static bool CultureResetPowerButtonPress(string _) {
      WorldTip.showNow("KGPLL_CultureFullReset_SelectCulture", true, "top");
      return false;
    }
    
    private static bool ClickWithCultureReset(WorldTile pTile, string pPowerID) {
      Culture cultureToReset = pTile.zone.city?.culture;
      if (cultureToReset != null) {
        KeyLib.Get<KeyGenLibCultureManipulationMethodCollection>().ResetCulture(cultureToReset);
        WorldTip.showNow("KGPLL_CultureFullReset_Success", true, "top");
        return true;
      }

      WorldTip.showNow("KGPLL_CultureFullReset_NoCultureSelectedError", true, "top");
      return false;
    }
  }
}
