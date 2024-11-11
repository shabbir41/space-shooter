using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 5.0f;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;
    [SerializeField]
    private AudioClip _clip;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is Null");
        }
        _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null)
        {
            Debug.LogError("Audio Source is NULL.");
        } else
        {
            _audioSource.clip = _clip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            GameObject explosionObj = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            if(explosionObj != null)
            {
                Destroy(other.gameObject);
                _audioSource.Play();
                Destroy(explosionObj, 2.5f);
                _spawnManager.StartSpawning();
                Destroy(this.gameObject, 0.5f);
            }

        }
    }
}
