using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class playerRomanceHandler : NetworkBehaviour
{


    public static playerRomanceHandler Local { get; set; }

    public GameObject heartParticle;
    public GameObject heartImg;

    public playerRomanceHandler otherPlayer;
    public playerRomanceHandler otherPlayer2;
    public playerRomanceHandler otherPlayer3;

    //romance levels
    [Networked]
    [Capacity(4)]
    public NetworkArray<NetworkString<_32>> players { get; } =
        MakeInitializer(new NetworkString<_32>[] { "", "", "", "" });

    public string[] playerstemp = { "", "", "", "" };

    [Networked]
    public NetworkString<_32> tagthing { get; set; }

    public string temptag;

    public AudioSource romanceSound;
    public AudioSource kissSound;

    

    public playerRomanceHandler[] crypids = { null, null, null, null };
    public playerRomanceHandler[] crypidstemp = { null, null, null, null };


    public GameObject[] children;
    public float maxRomance = 1000;

    [Networked]
    public playerRomanceHandler player2 { get; set; }

    [Networked(OnChanged = nameof(On0_1Changed))]
    public float romance0_1 { get; set; }

    [Networked(OnChanged = nameof(On0_2Changed))]
    public float romance0_2 { get; set; }

    [Networked(OnChanged = nameof(On0_3Changed))]
    public float romance0_3 { get; set; }

    [Networked(OnChanged = nameof(On1_2Changed))]
    public float romance1_2 { get; set; }

    [Networked(OnChanged = nameof(On1_3Changed))]
    public float romance1_3 { get; set; }

    [Networked(OnChanged = nameof(On2_3Changed))]
    public float romance2_3 { get; set; }


    /*
    public Slider romanceBar0;
    public Slider romanceBar1;
    public Slider romanceBar2;
    */
    public sliderBar rBar;

    public bool cannotRomance = false;

    public NetworkPlayer player;

    public CharacterInputHandler cih;
    public CharacterMovementHandler cmh;

    [Networked]
    public bool isInLove { get; set; }

    [Networked]
    public bool allTeamsSet { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        cannotRomance = false;
        isInLove = false;
        allTeamsSet = false;
    }

    void Update()
    {
        playerRomanceHandler other1 = null;
        playerRomanceHandler other2 = null;
        if (crypids[0] != null && crypids[1] != null && crypids[2] != null && crypids[3] != null)
        {
            foreach (playerRomanceHandler item in crypids)
            {
                if (!allTeamsSet && item.isInLove == true && other1 == null)
                {
                    other1 = item;
                }
                if (!allTeamsSet && item.isInLove == true && other2 == null)
                {
                    other2 = item;
                }
            }
            if (other1 != null && other2 != null)
            {
                setOtherPlayersAsFriends(other1, other2);
            }
        }
    }


    public override void Spawned()
    {

        if (!Object.HasInputAuthority)
        {


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
            cannotRomance = false;
            Debug.Log("before debug.log");
            RPC_SendInfo();

            Debug.Log("after debug.log");

            if (this.tagthing == "player0")
                changeLayer(this.gameObject, "Team1");
            else if (this.tagthing == "player1")
                changeLayer(this.gameObject, "Team2");
            else if (this.tagthing == "player2")
                changeLayer(this.gameObject, "Team3");
            else if (this.tagthing == "player3")
                changeLayer(this.gameObject, "Team4");


        }
        Debug.Log("beginning layer change for " + tagthing);
        if (this.tagthing == "player0")
            changeLayer(this.gameObject, "Team1");
        else if (this.tagthing == "player1")
            changeLayer(this.gameObject, "Team2");
        else if (this.tagthing == "player2")
            changeLayer(this.gameObject, "Team3");
        else if (this.tagthing == "player3")
            changeLayer(this.gameObject, "Team4");
    }



    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SendInfo()
    {
        playerRomanceHandler[] otherPlayers = GameObject.FindObjectsOfType<playerRomanceHandler>();
        foreach (playerRomanceHandler other in otherPlayers)
        {
            if (other != this && otherPlayer == null)
            {
                otherPlayer = other.GetComponent<playerRomanceHandler>();
            }
            else if (other != this && otherPlayer2 == null)
            {
                otherPlayer2 = other.GetComponent<playerRomanceHandler>();
            }
            else if (other != this && otherPlayer3 == null)
            {
                otherPlayer3 = other.GetComponent<playerRomanceHandler>();
            }
        }


        Debug.Log("running rpc");
        if (otherPlayer != null)
        {
            //otherPlayer.otherPlayer = this;
            for (int i = 0; i < players.Length; i++)
            {
                if (otherPlayer.crypids[i] != null)
                    crypids[i] = otherPlayer.crypids[i];
                if (otherPlayer.players.Get(i) != "")
                    players.Set(i, otherPlayer.players.Get(i));
                Debug.Log("other players array" + otherPlayer.players.Get(i));
            }
        }
        if (otherPlayer2 != null)
        {
            //otherPlayer.otherPlayer = this;
            for (int i = 0; i < players.Length; i++)
            {
                if (otherPlayer2.crypids[i] != null)
                    crypids[i] = otherPlayer2.crypids[i];
                if (otherPlayer2.players.Get(i) != "")
                    players.Set(i, otherPlayer2.players.Get(i));
                Debug.Log("other players 2 array" + otherPlayer2.players.Get(i));
            }
        }
        if (otherPlayer3 != null)
        {
            //otherPlayer.otherPlayer = this;
            for (int i = 0; i < players.Length; i++)
            {
                if (otherPlayer3.crypids[i] != null)
                    crypids[i] = otherPlayer3.crypids[i];
                if (otherPlayer3.players.Get(i) != "")
                    players.Set(i, otherPlayer3.players.Get(i));
                Debug.Log("other players 3 array" + otherPlayer3.players.Get(i));
            }
        }


        if (players.Get(0) == "")
        {
            players.Set(0, "player0");
            tagthing = "player0";
            temptag = "player0";
            crypids[0] = this;
            Debug.Log("local player ran 0");
            changeLayer(this.gameObject, "Team1");
        }
        else if (players.Get(1) == "")
        {
            players.Set(1, "player1");
            tagthing = "player1";
            temptag = "player1";
            crypids[1] = this;
            Debug.Log("local player ran 1");
            changeLayer(this.gameObject, "Team2");
        }
        else if (players.Get(2) == "")
        {
            players.Set(2, "player2");
            tagthing = "player2";
            crypids[2] = this;
            changeLayer(this.gameObject, "Team3");
        }
        else if (players.Get(3) == "")
        {
            players.Set(3, "player3");
            tagthing = "player3";
            crypids[3] = this;
            changeLayer(this.gameObject, "Team4");
        }


        if (otherPlayer != null)
        {
            for (int i = 0; i < players.Length; i++)
            {
                otherPlayer.crypids[i] = crypids[i];
                otherPlayer.players.Set(i, players.Get(i));
            }
        }
        if (otherPlayer2 != null)
        {
            for (int i = 0; i < players.Length; i++)
            {
                otherPlayer2.crypids[i] = crypids[i];
                otherPlayer2.players.Set(i, players.Get(i));
            }
        }
        if (otherPlayer3 != null)
        {
            for (int i = 0; i < players.Length; i++)
            {
                otherPlayer3.crypids[i] = crypids[i];
                otherPlayer3.players.Set(i, players.Get(i));
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

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SetBar(playerRomanceHandler prh, string whichPlayer, float rValue)
    {
        if (whichPlayer == tagthing && HasInputAuthority)
        {
            prh.rBar.changeTo(rValue);
            Debug.Log(tagthing + " changed" + prh.tagthing);
        }
        /*
        else if(prh.tagthing == tagthing && !HasInputAuthority)
        {
            this.rBar.changeTo(rValue);
        }
         */
    }
    /*
    public void SetBar(playerRomanceHandler prh, int whichBar, float rValue)
    {
        prh.rBar.onchange(rValue, maxRomance, -1);
        
    }
    */

    public void changeLayer(GameObject playerThis, string team)
    {
        Debug.Log("changing layer in children for" + tagthing + "to " + team);
        foreach (Transform child in playerThis.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer(team);
            foreach (Transform child1 in child.transform)
            {
                child1.gameObject.layer = LayerMask.NameToLayer(team);
                foreach (Transform child2 in child1.transform)
                {
                    child2.gameObject.layer = LayerMask.NameToLayer(team);
                    foreach (Transform child3 in child2.transform)
                    {
                        if (child3.gameObject.tag != "romanceAttk")
                            child3.gameObject.layer = LayerMask.NameToLayer(team);
                    }
                }
            }
        }
    }

    public void onFull(GameObject playerThis, GameObject playerOther, string team)
    {
        foreach (Transform child in playerThis.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer(team);
            foreach (Transform child1 in child.transform)
            {
                child1.gameObject.layer = LayerMask.NameToLayer(team);
                foreach (Transform child2 in child1.transform)
                {
                    child2.gameObject.layer = LayerMask.NameToLayer(team);
                    foreach (Transform child3 in child2.transform)
                    {
                        if (child3.gameObject.tag != "romanceAttk")
                            child3.gameObject.layer = LayerMask.NameToLayer(team);
                    }
                }
            }
        }
        if (playerOther != null)
        {
            //playerOther.GetComponentInChildren<heart>().gameObject.SetActive(true);
            foreach (Transform child in playerOther.transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer(team);
                foreach (Transform child1 in child.transform)
                {
                    child1.gameObject.layer = LayerMask.NameToLayer(team);
                    foreach (Transform child2 in child1.transform)
                    {
                        child2.gameObject.layer = LayerMask.NameToLayer(team);
                        foreach (Transform child3 in child2.transform)
                        {
                            if (child3.gameObject.tag != "romanceAttk")
                                child3.gameObject.layer = LayerMask.NameToLayer(team);
                        }
                    }
                }
            }
        }
        RPC_onRomanceFull();
        Instantiate(heartParticle,playerThis.transform.position,playerThis.transform.rotation);
        Instantiate(heartParticle,playerOther.transform.position,playerOther.transform.rotation);
        otherPlayer.heartImg.SetActive(true);
        romanceSound.Play();

    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_onRomanceFull()
    {
        cannotRomance = true;
    }

    void heal(playerRomanceHandler other)
    {
        CryptidScript otherCS = null;
        if(other.transform.Find("Capsule").transform.Find("Jack").gameObject.activeSelf == true)
            otherCS = other.transform.Find("Capsule").transform.Find("Jack").GetComponent<CryptidScript>();
        else if(other.transform.Find("Capsule").transform.Find("Sibone").gameObject.activeSelf == true)
            otherCS = other.transform.Find("Capsule").transform.Find("Sibone").GetComponent<CryptidScript>();
        else if (other.transform.Find("Capsule").transform.Find("Wendigo").gameObject.activeSelf == true)
            otherCS = other.transform.Find("Capsule").transform.Find("Wendigo").GetComponent<CryptidScript>();
        else if (other.transform.Find("Capsule").transform.Find("Dragur").gameObject.activeSelf == true)
            otherCS = other.transform.Find("Capsule").transform.Find("Dragur").GetComponent<CryptidScript>();
        if (otherCS != null && otherCS.netHealth < otherCS.maxHealth)
        {
            kissSound.Play();
            otherCS.netHealth += 100;
            otherCS.RPC_setHealthBarToCurrentHealth();
            Debug.Log("healed");
        }
            
        Debug.Log(otherCS);
    }


    void onHit(float romancePower, Collider collision)
    {
        
        int thisArrPos = 0;
        int otherArrPos = 0;
        playerRomanceHandler otherRef = null;
        playerRomanceHandler thirdPlayerRef = null;
        playerRomanceHandler fourthPlayerRef = null;
        
        for (int i = 0; i < crypids.Length; i++)
        {
            if (crypids[i] == this)
            {
                thisArrPos = i;
                Debug.Log(crypids[i] + "   " + i);
            }
            if (crypids[i] == collision.GetComponentInParent<playerRomanceHandler>())
            {
                otherArrPos = i;
                Debug.Log(crypids[i] + "   " + i);
                otherRef = crypids[i];
            }
            else if (thirdPlayerRef == null)
            {
                thirdPlayerRef = crypids[i];
            }
            else if (fourthPlayerRef == null)
            {
                fourthPlayerRef = crypids[i];
            }
        }
        if (thisArrPos == otherArrPos)
        {
            return;
        }
        if (cannotRomance == false)
        {
            kissSound.Play();
            //i tried to make this less messy but i couldn't
            if (thisArrPos == 0)
            {
                if (otherArrPos == 1)
                {
                    romance0_1 += romancePower;
                    otherRef.romance0_1 += romancePower;
                    if (romance0_1 >= 1000)
                    {
                        isInLove = true;
                        otherRef.isInLove = true;
                        onFull(this.gameObject, otherRef.gameObject, "Team1");
                        otherRef.onFull(this.gameObject, otherRef.gameObject, "Team1");
                        setOtherPlayersAsFriends(thirdPlayerRef, fourthPlayerRef);
                    }
                }
                else if (otherArrPos == 2)
                {
                    romance0_2 += romancePower;
                    otherRef.romance0_2 += romancePower;
                    if (romance0_2 >= 1000)
                    {
                        isInLove = true;
                        otherRef.isInLove = true;
                        onFull(this.gameObject, otherRef.gameObject, "Team1");
                        otherRef.onFull(this.gameObject, otherRef.gameObject, "Team1");
                        setOtherPlayersAsFriends(thirdPlayerRef, fourthPlayerRef);
                    }
                }
                else if (otherArrPos == 3)
                {
                    romance0_3 += romancePower;
                    otherRef.romance0_3 += romancePower;
                    if (romance0_3 >= 1000)
                    {
                        isInLove = true;
                        otherRef.isInLove = true;
                        onFull(this.gameObject, otherRef.gameObject, "Team1");
                        otherRef.onFull(this.gameObject, otherRef.gameObject, "Team1");
                        setOtherPlayersAsFriends(thirdPlayerRef, fourthPlayerRef);
                    }
                }
            }
            else if (thisArrPos == 1)
            {
                if (otherArrPos == 0)
                {
                    romance0_1 += romancePower;
                    otherRef.romance0_1 += romancePower;
                    if (romance0_1 >= 1000)
                    {
                        isInLove = true;
                        otherRef.isInLove = true;
                        onFull(this.gameObject, otherRef.gameObject, "Team1");
                        otherRef.onFull(this.gameObject, otherRef.gameObject, "Team1");
                        setOtherPlayersAsFriends(thirdPlayerRef, fourthPlayerRef);
                    }
                }
                else if (otherArrPos == 2)
                {
                    romance1_2 += romancePower;
                    otherRef.romance1_2 += romancePower;
                    if (romance0_2 >= 1000)
                    {
                        isInLove = true;
                        otherRef.isInLove = true;
                        onFull(this.gameObject, otherRef.gameObject, "Team1");
                        otherRef.onFull(this.gameObject, otherRef.gameObject, "Team1");
                        setOtherPlayersAsFriends(thirdPlayerRef, fourthPlayerRef);
                    }
                }
                else if (otherArrPos == 3)
                {
                    romance1_3 += romancePower;
                    otherRef.romance1_3 += romancePower;
                    if (romance1_3 >= 1000)
                    {
                        isInLove = true;
                        otherRef.isInLove = true;
                        onFull(this.gameObject, otherRef.gameObject, "Team1");
                        otherRef.onFull(this.gameObject, otherRef.gameObject, "Team1");
                        setOtherPlayersAsFriends(thirdPlayerRef, fourthPlayerRef);
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
                        isInLove = true;
                        otherRef.isInLove = true;
                        onFull(this.gameObject, otherRef.gameObject, "Team1");
                        otherRef.onFull(this.gameObject, otherRef.gameObject, "Team1");
                        setOtherPlayersAsFriends(thirdPlayerRef, fourthPlayerRef);
                    }
                }
                else if (otherArrPos == 0)
                {
                    romance0_2 += romancePower;
                    otherRef.romance0_2 += romancePower;
                    if (romance0_2 >= 1000)
                    {
                        isInLove = true;
                        otherRef.isInLove = true;
                        onFull(this.gameObject, otherRef.gameObject, "Team1");
                        otherRef.onFull(this.gameObject, otherRef.gameObject, "Team1");
                        setOtherPlayersAsFriends(thirdPlayerRef, fourthPlayerRef);
                    }
                }
                else if (otherArrPos == 1)
                {
                    romance1_2 += romancePower;
                    otherRef.romance1_2 += romancePower;
                    if (romance1_2 >= 1000)
                    {
                        isInLove = true;
                        otherRef.isInLove = true;
                        onFull(this.gameObject, otherRef.gameObject, "Team1");
                        otherRef.onFull(this.gameObject, otherRef.gameObject, "Team1");
                        setOtherPlayersAsFriends(thirdPlayerRef, fourthPlayerRef);
                    }
                }
            }
            else if (thisArrPos == 3)
            {
                if (otherArrPos == 0)
                {
                    romance0_3 += romancePower;
                    otherRef.romance0_3 += romancePower;
                    if (romance2_3 >= 1000)
                    {
                        isInLove = true;
                        otherRef.isInLove = true;
                        onFull(this.gameObject, otherRef.gameObject, "Team1");
                        otherRef.onFull(this.gameObject, otherRef.gameObject, "Team1");
                        setOtherPlayersAsFriends(thirdPlayerRef, fourthPlayerRef);
                    }
                }
                else if (otherArrPos == 1)
                {
                    romance1_3 += romancePower;
                    otherRef.romance1_3 += romancePower;
                    if (romance0_2 >= 1000)
                    {
                        isInLove = true;
                        otherRef.isInLove = true;
                        onFull(this.gameObject, otherRef.gameObject, "Team1");
                        otherRef.onFull(this.gameObject, otherRef.gameObject, "Team1");
                        setOtherPlayersAsFriends(thirdPlayerRef, fourthPlayerRef);
                    }
                }
                else if (otherArrPos == 2)
                {
                    romance2_3 += romancePower;
                    otherRef.romance2_3 += romancePower;
                    if (romance1_2 >= 1000)
                    {
                        isInLove = true;
                        otherRef.isInLove = true;
                        onFull(this.gameObject, otherRef.gameObject, "Team1");
                        otherRef.onFull(this.gameObject, otherRef.gameObject, "Team1");
                        setOtherPlayersAsFriends(thirdPlayerRef, fourthPlayerRef);
                    }
                }
            }
        }
        else if (isInLove)
        {
            heal(this);
        }

    }

    public void setOtherPlayersAsFriends(playerRomanceHandler romancedPlayer1, playerRomanceHandler romancedPlayer2)
    {
        playerRomanceHandler thing1;
        foreach (playerRomanceHandler person in crypids)
        {
            if (romancedPlayer1 != person && romancedPlayer2 != person)
            {
                thing1 = person;
                foreach (playerRomanceHandler person2 in crypids)
                {
                    if (romancedPlayer1 != person2 && romancedPlayer2 != person2 && thing1 != person2)
                    {
                        person.onFull(person2.gameObject, thing1.gameObject, "Team2");
                        romancedPlayer1.allTeamsSet = true;
                        romancedPlayer2.allTeamsSet = true;
                        thing1.allTeamsSet = true;
                        person2.allTeamsSet = true;
                    }

                }
            }
        }
    }



    //checks if any of the romance variables are changed and then checks who the player is and changes the other players bar if it is a related player

    public static void On0_1Changed(Changed<playerRomanceHandler> changed)
    {
        changed.Behaviour.On0_1Changed();
    }
    public static void On0_2Changed(Changed<playerRomanceHandler> changed)
    {
        changed.Behaviour.On0_2Changed();
    }
    public static void On0_3Changed(Changed<playerRomanceHandler> changed)
    {
        changed.Behaviour.On0_3Changed();
    }
    public static void On1_2Changed(Changed<playerRomanceHandler> changed)
    {
        changed.Behaviour.On1_2Changed();
    }
    public static void On1_3Changed(Changed<playerRomanceHandler> changed)
    {
        changed.Behaviour.On1_3Changed();
    }
    public static void On2_3Changed(Changed<playerRomanceHandler> changed)
    {
        changed.Behaviour.On2_3Changed();
    }



    private void On0_1Changed()
    {
        if (tagthing == "player0" || tagthing == "player1")
        {
            RPC_SetBar(crypids[1], "player0", romance0_1 / maxRomance);

            RPC_SetBar(crypids[0], "player1", romance0_1 / maxRomance);
        }
    }

    private void On0_2Changed()
    {
        if (tagthing == "player0" || tagthing == "player2")
        {
            RPC_SetBar(crypids[2], "player0", romance0_2 / maxRomance);

            RPC_SetBar(crypids[0], "player2", romance0_2 / maxRomance);
        }
    }
    private void On0_3Changed()
    {
        if (tagthing == "player0" || tagthing == "player3")
        {
            RPC_SetBar(crypids[3], "player0", romance0_3 / maxRomance);

            RPC_SetBar(crypids[0], "player3", romance0_3 / maxRomance);
        }
    }
    private void On1_2Changed()
    {
        if (tagthing == "player1" || tagthing == "player2")
        {
            RPC_SetBar(crypids[2], "player1", romance1_2 / maxRomance);

            RPC_SetBar(crypids[1], "player2", romance1_2 / maxRomance);
        }
    }
    private void On1_3Changed()
    {
        if (tagthing == "player1" || tagthing == "player3")
        {
            RPC_SetBar(crypids[3], "player1", romance1_3 / maxRomance);

            RPC_SetBar(crypids[1], "player3", romance1_3 / maxRomance);
        }
    }
    private void On2_3Changed()
    {
        if (tagthing == "player2" || tagthing == "player3")
        {
            RPC_SetBar(crypids[3], "player2", romance2_3 / maxRomance);

            RPC_SetBar(crypids[2], "player3", romance2_3 / maxRomance);
        }
    }
}
