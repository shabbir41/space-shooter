using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4.0f;
    private Player _player;

    private float _fireRate = 1.0f;
    private float _canFire = -1f;
    [SerializeField]
    private GameObject _enemyLaserPrefab;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();

        if(Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            Instantiate(_enemyLaserPrefab, transform.position + new Vector3(0, -1.9f, 0), Quaternion.identity);
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
        transform.position = new Vector3(Random.Range(-11f, 11f), 7, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player"){
            Player player = other.transform.GetComponent<Player>();
            if(player != null){
                player.Damage();
            }
            Destroy(this.gameObject);
        }
        else if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.AddScore(10);
            }
            Destroy(this.gameObject);
        }

        
    }

}
