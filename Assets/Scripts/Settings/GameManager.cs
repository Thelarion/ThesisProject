using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using StarterAssets;
using System;

public class GameManager : MonoBehaviour
{
    private static List<string> paletteOptions = new List<string>();
    private static string chosenPalette;
    public GameObject MenuInGameSettings;
    public GameObject InGameUI;
    public AudioMenu audioMenuInGame;
    private bool menuActive;
    public GameObject Blur;
    public GameObject player;
    private ClosedCaptions closedCaptions;
    private TargetIndicator targetIndicator;


    public static string ChosenPalette { get => chosenPalette; set => chosenPalette = value; }

    private void Awake()
    {
        SetPaletteChoice();
        ChooseRandomMaterialPalette();
    }

    private void Start()
    {
        // Initialization
        closedCaptions = GameObject.Find("ClosedCaptions").GetComponent<ClosedCaptions>();
        targetIndicator = GameObject.Find("TargetIndicator").GetComponent<TargetIndicator>();
        player = GameObject.Find("Player");

        // Logging
        LogManager.StartTime = Time.time;

        // Start of the Game: Melody introduction
        Invoke("PlayMelodyCoroutine", 1.5f);

        // Change the soundscape based on the game mode
        if (!StartMenuManager.InclusionState)
        {
            AkSoundEngine.PostEvent("Play_Birds", player);
            AkSoundEngine.SetRTPCValue("RTPC_ReverbMode", 1);
        }
        else if (StartMenuManager.InclusionState)
        {
            AkSoundEngine.SetRTPCValue("RTPC_ReverbMode", 0);
        }

    }

    void PlayMelodyCoroutine()
    {
        StartCoroutine(PlayMelody());
    }

    IEnumerator PlayMelody()
    {
        // Melody first
        AkSoundEngine.PostEvent("Play_M1", player);
        yield return new WaitForSeconds(2.5f);

        // Introduction
        AkSoundEngine.PostEvent("Reset_M1_Success_Announce", player); // Reset Playlist
        AkSoundEngine.PostEvent("Play_M1_Intro", player);
        closedCaptions.StartMelodyDone = true;
        targetIndicator.StartMelodyDone = true;

    }

    // Switch between Colour Palettes
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

    // In Game Menu Toggle
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
