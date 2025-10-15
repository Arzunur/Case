using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPool : MonoBehaviour
{
    private Dictionary<GameObject, Queue<GameObject>> poolDictionary = new();

    public GameObject GetFromPool(GameObject prefab)
    {
        if (!poolDictionary.ContainsKey(prefab) || poolDictionary[prefab].Count == 0)
        {
            // Dinamik pool objesi yarat
            GameObject newObj = Instantiate(prefab, transform);
            newObj.SetActive(false);

            if (!poolDictionary.ContainsKey(prefab))
                poolDictionary[prefab] = new Queue<GameObject>();

            poolDictionary[prefab].Enqueue(newObj);
        }

        return poolDictionary[prefab].Dequeue();
    }

    public void ReturnToPool(GameObject prefab, GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;

        if (!poolDictionary.ContainsKey(prefab))
            poolDictionary[prefab] = new Queue<GameObject>();

        poolDictionary[prefab].Enqueue(obj);
    }

    public void ResetPool()
    {
        foreach (var kvp in poolDictionary)
        {
            Queue<GameObject> queue = kvp.Value;
            GameObject[] objs = queue.ToArray();
            queue.Clear();

            foreach (var obj in objs)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                    obj.transform.SetParent(transform);
                    obj.transform.localPosition = Vector3.zero;
                    queue.Enqueue(obj);
                }
            }
        }
    }



}