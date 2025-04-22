using UnityEngine;

public class FaultCounter : MonoBehaviour
{
    [Header("Speeding Fault")]
    public int overSpeedingCount = 0;

    [Space(5)]
    [Header("Signal Fault")]
    public int rightSignalCount = 0;
    public int leftSignalCount = 0;

    [Space(5)]
    [Header("Mirror Fault")]
    public int rightMirrorCount = 0;
    public int leftMirrorCount = 0;

    [Space(5)]
    [Header("Blindspot Fault")]
    public int rightBlindspotCount = 0;
    public int leftBlindspotCount = 0;

    [Space(5)]
    [Header("Lane Discipline Fault")]
    public int onMiddleOfRoadFault = 0;
    public int onMiddleOfTwoLanesFault = 0;
    public int directionFault = 0;

    [Space(5)]
    [Header("Security Distance Fault")]
    public int securityDistanceFault = 0;
    public int stopLineDistanceFault = 0;

    [Space(5)]
    [Header("Traffic Light Violation")]
    public int redLightFault = 0;

    public int totalFaultCount;
    void Start()
    {
        ResetFaultsCounter();
    }

    void Update()
    {
        totalFaultCount = overSpeedingCount + rightSignalCount + leftSignalCount +
                            rightMirrorCount + leftMirrorCount + rightBlindspotCount + leftBlindspotCount +
                            onMiddleOfRoadFault + onMiddleOfTwoLanesFault + directionFault + securityDistanceFault + stopLineDistanceFault +
                            redLightFault;
    }


    public void ResetFaultsCounter()
    {
        totalFaultCount = 0;
        overSpeedingCount = 0;
        rightSignalCount = 0;
        leftSignalCount = 0;
        rightMirrorCount = 0;
        leftMirrorCount = 0;
        rightBlindspotCount = 0;
        leftBlindspotCount = 0;
        onMiddleOfRoadFault = 0;
        onMiddleOfTwoLanesFault = 0;
        directionFault = 0;
        securityDistanceFault = 0;
        stopLineDistanceFault = 0;
        redLightFault = 0;
    }
}
