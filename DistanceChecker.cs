using UnityEngine;

public class DistanceChecker : MonoBehaviour
{
    public float rayDistance = 10f;
    public SpeedChecker speedChecker;
    public Feedback feedback;
    public FaultCounter faultCounter;
    public float vehicleSecurityDistance = 3f;
    public float distanceToVehicle = 0f;
    public bool isRespectingVechileSecurityDistance;
    public float stopLineCorrectStopDistance = 2f;
    public float distanceToStopLine = 0f;
    public bool isRespectingStopLineDistance;
    private bool hasShownStopLineErrorMessage = false;
    private bool hasShownVehicleErrorMessage = false;
    private bool hasCountStopLineFault = false;
    private bool hasCountSecurityDistanceFault = false;

    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        // Drawing RayCast in Scene View
        Debug.DrawRay(origin, direction * rayDistance, Color.green);
        if (Physics.Raycast(origin, direction, out hit, rayDistance))
        {
            if (hit.collider.gameObject.CompareTag("Stop"))
            {
                distanceToStopLine = Vector3.Distance(hit.collider.gameObject.transform.position, transform.position);
                CheckDistanceToStopLine(distanceToStopLine);
            }

            if (hit.collider.gameObject.CompareTag("VehicleBack"))
            {
                distanceToVehicle = Vector3.Distance(hit.collider.gameObject.transform.position, transform.position);
                CheckDistanceToVehicleInfront(distanceToVehicle);
            }
        }
    }

    void CheckDistanceToStopLine(float distance)
    {
        if (distance > stopLineCorrectStopDistance)
        {
            isRespectingStopLineDistance = true;
            hasShownStopLineErrorMessage = false;
            hasCountStopLineFault = false;
        }
        else
        {
            isRespectingStopLineDistance = false;
            if (!hasShownStopLineErrorMessage)
            {
                if (!hasCountStopLineFault)
                {
                    faultCounter.stopLineDistanceFault++;
                    hasCountStopLineFault = true;
                }
                feedback.DisplayStopLineFault(" X" + faultCounter.stopLineDistanceFault);
                hasShownStopLineErrorMessage = true;
            }
        }
    }

    void CheckDistanceToVehicleInfront(float distance)
    {
        if (distance > vehicleSecurityDistance)
        {
            isRespectingVechileSecurityDistance = true;
            hasShownVehicleErrorMessage = false;
            hasCountSecurityDistanceFault = false;
        }
        else
        {
            isRespectingVechileSecurityDistance = false;
            if (!hasShownVehicleErrorMessage)
            {
                if(!hasCountSecurityDistanceFault)
                {
                    faultCounter.securityDistanceFault++;
                    hasCountSecurityDistanceFault = true;
                }
                feedback.DisplaySecurityDistanceFault(" X" + faultCounter.securityDistanceFault);
                hasShownVehicleErrorMessage = true;
            }
        }
    }
}
