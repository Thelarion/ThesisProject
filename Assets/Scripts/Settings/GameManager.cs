using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;
using System;

public class GameManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject menuAccessibility;
    private FirstPersonController firstPersonController;
    private bool menuActive;
    private static List<string> paletteOptions = new List<string>();
    private static string chosenPalette;


    public static string ChosenPalette { get => chosenPalette; set => chosenPalette = value; }

    private void Awake()
    {
        SetPaletteChoice();
        ChooseRandomMaterialPalette();
    }

    private void Start()
    {
        firstPersonController = GameObject.Find("Player").GetComponent<FirstPersonController>();
    }
    private void SetPaletteChoice()
    {
        paletteOptions.Add("Palette1");
        paletteOptions.Add("Palette2");
    }

    private void Update()
    {
        CheckCursorLockState();
    }
    void CheckCursorLockState()
    {
        if (menuPanel.activeSelf || menuAccessibility.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            firstPersonController.MenuToggle = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            firstPersonController.MenuToggle = false;
        }
    }

    private static void ChooseRandomMaterialPalette()
    {
        int indexPalette = UnityEngine.Random.Range(0, paletteOptions.Count);
        chosenPalette = paletteOptions[indexPalette];
    }

    public void ToggleMenu()
    {
        if (!menuActive)
        {
            menuPanel.SetActive(true);
            menuActive = true;
        }
        else
        {
            menuPanel.SetActive(false);
            menuAccessibility.SetActive(false);
            menuActive = false;
        }

    }

    public void Quit()
    {
        Application.Quit();
    }
}
