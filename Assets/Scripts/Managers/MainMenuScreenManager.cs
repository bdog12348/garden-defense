using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScreenManager : MonoBehaviour
{
    [SerializeField] GameObject[] screens;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableScreen(int screenIndex)
    {
        for(int i = 0; i < screens.Length; i++)
        {
            if (i == screenIndex)
            {
                screens[i].SetActive(true);
            }else
            {
                screens[i].SetActive(false);
            }
        }
    }
}
