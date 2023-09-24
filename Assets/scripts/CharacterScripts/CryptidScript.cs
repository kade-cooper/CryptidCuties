using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryptidScript : MonoBehaviour
{
    public int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("redAttack1"))
        {
            health -= 10;
            Debug.Log(health);
        }
        else if (collision.gameObject.CompareTag("blueAttack1"))
        {
            health -= 15;
            Debug.Log(health);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("redAttack1"))
        {
            health -= 10;
            Debug.Log(health);
        }
        else if (collision.gameObject.CompareTag("blueAttack1"))
        {
            health -= 15;
            Debug.Log(health);
        }
    }
}
