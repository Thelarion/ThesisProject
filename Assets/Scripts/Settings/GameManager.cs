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
    public GameObject player;


    public static string ChosenPalette { get => chosenPalette; set => chosenPalette = value; }

    private void Awake()
    {
        SetPaletteChoice();
        ChooseRandomMaterialPalette();
    }

    private void Start()
    {
        player = GameObject.Find("Player");

        Invoke("PlayMelodyCoroutine", 3f);
        if (!StartMenuManager.InclusionState)
        {
            AkSoundEngine.PostEvent("Play_Birds", player);
        }
    }

    int index = 0;
    void PlayMelodyCoroutine()
    {
        StartCoroutine(PlayMelody());
    }

    IEnumerator PlayMelody()
    {
        AkSoundEngine.PostEvent("Play_M1", player);
        yield return new WaitForSeconds(5f);
        if (index < 2)
        {
            StartCoroutine(PlayMelody());
            index++;
        }
        else
        {
            AkSoundEngine.PostEvent("Reset_M1_Success_Announce", player);
            AkSoundEngine.PostEvent("Play_M1_Intro", player);
        }

    }

    private void SetPaletteChoice()
    {
        paletteOptions.Add("Palette1");
        paletteOptions.Add("Palette2");
    }

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
