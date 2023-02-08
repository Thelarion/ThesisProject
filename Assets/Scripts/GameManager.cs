using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class GameManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject menuAccessibility;
    private FirstPersonController firstPersonController;
    private bool menuActive;

    private void Start()
    {
        firstPersonController = GameObject.Find("Player").GetComponent<FirstPersonController>();
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
