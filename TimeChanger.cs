using TMPro;
using UnityEngine;

public class TimeChanger : MonoBehaviour
{
    public DayNight dayNight;
    public RainSystem rainSystem;

    public GameObject timeChosenGameObject;
    public TextMeshProUGUI timeChosenText;

    public void SetDay()
    {
        dayNight.isNight = false;
        if (!rainSystem.canRain)
        {
            dayNight.ChangeMaterial();
        }
        dayNight.SetDirectionalLight();
        dayNight.SetStreetLights();
        timeChosenGameObject.SetActive(true);
        timeChosenText.text = "Day Time Chosen";
    }

    public void SetNight()
    {
        dayNight.isNight = true;
        dayNight.ChangeMaterial();
        dayNight.SetDirectionalLight();
        dayNight.SetStreetLights();
        timeChosenGameObject.SetActive(true);
        timeChosenText.text = "Night Time Chosen";
    }
}
