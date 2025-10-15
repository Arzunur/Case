using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerController characterMovement;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private ObjectPool ballPool; 

    public Transform shootPoint; 

    [Header("Variables")]
    [SerializeField] private float shootForce = 20f;
    [SerializeField] private float shootInterval = 0.3f;
    [SerializeField] private float planeZ = 0f;

    private float shootTimer = 0f;
    private bool canShoot = false; 

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!canShoot)
            return;

        if (IsPressing())
        {
            MoveCharacterToPointer(GetPointerPosition());

            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                ThrowBall();
                shootTimer = 0f;
            }
        }
        else
        {
            shootTimer = shootInterval;
        }
    }

    private void ThrowBall()
    {
        if (shootPoint == null)
        {
           // Debug.LogError("shoot point bos");
            return;
        }

        if (ballPool == null)
        {
            //Debug.LogError("ball pool atanmamis");
            return;
        }

        // pooldan ball alıyoruz 
        GameObject ball = ballPool.GetObject();
        ball.transform.position = shootPoint.position;
        ball.transform.rotation = Quaternion.identity;

        Rigidbody ballRb = ball.GetComponent<Rigidbody>();
        if (ballRb != null)
        {
            ballRb.velocity = Vector3.zero; //  hızı sıfırla
            ballRb.angularVelocity = Vector3.zero;
            ballRb.AddForce(Vector3.forward * shootForce, ForceMode.Impulse);
        }

        StartCoroutine(ReturnBallAfterSeconds(ball, 5f));
    }

    private IEnumerator ReturnBallAfterSeconds(GameObject ball, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (ball != null)
            ballPool.ReturnObject(ball);
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

    public void EnableShooting(bool enable)
    {
        canShoot = enable;
    }

}
