using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    private SpriteRenderer _playerSpriteRenderer;
    private SpriteRenderer _skillSpriteRenderer;

    private void Start()
    {
        _playerSpriteRenderer = GameManager.I.PlayerManager.Player.transform.GetChild(0).GetComponent<SpriteRenderer>();
        _skillSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        if (_playerSpriteRenderer.flipX) _skillSpriteRenderer.flipX = true;
        else _skillSpriteRenderer.flipX = false;
    }

    private void OnEnable()
    {
        if(_playerSpriteRenderer != null && _skillSpriteRenderer != null)
        {
            if (_playerSpriteRenderer.flipX) _skillSpriteRenderer.flipX = true;
            else _skillSpriteRenderer.flipX = false;
        }

        StartCoroutine(COSkillStart());
    }

    IEnumerator COSkillStart()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
