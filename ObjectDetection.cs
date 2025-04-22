using UnityEngine;

public class ObjectDetection : MonoBehaviour
{
    public float rayDistance;
    public GameObject detectedVehicle = null;

    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        Debug.DrawRay(origin, direction * rayDistance, Color.green);
        if (Physics.Raycast(origin, direction, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("IaCar"))
            {
                GameObject aiCar = hit.collider.gameObject;
                

                Vector3 aiCarForward = aiCar.transform.forward;
                Vector3 playerForward = transform.forward;

                float dotProduct = Vector3.Dot(aiCarForward, playerForward);

                if (dotProduct < 0)
                {
                    //Debug.Log("Facing each other");
                }
                else
                {
                    //Debug.Log("NOT Facing each other");
                    detectedVehicle = aiCar;
                }
            }
        }
    }
}
