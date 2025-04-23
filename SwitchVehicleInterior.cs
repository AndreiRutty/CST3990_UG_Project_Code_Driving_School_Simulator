using System.Collections.Generic;
using UnityEngine;

public class SwitchVehicleInterior : MonoBehaviour
{
    public bool canSwitch = false;
    public GameObject player;

    void Start()
    {

    }

    void Update()
    {
        if (canSwitch)
        {
            player.transform.localScale = new Vector3(-0.75f, 0.75f, 0.75f);
        }
        else
        {
            player.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        }
    }

    public void Switch(bool value)
    {
        canSwitch = value;
    }
}
