using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject menu;
    public GameObject errorsCanva;
    public GameObject testOverPanel;
    public GameObject pausePanel;
    public GameObject startButton;
    public bool hasClickDefaultMap = true;
    public bool hasClickRandomMap = false;
    public bool hasClickAddLeftTrafficButton = true;
    public bool hasClickAddRightTrafficButton = false;
    public GameObject mapChosenGameObject;
    public TextMeshProUGUI mapChosenText;
    public GameObject trafficChosenGameObject;
    public TextMeshProUGUI trafficChosenText;
    void Start()
    {
        OpenMenu();
    }

    void Update()
    {

    }

    public void OpenMenu()
    {
        menu.SetActive(true);
        errorsCanva.SetActive(false);
        testOverPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
        errorsCanva.SetActive(true);
        testOverPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void OpenPauseMenu()
    {
        pausePanel.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        pausePanel.SetActive(false);
    }

    public void PressedDefaultMapButton()
    {
        hasClickDefaultMap = true;
        hasClickRandomMap = false;
        DisplayAndHideMapText("Default Map Chosen");
    }

    public void PressedRandomMapButton()
    {
        hasClickDefaultMap = false;
        hasClickRandomMap = true;
        DisplayAndHideMapText("Random Map Chosen");
    }

    public void PressedAddLeftTrafficButton()
    {
        hasClickAddLeftTrafficButton = true;
        hasClickAddRightTrafficButton = false;
        DisplayAndHideTrafficText("Left Traffic Chosen");
    }

    public void PressedAddRightTrafficButton()
    {
        hasClickAddLeftTrafficButton = false;
        hasClickAddRightTrafficButton = true;
        DisplayAndHideTrafficText("Right Traffic Chosen");
    }


    public void DisplayAndHideMapText(string text)
    {
        mapChosenGameObject.SetActive(true);
        mapChosenText.text = text;
    }

    public void DisplayAndHideTrafficText(string text)
    {
        trafficChosenGameObject.SetActive(true);
        trafficChosenText.text = text;
    }
}
