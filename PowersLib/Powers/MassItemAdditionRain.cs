namespace KeyGeneralPurposeLibrary.PowersLib.Powers {
  public class MassItemAdditionRain : KeyGenLibPower {
    public MassItemAdditionRain() : base(new GodPower {
      id = "item_mass_addition_rain_keygui",
      name = "Item Mass Addition Rain",
      hold_action = true,
      show_tool_sizes = true,
      unselect_when_window = true,
      falling_chance = 0.05f,
      rank = PowerRank.Rank0_free,
      drop_id = "item_mass_addition_rain_keygui",
      click_power_action = (tTile, pPower) => {
        bool result = AssetManager.powers.spawnDrops(tTile, pPower);
        result = result && AssetManager.powers.flashPixel(tTile, pPower);
        result = result && AssetManager.powers.fmodDrawingSound(tTile, pPower);
        return result;
      },
      click_power_brush_action = AssetManager.powers.loopWithCurrentBrushPowerForDropsFull,
      sound_drawing = "event:/SFX/POWERS/GammaRain"
    }) {
    }
  }
}
