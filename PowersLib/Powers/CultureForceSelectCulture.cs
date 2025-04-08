using UnityEngine;
namespace KeyGeneralPurposeLibrary.PowersLib.Powers {
  public class CultureForceSelectCulture : KeyGenLibPower {
    public CultureForceSelectCulture() : base(new GodPower() {
      id = "culture_force_select_culture_keygui",
      name = "Culture Force Select Culture",
      force_map_mode = MetaType.Culture,
      select_button_action = CultureForceSelectCulturePowerButtonPress,
      click_special_action = ClickWithCultureForceSelectCulture
    }) {
    }
    
    private static bool CultureForceSelectCulturePowerButtonPress(string _) {
      CultureToForceUponCity = null;
      WorldTip.showNow("KGPLL_CultureForceConversion_SelectCulture", true, "top");
      return false;
    }
    
    internal static Culture CultureToForceUponCity;
    private static bool ClickWithCultureForceSelectCulture(WorldTile pTile, string pPowerID) {
      Culture cultureToForce = pTile.zone.city?.culture;
      CultureToForceUponCity = cultureToForce;
      if (cultureToForce != null) {
        GodPower power = KeyLib.Get<KeyGenLibGodPowerLibrary>().Get<CultureForceSelectCity>().Power;
        PowerButton button = KeyLib.Get<KeyGenLibGodPowerLibrary>().Get<CultureForceSelectCity>().Button;
        if (button != null) {
          WorldTip.showNow("KGPLL_CultureForceConversion_SelectCity", true, "top");
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
          return true;
        }
        Debug.LogError("Something went wrong with the Culture Conversion! Please report this to the mod author!");
        return false;
      }
      WorldTip.showNow("KGPLL_CultureForceConversion_NoCultureSelectedError", true, "top");
      return false;
    }
  }
}
