using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerups;
    [SerializeField]
    private float _enemySpawnTime = 5.0f;

    private bool _stopSpawning = false;
    [SerializeField]
    private int _enemyCount = 0;
    // Start is called before the first frame update

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            if(_enemyCount <= 100)
            {
                Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
                GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.Euler(0, 180, 0));
                _enemyCount++;
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(_enemySpawnTime);
            }
            else
            {
                _stopSpawning = true;
            }

        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerup = Random.Range(0, 3);
            GameObject newPowerup = Instantiate(_powerups[randomPowerup], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(8f, 13f));
        }
    }

    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }

    public void increaseLevel()
    {
        _enemySpawnTime -= 0.5f;
        if (_enemySpawnTime == 0.0f)
        {
            _enemySpawnTime = 0.5f;
        }
    }
}
