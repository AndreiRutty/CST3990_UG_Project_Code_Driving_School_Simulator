using FCG;
using UnityEngine;

public class TurnChecker : MonoBehaviour
{
    public MirrorSignalManoeuvre mirrorSignalManoeuvre;
    public Feedback feedback;
    public FaultCounter faultCounter;
    public string turnDirection = "";

    [Space(10)]

    // Left Mirror and Signal Flags
    public bool canCheckLeft;
    public bool hasDisplayLeftMirrorMessage;
    public bool hasCountLeftMirrorMessage;
    public bool hasDisplayLeftSignalMessage;
    public bool hasCountLeftSignalMessage;

    // Right Mirror and Signal Flags
    public bool canCheckRight;
    public bool hasDisplayRightMirrorMessage;
    public bool hasCountRightMirrorMessage;
    public bool hasDisplayRightSignalMessage;
    public bool hasCountRightSignalMessage;

    // Update is called once per frame
    void Update()
    {
        if (turnDirection == "Left")
        {
            CheckLeftTurnManoeuvre();
        }
        else if (turnDirection == "Right")
        {
            CheckRightTurnManoeuvre();
        }
    }

    void CheckLeftTurnManoeuvre()
    {
        // Checking if system can check correct turn procedure
        if (canCheckLeft)
        {   
            // Checking user has check mirrors
            if (!mirrorSignalManoeuvre.hasCheckedLeftMirror && !hasDisplayLeftMirrorMessage)
            {
                if (!hasCountLeftMirrorMessage)
                {
                    faultCounter.leftMirrorCount++;
                    hasCountLeftMirrorMessage = true;
                }

                feedback.DisplayLeftMirrorFault("X" + faultCounter.leftMirrorCount);
                hasDisplayLeftMirrorMessage = true;
            }
            
            // Checking user has check mirrors
            if (!mirrorSignalManoeuvre.isLeftBlinkerOn && !hasDisplayLeftSignalMessage)
            {
                if (!hasCountLeftSignalMessage)
                {
                    faultCounter.leftSignalCount++;
                    hasCountLeftSignalMessage = true;
                }

                feedback.DisplayLeftBlinkerFault("X" + faultCounter.leftSignalCount);
                hasDisplayLeftSignalMessage = true;
            }

            canCheckLeft = false;
        }
    }

    void CheckRightTurnManoeuvre()
    {
        if (canCheckRight)
        {
            if (!mirrorSignalManoeuvre.hasCheckedRightMirror && !hasDisplayRightMirrorMessage)
            {
                if (!hasCountRightMirrorMessage)
                {
                    faultCounter.rightMirrorCount++;
                    hasCountRightMirrorMessage = true;
                }

                feedback.DisplayRightMirrorFault("X" + faultCounter.rightMirrorCount);
                hasDisplayRightMirrorMessage = true;
            }

            if (!mirrorSignalManoeuvre.isRightBlinkerOn && !hasDisplayRightSignalMessage)
            {
                if (!hasCountRightSignalMessage)
                {
                    faultCounter.rightSignalCount++;
                    hasCountRightSignalMessage = true;
                }

                feedback.DisplayRightBlinkerFault("X" + faultCounter.rightSignalCount);
                hasDisplayRightSignalMessage = true;
            }

            canCheckRight = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TurningPointReceiver"))
        {
            TurnDirection direction = other.gameObject.GetComponent<TurnDirection>();
            turnDirection = direction.turnDirection.ToString();

            if (turnDirection == "Left")
            {
                canCheckLeft = true;
            }
            else
            {
                canCheckRight = true;
            }

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TurningPointReceiver"))
        {
            // Left Mirror and Signal Flags Reset
            hasCountLeftMirrorMessage = false;
            hasDisplayLeftMirrorMessage = false;
            mirrorSignalManoeuvre.hasCheckedLeftMirror = false;
            hasCountLeftSignalMessage = false;
            hasDisplayLeftSignalMessage = false;
            mirrorSignalManoeuvre.isLeftBlinkerOn = false;

            // Right Mirror and Signal Flags Reset
            hasCountRightMirrorMessage = false;
            hasDisplayRightMirrorMessage = false;
            mirrorSignalManoeuvre.hasCheckedRightMirror = false;
            hasCountRightSignalMessage = false;
            hasDisplayRightSignalMessage = false;
            mirrorSignalManoeuvre.isRightBlinkerOn = false;
        }
    }
}
