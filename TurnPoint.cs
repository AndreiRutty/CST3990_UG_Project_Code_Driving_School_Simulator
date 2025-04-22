using UnityEngine;

public class TurnPoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Feedback feedback;
    public GameObject receiver;
    public GameObject player = null;
    public GameObject wrongTurn = null;
    void Start()
    {
        //feedback = GameObject.FindGameObjectWithTag("Feedback").GetComponent<Feedback>();
        receiver.SetActive(false);
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            // Debug.Log(distance);

            if (distance > 35)
            {
                receiver.SetActive(false);
                if (wrongTurn != null)
                {
                    wrongTurn.SetActive(false);
                }

                player = null;
            }

            if (wrongTurn != null && !wrongTurn.activeSelf && receiver.activeSelf)
            {
                Debug.Log("Wrong Turn");
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            receiver.SetActive(true);
            player = other.gameObject;
            if (wrongTurn)
            {
                wrongTurn.SetActive(true);
            }
        }
    }
}
