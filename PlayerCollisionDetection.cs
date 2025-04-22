using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject gameOverPanel;

    void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("Collide with " + collision.gameObject.tag);

        if (collision.gameObject.tag == "untagged" || collision.gameObject.tag == "IaCar")
        {
            if (gameManager.testMode)
            {
                gameOverPanel.SetActive(true);
                Time.timeScale = 0f;
            }

        }
    }
}
