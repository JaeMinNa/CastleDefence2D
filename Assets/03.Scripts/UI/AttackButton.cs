using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    public float ClickTime { get; private set; } 
    public bool IsClick { get; private set; }
    private GameObject _player;
    private SpriteRenderer _playerSpriteRenderer;
    private Transform _collidersTransform;

    private float _collidersPositionX;
    private float _collidersPositionY;

    // 버튼 클릭이 시작했을 때
    public void ButtonDown()
    {
        _collidersPositionX = _collidersTransform.localPosition.x;

        _player.GetComponent<PlayerController>().PlayerSO.Speed *= -1;

        if (_player.GetComponent<PlayerController>().PlayerSO.Speed > 0)
        {
            _playerSpriteRenderer.flipX = false;

            if (_collidersPositionX < 0)
            {
                _collidersTransform.localPosition = new Vector3(-_collidersPositionX, _collidersPositionY, 0);
            }

        }
        else
        {
            _playerSpriteRenderer.flipX = true;

            if (_collidersPositionX > 0)
            {
                _collidersTransform.localPosition = new Vector3(-_collidersPositionX, _collidersPositionY, 0);
            }
        }

        IsClick = true;
    }

    // 버튼 클릭이 끝났을 때
    public void ButtonUp()
    {
        IsClick = false;

        if (ClickTime >= _player.GetComponent<PlayerController>().PlayerSO.SkillTime)
        {
            Debug.Log("스킬 발동!");
        }
    }

    private void Start()
    {
        _player = GameManager.I.PlayerManager.Player;
        _playerSpriteRenderer = _player.transform.GetChild(0).GetComponent<SpriteRenderer>();
        _collidersTransform = _player.transform.GetChild(1).GetComponent<Transform>();

        _collidersPositionX = _collidersTransform.localPosition.x;
        _collidersPositionY = _collidersTransform.localPosition.y;

        if (_player.GetComponent<PlayerController>().PlayerSO.Speed > 0)
        {
            _playerSpriteRenderer.flipX = false;

            if (_collidersPositionX < 0)
            {
                _collidersTransform.localPosition = new Vector3(-_collidersPositionX, _collidersPositionY, 0);
            }
        }
        else
        {
            _playerSpriteRenderer.flipX = true;

            if (_collidersPositionX > 0)
            {
                _collidersTransform.localPosition = new Vector3(-_collidersPositionX, _collidersPositionY, 0);
            }
        }
    }

    private void Update()
    {
        if (IsClick)
        {
            ClickTime += Time.deltaTime;
            if(ClickTime >= (_player.GetComponent<PlayerController>().PlayerSO.SkillTime / 4f))
            _player.GetComponent<PlayerAttackState>().time = 0f;
        }
        else
        {
            ClickTime = 0;
        }
    }
}
