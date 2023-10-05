using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class playerRomanceHandler : NetworkBehaviour
{
    public static playerRomanceHandler Local { get; set; }




    //romance levels
    [Networked]
    [Capacity(4)]
    public NetworkArray<NetworkString<_64>> players { get; } =
        MakeInitializer(new NetworkString<_64>[] { "", "", "", "" });

    public playerRomanceHandler[] crypids = { null, null, null, null };
    public playerRomanceHandler[] crypidstemp = { null, null, null, null };
    public float maxRomance = 1000;
    [Networked]
    public float romanceMEt2 { get; set; }
    [Networked]
    public playerRomanceHandler player2 { get; set; }
    [Networked]
    public float romanceMEt3 { get; set; }
    [Networked]
    public float romanceMEt4 { get; set; }

    public Slider romanceBar;



    public NetworkPlayer player;

    public CharacterInputHandler cih;
    public CharacterMovementHandler cmh;

    // Start is called before the first frame update
    void Start()
    {
    }


    //NEED TO NETWORK PLAYER TAG
    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Debug.Log("before debug.log");
            if (players.Get(0) == "")
            {
                players.Set(0, "player0");
                this.gameObject.tag = "player0";
                crypids[0] = this;
            }
            else if (players.Get(1) == "")
            {
                players.Set(1, "player1");
                this.gameObject.tag = "player1";
                crypids[1] = this;
            }
            else if (players.Get(2) == "")
            {
                players.Set(2, "player2");
                this.gameObject.tag = "player2";
                crypids[2] = this;
            }
            else if (players.Get(3) == "")
            {
                players.Set(3, "player3");
                this.gameObject.tag = "player3";
                crypids[3] = this;
            }

            //Debug.Log(players.ToString()+"players.tostring");
            Debug.Log("after debug.log");
            Local = this;
            RPC_SendCrypids();
            for (int i = 0; i < crypids.Length; i++)
            {
                crypids[i] = crypidstemp[i];
            }


        }
        else
        {

            for (int i = 0; i < players.Length; ++i)
            {
                Debug.Log(players.Get(i));
                if (!this.CompareTag(players.Get(i).ToString()))
                {
                    player2 = crypids[i];
                    break;
                }
            }

            RPC_SendCrypids();
            //RPC_SetPlayerTarget(this);
            //player2 = player2temp;

        }
    }


    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SendCrypids()
    {
        for (int i = 0; i < crypids.Length; i++)
        {
            Debug.Log("player" + i.ToString());
            crypidstemp[i] = GameObject.FindGameObjectWithTag("player" + i.ToString()).GetComponent<playerRomanceHandler>();
        }
    }

    /*
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

    }

    void onHit(float romancePower)
    {

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
