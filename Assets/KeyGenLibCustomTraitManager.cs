using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using KeyGeneralPurposeLibrary.Classes;
using UnityEngine;

namespace KeyGeneralPurposeLibrary.Assets {
  public class KeyGenLibCustomTraitManager : KLibComponent {
    public void AddTraitToLocalizedLibrary(string id, string description) {
      LocalizedTextManager.instance.localizedText.Remove("trait_" + id);
      LocalizedTextManager.instance.localizedText.Remove("trait_" + id + "_info");
      LocalizedTextManager.instance.localizedText.Add("trait_" + id, id);
      LocalizedTextManager.instance.localizedText.Add("trait_" + id + "_info", description);
    }

    public void SaveTraitsLocally(string modName, List<CustomTrait> traits) {
      if (!Directory.Exists(Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + modName + "/Traits"))) {
        Directory.CreateDirectory(Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + modName + "/Traits"));
      }

      string path = Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + modName + "/Traits/traits.txt");
      File.Create(path).Dispose();
      using (StreamWriter writer = new StreamWriter(path)) {
        foreach (CustomTrait trait in traits) {
          writer.WriteLine(trait + "|||");
        }
      }
    }

    public List<CustomTrait> LoadTraits(string modName) {
      List<CustomTrait> traits = new List<CustomTrait>();
      string path = Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + modName + "/Traits/");
      if (Directory.Exists(path)) {
        foreach (string filePath in Directory.GetFiles(path)) {
          Debug.Log(filePath);
          string traitData = "";
          using (StreamReader reader = new StreamReader(filePath)) {
            string line;
            while ((line = reader.ReadLine()) != null) {
              traitData += line;
            }
          }

          string[] traitDataArray = traitData.Split(new[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
          foreach (string traitDataString in traitDataArray) {
            try {
              CustomTrait trait = new CustomTrait();
              traits.Add(trait);
              trait.LoadTrait(traitDataString);
            } catch (Exception e) {
              Debug.LogError("Error loading trait: " + traitDataString + ", error: " + e);
            }
          }
        }
      }

      return traits;
    }

    public Dictionary<string, float> ConvertTraitStats(Dictionary<string, string> statDictionary) {
      Dictionary<string, float> stats = new Dictionary<string, float>();
      for (int i = 0; i < statDictionary.Count; ++i) {
        string statName = statDictionary.Keys.ElementAt(i);
        string statValue = statDictionary.Values.ElementAt(i);
        if (statValue.Contains('%')) {
          statValue = statValue.Replace("%", "");
          statValue = statValue.Replace(",", ".");
          try {
            stats.Add(statName, float.Parse(statValue, CultureInfo.InvariantCulture) / 100);
          } catch (Exception) {
            Debug.Log("Error parsing stat: " + statName + " with value: " + statValue + ", trying again differently.");
            stats.Add(statName, float.Parse(statValue.Replace(".", ",")) / 100);
          }
        } else {
          statValue = statValue.Replace(",", ".");
          try {
            stats.Add(statName, float.Parse(statValue, CultureInfo.InvariantCulture));
          } catch (Exception) {
            Debug.Log("Error parsing stat: " + statName + " with value: " + statValue + ", trying again differently.");
            stats.Add(statName, float.Parse(statValue.Replace(".", ",")));
          }
        }
      }

      return stats;
    }
  }
}