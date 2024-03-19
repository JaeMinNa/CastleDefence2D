using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    public float ClickTime { get; private set; } 
    public bool IsClick { get; private set; }
    private GameObject _player;

    // 버튼 클릭이 시작했을 때
    public void ButtonDown()
    {
        _player.GetComponent<PlayerController>().PlayerSO.Speed *= -1;

        IsClick = true;
    }

    // 버튼 클릭이 끝났을 때
    public void ButtonUp()
    {
        IsClick = false;

        if (ClickTime >= _player.GetComponent<PlayerController>().PlayerSO.SkillTime)
        {
            Debug.Log("스킬 발동!");
        }
    }

    private void Start()
    {
        _player = GameManager.I.PlayerManager.Player;
    }

    private void Update()
    {
        if (IsClick)
        {
            ClickTime += Time.deltaTime;
            if(ClickTime >= (_player.GetComponent<PlayerController>().PlayerSO.SkillTime / 4f))
            _player.GetComponent<PlayerAttackState>().time = 0f;
        }
        else
        {
            ClickTime = 0;
        }
    }
}
