using UnityEngine;

public class TurnLaneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public LaneDirection[] leftLanes = null;
    public LaneDirection[] rightLanes = null;

    public string direction;

    void Start()
    {
        foreach (LaneDirection lane in leftLanes)
        {
            CheckOrientation(lane, "Left");
        }

        foreach (LaneDirection lane in rightLanes)
        {
            CheckOrientation(lane, "Right");
        }
    }

    void CheckOrientation(LaneDirection lane, string laneName)
    {
        Vector3 transformForward = lane.gameObject.transform.forward;
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

        SetLaneOrientation(lane, laneName);
    }

    void SetLaneOrientation(LaneDirection lane, string laneName)
    {
        if (direction == "Forward")
        {
            if (laneName == "Left")
            {
                lane.direction = Direction.Forward;
            }
            else
            {
                lane.direction = Direction.Backward;
            }

        }
        else if (direction == "Backward")
        {
            if (laneName == "Left")
            {
                lane.direction = Direction.Backward;
            }
            else
            {
                lane.direction = Direction.Forward;
            }
        }
        else if (direction == "Right")
        {
            if (laneName == "Left")
            {
                lane.direction = Direction.Right;
            }
            else
            {
                lane.direction = Direction.Left;
            }
        }
        else if (direction == "Left")
        {
            if (laneName == "Left")
            {
                lane.direction = Direction.Left;
            }
            else
            {
                lane.direction = Direction.Right;
            }
        }
    }
}
