using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private float _speedMultiplier = 2.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldPrefab;

    [SerializeField]
    private float _fireRate = 0.3f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score;
    private bool _isTripleShot = false;
    private bool _isSpeedBoost = false;
    private bool _isShieldActive = false;

    private SpawnManager _spawnManager;
    private UIManager _uiManager;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && (Time.time > _canFire)){
            FireLaser();
        }
    }

    void CalculateMovement(){
                float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x > 11.3f) {
            transform.position = new Vector3(-11.3f, transform.position.y, transform.position.z);
        } else if (transform.position.x < -11.3f) {
            transform.position = new Vector3(11.3f, transform.position.y, transform.position.z);
        }
    }

    void FireLaser() {
        _canFire = Time.time + _fireRate;
        if (_isTripleShot)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        } else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
        }
    }

    public void Damage(){
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldPrefab.SetActive(false);
            return;
        }
        _lives--;
        _uiManager.UpdateLives(_lives);

        if (_lives <= 0){
            _spawnManager.onPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void EnableTripleShot()
    {
        _isTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void EnableSpeedBoost()
    {
        _isSpeedBoost = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostCoroutine());
    }

    public void EnableShield()
    {
        _isShieldActive = true;
        _shieldPrefab.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score = _score + points;
        if (_uiManager != null)
        {
            _uiManager.UpdateScore(_score);
        }
        if( _score % 200 == 0)
        {
            _spawnManager.increaseLevel();
        }
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShot = false;
    }

    IEnumerator SpeedBoostCoroutine()
    {
        yield return new WaitForSeconds(5.0f);
        if(_isSpeedBoost == true)
        {
            _isSpeedBoost = false;
            _speed /= _speedMultiplier;
        }

    }
}
