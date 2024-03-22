using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    public float ClickTime { get; private set; } 
    public bool IsClick { get; private set; }
    private PlayerController _playerController;
    private SpriteRenderer _playerSpriteRenderer;
    private PlayerAnimationEvent _playerAnimationEvent;
    private Transform _collidersTransform;
    private SkillSO _skillSOFirst;
    private SkillSO _skillSOSecond;
    private SkillSO _skillSOThird;

    private float _collidersPositionX;
    private float _collidersPositionY;

    // 버튼 클릭이 시작했을 때
    public void ButtonDown()
    {
        _collidersPositionX = _collidersTransform.localPosition.x;

        _playerController.PlayerSO.Speed *= -1;

        if (_playerController.PlayerSO.Speed > 0)
        {
            _playerSpriteRenderer.flipX = false;

            if (_collidersPositionX < 0)
            {
                _collidersTransform.localPosition = new Vector3(-_collidersPositionX, _collidersPositionY, 0);
                _collidersTransform.localEulerAngles = new Vector3(0, 0, 0);
            }

        }
        else
        {
            _playerSpriteRenderer.flipX = true;

            if (_collidersPositionX > 0)
            {
                _collidersTransform.localPosition = new Vector3(-_collidersPositionX, _collidersPositionY, 0);
                _collidersTransform.localEulerAngles = new Vector3(0, 180, 0);
            }
        }

        IsClick = true;
    }

    // 버튼 클릭이 끝났을 때
    public void ButtonUp()
    {
        IsClick = false;

        if (ClickTime >= _playerController.PlayerSO.SkillTime)
        {
            StartCoroutine(_playerAnimationEvent.COStartAreaSkill(_skillSOThird));
        }
        else if (ClickTime >= 2 * (_playerController.PlayerSO.SkillTime / 3) && ClickTime < _playerController.PlayerSO.SkillTime)
        {
            StartCoroutine(_playerAnimationEvent.COStartRangedSkill(_skillSOSecond));
        }
        else if (ClickTime >= _playerController.PlayerSO.SkillTime / 3 && ClickTime < 2 * (_playerController.PlayerSO.SkillTime / 3))
        {         
            StartCoroutine(_playerAnimationEvent.COStartMeleeSkill(_skillSOFirst));
        }
    }

    private void Start()
    {
        _playerController = GameManager.I.PlayerManager.Player.GetComponent<PlayerController>();
        _playerSpriteRenderer = _playerController.transform.GetChild(0).GetComponent<SpriteRenderer>();
        _playerAnimationEvent = _playerController.transform.GetChild(0).GetComponent<PlayerAnimationEvent>();
        _collidersTransform = _playerController.transform.GetChild(1).GetComponent<Transform>();
        _skillSOFirst = GameManager.I.DataManager.GameDataSO.MeleeSkill;
        _skillSOSecond = GameManager.I.DataManager.GameDataSO.RangedSkill;
        _skillSOThird = GameManager.I.DataManager.GameDataSO.AreaSkill;

        _collidersPositionX = _collidersTransform.localPosition.x;
        _collidersPositionY = _collidersTransform.localPosition.y;

        if (_playerController.PlayerSO.Speed > 0)
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
            if(ClickTime >= (_playerController.PlayerSO.SkillTime / 4f))
            _playerController.GetComponent<PlayerAttackState>().time = 0f;
        }
        else
        {
            ClickTime = 0;
        }
    }
}
