using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float damage = 20f;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;

        if (IsVisibleByCamera(obj, Camera.main))
        {
            // Camera icerisinde Enemy varsa hasar ver
            Enemy enemy = obj.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
      
private bool IsVisibleByCamera(GameObject obj, Camera cam)
    {
        //kameranin gormedigi enemylere damage vermiyor
        Vector3 viewportPos = cam.WorldToViewportPoint(obj.transform.position);
        return viewportPos.z > 0 &&
               viewportPos.x >= 0 && viewportPos.x <= 1 &&
               viewportPos.y >= 0 && viewportPos.y <= 1;
    }
}
