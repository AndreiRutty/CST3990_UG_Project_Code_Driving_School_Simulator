using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class Feedback : MonoBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI rightMirrorFaultText;
    public TextMeshProUGUI leftMirrorFaultText;
    public TextMeshProUGUI rightBlinkerFaultText;
    public TextMeshProUGUI leftBlinkerFaultText;
    public TextMeshProUGUI rightBlindspotFaultText;
    public TextMeshProUGUI leftBlindspotFaultText;
    public TextMeshProUGUI speedFaultText;
    public TextMeshProUGUI laneDisciplineFaultText;
    public TextMeshProUGUI directionFaultText;
    public TextMeshProUGUI redLightFaultText;
    public TextMeshProUGUI stopLineDistanceFaultText;
    public TextMeshProUGUI securityDistanceFaultText;
    private List<string> realTimeFaults = new List<string>();

    [Space(10)]
    [Header("Fault Message")]
    public string speedFaultMessage = "Over Speeding";
    public string leftSignalFaultMessage = "Didn't put left signal";
    public string rightSignalFaultMessage = "Didn't put right signal";
    public string leftMirrorFaultMessage = "Didn't check left mirror";
    public string rightMirrorFaultMessage = "Didn't check right mirror";
    public string leftBlindspotFaultMessage = "Didn't check left blindspot";
    public string rightBlindspotFaultMessage = "Didn't check right blindspot";
    public string redLightFaultMessage = "Didn't stop at red light";
    public string stopLineFaultMessage = "Didn't respect stopping distance";
    public string securityDistanceMessage = "Didn't respect security distance";

    void Start()
    {
        rightBlinkerFaultText.text = "";
        leftBlinkerFaultText.text = "";
        rightMirrorFaultText.text = "";
        leftMirrorFaultText.text = "";
        rightBlindspotFaultText.text = "";
        leftBlindspotFaultText.text = "";
        speedFaultText.text = "";
        laneDisciplineFaultText.text = "";
        directionFaultText.text = "";
        redLightFaultText.text = "";
        securityDistanceFaultText.text = "";
        stopLineDistanceFaultText.text = "";
    }

    // Speed Feedback
    public void DisplaySpeedFault(string more = "")
    {
        speedFaultText.text = speedFaultMessage + " " + more;
    }

    public void ClearSpeedFault()
    {
        speedFaultText.text = "";
    }

    // Lane Discipline Feedback
    public void DisplayLaneDisciplineFault(string text, string more = "")
    {
        laneDisciplineFaultText.text = text + " " + more;
    }

    public void clearLaneDisciplineFault()
    {
        laneDisciplineFaultText.text = "";
    }

    // Direction Feedback
    public void DisplayDirectionFault(string text, string more = "")
    {
        directionFaultText.text = text + " " + more;
    }

    public void clearDirectionFault()
    {
        directionFaultText.text = "";
    }

    // Red Light Feedback
    public void DisplayRedLightFault(string more = "")
    {
        redLightFaultText.text = redLightFaultMessage + more;
        StartCoroutine(WaitAndClearRedLightFault(5));
    }

    IEnumerator WaitAndClearRedLightFault(float delay)
    {
        yield return new WaitForSeconds(delay);
        redLightFaultText.text = "";
    }


    // Security Distance Feedback
    public void DisplaySecurityDistanceFault(string more = "")
    {
        securityDistanceFaultText.text = securityDistanceMessage + more;
        StartCoroutine(WaitAndClearSecurityDistanceFault(5));
    }

    IEnumerator WaitAndClearSecurityDistanceFault(float delay)
    {
        yield return new WaitForSeconds(delay);
        securityDistanceFaultText.text = "";
    }

    // Stop Line Distance Feedback
    public void DisplayStopLineFault(string more = "")
    {
        stopLineDistanceFaultText.text = stopLineFaultMessage + more;
        StartCoroutine(WaitAndClearStopLineFault(5));
    }

    IEnumerator WaitAndClearStopLineFault(float delay)
    {
        yield return new WaitForSeconds(delay);
        stopLineDistanceFaultText.text = "";
    }

    // Mirrors Feedback --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void DisplayRightMirrorFault(string more = "")
    {
        rightMirrorFaultText.text = rightMirrorFaultMessage + " " + more;
        StartCoroutine(WaitAndClearRightMirror(5));
    }

    IEnumerator WaitAndClearRightMirror(float delay)
    {
        yield return new WaitForSeconds(delay);
        rightMirrorFaultText.text = "";
    }

    public void DisplayLeftMirrorFault(string more = "")
    {
        leftMirrorFaultText.text = leftMirrorFaultMessage + " " + more;
        StartCoroutine(WaitAndClearLeftMirror(5));
    }

    IEnumerator WaitAndClearLeftMirror(float delay)
    {
        yield return new WaitForSeconds(delay);
        leftMirrorFaultText.text = "";
    }

    // Turn Signals / Blinkers Feedback -----------------------------------------------------------------------------------------------------------------------------------------------------------
    public void DisplayRightBlinkerFault(string more = "")
    {
        rightBlinkerFaultText.text = rightSignalFaultMessage + " " + more;
        StartCoroutine(WaitAndClearRightBlinker(5));
    }

    IEnumerator WaitAndClearRightBlinker(float delay)
    {
        yield return new WaitForSeconds(delay);
        rightBlinkerFaultText.text = "";
    }

    public void DisplayLeftBlinkerFault(string more = "")
    {
        leftBlinkerFaultText.text = leftSignalFaultMessage + " " + more;
        StartCoroutine(WaitAndClearLeftBlinker(5));
    }

    IEnumerator WaitAndClearLeftBlinker(float delay)
    {
        yield return new WaitForSeconds(delay);
        leftBlinkerFaultText.text = "";
    }

    // Blind spot Feedback -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void DisplayRightBlindspotFault(string more = "")
    {
        rightBlindspotFaultText.text = rightBlindspotFaultMessage + " " + more;
        StartCoroutine(WaitAndClearRightBlindspot(5));
    }

    IEnumerator WaitAndClearRightBlindspot(float delay)
    {
        yield return new WaitForSeconds(delay);
        rightBlindspotFaultText.text = "";
    }

    public void DisplayLeftBlindspotFault(string more = "")
    {
        leftBlindspotFaultText.text = leftBlindspotFaultMessage + " " + more;
        StartCoroutine(WaitAndClearLeftBlindspot(5));
    }

    IEnumerator WaitAndClearLeftBlindspot(float delay)
    {
        yield return new WaitForSeconds(delay);
        leftBlindspotFaultText.text = "";
    }
}
