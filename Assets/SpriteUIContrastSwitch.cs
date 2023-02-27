using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteUIContrastSwitch : MonoBehaviour
{
    private bool inclusionState = false;
    private bool previousState = false;
    private Image image;
    public Sprite[] SprArrNorm0Cont1;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        inclusionState = StartMenuManager.InclusionState;

        if (inclusionState != previousState)
        {
            previousState = inclusionState;

            switch (gameObject.transform.tag)
            {
                case "Image":
                    SwitchContrastModeImage();
                    break;
                case "Slider":
                    SwitchContrastModeSlider();
                    break;
                default:
                    print("Error");
                    break;
            }
        }
    }

    private void SwitchContrastModeImage()
    {
        if (inclusionState)
        {
            image.sprite = SprArrNorm0Cont1[1];
        }
        else
        {
            image.sprite = SprArrNorm0Cont1[0];
        }
    }

    private void SwitchContrastModeSlider()
    {
        if (inclusionState)
        {
            image.color = new Color32(255, 255, 0, 255);
        }
        else
        {
            image.color = new Color32(255, 255, 255, 255);
        }
    }
}