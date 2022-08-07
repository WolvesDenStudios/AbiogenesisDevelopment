using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpInteraction : MonoBehaviour
{
    public GameObject powerupSlots;

    void Awake()
    {
        powerupSlots = GameObject.Find("powerUpContainer");
    }

    void Start()
    {
        StartCoroutine(countdownDestroy());
    }

    void Update()
    {

    }

    public void powerUpClick()
    {
        if (powerupSlots.GetComponent<powerupManager>().powerUpCount < 3)
        {
            powerupSlots.GetComponent<powerupManager>().powerUpCount += 1;
            Destroy(gameObject);
        }
    }

    IEnumerator countdownDestroy()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    
}
