using FCG;
using UnityEngine;

public class LaneSetUp : MonoBehaviour
{
    public TrafficSystem ts;
    public RunTimeSample runTimeSample;
    public GameObject[] laneTags = null;
    public GameObject[] RH = null;
    public GameObject[] LH = null;
    public int trafficHand;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartActivateLaneSetting()
    {
        trafficHand = ts.trafficLightHand;
        laneTags = GameObject.FindGameObjectsWithTag("LaneTag");
        RH = GameObject.FindGameObjectsWithTag("RH");
        LH = GameObject.FindGameObjectsWithTag("LH");
        ActivateCorrectLaneSetting();
    }

    void ActivateCorrectLaneSetting()
    {
        Debug.Log(runTimeSample.rightHand);
        if (runTimeSample.rightHand)
        {
            foreach (GameObject rh in RH)
            {
                rh.SetActive(true);
            }

            foreach (GameObject lh in LH)
            {
                lh.SetActive(false);
            }

            // // Lane Tags
            foreach (GameObject laneTag in laneTags)
            {
                Quaternion currentRotation = laneTag.transform.rotation;

                // Set z rotation to be 180
                laneTag.transform.rotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, 180);
            }

        }
        else
        {
            foreach (GameObject rh in RH)
            {
                rh.SetActive(false);
            }

            foreach (GameObject lh in LH)
            {
                lh.SetActive(true);
            }

            // Lane Tags
            foreach (GameObject laneTag in laneTags)
            {
                Quaternion currentRotation = laneTag.transform.rotation;

                // Set z rotation to be 0
                laneTag.transform.rotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, 0);
            }
        }
    }
}
