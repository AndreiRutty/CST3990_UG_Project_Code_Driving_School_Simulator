using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Modules")]
    public UIManager uIManager;
    public RunTimeSample runTimeSample;
    public LaneSetUp laneSetUp;
    public TestMode testModeScript;
    public FaultCounter faultCounter;

    [Space(10)]
    [Header("Game Objects")]
    public GameObject player;
    public GameObject trafficRulesManager;
    public GameObject background;
    public GameObject defaultCity;
    public Transform spawnPoint;
    public GameObject feedbackPanel;

    [Space(10)]
    [Header("Buttons Game Objects")]
    public GameObject startFreeModeButton;
    public GameObject startTestModeButton;
    public bool testMode = true;

    [Space(10)]
    [Header("Feedback Messages")]
    public GameObject modeChosenGameObject;
    public TextMeshProUGUI modeChosenText;
    public GameObject gameOverPanel;


    void Start()
    {
        player.SetActive(false);
        Time.timeScale = 0f;
        ActivateDefaultCity();
        runTimeSample.RightHand(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.P) && !uIManager.menu.activeSelf)
        {
            uIManager.OpenPauseMenu();
            StopGame();
        }

        if (testMode)
        {
            trafficRulesManager.SetActive(true);
            feedbackPanel.SetActive(true);
        }
        else
        {
            trafficRulesManager.SetActive(false);
            feedbackPanel.SetActive(false);
        }

    }

    // Buttons Actions
    public void StartTest()
    {
        if (testModeScript.CheckTimeInput())
        {// Generating Map based on user input
         // Random Map
            if (uIManager.hasClickRandomMap)
            {
                GenerateCity();
                RemoveDefaultCity();
            }
            // Default Map
            else if (uIManager.hasClickDefaultMap)
            {
                ActivateDefaultCity();
                Destroy(GameObject.Find("City-Maker"));
            }

            // Adding traffic system based on user input]
            // Left Hand Traffic
            if (uIManager.hasClickAddLeftTrafficButton)
            {
                SetTrafficDirection(false);
            }
            // Right Hand Traffic
            else if (uIManager.hasClickAddRightTrafficButton)
            {
                SetTrafficDirection(true);
            }

            // Creating a new car container to store newly added cars
            GameObject carContainer = new GameObject("CarContainer");

            // Adding new AI cars in the scene
            runTimeSample.AddTrafficSystem();

            // reset test parameters
            ResetGameSetting();

            // close menu
            uIManager.CloseMenu();

            // Start game
            SetTestMode();
            background.SetActive(false);
            player.SetActive(true);
            Time.timeScale = 1f;
            testModeScript.StartCountdownFromInput();
        }

    }

    public void StartFreeMode()
    {
        if (uIManager.hasClickRandomMap)
        {
            GenerateCity();
            RemoveDefaultCity();
        }
        // Default Map
        else if (uIManager.hasClickDefaultMap)
        {
            ActivateDefaultCity();
            Destroy(GameObject.Find("City-Maker"));
        }

        // Adding traffic system based on user input]
        // Left Hand Traffic
        if (uIManager.hasClickAddLeftTrafficButton)
        {
            SetTrafficDirection(false);
        }
        // Right Hand Traffic
        else if (uIManager.hasClickAddRightTrafficButton)
        {
            SetTrafficDirection(true);
        }

        // Creating a new car container to store newly added cars
        GameObject carContainer = new GameObject("CarContainer");

        // Adding new AI cars in the scene
        runTimeSample.AddTrafficSystem();

        // reset test parameters
        ResetGameSetting();

        // close menu
        uIManager.CloseMenu();

        // Start game
        SetFreeMode();
        background.SetActive(false);
        player.SetActive(true);
        Time.timeScale = 1f;
    }


    void SetTrafficDirection(bool right)
    {
        Destroy(GameObject.Find("CarContainer"));
        runTimeSample.RightHand(right);
        laneSetUp.StartActivateLaneSetting();
    }

    public void StartGame()
    {
        SetTestMode();
        background.SetActive(false);
        player.SetActive(true);
        Time.timeScale = 1f;
        testModeScript.StartCountdownFromInput();
    }

    public void ShowMenuBackground()
    {
        background.SetActive(true);
        player.SetActive(false);
    }

    public void GenerateCity()
    {
        runTimeSample.GenerateCityAtRuntime(1);
        runTimeSample.GenerateBuildings();
    }

    public void ActivateDefaultCity()
    {
        if (!defaultCity.activeSelf)
        {
            defaultCity.SetActive(true);
        }
    }

    public void ChangePlayerPosition(bool rightHand)
    {
        if (rightHand)
        {
            spawnPoint.transform.position = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, -6);
        }
        else
        {
            spawnPoint.transform.position = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, 6);
        }
    }


    public void RemoveDefaultCity()
    {
        defaultCity.SetActive(false);
    }

    public void SetFreeMode()
    {
        testMode = false;
        startFreeModeButton.SetActive(true);
        startTestModeButton.SetActive(false);
        modeChosenGameObject.SetActive(true);
        modeChosenText.text = "Free Mode Chosen";
    }

    public void SetTestMode()
    {
        testMode = true;
        startTestModeButton.SetActive(true);
        startFreeModeButton.SetActive(false);
        modeChosenGameObject.SetActive(true);
        modeChosenText.text = "Test Mode Chosen";
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
    }

    public void StopGame()
    {
        Time.timeScale = 0f;
    }

    public void ResetGameSetting()
    {
        faultCounter.ResetFaultsCounter();
        player.transform.position = spawnPoint.position;
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
