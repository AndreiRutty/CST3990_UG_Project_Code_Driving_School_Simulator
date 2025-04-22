using System.Collections;
using UnityEngine;
using FCG;

public class LaneChangeChecker : MonoBehaviour
{
    public Feedback feedback;
    public MirrorSignalManoeuvre mirrorSignalManoeuvre;
    public TrafficSystem trafficSystem;
    public LaneDisciplineChecker laneDisciplineChecker;
    public FaultCounter faultCounter;
    public RunTimeSample runTimeSample;
    public string currentLane;
    public string previousLane;
    public bool onNormalLane;
    public bool onBothLane;
    public bool onOvertakingLane;
    public TriggerColliderOutput leftCollider;
    public TriggerColliderOutput rightCollider;
    public string leftTag;
    public string rightTag;
    public string checkDirection;

    // flags for allowing to check mirror, signal, blindspot
    [Space(10)]
    public bool hasCheckLeft;
    public bool hasCheckRight;

    [Space(10)]
    // flags for displaying messages
    public bool hasDisplayRightSignalMessage;
    public bool hasDisplayLeftSignalMessage;
    public bool hasDisplayRightMirrorMessage;
    public bool hasDisplayLeftMirrorMessage;
    public bool hasDisplayRightBlindspotMessage;
    public bool hasDisplayLeftBlindspotMessage;


    [Space(10)]
    // flags for counting message
    public bool hasCountRightSignalError;
    public bool hasCountLeftSignalError;
    public bool hasCountRightMirrorError;
    public bool hasCountLeftMirrorError;
    public bool hasCountRightBlindspotError;
    public bool hasCountLeftBlindspotError;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (laneDisciplineChecker.isRespectingLaneDiscipline)
        {
            CheckLaneName();
            CheckOnBothLane();
            CheckLaneChange();
        }
    }

    void CheckOnBothLane()
    {
        onBothLane = IsOnBothLane();
        if (onBothLane)
        {
            onNormalLane = false;
            onOvertakingLane = false;
        }
    }

    void CheckLaneChange()
    {
        if (onNormalLane)
        {
            if (!runTimeSample.rightHand)
            {
                CheckLeftManoeuvre();
            }
            else
            {
                CheckRightManoeuvre();
            }

        }
        else if (onOvertakingLane)
        {
            if (!runTimeSample.rightHand)
            {
                CheckRightManoeuvre();
            }
            else
            {
                CheckLeftManoeuvre();
            }
        }
    }

    void CheckLeftManoeuvre()
    {
        if (!hasCheckLeft)
        {
            CheckLeftMirror();
            CheckLeftBlindspot();
            CheckLeftSignal();
            if (hasCheckRight)
            {
                hasCheckRight = false;
            }

            hasCheckLeft = true;
        }
    }

    void CheckRightManoeuvre()
    {
        if (!hasCheckRight)
        {
            CheckRightMirror();
            CheckRightBlindspot();
            CheckRightSignal();
            if (hasCheckLeft)
            {
                hasCheckLeft = false;
            }

            hasCheckRight = true;
        }
    }

    void CheckRightMirror()
    {
        if (!mirrorSignalManoeuvre.hasCheckedRightMirror && !hasDisplayRightMirrorMessage)
        {
            if (!hasCountRightMirrorError)
            {
                faultCounter.rightMirrorCount++;
                hasCountRightMirrorError = true;
            }
            feedback.DisplayRightMirrorFault("X" + faultCounter.rightMirrorCount);
            hasDisplayRightMirrorMessage = true;
        }

        if (hasDisplayLeftMirrorMessage)
        {
            hasDisplayLeftMirrorMessage = false;
        }

        if (hasCountLeftMirrorError)
        {
            hasCountLeftMirrorError = false;
        }

        if (mirrorSignalManoeuvre.hasCheckedLeftMirror)
        {
            mirrorSignalManoeuvre.hasCheckedLeftMirror = false;
        }
    }

    void CheckLeftMirror()
    {
        if (!mirrorSignalManoeuvre.hasCheckedLeftMirror && !hasDisplayLeftMirrorMessage)
        {
            if (!hasCountLeftMirrorError)
            {
                faultCounter.leftMirrorCount++;
                hasCountLeftMirrorError = true;
            }
            feedback.DisplayLeftMirrorFault("X" + faultCounter.leftMirrorCount);
            hasDisplayLeftMirrorMessage = true;
        }

        if (hasDisplayRightMirrorMessage)
        {
            hasDisplayRightMirrorMessage = false;
        }

        if (hasCountRightMirrorError)
        {
            hasCountRightMirrorError = false;
        }

        if (mirrorSignalManoeuvre.hasCheckedRightMirror)
        {
            mirrorSignalManoeuvre.hasCheckedRightMirror = false;
        }
    }

    void CheckRightBlindspot()
    {
        if (!mirrorSignalManoeuvre.hasCheckedRightBlindspot && !hasDisplayRightBlindspotMessage)
        {
            if (!hasCountRightBlindspotError)
            {
                faultCounter.rightBlindspotCount++;
                hasCountRightBlindspotError = true;
            }
            feedback.DisplayRightBlindspotFault("X" + faultCounter.rightBlindspotCount);
            hasDisplayRightBlindspotMessage = true;
        }

        if (hasDisplayLeftBlindspotMessage)
        {
            hasDisplayLeftBlindspotMessage = false;
        }

        if (hasCountLeftBlindspotError)
        {
            hasCountLeftBlindspotError = false;
        }

        if (mirrorSignalManoeuvre.hasCheckedLeftBlindspot)
        {
            mirrorSignalManoeuvre.hasCheckedLeftBlindspot = false;
        }
    }

    void CheckLeftBlindspot()
    {
        if (!mirrorSignalManoeuvre.hasCheckedLeftBlindspot && !hasDisplayLeftBlindspotMessage)
        {
            if (!hasCountLeftBlindspotError)
            {
                faultCounter.leftBlindspotCount++;
                hasCountLeftBlindspotError = true;
            }
            feedback.DisplayLeftBlindspotFault("X" + faultCounter.leftBlindspotCount);
            hasDisplayLeftBlindspotMessage = true;
        }

        if (hasDisplayRightBlindspotMessage)
        {
            hasDisplayRightBlindspotMessage = false;
        }

        if (hasCountRightBlindspotError)
        {
            hasCountRightBlindspotError = false;
        }

        if (mirrorSignalManoeuvre.hasCheckedRightBlindspot)
        {
            mirrorSignalManoeuvre.hasCheckedRightBlindspot = false;
        }
    }

    void CheckRightSignal()
    {
        if (!mirrorSignalManoeuvre.isRightBlinkerOn && !hasDisplayRightSignalMessage)
        {
            if (!hasCountRightSignalError)
            {
                faultCounter.rightSignalCount++;
                hasCountRightSignalError = true;
            }
            feedback.DisplayRightBlinkerFault("X" + faultCounter.rightSignalCount);
            hasDisplayRightSignalMessage = true;
        }

        if (hasDisplayLeftSignalMessage)
        {
            hasDisplayLeftSignalMessage = false;
        }

        if (hasCountLeftSignalError)
        {
            hasCountLeftSignalError = false;
        }
    }

    void CheckLeftSignal()
    {
        if (!mirrorSignalManoeuvre.isLeftBlinkerOn && !hasDisplayLeftSignalMessage)
        {
            if (!hasCountLeftSignalError)
            {
                faultCounter.leftSignalCount++;
                hasCountLeftSignalError = true;
            }
            feedback.DisplayLeftBlinkerFault("X" + faultCounter.leftSignalCount);
            hasDisplayLeftSignalMessage = true;
        }

        if (hasDisplayRightSignalMessage)
        {
            hasDisplayRightSignalMessage = false;
        }

        if (hasCountRightSignalError)
        {
            hasCountRightSignalError = false;
        }
    }

    void CheckLaneName()
    {
        onNormalLane = IsOnNormalLaneSide() && IsOnNormalLane();
        onOvertakingLane = IsOnOvertakingLaneSide() && IsOnOvertakingLane();

        if (previousLane == "")
        {
            onNormalLane = false;
            onOvertakingLane = false;
        }

        if (OnLane(leftCollider.collisionTag))
        {
            leftTag = leftCollider.collisionTag;
        }

        if (OnLane(rightCollider.collisionTag))
        {
            rightTag = rightCollider.collisionTag;
        }
    }

    bool OnLane(string tag)
    {
        return tag == "Lane1" || tag == "OvertakingLane1" || tag == "OvertakingLane2" || tag == "Lane2";
    }

    bool IsOnNormalLane()
    {
        return currentLane == "Lane1" && previousLane == "OvertakingLane1" || currentLane == "Lane2" && previousLane == "OvertakingLane2";
    }

    bool IsOnNormalLaneSide()
    {
        return leftTag == "Lane1" && rightTag == "Lane1" || leftTag == "Lane2" && rightTag == "Lane2";
    }

    bool IsOnOvertakingLane()
    {
        return currentLane == "OvertakingLane1" && previousLane == "Lane1" || currentLane == "OvertakingLane2" && previousLane == "Lane2";
    }

    bool IsOnOvertakingLaneSide()
    {
        return leftTag == "OvertakingLane1" && rightTag == "OvertakingLane1" || leftTag == "OvertakingLane2" && rightTag == "OvertakingLane2";
    }

    bool IsOnBothLane()
    {
        return leftTag == "Lane1" && rightTag == "OvertakingLane1" || leftTag == "Lane1" && rightTag == "OvertakingLane1";
    }

    void OnTriggerEnter(Collider other)
    {
        if (OnLane(other.tag))
        {
            currentLane = other.tag;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (OnLane(other.tag) && !other.CompareTag(currentLane))
        {
            previousLane = other.tag;
        }
    }
}
