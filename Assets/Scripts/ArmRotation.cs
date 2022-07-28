using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    [SerializeField]
    private GameObject arm1, arm2;
    [SerializeField]
    private float arm1Speed, arm2Speed;

    void Start()
    {
        StartCoroutine(RotateObject(360, -Vector3.forward, 27, arm1));
        StartCoroutine(RotateObject(360, -Vector3.forward, 20, arm2));
    }

    void Update()
    {

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
