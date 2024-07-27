namespace KeyGeneralPurposeLibrary.BehaviourManipulation {
  public class KeyGenLibWorldGenerationManipulationMethodCollection : KLibComponent {
    public void SetWorldSeed(int seed) {
      KeyLib.Get<KeyGenLibHarmonyPatchCollection>().SetRandomSeed(seed);
      if (!KeyGenLibHarmonyPatchCollection.UseFixedRandomSeed) {
        KeyLib.Get<KeyGenLibHarmonyPatchCollection>().ToggleFixedSeedUsage();
      }
    }
  }
}