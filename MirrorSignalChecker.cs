using System.Collections;
using System.Threading;
using FCG;
using UnityEngine;

public class MirrorSignalChecker : MonoBehaviour
{
    [Header("Modules")]
    public Feedback feedback;
    public TrafficSystem ts;
    public LaneDisciplineChecker laneDisciplineChecker;
    public MirrorSignalManoeuvre mirrorSignal;
    public CheckOrientation orientation;

    [Space(10)]
    [Header("Flags")]
    //private bool onOvertaking = false;
    //private bool onNormalLane = true;
    public bool displayRightBlinkerFault = false;
    public bool displayLeftBlinkerFault = false;
    public bool displayRightMirrorFault = false;
    public bool displayLeftMirrorFault = false;
    public bool displayRightBlindspotFault = false;
    public bool displayLeftBlindspotFault = false;

    [Space(10)]
    [Header("Traffic Related Variables")]
    public string currentLaneTag = "";
    public string lastLaneTag = "";
    private int trafficHand;
    public GameObject turnPoint = null;

    [Space(10)]
    [Header("Fault Count")]
    public int countRightSignal = 0;
    public int countLeftSignal = 0;
    public int countRightMirror = 0;
    public int countLeftMirror = 0;
    public int countRightBlindspot = 0;
    public int countLeftBlindspot = 0;

    void Start()
    {
        trafficHand = ts.trafficLightHand;
    }

    void FixedUpdate()
    {
        LaneChangeManoeuvre();
        TurningManoeuvre();
    }
    private void OnTriggerEnter(Collider other)
    {
        // Checking if player is respecting lane discipline rule
        if (laneDisciplineChecker.isRespectingLaneDiscipline)
        {
            if (other.gameObject.CompareTag("OvertakingLane1"))
            {
                //onOvertaking = true;
                currentLaneTag = "OvertakingLane1";
                ResetParameters();
            }

            if (other.gameObject.CompareTag("OvertakingLane2"))
            {
                //onOvertaking = true;
                currentLaneTag = "OvertakingLane2";
                ResetParameters();
            }

            if (other.gameObject.CompareTag("Lane1"))
            {
                //onNormalLane = true;
                currentLaneTag = "Lane1";
                ResetParameters();
            }

            if (other.gameObject.CompareTag("Lane2"))
            {
                //onNormalLane = true;
                currentLaneTag = "Lane2";
                ResetParameters();
            }

        }

        // Turning Point
        if (other.gameObject.tag == "TurningPointReceiver")
        {
            turnPoint = other.gameObject;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (laneDisciplineChecker.isRespectingLaneDiscipline)
        {
            if (other.gameObject.CompareTag("Lane1"))
            {
                lastLaneTag = "Lane1";
                //onNormalLane = false;
                //ResetParameters();
            }

            if (other.gameObject.CompareTag("Lane2"))
            {
                lastLaneTag = "Lane2";
                //onNormalLane = false;
                ///ResetParameters();
            }

            if (other.gameObject.CompareTag("OvertakingLane1"))
            {
                lastLaneTag = "OvertakingLane1";
                //onOvertaking = false;
                //ResetParameters();
            }

            if (other.gameObject.CompareTag("OvertakingLane2"))
            {
                lastLaneTag = "OvertakingLane2";
                //onOvertaking = false;
                //ResetParameters();
            }
        }

        // Turning Point
        if (other.gameObject.tag == "TurningPointReceiver")
        {
            turnPoint = null;
        }
    }
    void TurningManoeuvre()
    {
        if (turnPoint != null)
        {
            TurnDirection turnDirection;
            if (turnPoint.TryGetComponent(out turnDirection))
            {
                string direction = turnDirection.turnDirection.ToString();
                CheckTurning(direction);
            }
        }
    }
    void LaneChangeManoeuvre()
    {
        if (trafficHand != 0)
        {
            if (currentLaneTag == "OvertakingLane1" && lastLaneTag == "Lane1")
            {
                CheckRightLaneChange();
            }
            else if (currentLaneTag == "Lane1" && lastLaneTag == "OvertakingLane1")
            {
                CheckLeftLaneChange();
            }

            if (currentLaneTag == "OvertakingLane2" && lastLaneTag == "Lane2")
            {
                //CheckLaneChange("Right");
            }
            else if (currentLaneTag == "Lane2" && lastLaneTag == "OvertakingLane2")
            {
                //CheckLaneChange("Left");
            }
        }
        else
        {
            // if (onOvertaking && lastLaneTag == "Lane1")
            // {
            //     CheckLaneChange("Left");
            // }
            // else if (onNormalLane && lastLaneTag == "OvertakingLane1")
            // {
            //     CheckLaneChange("Right");
            // }

            // if (onOvertaking && lastLaneTag == "Lane2")
            // {
            //     CheckLaneChange("Left");
            // }
            // else if (onNormalLane && lastLaneTag == "OvertakingLane2")
            // {
            //     CheckLaneChange("Right");
            // }
        }

    }

    void CheckTurning(string direction)
    {
        //CheckMirrors(direction);
        //CheckBlinker(direction);
    }

    void CheckRightLaneChange()
    {
        //StartCoroutine(ResetLastLaneTag());
        CheckRightBlinker();
    }

    void CheckLeftLaneChange()
    {
        //StartCoroutine(ResetLastLaneTag());
        CheckLeftBlinker();
    }

    // Function to check if player has put signal
    void CheckRightBlinker()
    {
        if (!mirrorSignal.isRightBlinkerOn)
        {
            if (!displayRightBlinkerFault)
            {
                // countRightSignal++;
                // feedback.DisplayRightBlinkerFault("X" + countRightSignal);
                // displayRightBlinkerFault = true;
            }

        }
    }

    void CheckLeftBlinker()
    {
        if (!mirrorSignal.isLeftBlinkerOn)
        {
            if (!displayLeftBlinkerFault)
            {
                // countLeftSignal++;
                // feedback.DisplayLeftBlinkerFault("X" + countLeftSignal);
                // displayLeftBlinkerFault = true;
            }
        }
        ;
    }

    // Function to check if player has check mirrors
    void CheckMirrors(string direction)
    {
        if (direction == "Right")
        {
            // if (!mirrorSignal.hasCheckedRightMirror && !displayRightMirrorFault)
            // {
            //     displayRightMirrorFault = true;
            //     countRightMirror++;
            //     feedback.DisplayRightMirrorFault("X" + countRightMirror);
            // }
        }
        else if (direction == "Left")
        {
            // if (!mirrorSignal.hasCheckedLeftMirror && !displayLeftMirrorFault)
            // {
            //     displayLeftMirrorFault = true;
            //     countLeftMirror++;
            //     feedback.DisplayLeftMirrorFault("X" + countLeftMirror);
            // }
        }
    }

    // Function to check if player has check blindspot
    void CheckBlindspot(string direction)
    {
        if (direction == "Right")
        {
            // if (!mirrorSignal.hasCheckedRightBlindspot && !displayRightBlindspotFault)
            // {
            //     displayRightBlindspotFault = true;
            //     countRightBlindspot++;
            //     feedback.DisplayRightBlindspotFault("X" + countRightBlindspot);
            // }
        }
        else if (direction == "Left")
        {
            // if (!mirrorSignal.hasCheckedLeftBlindspot && !displayLeftBlindspotFault)
            // {
            //     displayLeftBlindspotFault = true;
            //     countLeftBlindspot++;
            //     feedback.DisplayLeftBlindspotFault("X" + countRightBlindspot);
            // }
        }
    }

    // Function to reset mirror, signal and blindspot error message flags
    void ResetParameters()
    {
        mirrorSignal.hasCheckedLeftMirror = false;
        mirrorSignal.hasCheckedRightMirror = false;
        mirrorSignal.hasCheckedLeftBlindspot = false;
        mirrorSignal.hasCheckedRightBlindspot = false;

        displayRightBlinkerFault = false;
        displayLeftBlinkerFault = false;

        displayRightMirrorFault = false;
        displayLeftMirrorFault = false;

        displayRightBlindspotFault = false;
        displayLeftBlindspotFault = false;
    }

    IEnumerator ResetLastLaneTag()
    {
        yield return new WaitForSeconds(1f);
        lastLaneTag = "";
    }

}
