using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualLevel : MonoBehaviour, ILevelLoader
{
    [SerializeField] private List<GameObject> manualLevels;

    //indexe gore yukleyecegi prefabsleri secer
    public GameObject LoadLevel(int levelIndex, Transform parent = null)
    {
        if (levelIndex < 0 || levelIndex >= manualLevels.Count) //index kontrolu
        {
            Debug.LogWarning("level index gecersiz ");
            return null;
        }
        return Instantiate(manualLevels[levelIndex], parent);
    }
}
