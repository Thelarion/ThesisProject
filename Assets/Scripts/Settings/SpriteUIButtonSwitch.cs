using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteUIButtonSwitch : MonoBehaviour
{
    public Sprite[] Sprites;
    private bool currentSpriteState;

    public void ToggleState(bool value)
    {
        // [0] = false = Off
        // [1] = true = On
        if (value)
        {
            Sprite OnSprite = Sprites[1];
            transform.GetChild(0).GetComponent<Image>().sprite = OnSprite;
            gameObject.GetComponentInParent<AudioMenu>().SwitchToggleAndReadAloud(gameObject, OnSprite.name);
        }
        else
        {
            Sprite OffSprite = Sprites[0];
            transform.GetChild(0).GetComponent<Image>().sprite = OffSprite;
            gameObject.GetComponentInParent<AudioMenu>().SwitchToggleAndReadAloud(gameObject, OffSprite.name);
        }
    }
}