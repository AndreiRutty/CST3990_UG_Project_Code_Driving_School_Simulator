using Unity.VisualScripting;
using UnityEngine;

public class TurnPointReceiver : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        // if (other.gameObject.tag == "Player")
        // {
        //     MirrorSignalChecker mirrorSignalChecker = other.gameObject.GetComponent<MirrorSignalChecker>();
        // }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
        }
    }
}
