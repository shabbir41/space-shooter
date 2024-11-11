using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * 4.0f * Time.deltaTime);

        if(transform.position.y <= -4.7f)
        {
            Destroy(this.gameObject);
        }
    }
}
