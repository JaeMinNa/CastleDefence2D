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
    [SerializeField] private SkillData _getSkillData;
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
    private DataWrapper _dataWrapper;

    private void Start()
    {
        _playerData = GameManager.I.DataManager.PlayerData;
        _dataWrapper = GameManager.I.DataManager.DataWrapper;
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


    // 스킬 뽑기
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
        int length = _dataWrapper.SkillData.Length;
        int random1 = Random.Range(0, 4); // Skill Type
        int random2 = Random.Range(1, 101); // Rank
       
        if(random1 == 0) // Melee
        {
            while (true)
            {
                int random3 = Random.Range(0, length);
                if (_dataWrapper.SkillData[random3].Type != SkillData.SkillType.Melee) continue;

                if(_dataWrapper.SkillData[random3].Rank == SkillData.SkillRank.S)
                {
                    if (random2 >= 1 && random2 <= 10)
                    {
                        _getSkillData = _dataWrapper.SkillData[random3];
                        break;
                    }
                    else continue;
                }
                else if(_dataWrapper.SkillData[random3].Rank == SkillData.SkillRank.A)
                {
                    if (random2 >= 11 && random2 <= 35) // A
                    {
                        _getSkillData = _dataWrapper.SkillData[random3];
                        break;
                    }
                    else continue;
                }
                else
                {
                    if (random2 >= 36 && random2 <= 100) // B
                    {
                        _getSkillData = _dataWrapper.SkillData[random3];
                        break;
                    }
                    else continue;
                }
            }
        }
        else if(random1 == 1) // Ranged
        {
            while (true)
            {
                int random3 = Random.Range(0, length);
                if (_dataWrapper.SkillData[random3].Type != SkillData.SkillType.Ranged) continue;

                if (_dataWrapper.SkillData[random3].Rank == SkillData.SkillRank.S)
                {
                    if (random2 >= 1 && random2 <= 10)
                    {
                        _getSkillData = _dataWrapper.SkillData[random3];
                        break;
                    }
                    else continue;
                }
                else if (_dataWrapper.SkillData[random3].Rank == SkillData.SkillRank.A)
                {
                    if (random2 >= 11 && random2 <= 35) // A
                    {
                        _getSkillData = _dataWrapper.SkillData[random3];
                        break;
                    }
                    else continue;
                }
                else
                {
                    if (random2 >= 36 && random2 <= 100) // B
                    {
                        _getSkillData = _dataWrapper.SkillData[random3];
                        break;
                    }
                    else continue;
                }
            }
        }
        else // Area
        {
            while (true)
            {
                int random3 = Random.Range(0, length);
                if (_dataWrapper.SkillData[random3].Type != SkillData.SkillType.Area) continue;

                if (_dataWrapper.SkillData[random3].Rank == SkillData.SkillRank.S)
                {
                    if (random2 >= 1 && random2 <= 10)
                    {
                        _getSkillData = _dataWrapper.SkillData[random3];
                        break;
                    }
                    else continue;
                }
                else if (_dataWrapper.SkillData[random3].Rank == SkillData.SkillRank.A)
                {
                    if (random2 >= 11 && random2 <= 35) // A
                    {
                        _getSkillData = _dataWrapper.SkillData[random3];
                        break;
                    }
                    else continue;
                }
                else
                {
                    if (random2 >= 36 && random2 <= 100) // B
                    {
                        _getSkillData = _dataWrapper.SkillData[random3];
                        break;
                    }
                    else continue;
                }
            }
        }

        _getSkillData.CurrentUpgradeCount++;
        _getSkillTagText.text = _getSkillData.Tag;
        _getSkillTypeText.text = _getSkillData.Type.ToString();
        _getSkillImage.sprite = Resources.Load<Sprite>(_getSkillData.IconPath);

        if (_getSkillData.Rank == SkillData.SkillRank.B)
        {
            _getSkillRankText.color = new Color(25 / 255f, 144 / 255f, 0 / 255f, 255 / 255f);
        }
        else if (_getSkillData.Rank == SkillData.SkillRank.A)
        {
            _getSkillRankText.color = new Color(255 / 255f, 16 / 255f, 0 / 255f, 255 / 255f);
        }
        else
        {
            _getSkillRankText.color = new Color(244 / 255f, 255 / 255f, 40 / 255f, 255 / 255f);
        }
        _getSkillRankText.text = _getSkillData.Rank.ToString();

        _getSkillDescriptionText.text = _getSkillData.Description;
        if(!_getSkillData.IsGet)
        {
            _playerData.SkillInventory.Add(_getSkillData);
            _getSkillData.IsGet = true;
            _text1.text = "새로운 스킬을 뽑았습니다.";
            _text2.text = "";
        }
        else
        {
            if(_getSkillData.CurrentUpgradeCount < _getSkillData.MaxUpgradeCount)
            {
                _text1.text = "이미 획득한 스킬을 뽑았습니다.";
                _text2.text = "같은 스킬을 " + (_getSkillData.MaxUpgradeCount - _getSkillData.CurrentUpgradeCount).ToString() + "번 더 뽑으면 자동 강화합니다.";
            }
            else
            {
                _getSkillData.CurrentUpgradeCount = _getSkillData.CurrentUpgradeCount - _getSkillData.MaxUpgradeCount + 1;
                _getSkillData.MaxUpgradeCount *= 2;
                _getSkillData.Level++;
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
        if(_getSkillData.Type == SkillData.SkillType.Melee || _getSkillData.Type == SkillData.SkillType.Ranged)
        {
            _getSkillData.AtkRatio += 0.3f;
        }
        else
        {
            _getSkillData.AtkRatio += 0.3f;
            _getSkillData.Count++;

            if(_getSkillData.Interval >= 0.3f)
            {
                _getSkillData.Interval -= 0.1f;
            }
        }
    }
}
