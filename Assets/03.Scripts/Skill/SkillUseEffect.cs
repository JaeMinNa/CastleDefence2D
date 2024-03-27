using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUseEffect : MonoBehaviour
{
    [SerializeField] private float _inactiveTime;
    private GameObject _player;

    private void Start()
    {
        _player = GameManager.I.PlayerManager.Player;
        StartCoroutine(COInactiveSkill(_inactiveTime));
    }

    private void OnEnable()
    {
        if(_player != null)
        {
            StartCoroutine(COInactiveSkill(_inactiveTime));
        }
    }

    private void Update()
    {
        transform.position = _player.transform.position + Vector3.down * 2f;
    }

    IEnumerator COInactiveSkill(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
