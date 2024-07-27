using System;

namespace KeyGeneralPurposeLibrary.Assets {
  public class KeyGenLibCustomWarTypeLibrary : KLibAssetLibrary<WarTypeAsset> {
    public KeyGenLibCustomWarTypeLibrary() {
      AddAsset(_worldWarWarTypeAsset, out _worldWarWarTypeIndex);
    }
    public static int WorldWarWarTypeIndex => _worldWarWarTypeIndex;
    
    private static int _worldWarWarTypeIndex;

    private readonly WarTypeAsset _worldWarWarTypeAsset = new WarTypeAsset {
      id = "spite",
      name_template = "war_spite",
      localized_type = "war_type_spite",
      path_icon = "wars/war_spite",
      kingdom_for_name_attacker = true,
      forced_war = true,
      total_war = true,
      alliance_join = true,
      can_end_with_plot = UnityEngine.Random.value < 0.3f,
    };
  }
  
  [Obsolete("Use KeyGeneralPurposeLibraryCustomWarTypeLibrary instead")]
  public class KeyGeneralPurposeLibraryCustomWarTypeList : KeyGenLibCustomWarTypeLibrary {
    internal KeyGeneralPurposeLibraryCustomWarTypeList() { }
  }
}