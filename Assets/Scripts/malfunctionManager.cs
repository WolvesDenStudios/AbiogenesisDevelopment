using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class malfunctionManager : MonoBehaviour
{
    public int misclickCount = 0, countDownTimer = 3;
    public bool isCoroutineRunning = false, rotationUpdated = false;
    public GameObject malfunctionArm;
    Vector3 rotationEuler;
    public IEnumerator co1;
    
    void Awake()
    {
        malfunctionArm = GameObject.FindWithTag("malfunctionArm");
    }

    void Start()
    {
        
    }


    void Update()
    {
        if (misclickCount == 1 && rotationUpdated)
        {
            malfunctionArm.transform.rotation = Quaternion.Euler(0,0,115);
            // if (isCoroutineRunning)
            // {
            //     StopCoroutine(co1);
            //     isCoroutineRunning = false;
            // }
            // Debug.Log("1");
            // co1 = RotateObject(180, Vector3.forward, 20, malfunctionArm);
            // StartCoroutine(co1);
            rotationUpdated = false;
            
        }

        if (misclickCount == 2 && rotationUpdated)
        {
            malfunctionArm.transform.rotation = Quaternion.Euler(0,0,-2);
            // if (isCoroutineRunning)
            // {
            //     StopCoroutine(co1);
            //     isCoroutineRunning = false;
            // }
            // Debug.Log("2");
            // co1 = RotateObject(180, Vector3.forward, 20, malfunctionArm);
            StartCoroutine(cooldownMalfunction());
            rotationUpdated = false;
        }

        
    }

    IEnumerator cooldownMalfunction()
    {
        yield return new WaitForSeconds(1.5f);
        malfunctionArm.transform.rotation = Quaternion.Euler(0,0,115);
        yield return new WaitForSeconds(1.5f);
        malfunctionArm.transform.rotation = Quaternion.Euler(0,0,220);
        misclickCount = 0;
        
    }
}
