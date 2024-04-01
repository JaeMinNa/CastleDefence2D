using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillInventorySlot : MonoBehaviour
{
    private SkillSO _skillSO;
    private PlayerSO _playerSO;
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
        _playerSO = GameManager.I.PlayerManager.PlayerPrefab.GetComponent<PlayerController>().PlayerSO;
    }

    public void SkillText(SkillSO skillSO)
    {
        _isSkill = true;
        _skillSO = skillSO;

        _emptyImage.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        _skillLevelText.text = skillSO.Level.ToString();
        _skillLevel.SetActive(true);

        string rank;
        if (skillSO.Rank == SkillSO.SkillRank.B) rank = "B";
        else if (skillSO.Rank == SkillSO.SkillRank.A) rank = "A";
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

        if (skillSO.IsEquip) _equip.SetActive(true);
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

    public void EquipSkillText(SkillSO skillSO)
    {
        _skillLevelText.text = skillSO.Level.ToString();

        string rank;
        if (skillSO.Rank == SkillSO.SkillRank.B) rank = "B";
        else if (skillSO.Rank == SkillSO.SkillRank.A) rank = "A";
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
        _skillInventory.InfoSkillSO = _skillSO;

        string rank;
        if (_skillSO.Rank == SkillSO.SkillRank.B) rank = "B";
        else if (_skillSO.Rank == SkillSO.SkillRank.A) rank = "A";
        else rank = "S";

        if (_skillSO.Type == SkillSO.SkillType.Melee)
        {
            _meleeSkillTag.text = _skillSO.Tag;
            _equipMeleeSkillImage.sprite = _skillSO.Icon;
            _meleeSkillDescriptionText.text = _skillSO.Description;
            _meleeSkillLevelText.text = _skillSO.Level.ToString();
            _meleeSkillRankText.text = rank;
            _meleeSkillAtkText.text = ((int)(_playerSO.Atk * _skillSO.AtkRatio)).ToString();
            _meleeSkillUpgradeText.text = _skillSO.CurrentUpgradeCount.ToString() + " / " + _skillSO.MaxUpgradeCount.ToString();

            _meleeSkillInfo.SetActive(true);
        }
        else if(_skillSO.Type == SkillSO.SkillType.Ranged)
        {
            _rangedSkillTag.text = _skillSO.Tag;
            _equipRangedSkillImage.sprite = _skillSO.Icon;
            _rangedSkillDescriptionText.text = _skillSO.Description;
            _rangedSkillLevelText.text = _skillSO.Level.ToString();
            _rangedSkillRankText.text = rank;
            _rangedSkillAtkText.text = ((int)(_playerSO.Atk * _skillSO.AtkRatio)).ToString();
            _rangedSkillUpgradeText.text = _skillSO.CurrentUpgradeCount.ToString() + " / " + _skillSO.MaxUpgradeCount.ToString();

            _rangedSkillInfo.SetActive(true);
        }
        else
        {
            _areaSkillTag.text = _skillSO.Tag;
            _equipAreadSkillImage.sprite = _skillSO.Icon;
            _areaSkillDescriptionText.text = _skillSO.Description;
            _areaSkillLevelText.text = _skillSO.Level.ToString();
            _areaSkillRankText.text = rank;
            _areaSkillAtkText.text = ((int)(_playerSO.Atk * _skillSO.AtkRatio)).ToString();
            _areaSkillUpgradeText.text = _skillSO.CurrentUpgradeCount.ToString() + " / " + _skillSO.MaxUpgradeCount.ToString();
            _areaSkillCountText.text = _skillSO.Count.ToString();
            _areaSkillIntervalText.text = _skillSO.Interval.ToString();

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
