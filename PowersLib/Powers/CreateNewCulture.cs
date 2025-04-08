using KeyGeneralPurposeLibrary.BehaviourManipulation;
namespace KeyGeneralPurposeLibrary.PowersLib.Powers {
  public class CreateNewCulture : KeyGenLibPower {
    public CreateNewCulture() : base(new GodPower() {
      id = "create_new_culture_keygui",
      name = "Create New Culture",
      force_map_mode = MetaType.City,
      select_button_action = CreateNewCulturePowerButtonPress,
      click_special_action = ClickWithCreateNewCulture,
    }) {
    }
        
    private static bool CreateNewCulturePowerButtonPress(string _) {
      WorldTip.showNow("KGPLL_CultureCreation_SelectCity", true, "top");
      return false;
    }
    
    private static bool ClickWithCreateNewCulture(WorldTile pTile, string pPowerID) {
      City cityToCreateCultureFor = pTile.zone.city;
      if (cityToCreateCultureFor != null) {
        Culture newCulture = World.world.cultures.newCulture(cityToCreateCultureFor.leader);
        KeyLib.Get<KeyGenLibCultureManipulationMethodCollection>().ForceCultureOnCity(newCulture, cityToCreateCultureFor);
        WorldTip.showNow("KGPLL_CultureCreation_Success", true, "top");
        return true;
      }
      WorldTip.showNow("KGPLL_CultureCreation_NoCitySelected", true, "top");
      return false;
    }
  }
}
