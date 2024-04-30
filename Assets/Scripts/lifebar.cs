using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class lifebar : MonoBehaviour
{
    public Image life;
    public TMP_Text lifeText;
    public Image backgroundImage;
    public float vidaatual;
    public float vidamaxima;

    private bool isVisible = false;

    void Update()
    {
        life.fillAmount = vidaatual / vidamaxima;
        lifeText.text = vidaatual.ToString() + "/" + vidamaxima.ToString();
    }


    public void ToggleVisibility()
    {
        isVisible = !isVisible;


        if (life != null)
            life.enabled = isVisible;

        if (lifeText != null)
            lifeText.enabled = isVisible;

        if (backgroundImage != null)
            backgroundImage.enabled = isVisible;
    }
}
