using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    public int countdownTime;
    public Text display;
    public Text wavetxt;
    public Image img;
    public GameObject OGWaveImg;

    private void Start() 
    {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart() 
    {
        while (countdownTime > 0) 
        {
            display.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        display.text = "GO!";

        yield return new WaitForSeconds(1f);

        display.gameObject.SetActive(false);
        wavetxt.gameObject.SetActive(false);
        img.gameObject.SetActive(false);
        OGWaveImg.SetActive(true);
    }
}
