using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 3.0f;
    private Player _player;
    private Animator _mAnimator;
    [SerializeField]
    private AudioClip _enemyExplosion;
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _enemyLaserPrefab;

    private float _fireRate = 3.0f;
    private float _canFire = -1;

    [SerializeField]
    private bool _isBossCommander = false;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is Null.");
        }
        _mAnimator = gameObject.GetComponent<Animator>();
        if (_mAnimator == null)
        {
            Debug.LogError("Animator is Null.");
        }
        _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null)
        {
            Debug.LogError("Audio Source is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();
        if (_isBossCommander)
        {
            if (Time.time > _canFire)
            {
                _canFire = Time.time + _fireRate;
                GameObject laser = Instantiate(_enemyLaserPrefab, transform.position - new Vector3(0, 2.15f, 0), Quaternion.identity);
                laser.GetComponent<Laser>().AssignEnemyLaser();
            }
        }
    }

    void calculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -3.8f)
        {
            randomSpawn();
        }
    }

    void randomSpawn(){
        transform.position = new Vector3(Random.Range(-10f, 10f), 7, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _audioSource.clip = _enemyExplosion;
        if (other.tag == "Player"){
            Player player = other.transform.GetComponent<Player>();
            if(player != null && _speed != 0.0)
            {
                player.Damage();
            }
            _mAnimator.SetTrigger("OnEnemyDeath");
            _speed = 0.0f;
            _audioSource.Play();
            Destroy(this.gameObject, 2.5f);
        }
        else if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.AddScore(10);
            }
            _mAnimator.SetTrigger("OnEnemyDeath");
            Destroy(GetComponent<Collider2D>());
            _speed = 0.0f;
            _audioSource.Play();
            Destroy(this.gameObject, 2.5f);
        }

    }

    public void SetBossCommander()
    {
        _isBossCommander = true;
    }

}
