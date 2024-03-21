using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public ObjectPool ObjectPool { get; private set; }

    private GameObject _prefab;

    public void Init()
    {
        ObjectPool = GetComponent<ObjectPool>();
    }

    public void Release()
    {

    }

    public void InstantiatePrefab(string poolName, Vector3 startPosition)
    {
        _prefab = ObjectPool.SpawnFromPool(poolName);
        _prefab.transform.position = startPosition;

        _prefab.SetActive(true);
    }
}