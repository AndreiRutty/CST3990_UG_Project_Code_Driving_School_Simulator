using UnityEngine;

public class LaneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool TwoLanes = false;
    public LaneDirection leftLane;
    public LaneDirection leftOvertakingLane = null;
    public LaneDirection rightLane;
    public LaneDirection rightOvertakingLane = null;
    public string direction;

    void Start()
    {
        CheckOrientation();
    }

    void CheckOrientation()
    {
        Vector3 transformForward = transform.forward;
        Vector3 globalForward = Vector3.forward;
        Vector3 globalRight = Vector3.right;

        float dotThreshold = 0.7f;
        float dotProductForward = Vector3.Dot(transformForward.normalized, globalForward.normalized);
        float dotProductRight = Vector3.Dot(transformForward.normalized, globalRight.normalized);

        if (dotProductForward > dotThreshold)
        {
            direction = "Forward";
        }
        else if (dotProductForward < -dotThreshold)
        {
            direction = "Backward";
        }
        else if (dotProductRight > dotThreshold)
        {
            direction = "Right";
        }
        else if (dotProductRight < -dotThreshold)
        {
            direction = "Left";
        }

        if (TwoLanes)
        {
            SetTwoLaneOrientation();
        }
        else
        {
            SetFourLaneOrientation();
        }
    }

    void SetTwoLaneOrientation()
    {
        if (direction == "Forward")
        {
            leftLane.direction = Direction.Forward;
            rightLane.direction = Direction.Backward;
        }
        else if (direction == "Backward")
        {
            rightLane.direction = Direction.Forward;
            leftLane.direction = Direction.Backward;
        }
        else if (direction == "Right")
        {
            leftLane.direction = Direction.Right;
            rightLane.direction = Direction.Left;
        }
        else if (direction == "Left")
        {
            leftLane.direction = Direction.Left;
            rightLane.direction = Direction.Right;
        }
    }

    void SetFourLaneOrientation()
    {
        if (direction == "Forward")
        {
            leftLane.direction = Direction.Forward;
            rightLane.direction = Direction.Backward;
            leftOvertakingLane.direction = Direction.Forward;
            rightOvertakingLane.direction = Direction.Backward;
        }
        else if (direction == "Backward")
        {
            rightLane.direction = Direction.Forward;
            leftLane.direction = Direction.Backward;
            rightOvertakingLane.direction = Direction.Forward;
            leftOvertakingLane.direction = Direction.Backward;
        }
        else if (direction == "Right")
        {
            leftLane.direction = Direction.Right;
            rightLane.direction = Direction.Left;
            leftOvertakingLane.direction = Direction.Right;
            rightOvertakingLane.direction = Direction.Left;
        }
        else if (direction == "Left")
        {
            leftLane.direction = Direction.Left;
            rightLane.direction = Direction.Right;
            leftOvertakingLane.direction = Direction.Left;
            rightOvertakingLane.direction = Direction.Right;
        }
    }
}
