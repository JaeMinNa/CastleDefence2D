using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    public float ClickTime { get; private set; }
    public float SkillCoolTime;
    public bool IsClick { get; private set; }
    [SerializeField] private Animator _skillSliderAnimator;
    [SerializeField] private TutorialController _tutorialController;
    private PlayerController _playerController;
    private SpriteRenderer _playerSpriteRenderer;
    private PlayerAnimationEvent _playerAnimationEvent;
    private Transform _collidersTransform;
    private SkillData _meleeSkillData;
    private SkillData _rangedSkillData;
    private SkillData _areaSkillData;
    private GameData _gameData;
    private float _collidersPositionX;
    private float _collidersPositionY;
    private bool _meleeSkillStart;
    private bool _rangedSkillStart;
    private bool _areaSkillStart;

    // 버튼 클릭이 시작했을 때
    public void ButtonDown()
    {
        //if(_gameData.TutorialCount == 1)
        //{
        //    _tutorialController.StopTutorial(1);
        //}
        //else if(_gameData.TutorialCount == 2)
        //{
        //    _tutorialController.StopTutorial(2);
        //}

        _collidersPositionX = _collidersTransform.localPosition.x;

        _playerController.Speed *= -1;

        if (_playerController.Speed > 0)
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
        _meleeSkillStart = false;
        _rangedSkillStart = false;
        _areaSkillStart = false;

        if (ClickTime >= SkillCoolTime)
        {
            StartCoroutine(_playerAnimationEvent.COStartAreaSkill(_areaSkillData));
        }
        else if ((ClickTime >= 2 * (SkillCoolTime / 3) && ClickTime < SkillCoolTime))
        {
            StartCoroutine(_playerAnimationEvent.COStartRangedSkill(_rangedSkillData));
        }
        else if (ClickTime >= SkillCoolTime / 3 && ClickTime < 2 * (SkillCoolTime / 3))
        {
            StartCoroutine(_playerAnimationEvent.COStartMeleeSkill(_meleeSkillData));
        }

    }

    private void Start()
    {
        _playerController = GameManager.I.PlayerManager.Player.GetComponent<PlayerController>();
        _playerSpriteRenderer = _playerController.transform.GetChild(0).GetComponent<SpriteRenderer>();
        _playerAnimationEvent = _playerController.transform.GetChild(0).GetComponent<PlayerAnimationEvent>();
        _collidersTransform = _playerController.transform.GetChild(1).GetComponent<Transform>();
        _meleeSkillData = GameManager.I.DataManager.PlayerData.EquipMeleeSkillData;
        _rangedSkillData = GameManager.I.DataManager.PlayerData.EquipRangedSkillData;
        _areaSkillData = GameManager.I.DataManager.PlayerData.EquipAreaSkillData;
        _gameData = GameManager.I.DataManager.GameData;

        _collidersPositionX = _collidersTransform.localPosition.x;
        _collidersPositionY = _collidersTransform.localPosition.y;
        _meleeSkillStart = false;
        _rangedSkillStart = false;
        _areaSkillStart = false;
        SkillCoolTime = GameManager.I.DataManager.PlayerData.SkillTime;

        if (GameManager.I.DataManager.PlayerData.Speed > 0)
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

            if((ClickTime >= SkillCoolTime) && !_areaSkillStart)
            {
                GameManager.I.SoundManager.StartSFX("Gauge");
                StartCoroutine(COStartSkillSliderAnimation());
                GameManager.I.ObjectPoolManager.ActivePrefab("SkillUseEffect", _playerController.transform.position + Vector3.down * 2f);
                _areaSkillStart = true;
            }
            else if((ClickTime >= 2 * (SkillCoolTime / 3) && ClickTime < SkillCoolTime)
                 && !_rangedSkillStart)
            {
                GameManager.I.SoundManager.StartSFX("Gauge");
                StartCoroutine(COStartSkillSliderAnimation());
                GameManager.I.ObjectPoolManager.ActivePrefab("SkillUseEffect", _playerController.transform.position + Vector3.down * 2f);
                _rangedSkillStart = true;
            }
            else if((ClickTime >= SkillCoolTime / 3 && ClickTime < 2 * (SkillCoolTime / 3))
                 && !_meleeSkillStart)
            {
                GameManager.I.SoundManager.StartSFX("Gauge");
                StartCoroutine(COStartSkillSliderAnimation());
                GameManager.I.ObjectPoolManager.ActivePrefab("SkillUseEffect", _playerController.transform.position + Vector3.down * 2f);
                _meleeSkillStart = true;
            }

            if (ClickTime >= (SkillCoolTime / 4f))
                _playerController.GetComponent<PlayerAttackState>().time = 0f;
        }
        else
        {
            ClickTime = 0;
        }
    }

    IEnumerator COStartSkillSliderAnimation()
    {
        _skillSliderAnimator.SetBool("SkillSlider", true);
        yield return new WaitForSeconds(0.2f);
        _skillSliderAnimator.SetBool("SkillSlider", false);
    }
}
