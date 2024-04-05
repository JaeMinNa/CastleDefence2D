using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float _originAngle;
    private CastleData _castleData;
    

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _castleData = GameManager.I.DataManager.CastleData;
        _rigidbody.AddForce(transform.right * _castleData.Speed, ForceMode2D.Impulse);
        _originAngle = transform.rotation.eulerAngles.z;
    }

    private void OnEnable()
    {
        if (_rigidbody != null)
        {
            transform.rotation = Quaternion.Euler(0, 0, _originAngle);
            _rigidbody.AddForce(transform.right * _castleData.Speed, ForceMode2D.Impulse);
        }
    }

    private void Update()
    {
        transform.right = _rigidbody.velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            GameManager.I.SoundManager.StartSFX("ArrowHit", collision.transform.position);
            collision.transform.GetComponent<EnemyController>().Ishit = true;
            Vector2 _dir = collision.transform.position - transform.position;
            collision.transform.GetComponent<EnemyController>().Hp -= _castleData.Atk;
            if (_dir.x > 0)
            {
                GameManager.I.ObjectPoolManager.ActiveDamage("DamageText", collision.transform.position - new Vector3(0, 2, 0), (int)_castleData.Atk, 255);
                collision.GetComponent<EnemyController>().Rigdbody.AddForce(new Vector2(1, 1) * _castleData.NuckbackPower, ForceMode2D.Impulse);
            }
            else
            {
                GameManager.I.ObjectPoolManager.ActiveDamage("DamageText", collision.transform.position - new Vector3(0, 2, 0), (int)_castleData.Atk, 255);
                collision.GetComponent<EnemyController>().Rigdbody.AddForce(new Vector2(-1, 1) * _castleData.NuckbackPower, ForceMode2D.Impulse);
            }
            gameObject.SetActive(false);
        }
        else if(collision.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }
    }
}
