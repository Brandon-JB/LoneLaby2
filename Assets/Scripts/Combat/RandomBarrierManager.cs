using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBarrierManager : MonoBehaviour
{
    public GameObject objectToDestroy;
    public GameObject[] enemies; // Assign this in the Inspector or dynamically

    void Start()
    {
        StartCoroutine(CheckEnemies());
    }

    IEnumerator CheckEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Check every second

            if (AllEnemiesDestroyed())
            {
                Destroy(objectToDestroy);
                yield break; // Stop the coroutine after destruction
            }
        }
    }

    bool AllEnemiesDestroyed()
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null) return false;
        }
        return true;
    }


}
