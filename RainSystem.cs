using TMPro;
using UnityEngine;

public class RainSystem : MonoBehaviour
{
    public DayNight dayNight;
    public Material clearDay;
    public Material cloudyDay;
    public GameObject rainParticles;
    public GameObject player;
    public bool canRain;
    public GameObject weatherChosenGameObject;
    public TextMeshProUGUI weatherChosenText;

    void Start()
    {
        rainParticles.SetActive(false);
    }

    void Update()
    {
        if (canRain)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        }

    }

    public void AddRain()
    {
        canRain = true;
        rainParticles.SetActive(true);
        if (!dayNight.isNight)
        {
            RenderSettings.skybox = cloudyDay;
            dayNight.skyBoxDay = cloudyDay;
        }
        weatherChosenGameObject.SetActive(true);
        weatherChosenText.text = "Rainy Weather Chosen";

    }

    public void RemoveRain()
    {
        canRain = false;
        rainParticles.SetActive(false);
        if (!dayNight.isNight)
        {
            RenderSettings.skybox = clearDay;
            dayNight.skyBoxDay = clearDay;
        }
        weatherChosenGameObject.SetActive(true);
        weatherChosenText.text = "Clear Weather Chosen";
    }

}
