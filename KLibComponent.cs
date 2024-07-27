namespace KeyGeneralPurposeLibrary {
  public abstract class KLibComponent {
    internal bool IsInitialized { get; private set; }

    internal virtual void Initialize() {
      IsInitialized = true;
    }
    
    internal virtual void Update() { }
  }
}