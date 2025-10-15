using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float clampX = 5f;
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float horizontalSmoothTime = 0.08f;
    [SerializeField] private float horizontalMaxSpeed = 2f;     // max geçiş hızı

    private Rigidbody rb;
    private float targetX;
    private float currentVelocityX;
    private bool isMovingForward = false;
    private bool levelCompleted = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate; // hareketleri yumuşatır
    }

    private void FixedUpdate()
    {
        if (isMovingForward)
            Move();
    }

    private void Move()
    {
        // SmoothDamp akıcı sag sol gecis
        float smoothX = Mathf.SmoothDamp(rb.position.x, targetX, ref currentVelocityX, horizontalSmoothTime, horizontalMaxSpeed, Time.fixedDeltaTime);

        // ileri hareket
        float newZ = rb.position.z + forwardSpeed * Time.fixedDeltaTime;

        rb.MovePosition(new Vector3(smoothX, rb.position.y, newZ));
    }

    public void SetTargetX(float x)
    {
        targetX = Mathf.Clamp(x, -clampX, clampX);
    }

    public void StartMoving() => isMovingForward = true;
    public void StopMoving() => isMovingForward = false;

    private void OnTriggerEnter(Collider other)
    {
        if (levelCompleted) return;

        if (other.CompareTag("Finish"))
        {
            levelCompleted = true;
            Time.timeScale = 0f;
        }
    }
    public void ResetLevelCompletion() => levelCompleted = false;
}
