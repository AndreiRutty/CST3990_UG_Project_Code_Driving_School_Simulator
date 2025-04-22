using UnityEngine;

public class RedLightViolationChecker : MonoBehaviour
{
    public Feedback feedback;
    public FaultCounter faultCounter;
    public bool hasViolatedRedLight = false;

    public bool canDisplayMessage = false;
    public bool hasDisplayedMessage = false;

    void Update()
    {
        // Checking if the system can display feedback for red light violation 
        if (canDisplayMessage)
        {
            // Checking if driver didn't stop at red light and the system hasn't display a feedback message
            if (hasViolatedRedLight && !hasDisplayedMessage)
            {
                // Increment red light fault count
                faultCounter.redLightFault++;

                // Calling feedback system to display feedback message
                feedback.DisplayRedLightFault("X" + faultCounter.redLightFault);

                // Setting the displayMessage flag to true;
                hasDisplayedMessage = true;
            }

            // Setting the canDisplayMessage flag to false to prevent continous message display;
            canDisplayMessage = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Checking for trigger collision with the stop game object
        if (other.gameObject.CompareTag("Stop"))
        {
            // set red light violation flag to true
            hasViolatedRedLight = true;

            // setting the can display message flag to true to allow the system to display feedback messgae
            canDisplayMessage = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        // Checking for trigger collision exit with the stop game object
        if (other.gameObject.CompareTag("Stop"))
        {
            // Resetting flags for the next check
            hasViolatedRedLight = false;
            hasDisplayedMessage = false;
        }
    }
}
