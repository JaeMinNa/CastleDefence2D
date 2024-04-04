using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillDraw : MonoBehaviour
{
    [Header("SkillDraw")]
    [SerializeField] private GameObject _skillDrawPanel;
    [SerializeField] private TMP_Text _currentSkillDrawCountText;
    [SerializeField] private TMP_Text _currentCoinText;

    [Header("SkillInfo")]
    [SerializeField] private SkillSO _getSkillSO;
    [SerializeField] private GameObject _skillInfoPanel;
    [SerializeField] private TMP_Text _getSkillTagText;
    [SerializeField] private TMP_Text _getSkillTypeText;
    [SerializeField] private Image _getSkillImage;
    [SerializeField] private TMP_Text _getSkillRankText;
    [SerializeField] private TMP_Text _getSkillDescriptionText;
    [SerializeField] private TMP_Text _text1;
    [SerializeField] private TMP_Text _text2;

    [Header("Loby")]
    [SerializeField] private TMP_Text _lobyCurrentSkillDrawCountText;
    [SerializeField] private TMP_Text _lobyCurrentCoinText;

    private PlayerData _playerData;
    private SkillSO[] _meleeSkillData;
    private SkillSO[] _rangedSkillData;
    private SkillSO[] _areaSkillData;

    private void Start()
    {
        _playerData = GameManager.I.DataManager.PlayerData;
        _meleeSkillData = GameManager.I.DataManager.MeleeSkillSO;
        _rangedSkillData = GameManager.I.DataManager.RangedSkillSO;
        _areaSkillData = GameManager.I.DataManager.AreaSkillSO;
        UpdateCoin();
        UpdateSkillDrawCount();
    }


    public void SkillDrawButton()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _skillDrawPanel.SetActive(true);
    }

    public void SkillDrawExitButton()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _skillDrawPanel.SetActive(false);
    }

    public void SkillDrawBuyButton(int count)
    {
        if(count == 1 && GameManager.I.DataManager.GameData.Coin >= 5000)
        {
            GameManager.I.SoundManager.StartSFX("Buy");
            GameManager.I.DataManager.GameData.Coin -= 5000;
            GameManager.I.DataManager.GameData.SkillDrawCount++;
            UpdateSkillDrawCount();
            UpdateCoin();
        }
        else
        {
            GameManager.I.SoundManager.StartSFX("ButtonClickMiss");
        }

        if (count == 10 && GameManager.I.DataManager.GameData.Coin >= 45000)
        {
            GameManager.I.SoundManager.StartSFX("Buy");
            GameManager.I.DataManager.GameData.Coin -= 45000;
            GameManager.I.DataManager.GameData.SkillDrawCount += 10;
            UpdateSkillDrawCount();
            UpdateCoin();
        }
        else
        {
            GameManager.I.SoundManager.StartSFX("ButtonClickMiss");
        }

        GameManager.I.DataManager.DataSave();
    }

    /// <summary>
    /// 스킬 뽑기
    /// </summary>
    public void SkillIInfoButton()
    {
        if(GameManager.I.DataManager.GameData.SkillDrawCount < 1)
        {
            GameManager.I.SoundManager.StartSFX("ButtonClickMiss");
            return;
        }

        GameManager.I.SoundManager.StartSFX("GetSkill");
        GameManager.I.DataManager.GameData.SkillDrawCount--;
        UpdateSkillDrawCount();
        int random1 = Random.Range(0, 4); // Skill Type
        int random2 = Random.Range(1, 101); // Rank
       
        if(random1 == 0) // Melee
        {
            int length = _meleeSkillData.Length;

            while (true)
            {
                int random3 = Random.Range(0, length);

                if(_meleeSkillData[random3].Rank == SkillSO.SkillRank.S)
                {
                    if (random2 >= 1 && random2 <= 10)
                    {
                        _getSkillSO = _meleeSkillData[random3];
                        break;
                    }
                    else continue;
                }
                else if(_meleeSkillData[random3].Rank == SkillSO.SkillRank.A)
                {
                    if (random2 >= 11 && random2 <= 37) // A
                    {
                        _getSkillSO = _meleeSkillData[random3];
                        break;
                    }
                    else continue;
                }
                else
                {
                    if (random2 >= 38 && random2 <= 100) // A
                    {
                        _getSkillSO = _meleeSkillData[random3];
                        break;
                    }
                    else continue;
                }
            }
        }
        else if(random1 == 1) // Ranged
        {
            int length = _rangedSkillData.Length;

            while (true)
            {
                int random3 = Random.Range(0, length);

                if (_rangedSkillData[random3].Rank == SkillSO.SkillRank.S)
                {
                    if (random2 >= 1 && random2 <= 10)
                    {
                        _getSkillSO = _rangedSkillData[random3];
                        break;
                    }
                    else continue;
                }
                else if (_rangedSkillData[random3].Rank == SkillSO.SkillRank.A)
                {
                    if (random2 >= 11 && random2 <= 37) // A
                    {
                        _getSkillSO = _rangedSkillData[random3];
                        break;
                    }
                    else continue;
                }
                else
                {
                    if (random2 >= 38 && random2 <= 100) // A
                    {
                        _getSkillSO = _rangedSkillData[random3];
                        break;
                    }
                    else continue;
                }
            }
        }
        else // Area
        {
            int length = _areaSkillData.Length;

            while (true)
            {
                int random3 = Random.Range(0, length);

                if (_areaSkillData[random3].Rank == SkillSO.SkillRank.S)
                {
                    if (random2 >= 1 && random2 <= 10)
                    {
                        _getSkillSO = _areaSkillData[random3];
                        break;
                    }
                    else continue;
                }
                else if (_areaSkillData[random3].Rank == SkillSO.SkillRank.A)
                {
                    if (random2 >= 11 && random2 <= 37) // A
                    {
                        _getSkillSO = _areaSkillData[random3];
                        break;
                    }
                    else continue;
                }
                else
                {
                    if (random2 >= 38 && random2 <= 100) // A
                    {
                        _getSkillSO = _areaSkillData[random3];
                        break;
                    }
                    else continue;
                }
            }
        }

        _getSkillSO.CurrentUpgradeCount++;
        _getSkillTagText.text = _getSkillSO.Tag;
        _getSkillTypeText.text = _getSkillSO.Type.ToString();
        _getSkillImage.sprite = _getSkillSO.Icon;

        if (_getSkillSO.Rank == SkillSO.SkillRank.B)
        {
            _getSkillRankText.color = new Color(25 / 255f, 144 / 255f, 0 / 255f, 255 / 255f);
        }
        else if (_getSkillSO.Rank == SkillSO.SkillRank.A)
        {
            _getSkillRankText.color = new Color(255 / 255f, 16 / 255f, 0 / 255f, 255 / 255f);
        }
        else
        {
            _getSkillRankText.color = new Color(244 / 255f, 255 / 255f, 40 / 255f, 255 / 255f);
        }
        _getSkillRankText.text = _getSkillSO.Rank.ToString();

        _getSkillDescriptionText.text = _getSkillSO.Description;
        if(!_getSkillSO.IsGet)
        {
            _playerData.SkillInventory.Add(_getSkillSO);
            _getSkillSO.IsGet = true;
            _text1.text = "새로운 스킬을 뽑았습니다.";
            _text2.text = "";
        }
        else
        {
            if(_getSkillSO.CurrentUpgradeCount < _getSkillSO.MaxUpgradeCount)
            {
                _text1.text = "이미 획득한 스킬을 뽑았습니다.";
                _text2.text = "같은 스킬을 " + (_getSkillSO.MaxUpgradeCount - _getSkillSO.CurrentUpgradeCount).ToString() + "번 더 뽑으면 자동 강화합니다.";
            }
            else
            {
                _getSkillSO.CurrentUpgradeCount = _getSkillSO.CurrentUpgradeCount - _getSkillSO.MaxUpgradeCount + 1;
                _getSkillSO.MaxUpgradeCount *= 2;
                _getSkillSO.Level++;
                _text1.text = "이미 획득한 스킬을 뽑았습니다.";
                _text2.text = "스킬을 자동 강화했습니다.";
                UpgradeSkill();
            }
        }


        _skillInfoPanel.SetActive(true);

        GameManager.I.DataManager.DataSave();
    }

    public void SkillInfoExitButton()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _skillInfoPanel.SetActive(false);
    }

    private void UpdateSkillDrawCount()
    {
        _currentSkillDrawCountText.text = GameManager.I.DataManager.GameData.SkillDrawCount.ToString();
        _lobyCurrentSkillDrawCountText.text = GameManager.I.DataManager.GameData.SkillDrawCount.ToString();
    }

    private void UpdateCoin()
    {
        _currentCoinText.text = GameManager.I.DataManager.GameData.Coin.ToString();
        _lobyCurrentCoinText.text = GameManager.I.DataManager.GameData.Coin.ToString();
    }

    private void UpgradeSkill()
    {
        if(_getSkillSO.Type == SkillSO.SkillType.Melee || _getSkillSO.Type == SkillSO.SkillType.Ranged)
        {
            _getSkillSO.AtkRatio += 0.3f;
        }
        else
        {
            _getSkillSO.AtkRatio += 0.3f;
            _getSkillSO.Count++;

            if(_getSkillSO.Interval >= 0.3f)
            {
                _getSkillSO.Interval -= 0.1f;
            }
        }
    }
}
