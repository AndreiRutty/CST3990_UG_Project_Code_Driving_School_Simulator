using FCG;
using UnityEngine;

public class CheckOrientation : MonoBehaviour
{
    public float dotThreshold = 0.7f;
    public LaneDirection laneDirection = null;
    public LaneDisciplineChecker laneDisciplineChecker;
    public string direction;
    public bool facingForward;
    public bool facingBackward;
    public bool facingLeft;
    public bool facingRight;
    public bool facingCorrectDirection;

    void Update()
    {
        CheckPlayerDirection();
        CheckCarPositionRelativeToRoad();
    }

    void CheckPlayerDirection()
    {
        Vector3 playerForward = transform.forward;
        Vector3 globalForward = Vector3.forward;
        Vector3 globalRight = Vector3.right;

        float dotProductForward = Vector3.Dot(playerForward.normalized, globalForward.normalized);

        float dotProductRight = Vector3.Dot(playerForward.normalized, globalRight.normalized);

        // Check if player is facing forward
        if (dotProductForward > dotThreshold)
        {
            facingForward = true;
            facingBackward = false;
            facingRight = false;
            facingLeft = false;
        }
        // Check if player is facing backward
        else if (dotProductForward < -dotThreshold)
        {
            facingBackward = true;
            facingForward = false;
            facingRight = false;
            facingLeft = false;
        }
        // Check if player is facing right
        else if (dotProductRight > dotThreshold)
        {
            facingRight = true;
            facingBackward = false;
            facingForward = false;
            facingLeft = false;
        }
        // Check if player is facing left
        else if (dotProductRight < -dotThreshold)
        {
            facingLeft = true;
            facingRight = false;
            facingBackward = false;
            facingForward = false;
        }
    }

    // void CheckCarPositionRelativeToRoad()
    // {

    //     if (laneDisciplineChecker.shouldTurn)
    //     {
    //         facingCorrectDirection = true;
    //     }
    //     else
    //     {
    //         if (direction != "")
    //         {
    //             if (facingForward && direction == "Forward")
    //             {
    //                 facingCorrectDirection = true;
    //             }
    //             else if (facingBackward && direction == "Backward")
    //             {
    //                 facingCorrectDirection = true;
    //             }
    //             else if (facingLeft && direction == "Left")
    //             {
    //                 facingCorrectDirection = true;
    //             }
    //             else if (facingRight && direction == "Right")
    //             {
    //                 facingCorrectDirection = true;
    //             }
    //             else
    //             {
    //                 facingCorrectDirection = false;
    //             }
    //         }
    //         else
    //         {
    //             facingCorrectDirection = false;
    //         }
    //     }
    // }

    void CheckCarPositionRelativeToRoad()
    {

        if (direction != "")
        {
            if (facingForward && direction == "Forward")
            {
                facingCorrectDirection = true;
            }
            else if (facingBackward && direction == "Backward")
            {
                facingCorrectDirection = true;
            }
            else if (facingLeft && direction == "Left")
            {
                facingCorrectDirection = true;
            }
            else if (facingRight && direction == "Right")
            {
                facingCorrectDirection = true;
            }
            else
            {
                facingCorrectDirection = false;
            }
        }
        else
        {
            facingCorrectDirection = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Lane"))
        {
            laneDirection = other.gameObject.GetComponent<LaneDirection>();
            direction = laneDirection.direction.ToString();
        }
    }
}
