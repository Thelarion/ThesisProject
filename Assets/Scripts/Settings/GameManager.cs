using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using StarterAssets;
using System;

public class GameManager : MonoBehaviour
{
    // public GameObject menuAccessibility;
    // private FirstPersonController firstPersonController;
    private static List<string> paletteOptions = new List<string>();
    private static string chosenPalette;
    public GameObject MenuInGameSettings;
    public GameObject InGameUI;
    public AudioMenu audioMenuInGame;
    private bool menuActive;
    public GameObject Blur;


    public static string ChosenPalette { get => chosenPalette; set => chosenPalette = value; }

    private void Awake()
    {
        SetPaletteChoice();
        ChooseRandomMaterialPalette();

    }

    // private void Start()
    // {
    //     firstPersonController = GameObject.Find("Player").GetComponent<FirstPersonController>();
    // }
    private void SetPaletteChoice()
    {
        paletteOptions.Add("Palette1");
        paletteOptions.Add("Palette2");
    }

    // private void Update()
    // {
    //     CheckCursorLockState();
    // }
    // void CheckCursorLockState()
    // {
    //     // if (menuPanel.activeSelf || menuAccessibility.activeSelf)
    //     if (MenuInGameSettings.activeSelf)
    //     {
    //         Cursor.lockState = CursorLockMode.None;
    //         Cursor.visible = true;
    //         firstPersonController.MenuToggle = true;
    //     }
    //     else
    //     {
    //         Cursor.lockState = CursorLockMode.Locked;
    //         Cursor.visible = false;
    //         firstPersonController.MenuToggle = false;
    //     }
    // }

    private static void ChooseRandomMaterialPalette()
    {
        int indexPalette = UnityEngine.Random.Range(0, paletteOptions.Count);
        chosenPalette = paletteOptions[indexPalette];
    }


    public void Quit()
    {
        Application.Quit();
    }

    public void ToggleMenu()
    {
        if (!menuActive)
        {
            AkSoundEngine.PostEvent("Play_MenuOpen", gameObject);
            InGameUI.SetActive(false);
            Blur.SetActive(true);
            MenuInGameSettings.SetActive(true);
            audioMenuInGame.ButtonReadAloud();
            menuActive = true;
        }
        else
        {
            AkSoundEngine.PostEvent("Play_MenuClose", gameObject);
            InGameUI.SetActive(true);
            Blur.SetActive(false);
            MenuInGameSettings.SetActive(false);
            menuActive = false;
        }
    }
}
