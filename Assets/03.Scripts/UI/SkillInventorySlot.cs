using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillInventorySlot : MonoBehaviour
{
    //private SkillSO _skillSO;
    private PlayerData _playerData;
    private SkillData _skillData;
    public bool _isSkill;

    [Header("Inventory")]
    [SerializeField] private SkillInventory _skillInventory;

    [Header("Slot")]
    [SerializeField] private TMP_Text _skillLevelText;
    [SerializeField] private TMP_Text _skillRankText;
    [SerializeField] private GameObject _skillLevel;
    [SerializeField] private GameObject _skillRank;
    [SerializeField] private GameObject _equip;
    [SerializeField] private Image _emptyImage;

    [Header("MeleeSkillInfo")]
    [SerializeField] private GameObject _meleeSkillInfo;
    [SerializeField] private TMP_Text _meleeSkillTag;
    [SerializeField] private Image _equipMeleeSkillImage;
    [SerializeField] private TMP_Text _meleeSkillDescriptionText;
    [SerializeField] private TMP_Text _meleeSkillLevelText;
    [SerializeField] private TMP_Text _meleeSkillRankText;
    [SerializeField] private TMP_Text _meleeSkillAtkText;
    [SerializeField] private TMP_Text _meleeSkillUpgradeText;

    [Header("RangedSkillInfo")]
    [SerializeField] private GameObject _rangedSkillInfo;
    [SerializeField] private TMP_Text _rangedSkillTag;
    [SerializeField] private Image _equipRangedSkillImage;
    [SerializeField] private TMP_Text _rangedSkillDescriptionText;
    [SerializeField] private TMP_Text _rangedSkillLevelText;
    [SerializeField] private TMP_Text _rangedSkillRankText;
    [SerializeField] private TMP_Text _rangedSkillAtkText;
    [SerializeField] private TMP_Text _rangedSkillUpgradeText;

    [Header("AreaSkillInfo")]
    [SerializeField] private GameObject _areaSkillInfo;
    [SerializeField] private TMP_Text _areaSkillTag;
    [SerializeField] private Image _equipAreadSkillImage;
    [SerializeField] private TMP_Text _areaSkillDescriptionText;
    [SerializeField] private TMP_Text _areaSkillLevelText;
    [SerializeField] private TMP_Text _areaSkillRankText;
    [SerializeField] private TMP_Text _areaSkillAtkText;
    [SerializeField] private TMP_Text _areaSkillUpgradeText;
    [SerializeField] private TMP_Text _areaSkillCountText;
    [SerializeField] private TMP_Text _areaSkillIntervalText;

    private void Start()
    {
        _playerData = GameManager.I.DataManager.PlayerData;
    }

    public void SkillText(SkillData skillData)
    {
        _isSkill = true;
        _skillData = skillData;

        _emptyImage.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        _skillLevelText.text = skillData.Level.ToString();
        _skillLevel.SetActive(true);

        string rank;
        if (skillData.Rank == SkillData.SkillRank.B) rank = "B";
        else if (skillData.Rank == SkillData.SkillRank.A) rank = "A";
        else rank = "S";

        if (rank == "B")
        {
            _skillRankText.color = new Color(25 / 255f, 144 / 255f, 0 / 255f, 255 / 255f);
        }
        else if(rank == "A")
        {
            _skillRankText.color = new Color(255 / 255f, 16 / 255f, 0 / 255f, 255 / 255f);
        }
        else
        {
            _skillRankText.color = new Color(244 / 255f, 255 / 255f, 40 / 255f, 255 / 255f);
        }
        _skillRankText.text = rank;
        _skillRank.SetActive(true);

        if (skillData.IsEquip) _equip.SetActive(true);
        else _equip.SetActive(false);
    }

    public void SkillEmpty()
    {
        _isSkill = false;

        _emptyImage.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
        _skillLevel.SetActive(false);
        _skillRank.SetActive(false);
        _equip.SetActive(false);
    }

    public void EquipSkillText(SkillData skillData)
    {
        _skillLevelText.text = skillData.Level.ToString();

        string rank;
        if (skillData.Rank == SkillData.SkillRank.B) rank = "B";
        else if (skillData.Rank == SkillData.SkillRank.A) rank = "A";
        else rank = "S";

        if (rank == "B")
        {
            _skillRankText.color = new Color(25 / 255f, 144 / 255f, 0 / 255f, 255 / 255f);
        }
        else if (rank == "A")
        {
            _skillRankText.color = new Color(255 / 255f, 16 / 255f, 0 / 255f, 255 / 255f);
        }
        else
        {
            _skillRankText.color = new Color(244 / 255f, 255 / 255f, 40 / 255f, 255 / 255f);
        }
        _skillRankText.text = rank;
    }

    public void SkillInfoButton()
    {
        if (!_isSkill)
        {
            GameManager.I.SoundManager.StartSFX("ButtonClickMiss");
            return;
        }

        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _skillInventory.InfoSkillData = _skillData;

        string rank;
        if (_skillData.Rank == SkillData.SkillRank.B) rank = "B";
        else if (_skillData.Rank == SkillData.SkillRank.A) rank = "A";
        else rank = "S";

        if (_skillData.Type == SkillData.SkillType.Melee)
        {
            if(_skillData.Level >= 2)
            {
                _meleeSkillLevelText.color = new Color(221 / 255f, 160 / 255f, 26 / 255f, 255 / 255f);
                _meleeSkillAtkText.color = new Color(221 / 255f, 160 / 255f, 26 / 255f, 255 / 255f);
            }
            else
            {
                _meleeSkillLevelText.color = new Color(64 / 255f, 64 / 255f, 75 / 255f, 255 / 255f);
                _meleeSkillAtkText.color = new Color(64 / 255f, 64 / 255f, 75 / 255f, 255 / 255f);
            }
            _meleeSkillTag.text = _skillData.Tag;
            _equipMeleeSkillImage.sprite = Resources.Load<Sprite>(_skillData.IconPath);
            _meleeSkillDescriptionText.text = _skillData.Description;
            _meleeSkillLevelText.text = _skillData.Level.ToString();
            _meleeSkillRankText.text = rank;
            _meleeSkillAtkText.text = ((int)(_playerData.Atk * _skillData.AtkRatio)).ToString();
            _meleeSkillUpgradeText.text = _skillData.CurrentUpgradeCount.ToString() + " / " + _skillData.MaxUpgradeCount.ToString();

            _meleeSkillInfo.SetActive(true);
        }
        else if(_skillData.Type == SkillData.SkillType.Ranged)
        {
            if(_skillData.Level >= 2)
            {
                _rangedSkillLevelText.color = new Color(221 / 255f, 160 / 255f, 26 / 255f, 255 / 255f);
                _rangedSkillAtkText.color = new Color(221 / 255f, 160 / 255f, 26 / 255f, 255 / 255f);
            }
            else
            {
                _rangedSkillLevelText.color = new Color(64 / 255f, 64 / 255f, 75 / 255f, 255 / 255f);
                _rangedSkillAtkText.color = new Color(64 / 255f, 64 / 255f, 75 / 255f, 255 / 255f);
            }
            _rangedSkillTag.text = _skillData.Tag;
            _equipRangedSkillImage.sprite = Resources.Load<Sprite>(_skillData.IconPath);
            _rangedSkillDescriptionText.text = _skillData.Description;
            _rangedSkillLevelText.text = _skillData.Level.ToString();
            _rangedSkillRankText.text = rank;
            _rangedSkillAtkText.text = ((int)(_playerData.Atk * _skillData.AtkRatio)).ToString();
            _rangedSkillUpgradeText.text = _skillData.CurrentUpgradeCount.ToString() + " / " + _skillData.MaxUpgradeCount.ToString();

            _rangedSkillInfo.SetActive(true);
        }
        else
        {
            if(_skillData.Level >= 2)
            {
                _areaSkillLevelText.color = new Color(221 / 255f, 160 / 255f, 26 / 255f, 255 / 255f);
                _areaSkillAtkText.color = new Color(221 / 255f, 160 / 255f, 26 / 255f, 255 / 255f);
                _areaSkillCountText.color = new Color(221 / 255f, 160 / 255f, 26 / 255f, 255 / 255f);
                _areaSkillIntervalText.color = new Color(221 / 255f, 160 / 255f, 26 / 255f, 255 / 255f);
            }
            else
            {
                _areaSkillLevelText.color = new Color(64 / 255f, 64 / 255f, 75 / 255f, 255 / 255f);
                _areaSkillAtkText.color = new Color(64 / 255f, 64 / 255f, 75 / 255f, 255 / 255f);
                _areaSkillCountText.color = new Color(64 / 255f, 64 / 255f, 75 / 255f, 255 / 255f);
                _areaSkillIntervalText.color = new Color(64 / 255f, 64 / 255f, 75 / 255f, 255 / 255f);
            }
            _areaSkillTag.text = _skillData.Tag;
            _equipAreadSkillImage.sprite = Resources.Load<Sprite>(_skillData.IconPath);
            _areaSkillDescriptionText.text = _skillData.Description;
            _areaSkillLevelText.text = _skillData.Level.ToString();
            _areaSkillRankText.text = rank;
            _areaSkillAtkText.text = ((int)(_playerData.Atk * _skillData.AtkRatio)).ToString();
            _areaSkillUpgradeText.text = _skillData.CurrentUpgradeCount.ToString() + " / " + _skillData.MaxUpgradeCount.ToString();
            _areaSkillCountText.text = _skillData.Count.ToString();
            _areaSkillIntervalText.text = _skillData.Interval.ToString();

            _areaSkillInfo.SetActive(true);
        }
    }

    public void SkillInfoExitButton()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _meleeSkillInfo.SetActive(false);
        _rangedSkillInfo.SetActive(false);
        _areaSkillInfo.SetActive(false);
    }
}
