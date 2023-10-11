using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class playerRomanceHandler : NetworkBehaviour
{
    public static playerRomanceHandler Local { get; set; }


    public playerRomanceHandler otherPlayer;

    //romance levels
    [Networked]
    [Capacity(4)]
    public NetworkArray<NetworkString<_32>> players { get; } =
        MakeInitializer(new NetworkString<_32>[] { "", "", "", "" });

    public string[] playerstemp = { "", "", "", "" };

    [Networked]
    public NetworkString<_32> tagthing { get; set; }

    public string temptag;


    public playerRomanceHandler[] crypids = { null, null, null, null };
    public playerRomanceHandler[] crypidstemp = { null, null, null, null };
    public float maxRomance = 1000;

    [Networked]
    public playerRomanceHandler player2 { get; set; }

    [Networked]
    public float romance0_1 { get; set; }
    [Networked]
    public float romance0_2 { get; set; }
    [Networked]
    public float romance0_3 { get; set; }
    [Networked]
    public float romance1_2 { get; set; }
    [Networked]
    public float romance1_3 { get; set; }
    [Networked]
    public float romance2_3 { get; set; }



    public Slider romanceBar;



    public NetworkPlayer player;

    public CharacterInputHandler cih;
    public CharacterMovementHandler cmh;

    // Start is called before the first frame update
    void Start()
    {
        /*
        playerRomanceHandler[] otherPlayers = GameObject.FindObjectsOfType<playerRomanceHandler>();
        foreach (playerRomanceHandler other in otherPlayers)
        {
            if (other != this)
            {
                otherPlayer = other.GetComponent<playerRomanceHandler>();
                return;
            }
        }
        */
    }


    public override void Spawned()
    {

        if (!Object.HasInputAuthority)
        {

            //RPC_SendTag();
            //tagthing = temptag;
            /*
            for (int i = 0; i < players.Length; ++i)
            {
                Debug.Log(players.Get(i));
                if (!(tagthing==players.Get(i)))
                {
                    player2 = crypids[i];
                    //break;
                }
            }
            */

            RPC_SendCrypids();
            for (int i = 0; i < crypids.Length; ++i)
            {
                crypids[i] = crypidstemp[i];
                //break;
            }
            RPC_SendPlayers();
            for (int i = 0; i < crypids.Length; ++i)
            {
                players.Set(i, playerstemp[i]);


            }

        }
        else
        {
            Debug.Log("before debug.log");
            RPC_SendInfo();

            //Debug.Log(players.ToString()+"players.tostring");
            Debug.Log("after debug.log");
            //Local = this;
            /*
            for (int i = 0; i < crypids.Length; i++)
            {
                crypids[i] = crypidstemp[i];
            }
            */


        }
    }



    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SendInfo()
    {
        playerRomanceHandler[] otherPlayers = GameObject.FindObjectsOfType<playerRomanceHandler>();
        foreach (playerRomanceHandler other in otherPlayers)
        {
            if (other != this)
            {
                otherPlayer = other.GetComponent<playerRomanceHandler>();
                break;
            }
        }


        Debug.Log("running rpc");
        if (otherPlayer != null)
        {
            //otherPlayer.otherPlayer = this;
            for (int i = 0; i < players.Length; i++)
            {
                crypids[i] = otherPlayer.crypids[i];
                players.Set(i, otherPlayer.players.Get(i));
                Debug.Log("other players array" + otherPlayer.players.Get(i));
            }
        }
        if (players.Get(0) == "")
        {
            players.Set(0, "player0");
            tagthing = "player0";
            temptag = "player0";
            crypids[0] = this;
            Debug.Log("local player ran 0");
        }
        else if (players.Get(1) == "")
        {
            players.Set(1, "player1");
            tagthing = "player1";
            temptag = "player1";
            crypids[1] = this;
            Debug.Log("local player ran 1");
        }
        else if (players.Get(2) == "")
        {
            players.Set(2, "player2");
            tagthing = "player2";
            crypids[2] = this;
        }
        else if (players.Get(3) == "")
        {
            players.Set(3, "player3");
            tagthing = "player3";
            crypids[3] = this;
        }


        if (otherPlayer != null)
        {
            for (int i = 0; i < players.Length; i++)
            {
                otherPlayer.crypids[i] = crypids[i];
                otherPlayer.players.Set(i, players.Get(i));
            }
        }


        romance0_1 = 0;
        romance0_2 = 0;
        romance0_3 = 0;
        romance1_2 = 0;
        romance1_3 = 0;
        romance2_3 = 0;


    }




    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SendCrypids()
    {
        for (int i = 0; i < crypids.Length; i++)
        {
            //Debug.Log("player" + i.ToString());
            crypidstemp[i] = crypids[i];
        }
    }


    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SendPlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            //Debug.Log("player" + i.ToString());
            playerstemp[i] = players.Get(i).ToString();
            Debug.Log(playerstemp[i]);
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SendTag()
    {
        temptag = tagthing.ToString();
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("onRomanceTriggerEnter");
        if (collision.CompareTag("romanceAttk"))
            onHit(100, collision);
    }

    public void onFull(GameObject playerThis, GameObject playerOther)
    {
        playerThis.layer = LayerMask.NameToLayer("Team1");
        playerOther.layer = LayerMask.NameToLayer("Team1");
    }


    void onHit(float romancePower, Collider collision)
    {
        int thisArrPos = 0;
        int otherArrPos = 0;
        playerRomanceHandler otherRef = null;
        for (int i = 0; i < crypids.Length; i++)
        {
            if (crypids[i] == this)
            {
                thisArrPos = i;
                Debug.Log(crypids[i]+"   "+i);
            }
            if (crypids[i] == collision.GetComponentInParent<playerRomanceHandler>())
            {
                otherArrPos = i;
                Debug.Log(crypids[i] + "   " + i);
                otherRef = crypids[i];
            }
        }

        //i tried to make this less messy but i couldn't
        if (thisArrPos == 0)
        {
            if (otherArrPos == 1)
            {
                romance0_1 += romancePower;
                otherRef.romance0_1 += romancePower;
                if (romance0_1 >= 1000)
                {
                    onFull(this.gameObject, otherRef.gameObject);
                }
            }
            else if (otherArrPos == 2)
            {
                romance0_2 += romancePower;
                otherRef.romance0_2 += romancePower;
                if (romance0_2 >= 1000)
                {
                    onFull(this.gameObject, otherRef.gameObject);
                }
            }
            else if (otherArrPos == 3)
            {
                romance0_3 += romancePower;
                otherRef.romance0_3 += romancePower;
                if (romance0_3 >= 1000)
                {
                    onFull(this.gameObject, otherRef.gameObject);
                }
            }
        }
        else if (thisArrPos == 1)
        {
            if(otherArrPos == 0)
            {
                romance0_1 += romancePower;
                otherRef.romance0_1 += romancePower;
                if (romance0_1 >= 1000)
                {
                    onFull(this.gameObject, otherRef.gameObject);
                }
            }
            else if (otherArrPos == 2)
            {
                romance1_2 += romancePower;
                otherRef.romance1_2 += romancePower;
                if (romance0_2 >= 1000)
                {
                    onFull(this.gameObject, otherRef.gameObject);
                }
            }
            else if (otherArrPos == 3)
            {
                romance1_3 += romancePower;
                otherRef.romance1_3 += romancePower;
                if (romance1_3 >= 1000)
                {
                    onFull(this.gameObject, otherRef.gameObject);
                }
            }
        }
        else if (thisArrPos == 2)
        {
            if (otherArrPos == 3)
            {
                romance2_3 += romancePower;
                otherRef.romance2_3 += romancePower;
                if (romance2_3 >= 1000)
                {
                    onFull(this.gameObject, otherRef.gameObject);
                }
            }
            else if (otherArrPos == 0)
            {
                romance0_2 += romancePower;
                otherRef.romance0_1 += romancePower;
                if (romance0_2 >= 1000)
                {
                    onFull(this.gameObject, otherRef.gameObject);
                }
            }
            else if(otherArrPos == 1)
            {
                romance1_2 += romancePower;
                otherRef.romance1_2 += romancePower;
                if (romance1_2 >= 1000)
                {
                    onFull(this.gameObject, otherRef.gameObject);
                }
            }
        }

    }
}
