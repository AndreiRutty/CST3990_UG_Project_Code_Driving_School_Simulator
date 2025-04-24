using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

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
    public GameObject controlPanel;
    bool openControl = true;

    public int citySize;
    void Start()
    {
        OpenMenu();
    }

    void Update()
    {

        if (!menu.activeSelf)
        {
            if (Input.GetKey(KeyCode.T))
            {
                openControl = !openControl;
            }
            controlPanel.SetActive(openControl);
        }

    }

    public void OpenMenu()
    {
        menu.SetActive(true);
        errorsCanva.SetActive(false);
        testOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        controlPanel.SetActive(false);
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
        errorsCanva.SetActive(true);
        testOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        controlPanel.SetActive(true);
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

    public void SetCitySize(int value)
    {
        citySize = value;
        Debug.Log(citySize);
    }
}
