using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestMode : MonoBehaviour
{
    public GameObject testOverPanel;
    public FaultCounter faultCounter;
    public TextMeshProUGUI timerText;
    public TMP_InputField inputField;

    public TextMeshProUGUI commentText;

    [Header("Fault Counter Text")]
    public TextMeshProUGUI speedFaultText;
    public TextMeshProUGUI redLightFaultText;
    public TextMeshProUGUI mirrorFaultText;
    public TextMeshProUGUI signalFaultText;
    public TextMeshProUGUI blindspotFaultText;
    public TextMeshProUGUI laneDisciplineFaultText;
    public TextMeshProUGUI directionFaultText;
    public TextMeshProUGUI securityDistanceFaultText;
    public TextMeshProUGUI stopLineFaultText;
    private float countdownTime;
    public bool isCountingDown = false;
    public bool isTimeValid = true;
    public GameObject incorrectTimeTextGameObject;

    void Start()
    {
        inputField.text = "1";

        speedFaultText.text = "";
        redLightFaultText.text = "";
        signalFaultText.text = "";
        mirrorFaultText.text = "";
        blindspotFaultText.text = "";
        laneDisciplineFaultText.text = "";
        directionFaultText.text = "";
        securityDistanceFaultText.text = "";
        stopLineFaultText.text = "";
        commentText.text = "";
    }

    public bool CheckTimeInput()
    {
        if (int.TryParse(inputField.text, out int inputTime) && inputTime > 0)
        {
            isTimeValid = true;
        }
        else
        {
            isTimeValid = false;
            Debug.Log("Incorrect Time Value");
            StartCoroutine(DisplayAndHideErrorText());
        }

        return isTimeValid;
    }

    public void StartCountdownFromInput()
    {
        int time;
        if (int.TryParse(inputField.text, out time))
        {
            countdownTime = time * 60; 
            isCountingDown = true;
        }
    }

    void Update()
    {
        if (isCountingDown)
        {
            countdownTime -= Time.deltaTime;

            if (countdownTime > 0)
            {
                DisplayTime(countdownTime);
            }
            else
            {
                countdownTime = 0;
                isCountingDown = false;
                StopTest();
            }
        }

        UpdateFaultsCounterText();
        UpdateCommentText();
    }

    void DisplayTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void StopTest()
    {
        Time.timeScale = 0; // Pause the game
        Debug.Log("Test Over!");
        testOverPanel.SetActive(true);
    }

    void UpdateFaultsCounterText()
    {
        // Speed 
        if (faultCounter.overSpeedingCount > 0)
        {
            speedFaultText.text = "Speed Fault X" + faultCounter.overSpeedingCount;
        }

        // Red Light
        if (faultCounter.redLightFault > 0)
        {
            redLightFaultText.text = "Red Light Violation X" + faultCounter.redLightFault;
        }

        // Signal
        if (faultCounter.rightSignalCount > 0 || faultCounter.leftSignalCount > 0)
        {
            signalFaultText.text = "Not using signal X" + (faultCounter.rightSignalCount + faultCounter.leftSignalCount);
        }

        // Mirror
        if (faultCounter.rightMirrorCount > 0 || faultCounter.leftMirrorCount > 0)
        {
            mirrorFaultText.text = "Not checking mirror X" + (faultCounter.rightMirrorCount + faultCounter.leftMirrorCount);
        }

        // Blindspot
        if (faultCounter.rightBlindspotCount > 0 || faultCounter.leftBlindspotCount > 0)
        {
            blindspotFaultText.text = "Not checking blindspot X" + (faultCounter.rightBlindspotCount + faultCounter.leftBlindspotCount);
        }

        // Lane Discipline
        if (faultCounter.onMiddleOfRoadFault > 0 || faultCounter.onMiddleOfTwoLanesFault > 0 || faultCounter.directionFault > 0)
        {
            laneDisciplineFaultText.text = "Not respecting lane discipline X" + (faultCounter.onMiddleOfRoadFault + faultCounter.onMiddleOfTwoLanesFault + faultCounter.directionFault);
        }

        // Security Distance
        if (faultCounter.securityDistanceFault > 0)
        {
            securityDistanceFaultText.text = "Not respecting security distance X" + faultCounter.securityDistanceFault;
        }

        // Stop Line Distance
        if (faultCounter.stopLineDistanceFault > 0)
        {
            stopLineFaultText.text = "Not stopping after stop line X" + faultCounter.stopLineDistanceFault;
        }
    }

    void UpdateCommentText()
    {
        if (faultCounter.totalFaultCount < 5)
        {
            commentText.text = "Congratulation!\nYou've passed the simulated test!";
        }
        else if (faultCounter.totalFaultCount >= 5 && faultCounter.totalFaultCount < 10)
        {
            commentText.text = "You've passed the simulated test!\nBut you need a litle bit  more practice";
        }
        else
        {
            commentText.text = "You've failed!\nYou need to practice more!";
        }
    }

    IEnumerator DisplayAndHideErrorText()
    {
        incorrectTimeTextGameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        incorrectTimeTextGameObject.SetActive(false);
    }
}
