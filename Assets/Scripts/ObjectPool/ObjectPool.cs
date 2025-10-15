using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Prefab ve Pool Ayarlarý")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private int initialPoolSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        // poolu doldur
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(ballPrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        // pool yeterli gelmediyse pool olusturur
        GameObject newObj = Instantiate(ballPrefab);
        return newObj;
    }

    public void ReturnObject(GameObject obj)
    {
        if (obj != null) 
        {
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

}
