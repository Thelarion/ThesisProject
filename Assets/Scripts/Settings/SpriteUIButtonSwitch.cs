using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteUIButtonSwitch : MonoBehaviour
{
    public Sprite[] Sprites;
    private bool currentSpriteState;

    public void ToggleSprite(bool value)
    {
        if (value)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = Sprites[1];
        }
        else
        {
            transform.GetChild(0).GetComponent<Image>().sprite = Sprites[0];
        }

    }
}
