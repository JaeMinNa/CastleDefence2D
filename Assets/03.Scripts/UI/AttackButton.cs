using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    [HideInInspector] public float ClickTime; 
    private bool _isClick; 
    private GameObject _player;

    // 버튼 클릭이 시작했을 때
    public void ButtonDown()
    {
        _player.GetComponent<PlayerController>().PlayerSO.Speed *= -1;

        _isClick = true;
    }

    // 버튼 클릭이 끝났을 때
    public void ButtonUp()
    {
        _isClick = false;

        if (ClickTime >= GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().PlayerSO.SkillTime)
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
        if (_isClick)
        {
            ClickTime += Time.deltaTime;
        }
        else
        {
            ClickTime = 0;
        }
    }
}
