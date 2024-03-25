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

    public void ActivePrefab(string poolName, Vector3 startPosition)
    {
        _prefab = ObjectPool.SpawnFromPool(poolName);
        _prefab.transform.position = startPosition;
        _prefab.SetActive(true);
    }

    public void ActiveDamage(string poolName, Vector3 startPosition, int damege, int color)
    {
        _prefab = ObjectPool.SpawnFromPool(poolName);
        _prefab.transform.position = startPosition;
        _prefab.GetComponent<DamageText>().Text.text = damege.ToString();
        _prefab.GetComponent<DamageText>().Text.color = new Color(255 / 255f, color / 255f, color / 255f, 255 / 255f);
        _prefab.SetActive(true);
    }
}
