using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{
    private PlayerController _playerController;
    private SkillSO _meleeSkillSO;
    private CameraShake _cameraShake;

    private void Start()
    {
        _playerController = GameManager.I.PlayerManager.Player.GetComponent<PlayerController>();
        _meleeSkillSO = _playerController.PlayerSO.MeleeSkill;
        _cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Vector2 _dir = collision.transform.position - _playerController.transform.position;
            collision.transform.GetComponent<EnemyController>().Ishit = true;

            if(transform.CompareTag("AttackCollider"))
            {
                GameManager.I.ObjectPoolManager.ActiveDamage("DamageText", collision.transform.position - new Vector3(0, 2, 0), (int)_playerController.PlayerSO.Atk, 255);
                collision.transform.GetComponent<EnemyController>().Hp -= _playerController.PlayerSO.Atk;
                if (_dir.x > 0)
                {
                    collision.GetComponent<EnemyController>().Rigdbody.AddForce(new Vector2(1, 1) * _playerController.PlayerSO.NuckbackPower, ForceMode2D.Impulse);
                }
                else
                {
                    collision.GetComponent<EnemyController>().Rigdbody.AddForce(new Vector2(-1, 1) * _playerController.PlayerSO.NuckbackPower, ForceMode2D.Impulse);
                }
            }
            else if(transform.CompareTag("MeleeCollider"))
            {
                StartCoroutine(_cameraShake.COShake(1f, 1.5f));
                GameManager.I.ObjectPoolManager.ActiveDamage("DamageText", collision.transform.position - new Vector3(0, 2, 0), (int)_meleeSkillSO.Atk, 31);
                collision.transform.GetComponent<EnemyController>().Hp -= _meleeSkillSO.Atk;
                if (_dir.x > 0)
                {
                    collision.GetComponent<EnemyController>().Rigdbody.AddForce(new Vector2(1, 1) * _meleeSkillSO.NuckbackPower, ForceMode2D.Impulse);
                }
                else
                {
                    collision.GetComponent<EnemyController>().Rigdbody.AddForce(new Vector2(-1, 1) * _meleeSkillSO.NuckbackPower, ForceMode2D.Impulse);
                }
            }

            Debug.Log("Àû Hp : " + collision.transform.GetComponent<EnemyController>().Hp);
        }
    }
}
