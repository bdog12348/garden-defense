using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuObject;

    [SerializeField] Slider sensitivitySlider;
    [SerializeField] StarterAssets.FirstPersonController firstPersonController; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Paused = !GameManager.Paused;
            Time.timeScale = GameManager.Paused ? 0 : 1;
            pauseMenuObject.SetActive(GameManager.Paused);
            sensitivitySlider.value = firstPersonController.RotationSpeed;
        }

        if (GameManager.Paused)
        {
            firstPersonController.RotationSpeed = sensitivitySlider.value;
        }
    }

    public void ResumeGame()
    {
        GameManager.Paused = false;
        Time.timeScale = 1f;
        pauseMenuObject.SetActive(false);
    }
}
