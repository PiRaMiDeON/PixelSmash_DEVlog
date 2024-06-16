using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class EnemiesController : MonoBehaviour
{
    private List<GameObject> _enemies;

    public GameObject[] EnemiesPattern;
    public GameObject CurrentEnemy;

    private List<int> _bannedIndexes;
    private int _enemyIndex;

    private bool _enemiesOver;

    public void SpawnEnemy()
    {
        if(_enemies == null)
        {
            _enemies = EnemiesPattern.ToList();
        }

        do
        {
            _enemyIndex = Random.Range(0, EnemiesPattern.Length);
        } while (!CheckEnemyIndex(_enemyIndex));

        CurrentEnemy = Instantiate(_enemies[_enemyIndex], _enemies[_enemyIndex].transform.localPosition, Quaternion.identity);
        CurrentEnemy.GetComponent<Enemy>().EnemiesController = this;
    }

    public IEnumerator SpawnNewEnemy(float time)
    {
        yield return new WaitForSeconds(time);

        _enemies.Remove(CurrentEnemy);

        if(_enemies == null)
        {
            _enemiesOver = true;
            yield break;
        }

        SpawnEnemy();
    }

    private bool CheckEnemyIndex(int index)
    {
        if (_bannedIndexes.Count == EnemiesPattern.Length)
        {
            _bannedIndexes.Clear();
            return true;
        }

        for (int i = 0; i < _bannedIndexes.Count; i++)
        {
            if (index == _bannedIndexes[i])
            {
                return false;
            }
        }

        _bannedIndexes.Add(index);
        return true;
    }

    public bool EnemyIsOver()
    {
        return _enemiesOver;
    }
}
