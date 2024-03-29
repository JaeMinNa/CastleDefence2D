using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInventory : MonoBehaviour
{
    [field:SerializeField] public PlayerSO PlayerSO;

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

    private List<SkillSO> _skills;
    private SkillInventorySlot _skillInventorySlot;

    private void Awake()
    {
        _skills = new List<SkillSO>();
    }

    void Start()
    {
        MeleeButton();
    }

    void Update()
    {
        
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
        for (int i = 0; i < PlayerSO.SkillInventroy.Count; i++)
        {
            if(PlayerSO.SkillInventroy[i].Type == SkillSO.SkillType.Melee)
            {
                _skills.Add(PlayerSO.SkillInventroy[i]);
            }
        }

        for (int i = 0; i < _skills.Count; i++)
        {
            _meleeSlotContent.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = _skills[i].Icon;

            _skillInventorySlot = _meleeSlotContent.transform.GetChild(i).GetComponent<SkillInventorySlot>();

            string rank;
            if (_skills[i].Rank == SkillSO.SkillRank.B) rank = "B";
            else if (_skills[i].Rank == SkillSO.SkillRank.A) rank = "A";
            else rank = "S";

            _skillInventorySlot.SkillText(_skills[i].Level, rank, _skills[i].IsEquip);
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
        for (int i = 0; i < PlayerSO.SkillInventroy.Count; i++)
        {
            if (PlayerSO.SkillInventroy[i].Type == SkillSO.SkillType.Ranged)
            {
                _skills.Add(PlayerSO.SkillInventroy[i]);
            }
        }

        for (int i = 0; i < _skills.Count; i++)
        {
            _rangedSlotContent.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = _skills[i].Icon;
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
        for (int i = 0; i < PlayerSO.SkillInventroy.Count; i++)
        {
            if (PlayerSO.SkillInventroy[i].Type == SkillSO.SkillType.Area)
            {
                _skills.Add(PlayerSO.SkillInventroy[i]);
            }
        }

        for (int i = 0; i < _skills.Count; i++)
        {
            _areaSlotContent.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = _skills[i].Icon;
        }

        _skills.Clear();
    }
}
