using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillInventorySlot : MonoBehaviour
{
    [SerializeField] private TMP_Text _skillLevelText;
    [SerializeField] private TMP_Text _skillRankText;
    [SerializeField] private GameObject _skillLevel;
    [SerializeField] private GameObject _skillRank;
    [SerializeField] private GameObject _equip;
    [SerializeField] private Image _emptyImage;
    
    public void SkillText(int level, string rank, bool isEquip)
    {
        _emptyImage.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        _skillLevelText.text = level.ToString();
        _skillLevel.SetActive(true);

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

        if (isEquip) _equip.SetActive(true);
        else _equip.SetActive(false);
    }

    public void SkillEmpty()
    {
        _emptyImage.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
        _skillLevel.SetActive(false);
        _skillRank.SetActive(false);
        _equip.SetActive(false);
    }

}
