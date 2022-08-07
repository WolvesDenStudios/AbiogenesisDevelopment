using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupUse : MonoBehaviour
{
    public GameObject powerupSlots;
    public GameObject armController;

    void Awake()
    {
        powerupSlots = GameObject.Find("powerUpContainer");
        armController = GameObject.Find("armController");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void powerUse()
    {
        powerupSlots.GetComponent<powerupManager>().powerUpCount -= 1;
        armController.GetComponent<ArmRotation>().isPowerUpUsed = true;
    }
}
