using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private Transform _spawnLeft;
    [SerializeField] private Transform _spawnRigth;

    private void Start()
    {
        StartCoroutine(COSpawnEnemy());
    }

    IEnumerator COSpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);

            int random = Random.Range(0, 2);
            if (random == 0) GameManager.I.ObjectPoolManager.InstantiateEnemy("Snail", _spawnLeft.position);
            else GameManager.I.ObjectPoolManager.InstantiateEnemy("Snail", _spawnRigth.position);
        }
    }
}
