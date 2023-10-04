using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class CryptidScript : MonoBehaviour
{
    public float maxHealth;
    public float health = 0;

    public float redAttackPower = 10;
    public float blueAttackPower = 15;
    public Slider healhBar;
    public Vector3 spawnpoint;
    public Transform wholePlayer;
    public NetworkPlayer player;

    public CharacterInputHandler cih;
    public CharacterMovementHandler cmh;

    // Start is called before the first frame update
    void Start()
    {
        spawnpoint = player.spawnpoint;
    }
    private void OnEnable()
    {
        startHealth();
    }


    public void startHealth()
    {
        if(cmh.Object.HasInputAuthority)
            health = maxHealth;
        else
        {
            if (health == -1000)
            {
                health = maxHealth;
                Debug.Log("I run");
            }
            else
                health = RPC_SendHealth();
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.Proxies)]
    public float RPC_SendHealth()
    {
            return health;
        Debug.Log("rpc health:"+health);
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("redAttack1"))
        {
            
            health -= redAttackPower;
            healhBar.value = health / maxHealth;
            if (health <= 0)
            {
                Respawn();
            }

            Debug.Log(health);
        }
        else if (collision.gameObject.CompareTag("blueAttack1"))
        {
            health -= blueAttackPower;
            healhBar.value = health / maxHealth;
            if (health <= 0)
            {
                Respawn();
            }
            Debug.Log(health);
        }
    }

    void Respawn()
    {
        cih.canInput = false;
        wholePlayer.GetComponent<CharacterController>().Move(new Vector3(0, 0, -100));
        wholePlayer.GetComponent<CharacterController>().Move(getRespawnVector());
        wholePlayer.GetComponent<CharacterController>().Move(new Vector3(0, 0, 100));
        //this.GetComponent<Collider2D>().gameObject.SetActive(true);
        cih.canInput = true;
        health = maxHealth;
        healhBar.value = health / maxHealth;
    }

    Vector3 getRespawnVector()
    {
        return spawnpoint - wholePlayer.position + new Vector3(0,0,-100);
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if (collision.gameObject.CompareTag("redAttack1"))
        {

            health -= 10;
            healhBar.value = health / maxHealth;
            if (health <= 0)
            {
                health = maxHealth;
                wholePlayer.position = spawnpoint;
            }

            Debug.Log(health);
        }
        else if (collision.gameObject.CompareTag("blueAttack1"))
        {
            health -= 15;
            healhBar.value = health / maxHealth;
            if (health <= 0)
            {
                health = maxHealth;
                wholePlayer.position = spawnpoint;
            }
            Debug.Log(health);
        }
        */
    }
}
