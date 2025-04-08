using KeyGeneralPurposeLibrary.BehaviourManipulation;
namespace KeyGeneralPurposeLibrary.PowersLib.Powers {
  public class CultureDeletion : KeyGenLibPower {
    public CultureDeletion() : base(new GodPower() {
      id = "culture_wipe_keygui",
      name = "Culture Wipe",
      force_map_mode = MetaType.Culture,
      select_button_action = CultureDeletionPowerButtonPress,
      click_special_action = ClickWithCultureDeletion,
    }) {
    }
    
    private static bool CultureDeletionPowerButtonPress(string _) {
      WorldTip.showNow("KGPLL_CultureDeletion_SelectCulture", true, "top");
      return false;
    }
    
    private static bool ClickWithCultureDeletion(WorldTile pTile, string pPowerID) {
      Culture cultureToWipe = pTile.zone.city?.culture;
      if (cultureToWipe != null) {
        KeyLib.Get<KeyGenLibCultureManipulationMethodCollection>().DeleteCulture(cultureToWipe);
        WorldTip.showNow("KGPLL_CultureDeletion_Success", true, "top");
        return true;
      }

      WorldTip.showNow("KGPLL_CultureDeletion_NoCultureSelectedError", true, "top");
      return false;
    }
  }
}
