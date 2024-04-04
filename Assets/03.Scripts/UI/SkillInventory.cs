using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInventory : MonoBehaviour
{
    private PlayerData _playerData;

    [Header("Equip Skill")]
    [SerializeField] private GameObject _equipSlotContent;
    [SerializeField] private Image _equipMeleeSkill;
    [SerializeField] private Image _equipRangedSkill;
    [SerializeField] private Image _equipAreaSkill;

    [Header("Melee Skill")]
    [SerializeField] private GameObject _meleeSkillScroll;
    [SerializeField] private GameObject _meleeSlotContent;
    [SerializeField] private Image _meleeButton;

    [Header("Ranged Skill")]
    [SerializeField] private GameObject _rangedSkillScroll;
    [SerializeField] private GameObject _rangedSlotContent;
    [SerializeField] private Image _rangedButton;

    [Header("Area Skill")]
    [SerializeField] private GameObject _areaSkillScroll;
    [SerializeField] private GameObject _areaSlotContent;
    [SerializeField] private Image _areaButton;

    [Header("Skill Info")]
    [SerializeField] private GameObject[] _skillInfo;

    private List<SkillSO> _skills;
    private SkillInventorySlot _skillInventorySlot;
    [HideInInspector] public SkillSO InfoSkillSO;

    private void Awake()
    {
        _skills = new List<SkillSO>();
    }

    void Start()
    {
        _playerData = GameManager.I.DataManager.PlayerData;
        MeleeButton();
        ShowEquipSkill();
    }

    private void OnEnable()
    {
        if(_playerData != null)
        {
            MeleeButton();
            ShowEquipSkill();
        }
    }

    public void MeleeButton()
    {
        GameManager.I.SoundManager.StartSFX("UIClick");
        UpdateMeleeSKillInventory();
        _meleeButton.color = new Color(224 / 255f, 224 / 255f, 224 / 255f, 255 / 255f);
        _rangedButton.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        _areaButton.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        _rangedSkillScroll.SetActive(false);
        _areaSkillScroll.SetActive(false);
        _meleeSkillScroll.SetActive(true);
    }

    private void UpdateMeleeSKillInventory()
    {
        for (int i = 0; i < _meleeSlotContent.transform.childCount; i++)
        {
            _skillInventorySlot = _meleeSlotContent.transform.GetChild(i).GetComponent<SkillInventorySlot>();
            _skillInventorySlot.SkillEmpty();
        }

        for (int i = 0; i < _playerData.SkillInventory.Count; i++)
        {
            if(_playerData.SkillInventory[i].Type == SkillSO.SkillType.Melee)
            {
                _skills.Add(_playerData.SkillInventory[i]);
            }
        }

        for (int i = 0; i < _skills.Count; i++)
        {
            _meleeSlotContent.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = _skills[i].Icon;
            _skillInventorySlot = _meleeSlotContent.transform.GetChild(i).GetComponent<SkillInventorySlot>();
            _skillInventorySlot.SkillText(_skills[i]);
        }

        _skills.Clear();
    }

    public void RangedButton()
    {
        GameManager.I.SoundManager.StartSFX("UIClick");
        UpdateRangedSKillInventory();
        _meleeButton.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        _rangedButton.color = new Color(224 / 255f, 224 / 255f, 224 / 255f, 255 / 255f);
        _areaButton.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        _meleeSkillScroll.SetActive(false);
        _areaSkillScroll.SetActive(false);
        _rangedSkillScroll.SetActive(true);
    }

    private void UpdateRangedSKillInventory()
    {
        for (int i = 0; i < _rangedSlotContent.transform.childCount; i++)
        {
            _skillInventorySlot = _rangedSlotContent.transform.GetChild(i).GetComponent<SkillInventorySlot>();
            _skillInventorySlot.SkillEmpty();
        }

        for (int i = 0; i < _playerData.SkillInventory.Count; i++)
        {
            if (_playerData.SkillInventory[i].Type == SkillSO.SkillType.Ranged)
            {
                _skills.Add(_playerData.SkillInventory[i]);
            }
        }

        for (int i = 0; i < _skills.Count; i++)
        {
            _rangedSlotContent.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = _skills[i].Icon;
            _skillInventorySlot = _rangedSlotContent.transform.GetChild(i).GetComponent<SkillInventorySlot>();
            _skillInventorySlot.SkillText(_skills[i]);
        }

        _skills.Clear();
    }

    public void AreaButton()
    {
        GameManager.I.SoundManager.StartSFX("UIClick");
        UpdateAreaSKillInventory();
        _meleeButton.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        _rangedButton.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        _areaButton.color = new Color(224 / 255f, 224 / 255f, 224 / 255f, 255 / 255f);
        _meleeSkillScroll.SetActive(false);
        _rangedSkillScroll.SetActive(false);
        _areaSkillScroll.SetActive(true);
    }

    private void UpdateAreaSKillInventory()
    {
        for (int i = 0; i < _areaSlotContent.transform.childCount; i++)
        {
            _skillInventorySlot = _areaSlotContent.transform.GetChild(i).GetComponent<SkillInventorySlot>();
            _skillInventorySlot.SkillEmpty();
        }

        for (int i = 0; i < _playerData.SkillInventory.Count; i++)
        {
            if (_playerData.SkillInventory[i].Type == SkillSO.SkillType.Area)
            {
                _skills.Add(_playerData.SkillInventory[i]);
            }
        }

        for (int i = 0; i < _skills.Count; i++)
        {
            _areaSlotContent.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = _skills[i].Icon;
            _skillInventorySlot = _areaSlotContent.transform.GetChild(i).GetComponent<SkillInventorySlot>();
            _skillInventorySlot.SkillText(_skills[i]);
        }

        _skills.Clear();
    }

    public void ShowEquipSkill()
    {
        _equipMeleeSkill.sprite = _playerData.EquipMeleeSkill.Icon;
        _equipRangedSkill.sprite = _playerData.EquipRangedSkill.Icon;
        _equipAreaSkill.sprite = _playerData.EquipAreaSkill.Icon;

        for (int i = 0; i < 3; i++)
        {
            _skillInventorySlot = _equipSlotContent.transform.GetChild(i).GetComponent<SkillInventorySlot>();

            if(i == 0)
            {
                _skillInventorySlot.EquipSkillText(_playerData.EquipMeleeSkill);
            }
            else if (i == 1)
            {
                _skillInventorySlot.EquipSkillText(_playerData.EquipRangedSkill);
            }
            else
            {
                _skillInventorySlot.EquipSkillText(_playerData.EquipAreaSkill);
            }
        }
    }

    public void EquipButton()
    {
        GameManager.I.SoundManager.StartSFX("Equip");
        if (InfoSkillSO.Type == SkillSO.SkillType.Melee)
        {
            _playerData.EquipMeleeSkill.IsEquip = false;
            _playerData.EquipMeleeSkill = InfoSkillSO;
            InfoSkillSO.IsEquip = true;
            ShowEquipSkill();
            UpdateMeleeSKillInventory();
            ExitSkillInfo();
        }
        else if (InfoSkillSO.Type == SkillSO.SkillType.Ranged)
        {
            _playerData.EquipRangedSkill.IsEquip = false;
            _playerData.EquipRangedSkill = InfoSkillSO;
            InfoSkillSO.IsEquip = true;
            ShowEquipSkill();
            UpdateRangedSKillInventory();
            ExitSkillInfo();
        }
        else
        {
            _playerData.EquipAreaSkill.IsEquip = false;
            _playerData.EquipAreaSkill = InfoSkillSO;
            InfoSkillSO.IsEquip = true;
            ShowEquipSkill();
            UpdateAreaSKillInventory();
            ExitSkillInfo();
        }

        GameManager.I.DataManager.DataSave();
    }

    private void ExitSkillInfo()
    {
        for (int i = 0; i < _skillInfo.Length; i++)
        {
            _skillInfo[i].SetActive(false);
        }
    }
}
