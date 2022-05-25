using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    [SerializeField] Color m_crosshairInactiveColor;
    [SerializeField] Color m_crosshairActiveColor;

    bool m_crosshairActive = false;

    Image m_crosshairImage;

    private void Start()
    {
        m_crosshairImage = GetComponent<Image>();
    }

    public void SetActiveState(bool state)
    {
        m_crosshairActive = state;
        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (m_crosshairActive)
        {
            m_crosshairImage.color = m_crosshairActiveColor;
        }else
        {
            m_crosshairImage.color = m_crosshairInactiveColor;
        }
    }
}
