using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 offset;
    Quaternion offsetRotation;
    
    public Transform target;
    [SerializeField] float damping = 1;

    void Start()
    {
        offset = transform.position - target.position;
        offsetRotation = transform.rotation;
    }

    void LateUpdate()
    {

        if (!target)
            return;


        float currentAngle = transform.eulerAngles.y;
        float desiredAngle = target.eulerAngles.y;
        float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * damping);

        Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
        transform.position = target.position + (rotation * offset);

        transform.LookAt(target);
        transform.rotation *= Quaternion.Euler(-10f, 0f, 0f);
    }
}