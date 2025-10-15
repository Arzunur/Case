using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    [Header("Target Character")]
    [SerializeField] private Transform target; 

    [Header("Camera Settings")]
    [SerializeField] private float smoothSpeed = 5f; 
    [SerializeField] private float fixedX = 0f;      
    [SerializeField] private float fixedY = 20f;     
    [SerializeField] private float zOffset = -10f;   

    private void LateUpdate()
    {
        if (target == null) return;
        float desiredZ = target.position.z + zOffset;
        Vector3 desiredPosition = new Vector3(fixedX, fixedY, desiredZ);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);//top down goruntu
    }
}
