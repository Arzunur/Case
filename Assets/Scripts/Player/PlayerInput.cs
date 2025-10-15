using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerController characterMovement;
    [SerializeField] private float planeZ = 0f;


    private bool canMove = false; //karakter hareket kontrolu

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (characterMovement == null)
            characterMovement = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (!canMove) return; // play tusuna basilmadan etkilesime girme 

        if (IsPressing())
        {
            Vector2 pointerPos = GetPointerPosition();
            if (!characterMovement) return;
            characterMovement.StartMoving(); // oyuna baslar
            MoveCharacterToPointer(pointerPos);
        }
    }
    public void EnableInput(bool enable) 
    {
        canMove = enable;
    }

    private bool IsPressing()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        return Input.GetMouseButton(0);
#else
        return Input.touchCount > 0;
#endif
    }

    private Vector2 GetPointerPosition()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        return Input.mousePosition;
#else
        return Input.GetTouch(0).position;
#endif
    }

    private void MoveCharacterToPointer(Vector2 pointerPos)
    {
        Ray ray = mainCamera.ScreenPointToRay(pointerPos);
        Plane plane = new Plane(Vector3.forward, new Vector3(0, 0, planeZ));

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 worldPoint = ray.GetPoint(distance);
            characterMovement.SetTargetX(worldPoint.x);
        }
    }
}
