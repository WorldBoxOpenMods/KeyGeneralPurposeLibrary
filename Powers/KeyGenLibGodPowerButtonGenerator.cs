using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace KeyGeneralPurposeLibrary.Powers {
  public class KeyGenLibGodPowerButtonGenerator : KLibComponent {
    public PowerButton CreateHiddenPowerButton(Sprite buttonParentSprite, GodPower power, Sprite buttonSprite) {
      GameObject buttonParent = null;
      {
        GameObject[] objectsOfTypeAll = Resources.FindObjectsOfTypeAll<GameObject>();
        GameObject evenInactive = objectsOfTypeAll.FirstOrDefault(t => t.gameObject.gameObject.name == "inspect");
        if (evenInactive != null) {
          evenInactive.SetActive(false);
          buttonParent = Object.Instantiate(evenInactive);
          evenInactive.SetActive(true);
        }
      }
      if (buttonParent != null) {
        buttonParent.SetActive(false);
        buttonParent.transform.Find("Icon").GetComponent<Image>().sprite = buttonParentSprite;
        PowerButton powerButton = buttonParent.GetComponent<PowerButton>();
        powerButton.open_window_id = string.Empty;
        powerButton.type = PowerButtonType.Active;
        powerButton.godPower = power;
        powerButton.icon.sprite = buttonSprite;
        return powerButton;
      }
      return null;
    }
  }
}