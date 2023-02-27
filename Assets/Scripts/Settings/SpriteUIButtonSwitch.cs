using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteUIButtonSwitch : MonoBehaviour
{
    public Sprite[] Sprites;

    public Sprite[] SpritesContrast;
    private bool currentSpriteState;

    public void ToggleState(bool value)
    {
        // [0] = false = Off
        // [1] = true = On
        if (value)
        {
            Sprite OnSprite;

            OnSprite = StartMenuManager.InclusionState ? SpritesContrast[1] : Sprites[1];

            transform.GetChild(0).GetComponent<Image>().sprite = OnSprite;
            gameObject.GetComponentInParent<AudioMenu>().SwitchToggleAndReadAloud(gameObject, OnSprite.name);
        }
        else
        {
            Sprite OffSprite;

            OffSprite = StartMenuManager.InclusionState ? SpritesContrast[0] : Sprites[0];

            transform.GetChild(0).GetComponent<Image>().sprite = OffSprite;
            gameObject.GetComponentInParent<AudioMenu>().SwitchToggleAndReadAloud(gameObject, OffSprite.name);
        }
    }
}