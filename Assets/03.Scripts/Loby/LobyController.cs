using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobyController : MonoBehaviour
{
    [Header("Coin")]
    [SerializeField] private TMP_Text _coinText;

    [Header("Stage")]
    [SerializeField] private TMP_Text _stageText;

    [SerializeField] private TMP_Text _skillDrawCountText;

    [Header("Level")]
    //private PlayerSO _playerSO;
    private PlayerData _playerData;
    private CastleData _castleData;
    //[SerializeField] private CastleSO _castleSO;
    [SerializeField] private TMP_Text _playerLevelText;
    [SerializeField] private TMP_Text _castleLevelText;
    [SerializeField] private Slider _playerExpSlider;
    [SerializeField] private Slider _castleExpSlider;
    [SerializeField] private int _expPrice;
    [SerializeField] private int _exp;

    [Header("Setting")]
    [SerializeField] private GameObject _setting;
    [SerializeField] private GameObject _reset;
    [SerializeField] private SoundController _soundController;

    [Header("Info")]
    [SerializeField] private GameObject _playerInfo;
    [SerializeField] private TMP_Text _playerLvText;
    [SerializeField] private TMP_Text _playerExpText;
    [SerializeField] private TMP_Text _playerAtkText;
    [SerializeField] private TMP_Text _playerSpeedText;
    [SerializeField] private GameObject _castleInfo;
    [SerializeField] private TMP_Text _castleLvText;
    [SerializeField] private TMP_Text _castleExpText;
    [SerializeField] private TMP_Text _castleHpText;
    [SerializeField] private TMP_Text _castleAtkText;
    [SerializeField] private TMP_Text _castleTimeText;

    [Header("Skill Inventory")]
    [SerializeField] private GameObject _skillInventory;
    [SerializeField] private Image _meleeSkillImage;
    [SerializeField] private Image _rangedSkillImage;
    [SerializeField] private Image _areaSkillImage;

    private void Start()
    {
        //_playerSO = GameManager.I.PlayerManager.PlayerPrefab.GetComponent<PlayerController>().PlayerSO;
        _playerData = GameManager.I.DataManager.PlayerData;
        _castleData = GameManager.I.DataManager.CastleData;
        SoundSetting();
        GameManager.I.SoundManager.StartBGM("Loby");
        Init();
    }

    private void Init()
    {
        _coinText.text = GameManager.I.DataManager.GameData.Coin.ToString();
        _stageText.text = "Stage " + GameManager.I.DataManager.GameData.Stage.ToString();
        _playerLevelText.text = "Lv " + _playerData.Level.ToString();
        _castleLevelText.text = "Lv " + _castleData.Level.ToString();
        _skillDrawCountText.text = GameManager.I.DataManager.GameData.SkillDrawCount.ToString();
        _playerExpSlider.value = _playerData.CurrentExp / _playerData.MaxExp;
        _castleExpSlider.value = _castleData.CurrentExp / _castleData.MaxExp;
    }

    public void BattleScene0Button()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        GameManager.I.ScenesManager.SceneMove("BattleScene0");
    }

    public void BattleScene1Button()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClickMiss");
    }

    public void ActiveSetting()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _setting.SetActive(true);
    }

    public void InactiveSetting()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _setting.SetActive(false);
    }

    public void ActiveReset()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _reset.SetActive(true);
    }

    public void InactiveReset()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _reset.SetActive(false);
    }

    public void PlayerExpButton()
    {
        if(GameManager.I.DataManager.GameData.Coin >= _expPrice)
        {
            GameManager.I.SoundManager.StartSFX("ButtonExp");
            GameManager.I.DataManager.GameData.Coin -= _expPrice;
            _coinText.text = GameManager.I.DataManager.GameData.Coin.ToString();
            _playerData.CurrentExp += _exp;

            if (_playerData.CurrentExp >= _playerData.MaxExp)
            {
                _playerData.CurrentExp -= _playerData.MaxExp;
                _playerData.Level++;
                _playerData.MaxExp *= 1.5f;
                _playerLevelText.text = "Lv " + _playerData.Level.ToString();
                PlayerLevelUp();
            }

            _playerExpSlider.value = _playerData.CurrentExp / _playerData.MaxExp;
        }
        else 
        {
            GameManager.I.SoundManager.StartSFX("ButtonClickMiss");
        }

        GameManager.I.DataManager.DataSave();
    }

    public void CastleExpButton()
    {
        if (GameManager.I.DataManager.GameData.Coin >= _expPrice)
        {
            GameManager.I.SoundManager.StartSFX("ButtonExp");
            GameManager.I.DataManager.GameData.Coin -= _expPrice;
            _coinText.text = GameManager.I.DataManager.GameData.Coin.ToString();
            _castleData.CurrentExp += _exp;

            if (_castleData.CurrentExp >= _castleData.MaxExp)
            {
                _castleData.CurrentExp -= _castleData.MaxExp;
                _castleData.Level++;
                _castleData.MaxExp *= 1.5f;
                _castleLevelText.text = "Lv " + _castleData.Level.ToString();
                CastleLevelUp();
            }

            _castleExpSlider.value = _castleData.CurrentExp / _castleData.MaxExp;
        }
        else
        {
            GameManager.I.SoundManager.StartSFX("ButtonClickMiss");
        }

        GameManager.I.DataManager.DataSave();
    }

    private void PlayerLevelUp()
    {
        GameManager.I.SoundManager.StartSFX("LevelUp");
        _playerData.Speed += 0.1f;
        _playerData.Atk += 1f;
    }

    private void CastleLevelUp()
    {
        GameManager.I.SoundManager.StartSFX("LevelUp");
        _castleData.Atk++;
        _castleData.Hp += 10f;
        if(_castleData.AttackCoolTime >= 0.5f)
        {
            _castleData.AttackCoolTime -= 0.1f;
        }
    }

    public void DataReset()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        GameManager.I.DataManager.DataReset();
        Init();
        _reset.SetActive(false);
        _setting.SetActive(false);
    }

    public void ActivePlayerInfo()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _playerLvText.text = _playerData.Level.ToString();
        _playerExpText.text = ((int)_playerData.CurrentExp).ToString() + " / " + ((int)_playerData.MaxExp).ToString();
        _playerAtkText.text = _playerData.Atk.ToString();
        if(_playerData.Speed > 0)
        {
            _playerSpeedText.text = _playerData.Speed.ToString();
        }
        else
        {
            _playerSpeedText.text = (-_playerData.Speed).ToString();
        }
        _playerInfo.SetActive(true);
    }

    public void InactivePlayerInfo()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _playerInfo.SetActive(false);
    }

    public void ActiveCastleInfo()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _castleLvText.text = _castleData.Level.ToString();
        _castleExpText.text = ((int)_castleData.CurrentExp).ToString() + " / " + ((int)_castleData.MaxExp).ToString();
        _castleHpText.text = _castleData.Hp.ToString();
        _castleAtkText.text = _castleData.Atk.ToString();
        _castleTimeText.text = _castleData.AttackCoolTime.ToString();
        _castleInfo.SetActive(true);
    }

    public void InactiveCastleInfo()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _castleInfo.SetActive(false);
    }

    public void ActiveSkillInventory()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _skillInventory.SetActive(true);
    }

    public void InactiveSkillInventory()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _skillInventory.SetActive(false);
    }

    private void SoundSetting()
    {
        _soundController.BGMSlider.value = GameManager.I.DataManager.GameData.BGMVolume;
        _soundController.SFXSlider.value = GameManager.I.DataManager.GameData.SFXVolume;
        _soundController.SFXControll();
        _soundController.BGMControll();
    }
}
