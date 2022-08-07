using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    [SerializeField]
    private GameObject arm1, arm2;
    public float arm1Speed = 27f, arm2Speed = 20f;
    public bool isPowerUpUsed = false, returnToNormalSpeed = false;
    public IEnumerator co1, co2;

    void Start()
    {
        co1 = RotateObject(360, -Vector3.forward, arm1Speed, arm1);
        StartCoroutine(co1);
        co2 = RotateObject(360, -Vector3.forward, arm2Speed, arm2);
        StartCoroutine(co2);
    }

    void Update()
    {
        if (isPowerUpUsed)
        {
            arm1Speed = 27 / 2;
            arm2Speed = 20f / 2;
            restartArmRotation();
            StartCoroutine(powerUpUsed());
            isPowerUpUsed = false;
        }

        if (!isPowerUpUsed && returnToNormalSpeed)
        {
            arm1Speed = 27f;
            arm2Speed = 20f;
            restartArmRotation();
            returnToNormalSpeed = false;
        }
    }

    private void restartArmRotation()
    {
        StopCoroutine(co1);
        StopCoroutine(co2);
        co1 = RotateObject(360, -Vector3.forward, arm1Speed, arm1);
        StartCoroutine(co1);
        co2 = RotateObject(360, -Vector3.forward, arm2Speed, arm2);
        StartCoroutine(co2);
    }

    IEnumerator powerUpUsed()
    {
        yield return new WaitForSeconds(5);
        returnToNormalSpeed = true;
    }

    IEnumerator RotateObject(float angle, Vector3 axis, float inTime, GameObject armToRotate)
     {
         // calculate rotation speed
         float rotationSpeed = angle / inTime;
 
         while (true)
         {
             // save starting rotation position
             Quaternion startRotation = armToRotate.transform.rotation;
 
             float deltaAngle = 0;
 
             // rotate until reaching angle
             while (deltaAngle < angle)
             {
                 deltaAngle += rotationSpeed * Time.deltaTime;
                 deltaAngle = Mathf.Min(deltaAngle, angle);
 
                 armToRotate.transform.rotation = startRotation * Quaternion.AngleAxis(deltaAngle, axis);
 
                 yield return null;
             }
         }
     }
}
