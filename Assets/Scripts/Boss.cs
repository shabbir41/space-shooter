using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;
    private bool _moveLeft = true;
    [SerializeField]
    private int _life = 100;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _explosionPrefab;

    private float _fireRate = 2.0f;
    private float _canFire = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= 3.5f)
        {
            moveDown();
        }
        else
        {
            if (_moveLeft)
            {
                moveLeft();
            } else
            {
                moveRight();
            }
        }

        if(Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            GameObject laser = Instantiate(_laserPrefab, transform.position - new Vector3(0, 2.15f, 0), Quaternion.identity);
            laser.GetComponent<Laser>().AssignEnemyLaser();
        }
    }

    void moveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }
    void moveLeft()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
        if ( transform.position.x < -6.3f)
        {
            _moveLeft = false;
        }
    }
    void moveRight()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
        if (transform.position.x > 6.3f)
        {
            _moveLeft = true;
        }
    }

    public void Damage()
    {
        _life -= 10;
        if(_life == 0)
        {
            Destroy(this.gameObject);
            GameObject explosionObj = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            if(_explosionPrefab != null)
            {
                Destroy(_explosionPrefab, 2.5f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Damage();
            Destroy(other.gameObject);
        }
    }
}
