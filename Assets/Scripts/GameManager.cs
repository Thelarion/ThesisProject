using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class GameManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject menuAccessibility;
    private FirstPersonController firstPersonController;

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
        menuPanel.SetActive(!menuPanel.activeSelf);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
