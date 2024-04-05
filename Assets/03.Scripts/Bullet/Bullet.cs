using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum EnemyTag
    {
        Plant,
        Trunk,
        Radish,
        Skull,
    }

    private CastleController _castleController;
    private SpriteRenderer _bulletSpriteRenderer;
    private StageController _stageController;
    private DataWrapper _dataWrapper;
    private EnemyData _enemyData;
    public EnemyTag Tag;

    void Start()
    {
        _castleController = GameObject.FindWithTag("Castle").GetComponent<CastleController>();
        _bulletSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _stageController = GameObject.FindWithTag("StageController").GetComponent<StageController>();
        _dataWrapper = GameManager.I.DataManager.DataWrapper;

        switch (Tag)
        {
            case EnemyTag.Plant:
                _enemyData = _dataWrapper.EnemyData[7];
                break;
            case EnemyTag.Trunk:
                _enemyData = _dataWrapper.EnemyData[8];
                break;
            case EnemyTag.Radish:
                _enemyData = _dataWrapper.EnemyData[9];
                break;
            case EnemyTag.Skull:
                _enemyData = _dataWrapper.EnemyData[10];
                break;
            default:
                break;
        }

        if (transform.position.x > 0)
        {
            _bulletSpriteRenderer.flipX = true;
        }
        else
        {
            _bulletSpriteRenderer.flipX = true;
        }
    }

    private void OnEnable()
    {
        if (_castleController != null)
        {
            if (transform.position.x > 0)
            {
                _bulletSpriteRenderer.flipX = true;
            }
            else
            {
                _bulletSpriteRenderer.flipX = true;
            }
        }
    }

    void Update()
    {
        if(transform.position.x > 0)
        {
            transform.position += new Vector3(-_enemyData.BulletSpeed, 0, 0) * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(_enemyData.BulletSpeed, 0, 0) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Castle"))
        {
            if(_stageController.IsDangerTime) _castleController.CastleHit(_enemyData.Atk * _stageController.DangerTimeAtkRatio);
            else _castleController.CastleHit(_enemyData.Atk);
            gameObject.SetActive(false);
        }
    }
}
