using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public ObjectPool ObjectPool { get; private set; }

    private GameObject _enemy;

    public void Init()
    {
        ObjectPool = GetComponent<ObjectPool>();
    }

    public void Release()
    {

    }

    public void InstantiateEnemy(string poolName, Vector3 startPosition)
    {
        _enemy = ObjectPool.SpawnFromPool(poolName);
        _enemy.transform.position = startPosition;

        _enemy.SetActive(true);
    }
}