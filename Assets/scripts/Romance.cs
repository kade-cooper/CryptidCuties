using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Romance : NetworkBehaviour
{
    public float maxRomance = 1000;
    public float romance = 0;
  //  public Slider romanceBar;

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
            
            //here is the where the romance bar starts to build after each successful attack
            romance += 100;
          //  romanceBar.value = romance / maxRomance;
            if (romance == 1000)
            {
                //this allows for the player to use romance 
                //attack2.SetActive(true);
            }
        }
        else if (collision.gameObject.CompareTag("blueAttack1"))
        {
            //here is the where the romance bar starts to build after each successful attack
            romance += 100;
          //  romanceBar.value = romance / maxRomance;
            if (romance == 1000)
            {
                //this allows for the player to use romance 
                
            }
        }
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("redAttack1"))
        {

            //here is the where the romance bar starts to build after each successful attack
            romance += 100;
          //  romanceBar.value = romance / maxRomance;
            if (romance == 1000)
            {
                //this allows for the player to use romance 
                
            }
        }
        else if (collision.gameObject.CompareTag("blueAttack1"))
        {
            //here is the where the romance bar starts to build after each successful attack
            romance += 100;
          //  romanceBar.value = romance / maxRomance;
            if (romance == 1000)
            {
                //this allows for the player to use romance 
                
            }
        }
    }
}
