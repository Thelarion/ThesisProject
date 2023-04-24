using UnityEngine;
using UnityEngine.UI;

// Details: SpriteUIButtonSwitch
// Change of the sprite according to the mode (contrasting menu)

public class SpriteUIButtonSwitch : MonoBehaviour
{
    public Sprite[] Sprites;
    public Sprite[] SpritesContrast;
    private bool currentSpriteState;

    // Toggle the sprite based on the passed boolean value
    public void ToggleState(bool value)
    {
        // [0] = false = Off
        // [1] = true = On
        if (value)
        {
            Sprite OnSprite;

            OnSprite = StartMenuManager.InclusionState ? SpritesContrast[1] : Sprites[1];

            // Set up the new sprite
            transform.GetChild(0).GetComponent<Image>().sprite = OnSprite;
            // Also play accordingly the audio menu voice over
            gameObject.GetComponentInParent<AudioMenu>().SwitchToggleAndReadAloud(gameObject, OnSprite.name);
        }
        else
        {
            Sprite OffSprite;

            OffSprite = StartMenuManager.InclusionState ? SpritesContrast[0] : Sprites[0];

            // Set up the new sprite
            transform.GetChild(0).GetComponent<Image>().sprite = OffSprite;
            // Also play accordingly the audio menu voice over
            gameObject.GetComponentInParent<AudioMenu>().SwitchToggleAndReadAloud(gameObject, OffSprite.name);
        }
    }
}