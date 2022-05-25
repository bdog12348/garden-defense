using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    [SerializeField] GameObject popupCanvas;
    public void ShowPopup()
    {
        popupCanvas.SetActive(true);
    }

    public void HidePopup()
    {
        popupCanvas.SetActive(false);
    }
}
