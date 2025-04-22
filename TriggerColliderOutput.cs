using UnityEngine;

public class TriggerColliderOutput : MonoBehaviour
{
    public string collisionTag = "";
    public string collisionExitTag = "";
    public GameObject collisionGameObject;

    private void OnTriggerEnter(Collider other)
    {
        collisionTag = other.gameObject.tag;
        collisionGameObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != collisionTag)
        {
            collisionExitTag = other.gameObject.tag;
        }

    }

}
