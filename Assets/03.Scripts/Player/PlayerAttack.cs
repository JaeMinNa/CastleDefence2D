using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float minClickTime = 1; // �ּ� Ŭ���ð�
    private float _clickTime; // Ŭ�� ���� �ð�
    private bool _isClick; // Ŭ�� ������ �Ǵ� 
    private GameObject _player;

    // ��ư Ŭ���� �������� ��
    public void ButtonDown()
    {
        _player.GetComponent<PlayerMove>().PlayerSO.Speed *= -1;

        _isClick = true;
    }

    // ��ư Ŭ���� ������ ��
    public void ButtonUp()
    {
        _isClick = false;

        if (_clickTime >= minClickTime)
        {
            Debug.Log("��ų �ߵ�!");
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
            _clickTime += Time.deltaTime;
        }
        else
        {
            _clickTime = 0;
        }
    }
}
