using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class CryptidScript : NetworkBehaviour
{
    public float maxHealth;

    [Networked]
    public float netHealth {get; set;}

    public float health;

    public static CryptidScript Local { get; set; }

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



    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {

            RPC_ChangeHealthToMax();


        }
        else
        {
            RPC_SendHealth();
            netHealth = health;

        }
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SendHealth()
    {
        Debug.Log("rpc health:" + netHealth);
        health = netHealth;
    }


    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_ChangeHealthToMax()
    {
        Debug.Log("running change health rpc");
        netHealth = maxHealth;
    }
    /*
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SendCrypids()
    {
           for(int i=0; i<crypids.Length; i++)
        {
            crypidstemp[i] = GameObject.FindGameObjectWithTag("player"+i.ToString()).GetComponent<CryptidScript>();
        }
    }

    
    [Rpc(RpcSources.Proxies, RpcTargets.InputAuthority)]
    public void RPC_SetPlayerTarget(CryptidScript otherPlayer)
    {
        player2 = otherPlayer;
    }
    */


    // Update is called once per frame
    void Update()
    {

    }

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("redAttack1"))
        {
            onHit(redAttackPower);
        }
        else if (collision.gameObject.CompareTag("blueAttack1"))
        {
            onHit(blueAttackPower);
        }
    }

    void onHit(float attackPower)
    {
        netHealth -= attackPower;
        healhBar.value = netHealth / maxHealth;
        if (netHealth <= 0)
        {
            cih.canInput = false;
            wholePlayer.GetComponent<CharacterController>().Move(new Vector3(0, 0, -100));
            wholePlayer.GetComponent<CharacterController>().Move(getRespawnVector());
            wholePlayer.GetComponent<CharacterController>().Move(new Vector3(0, 0, 100));
            //this.GetComponent<Collider2D>().gameObject.SetActive(true);
            cih.canInput = true;
            netHealth = maxHealth;
            healhBar.value = netHealth / maxHealth;
        }
        Debug.Log(netHealth);
    }

    /*
    void onHitRomance(float attackPower)
    {
        romance -= attackPower;
        romanceBar.value = netHealth / maxHealth;
        if(romance == 1000)
        {
            //implement thing
        }
        Debug.Log(romance);
    }
    */

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
