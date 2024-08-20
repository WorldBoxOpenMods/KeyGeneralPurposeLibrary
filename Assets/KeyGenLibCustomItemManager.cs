using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using KeyGeneralPurposeLibrary.Classes;
using UnityEngine;

namespace KeyGeneralPurposeLibrary.Assets {
  public class KeyGenLibCustomItemManager : KLibComponent {
    public void SaveItemsLocally(string modName, List<CustomItemAsset> items) {
      if (!Directory.Exists(Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + modName + "/Items"))) {
        Directory.CreateDirectory(Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + modName + "/Items"));
      }

      string path = Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + modName + "/Items/items.txt");
      File.Create(path).Dispose();
      using (StreamWriter writer = new StreamWriter(path)) {
        foreach (CustomItemAsset item in items) {
          writer.WriteLine(item + "<+|-|-|+>");
        }
      }
    }

    public List<CustomItemAsset> LoadItems(string modName) {
      List<CustomItemAsset> items = new List<CustomItemAsset>();
      string path = Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + modName + "/Items/items.txt");
      if (File.Exists(path)) {
        string itemData = "";
        using (StreamReader reader = new StreamReader(path)) {
          string line;
          while ((line = reader.ReadLine()) != null) {
            itemData += line;
          }
        }

        string[] itemDataArray = itemData.Split(new[] { "<+|-|-|+>" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string itemDataString in itemDataArray) {
          CustomItemAsset item = new CustomItemAsset();
          items.Add(item);
          item.LoadItem(itemDataString);
        }
      }

      return items;
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

    public Dictionary<string, float> ConvertItemStats(Dictionary<string, string> statDictionary) {
      Dictionary<string, float> stats = new Dictionary<string, float>();
      for (int i = 0; i < statDictionary.Count; ++i) {
        string statName = statDictionary.Keys.ElementAt(i);
        string statValue = statDictionary.Values.ElementAt(i);
        if (statValue.Contains('%')) {
          statValue = statValue.Replace("%", "");
          statValue = statValue.Replace(",", ".");
          try {
            stats.Add(ConvertToSnakeCase(statName), float.Parse(statValue, CultureInfo.InvariantCulture) / 100);
          } catch (Exception) {
            Debug.Log("Error parsing stat: " + ConvertToSnakeCase(statName) + " with value: " + statValue + ", trying again differently.");
            stats.Add(ConvertToSnakeCase(statName), float.Parse(statValue.Replace(".", ",")) / 100);
          }
        } else {
          statValue = statValue.Replace(",", ".");
          try {
            stats.Add(ConvertToSnakeCase(statName), float.Parse(statValue, CultureInfo.InvariantCulture));
          } catch (Exception) {
            Debug.Log("Error parsing stat: " + ConvertToSnakeCase(statName) + " with value: " + statValue + ", trying again differently.");
            stats.Add(ConvertToSnakeCase(statName), float.Parse(statValue.Replace(".", ",")));
          }
        }
      }

      return stats;
    }
  }
}
