using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KeyGeneralPurposeLibrary.Assets;
using UnityEngine;

namespace KeyGeneralPurposeLibrary.Classes {
  public class CustomItemAsset : ItemAsset {
    private const string NameValueGroupSeparator = "{+-Group-+}";
    private const string NameValuePairSeparator = "{+-Pair-+}";
    private const string NameValueSeparator = "{+-Value-+}";
    public string Author { get; private set; }
    public string Sprite { get; private set; }
    public int Version { get; private set; }

    public CustomItemAsset(string author, string sprite) {
      Author = author;
      Sprite = sprite;
    }

    public CustomItemAsset() { }

    private static void AddItemToLocalizedLibrary(string id) {
      LocalizedTextManager.instance.localizedText.Remove("item_" + id);
      LocalizedTextManager.instance.localizedText.Add("item_" + id, id);
    }

    private static string ConvertToSnakeCase(string text) {
      if (text == null) {
        throw new ArgumentNullException(nameof(text));
      }

      if (text.Length < 2) {
        return text;
      }

      StringBuilder sb = new StringBuilder();
      sb.Append(char.ToLowerInvariant(text[0]));
      for (int i = 1; i < text.Length; ++i) {
        char c = text[i];
        if (char.IsUpper(c)) {
          sb.Append('_');
          sb.Append(char.ToLowerInvariant(c));
        } else {
          sb.Append(c);
        }
      }

      return sb.ToString();
    }
    /*
     * Information to store:
     * Name (translation key)
     * Author
     * Sprite
     * Version
     * Material
     * Metallic
     * EquipmentType
     * AttackType (if weapon)
     * Base stats
     * Modifiers
     */

    public void LoadItem(string item) {
      if (!item.Contains("ItemVersion" + NameValueSeparator + "2")) {
        item = ConvertVersionOneItemToVersionTwo(item);
      }

      string[] groups = item.Split(new[] { NameValueGroupSeparator }, StringSplitOptions.None);
      string[] generalInfo = groups[0].Split(new[] { NameValuePairSeparator }, StringSplitOptions.None);
      KeyValuePair<string, string>[] generalInfoPairs = new KeyValuePair<string, string>[generalInfo.Length];
      for (int i = 0; i < generalInfo.Length; i++) {
        string[] pair = generalInfo[i].Split(new[] { NameValueSeparator }, StringSplitOptions.None);
        generalInfoPairs[i] = new KeyValuePair<string, string>(ConvertToSnakeCase(pair[0]), pair[1]);
      }

      string[] baseStats = groups[1].Split(new[] { NameValuePairSeparator }, StringSplitOptions.None);
      KeyValuePair<string, string>[] baseStatsPairs = new KeyValuePair<string, string>[baseStats.Length];
      for (int i = 0; i < baseStats.Length; i++) {
        string[] pair = baseStats[i].Split(new[] { NameValueSeparator }, StringSplitOptions.None);
        baseStatsPairs[i] = new KeyValuePair<string, string>(ConvertToSnakeCase(pair[0]), pair[1]);
      }

      string[] modifiers = groups[2].Split(new[] { NameValuePairSeparator }, StringSplitOptions.None);
      KeyValuePair<string, string>[] modifierPairs = new KeyValuePair<string, string>[modifiers.Length];
      for (int i = 0; i < modifiers.Length; i++) {
        string[] pair = modifiers[i].Split(new[] { NameValueSeparator }, StringSplitOptions.None);
        modifierPairs[i] = new KeyValuePair<string, string>(ConvertToSnakeCase(pair[0]), pair[1]);
      }

      Dictionary<string, string> generalInfoDictionary = generalInfoPairs.ToDictionary(x => x.Key, x => x.Value);
      id = generalInfoDictionary["item_name"];
      Author = generalInfoDictionary["item_author"];
      Sprite = generalInfoDictionary["item_sprite"];
      Version = int.Parse(generalInfoDictionary["item_version"]);
      materials = new List<string> { generalInfoDictionary["item_material"] };
      metallic = generalInfoDictionary["item_metallic"] == "true";
      switch (generalInfoDictionary["item_equipment_type"]) {
        case "Weapon":
          equipmentType = EquipmentType.Weapon;
          break;
        case "Armor":
          equipmentType = EquipmentType.Armor;
          break;
        case "Amulet":
          equipmentType = EquipmentType.Amulet;
          break;
        case "Boots":
          equipmentType = EquipmentType.Boots;
          break;
        case "Helmet":
          equipmentType = EquipmentType.Helmet;
          break;
        case "Ring":
          equipmentType = EquipmentType.Ring;
          break;
      }

      if (generalInfoDictionary["item_equipment_type"] == "Weapon") {
        switch (generalInfoDictionary["item_attack_type"]) {
          case "Melee":
            attackType = WeaponType.Melee;
            break;
          case "Ranged":
            attackType = WeaponType.Range;
            break;
        }
      }

      Debug.Log("Trying to set the base stats.");
      baseStatsPairs = baseStatsPairs.GroupBy(x => x.Key).Select(x => x.First()).ToArray();
      Dictionary<string, float> baseStatsDictionary = KeyLib.Get<KeyGenLibCustomItemManager>().ConvertItemStats(baseStatsPairs.ToDictionary(x => x.Key, x => x.Value));
      for (int i = 0; i < baseStatsDictionary.Count; ++i) {
        base_stats[baseStatsDictionary.Keys.ElementAt(i)] = baseStatsDictionary[baseStatsDictionary.Keys.ElementAt(i)];
      }

      //List<string> modifiersList = modifierPairs.Select(x => x.Value).ToList();
      //item_modifiers = modifiersList;
      equipment_value = 50;
      // TODO: This only works for swords.
      name_class = "item_class_weapon";
      path_slash_animation = "effects/slashes/slash_sword";
      name_templates = Toolbox.splitStringIntoList("sword_name#30", "sword_name_king#3", "weapon_name_city", "weapon_name_kingdom", "weapon_name_culture", "weapon_name_enemy_king", "weapon_name_enemy_kingdom");
      AssetManager.items.add(this);
      ActorAnimationLoader.dictItems.Add("w_" + id + "_" + materials[0].ToLower(), KeyGenLibFileAssetManager.CreateSprite("KeyGUI", Sprite));
      cached_sprite = KeyGenLibFileAssetManager.CreateSprite("KeyGUI", Sprite);
      AddItemToLocalizedLibrary(generalInfoDictionary["item_name"]);
      base_stats[S.damage_range] = 0.5f;
    }

    public void LoadItem(string name, int version, string itemMaterial, bool isMetallic, EquipmentType itemEquipmentType, WeaponType weaponType, Dictionary<string, float> baseStats, List<string> modifiers) {
      id = name;
      Version = version;
      materials = new List<string> { itemMaterial };
      metallic = isMetallic;
      equipmentType = itemEquipmentType;
      attackType = weaponType;
      // TODO: Temp testing code for ranged weapons, it doesn't even work
      if (weaponType == WeaponType.Range) {
        base_stats[S.projectiles] = 1;
        base_stats[S.damage_range] = 0.9f;
        projectile = "arrow";
      }
      base_stats.stats_dict = new Dictionary<string, BaseStatsContainer>();
      base_stats.stats_list = new ListPool<BaseStatsContainer>();
      base_stats.mods_list = new ListPool<BaseStatsContainer>();
      for (int i = 0; i < baseStats.Count; ++i) {
        try {
          base_stats[baseStats.Keys.ElementAt(i)] = baseStats[baseStats.Keys.ElementAt(i)];
        } catch (Exception) {
          Debug.Log("Failed to apply stat: " + baseStats.Keys.ElementAt(i) + " to item: " + id);
        }
      }

      item_modifiers = modifiers;
      equipment_value = 50;
      // TODO: This only works for swords.
      name_class = "item_class_weapon";
      path_slash_animation = "effects/slashes/slash_sword";
      name_templates = Toolbox.splitStringIntoList("sword_name#30", "sword_name_king#3", "weapon_name_city", "weapon_name_kingdom", "weapon_name_culture", "weapon_name_enemy_king", "weapon_name_enemy_kingdom");
      AssetManager.items.add(this);
      ActorAnimationLoader.dictItems.Add("w_" + id + "_" + materials[0].ToLower(), KeyGenLibFileAssetManager.CreateSprite("KeyGUI", Sprite));
      cached_sprite = KeyGenLibFileAssetManager.CreateSprite("KeyGUI", Sprite);
      AddItemToLocalizedLibrary(name);
      base_stats[S.damage_range] = 0.5f;
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.Append("ItemName");
      sb.Append(NameValueSeparator);
      sb.Append(id);
      sb.Append(NameValuePairSeparator);
      sb.Append("ItemAuthor");
      sb.Append(NameValueSeparator);
      sb.Append(Author);
      sb.Append(NameValuePairSeparator);
      sb.Append("ItemSprite");
      sb.Append(NameValueSeparator);
      sb.Append(Sprite);
      sb.Append(NameValuePairSeparator);
      sb.Append("ItemVersion");
      sb.Append(NameValueSeparator);
      sb.Append(Version);
      sb.Append(NameValuePairSeparator);
      sb.Append("ItemMaterial");
      sb.Append(NameValueSeparator);
      sb.Append(materials[0]);
      sb.Append(NameValuePairSeparator);
      sb.Append("ItemMetallic");
      sb.Append(NameValueSeparator);
      sb.Append(metallic ? "true" : "false");
      sb.Append(NameValuePairSeparator);
      sb.Append("ItemEquipmentType");
      sb.Append(NameValueSeparator);
      switch (equipmentType) {
        case EquipmentType.Amulet:
          sb.Append("Amulet");
          break;
        case EquipmentType.Armor:
          sb.Append("Armor");
          break;
        case EquipmentType.Boots:
          sb.Append("Boots");
          break;
        case EquipmentType.Helmet:
          sb.Append("Helmet");
          break;
        case EquipmentType.Ring:
          sb.Append("Ring");
          break;
        case EquipmentType.Weapon:
          sb.Append("Weapon");
          sb.Append(NameValuePairSeparator);
          sb.Append("ItemAttackType");
          sb.Append(NameValueSeparator);
          sb.Append(attackType == WeaponType.Melee ? "Melee" : "Ranged");
          break;
        default:
          sb.Append("Amulet");
          break;
      }

      sb.Append(NameValueGroupSeparator);
      foreach (BaseStatsContainer baseStat in base_stats.stats_list) {
        sb.Append(baseStat.id);
        sb.Append(NameValueSeparator);
        sb.Append(baseStat.value);
        sb.Append(NameValuePairSeparator);
      }

      sb.Append("DamageRange");
      sb.Append(NameValueSeparator);
      sb.Append(0.5);
      sb.Append(NameValueGroupSeparator);
      foreach (string modifier in item_modifiers) {
        sb.Append("ItemModifier");
        sb.Append(NameValueSeparator);
        sb.Append(modifier);
        sb.Append(NameValuePairSeparator);
      }

      sb.Remove(sb.Length - NameValuePairSeparator.Length - 1, NameValuePairSeparator.Length);
      return sb.ToString();
    }

    private static string ConvertVersionOneItemToVersionTwo(string item) {
      item = item.Replace(":::", NameValueSeparator);
      item = item.Replace("{+-+}", NameValuePairSeparator);
      return item;
    }
  }
}