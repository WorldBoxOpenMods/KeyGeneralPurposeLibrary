namespace KeyGeneralPurposeLibrary.Classes {
  public class CustomMoodAsset : MoodAsset {
    public string Author { get; private set; }
    public string Description { get; private set; }
    public string Sprite { get; private set; }

    public CustomMoodAsset(string author, string description, string sprite) {
      Author = author;
      Description = description;
      Sprite = sprite;
    }

    public CustomMoodAsset() { }
  }
}