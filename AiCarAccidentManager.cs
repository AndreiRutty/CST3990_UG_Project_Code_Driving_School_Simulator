using FCG;
using UnityEngine;

public class AiCarAccidentManager : MonoBehaviour
{
    private Rigidbody myRigidbody;
    public int currentSpeed = 0;
    public bool isMoving = false;
    public bool hasCollide = false;
    public float timeStopped;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        currentSpeed = (int)myRigidbody.linearVelocity.magnitude;
        isMoving = currentSpeed > 0;

        if (!isMoving && hasCollide)
        {
            timeStopped += Time.deltaTime;
        }
        else
        {
            timeStopped = 0;
        }

        if (timeStopped > 5)
        {
            Destroy(this.gameObject);
        }

    }

    // Handle collision events
    void OnCollisionEnter(Collision collision)
    {
        hasCollide = true;
    }


    void OnCollisionExit(Collision collision)
    {
        hasCollide = false;
    }
}
