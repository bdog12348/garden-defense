using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    #region Inspector Fields
    [Header("Inspector Fields")]
    [SerializeField] Slider gardenHealth;
    #endregion

    GameSettingsSO gameSettings;

    private void Start()
    {
    }

    public void Initialize(GameManager manager)
    {
        gameSettings = manager.GameSettings;
        gardenHealth.maxValue = gameSettings.maxGardenHealth;
        gardenHealth.value = gardenHealth.maxValue;
    }

    public void UpdateGardenHealthUI(float newGardenHealth)
    {
        gardenHealth.value = newGardenHealth;
    }
}
