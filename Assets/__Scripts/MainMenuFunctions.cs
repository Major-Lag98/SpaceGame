using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuFunctions : MonoBehaviour
{
    public GameObject MainMenuContainer;
    public GameObject ControlsContainer;

    public void GoToControls()
    {
        MainMenuContainer.SetActive(false);
        ControlsContainer.SetActive(true);
    }

    public void GotoMainMenu()
    {
        MainMenuContainer.SetActive(true);
        ControlsContainer.SetActive(false);
    }
}
