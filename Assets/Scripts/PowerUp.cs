using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y <= -3.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player)
            {
                switch (powerupID)
                {
                    case 0:
                        player.EnableTripleShot();
                        break;
                    case 1:
                        player.EnableSpeedBoost();
                        break;
                    case 2:
                        player.EnableShield();
                        break;
                    default:
                        Debug.Log("Invalid powerupID!!");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
