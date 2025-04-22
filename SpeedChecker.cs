using UnityEngine;
using NWH.VehiclePhysics2;
public class SpeedChecker : MonoBehaviour
{
    [Header("Modules")]
    public Feedback feedback;
    public VehicleController vc;
    public FaultCounter faultCounter;

    [Space(10)]
    [Header("Speed Checker Settings")]
    public bool isOverSpeeding = false;
    private float currentSpeed;
    public float speedLimit;
    private bool canIncrement = true;

    void Update()
    {
        CheckCurrentSpeed();
    }

    void CheckCurrentSpeed()
    {
        // Converting vehicle's speed from m/s to km/h
        currentSpeed = vc.Speed * 3.6f;

        // Checking if current speed is greater than speed limit
        if (currentSpeed > speedLimit)
        {
            isOverSpeeding = true;
        }
        else
        {
            isOverSpeeding = false;
        }

        // Displaying a message when overspeeding
        if (isOverSpeeding)
        {
            // Increment Fault Count Once
            if (canIncrement)
            {
                faultCounter.overSpeedingCount++;

                // Setting the increment flag to false to prevent continous increment
                canIncrement = false;
            }

            // Displaying fault and its count through the feedback system
            string faultCountText = "X" + faultCounter.overSpeedingCount;
            feedback.DisplaySpeedFault(faultCountText);
        }
        else
        {
            // Clearing the fault message when not over speeding
            feedback.ClearSpeedFault();

            // resetting the increment flag
            canIncrement = true;
        }
    }
}
