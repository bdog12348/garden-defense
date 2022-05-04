using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameSettingsSO gameSettings;

    public GameSettingsSO GameSettings { get => gameSettings; set => gameSettings = value; }

    UIHandler uiHandler;
    SceneLoader loader;

    float gardenHealth;

    // Start is called before the first frame update
    void Start()
    {
        gardenHealth = GameSettings.maxGardenHealth;
        uiHandler = GetComponent<UIHandler>();
        loader = GetComponent<SceneLoader>();
        uiHandler.Initialize(this);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (gardenHealth <= 0)
        {
            LoseGame();
        }
    }

    public void TakeGardenDamage(float amount)
    {
        gardenHealth -= amount;
        uiHandler.UpdateGardenHealthUI(gardenHealth);
        //Debug.Log($"Garden is at {gardenHealth}");
    }

    public void WinGame()
    {
        Cursor.lockState = CursorLockMode.None;
        loader.LoadScene("You Win Screen");

    }
    public void LoseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        loader.LoadScene("Game Over Screen");

    }
}
