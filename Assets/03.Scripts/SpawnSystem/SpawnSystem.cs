using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [System.Serializable]
    public struct StageInfo
    {
        public int Stage;
        public string[] enemys; // enemy + 생성되는 시간 입력 ex) "Snail 5"
    }
    public List<StageInfo> Stages;

    private int _currentStage;
    private string _enemy;
    private int _spawnTime;

    [SerializeField] private Transform _spawnLeft;
    [SerializeField] private Transform _spawnRigth;

    private void Start()
    {
        _currentStage = GameManager.I.DataManager.GameDataSO.Stage;
        for (int i = 0; i < Stages[_currentStage - 1].enemys.Length; i++)
        {
            string[] words = Stages[_currentStage - 1].enemys[i].Split(' ');
            _enemy = words[0];
            _spawnTime = int.Parse(words[1]);

            StartCoroutine(COSpawnEnemy(_enemy, _spawnTime));
        }
    }

    IEnumerator COSpawnEnemy(string enemy, int time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);

            int random = Random.Range(0, 2);
            if (random == 0) GameManager.I.ObjectPoolManager.InactivePrefab(enemy, _spawnLeft.position);
            else GameManager.I.ObjectPoolManager.InactivePrefab(enemy, _spawnRigth.position);
        }
    }
}
