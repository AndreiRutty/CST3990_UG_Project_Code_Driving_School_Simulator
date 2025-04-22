using UnityEngine;

public class LaneDisciplineChecker : MonoBehaviour
{
    [Header("Modules")]
    public Feedback feedback;
    public FaultCounter faultCounter;
    public TriggerColliderOutput leftTrigger;
    public TriggerColliderOutput rightTrigger;
    public CheckOrientation orientation;

    [Space(10)]
    [Header("Lane Discipline Flags")]
    public bool isRespectingLaneDiscipline = true;
    public bool onMiddleOfTheRoad;
    public bool facingCorrectDirection;
    public bool shouldTurn;
    public bool timerRunning = false;

    [Space(10)]
    [Header("Counting Faults Flags")]
    public bool hasCountMiddleOfRoadFault;
    public bool hasCountMiddleOfTwoLanesFault;
    public bool hasCountWrongDirectionFault;

    [Space(10)]
    [Header("Outputs")]
    public string left;
    public string right;
    public float currentTime;

    void Start()
    {
        isRespectingLaneDiscipline = true;
        onMiddleOfTheRoad = false;
    }

    void Update()
    {
        left = leftTrigger.collisionTag;
        right = rightTrigger.collisionTag;
        CheckLaneDiscipline();
        facingCorrectDirection = orientation.facingCorrectDirection;
        DisplayFaultMessage();
    }

    // Function to check if 
    void CheckLaneDiscipline()
    {
        if (!facingCorrectDirection)
        {
            isRespectingLaneDiscipline = false;
        }

        // Normal Lanes
        if ((left == "Lane1" && right == "Lane1" && facingCorrectDirection) || (left == "Lane2" && right == "Lane2" && facingCorrectDirection))
        {
            isRespectingLaneDiscipline = true;
            onMiddleOfTheRoad = false;
            shouldTurn = false;
        }
        else if ((left == "Lane1" && right == "Lane1" && !facingCorrectDirection) || (left == "Lane2" && right == "Lane2" && !facingCorrectDirection))
        {
            isRespectingLaneDiscipline = false;
            onMiddleOfTheRoad = false;
            shouldTurn = false;
        }
        else if (left == "Lane1" && right == "Lane2" || left == "Lane2" && right == "Lane1")
        {
            onMiddleOfTheRoad = true;
            isRespectingLaneDiscipline = false;
            shouldTurn = false;
        }

        // Overtaking Lanes
        if ((left == "OvertakingLane1" && right == "OvertakingLane1" && facingCorrectDirection) || (left == "OvertakingLane2" && right == "OvertakingLane2" && facingCorrectDirection))
        {
            isRespectingLaneDiscipline = true;
            onMiddleOfTheRoad = false;
            shouldTurn = false;
        }
        else if ((left == "OvertakingLane1" && right == "OvertakingLane1" && !facingCorrectDirection) || (left == "OvertakingLane2" && right == "OvertakingLane2" && !facingCorrectDirection))
        {
            isRespectingLaneDiscipline = false;
            onMiddleOfTheRoad = false;
            shouldTurn = false;
        }

        // Checking if player is in the middle of the 4 lanes
        if (left == "OvertakingLane1" && right == "OvertakingLane2" || left == "OvertakingLane2" && right == "OvertakingLane1")
        {
            onMiddleOfTheRoad = true;
            isRespectingLaneDiscipline = false;
            shouldTurn = false;
        }

        if (
            (left == "Lane1" && right == "OvertakingLane1" && facingCorrectDirection) ||
            (left == "Lane2" && right == "OvertakingLane2" && facingCorrectDirection) ||
            (right == "Lane1" && left == "OvertakingLane1" && facingCorrectDirection) ||
            (right == "Lane2" && left == "OvertakingLane2" && facingCorrectDirection))
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            ResetTimer();
            feedback.clearLaneDisciplineFault();
        }

        if (left == "LH" || right == "LH" || left == "RH" || right == "LH")
        {
            shouldTurn = true;
        }
    }

    void DisplayFaultMessage()
    {
        if (!facingCorrectDirection && !onMiddleOfTheRoad)
        {
            if (!hasCountWrongDirectionFault)
            {
                faultCounter.directionFault++;
                hasCountWrongDirectionFault = true;
            }
            feedback.DisplayDirectionFault("Not facing correct direction ", "X" + faultCounter.directionFault);
        }
        else
        {
            feedback.clearDirectionFault();
            hasCountWrongDirectionFault = false;
        }

        if (onMiddleOfTheRoad)
        {
            if (!hasCountMiddleOfRoadFault)
            {
                faultCounter.onMiddleOfRoadFault++;
                hasCountMiddleOfRoadFault = true;
            }
            feedback.DisplayLaneDisciplineFault("You're on the middle of the road ", "X" + faultCounter.onMiddleOfRoadFault);
        }
        else
        {
            feedback.clearLaneDisciplineFault();
            hasCountMiddleOfRoadFault = false;
        }

        if (currentTime > 3.0f)
        {
            if(!hasCountMiddleOfTwoLanesFault)
            {
                faultCounter.onMiddleOfTwoLanesFault++;
                hasCountMiddleOfTwoLanesFault = true;
            }
            feedback.DisplayLaneDisciplineFault("You're on the middle of the 2 lanes ", "X" + faultCounter.onMiddleOfTwoLanesFault);
        }else
        {
            hasCountMiddleOfTwoLanesFault = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TurnDirection>())
        {
            shouldTurn = true;
        }
    }

    void ResetTimer()
    {
        currentTime = 0f;
    }
}
