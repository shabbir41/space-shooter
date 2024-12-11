using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _bossCommanderPrefab;
    [SerializeField]
    private GameObject _bossEnemyPrefab;

    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _bossContainer;
    [SerializeField]
    private GameObject[] _powerups;
    [SerializeField]
    private float _enemySpawnTime = 5.0f;
    private float _bossCommanderSpawnTime = 3.0f;

    private bool _stopSpawningEnemies = false;
    private bool _stopSpawningPowerups = false;
    private bool _stopSpawningBosses = true;
    [SerializeField]
    private int _enemyCount = 0;
    [SerializeField]
    private int _bossCommanderCount = 0;
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
        while (_stopSpawningEnemies == false)
        {
            if(_enemyCount < 30)
            {
                Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
                GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.Euler(0, 180, 0));
                _enemyCount++;
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(_enemySpawnTime);
            }
            else
            {
                _stopSpawningEnemies = true;

                _stopSpawningBosses = false;
                SpawnBossEnemy();
            }
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawningPowerups == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerup = Random.Range(0, 3);
            GameObject newPowerup = Instantiate(_powerups[randomPowerup], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(8f, 13f));
        }
    }

    IEnumerator SpawnBossCommadersRoutine()
    {
        yield return new WaitForSeconds(15.0f);
        while(_stopSpawningBosses == false)
        {
            if(_bossCommanderCount < 20)
            {
                Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
                GameObject newEnemy = Instantiate(_bossCommanderPrefab, posToSpawn, Quaternion.Euler(0, 180, 0));
                newEnemy.GetComponent<Enemy>().SetBossCommander();
                _bossCommanderCount++;
                newEnemy.transform.parent = _bossContainer.transform;
                yield return new WaitForSeconds(_bossCommanderSpawnTime);
            }
            else
            {
                _stopSpawningBosses = true;
                Instantiate(_bossEnemyPrefab, new Vector3(-4.5f, 9, 0), Quaternion.identity);
                Instantiate(_bossEnemyPrefab, new Vector3(4.5f, 9, 0), Quaternion.identity);
            }
        }
    }

    public void onPlayerDeath()
    {
        _stopSpawningEnemies = true;
        _stopSpawningBosses = true;
        _stopSpawningPowerups = true;
    }

    public void SpawnBossEnemy()
    {
        Instantiate(_bossEnemyPrefab, new Vector3(0, 9, 0), Quaternion.identity);
        StartCoroutine(SpawnBossCommadersRoutine());
    }
}
