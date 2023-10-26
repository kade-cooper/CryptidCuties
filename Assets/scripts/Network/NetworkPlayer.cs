using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
//using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

//kade's branch

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{

    public GameObject canvas;
    public GameObject blueGuy;
    public GameObject redGuy;
    public GameObject wendigo;
    public Vector3 spawnpoint;

    [Networked(OnChanged = nameof(sendSelection))]
    public int selectedCharacter { get; set; }

    public KeyCode character1_attk;
    public KeyCode character1_RomanceAttk;
    public KeyCode character1_Ability1;

    public KeyCode character2_attk;
    public KeyCode character2_RomanceAttk;
    public KeyCode character2_Ability1;


    public KeyCode Wendigo3_attk;
    public KeyCode Wendigo3_RomanceAttk;
    public KeyCode Wendigo3_Ability1;

    public Transform playerUI;
    public Transform playerUIFighting;
    public static NetworkPlayer Local { get; set; }
    void Start()
    {
        Spawner spawner = Spawner.FindObjectOfType<Spawner>();
        spawnpoint = spawner.spawn;
    }

    public static void sendSelection(Changed<NetworkPlayer> changed)
    {
        changed.Behaviour.sendSelectionOnChange();
    }

    private void sendSelectionOnChange()
    {
        if (selectedCharacter == 1)
        {
            this.GetComponent<CharacterInputHandler>().cryptidSelected(character1_attk, character1_RomanceAttk, character1_Ability1);
        }
        else if(selectedCharacter == 2)
        {
            this.GetComponent<CharacterInputHandler>().cryptidSelected(character2_attk, character2_RomanceAttk, character2_Ability1);
        }
        else if (selectedCharacter == 3)
        {
            this.GetComponent<CharacterInputHandler>().cryptidSelected(Wendigo3_attk, Wendigo3_RomanceAttk, Wendigo3_Ability1);
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_ChangeToSelectedCharacter()
    {
        //Debug.Log(Object.HasInputAuthority);
        //Debug.Log(selectedCharacter);
        if (selectedCharacter != null)
        {
            if (selectedCharacter == 1)
            {
                blueGuy.SetActive(true);
            }
            else if(selectedCharacter == 2)
            {
                redGuy.SetActive(true);
            }
            else if (selectedCharacter == 3)
            {
                wendigo.SetActive(true);
            }
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_CharacterBlueSelected()
    {
        blueGuy.SetActive(true);
        selectedCharacter = 1;
        canvas.SetActive(false);

    }

    [Rpc(RpcSources.InputAuthority,RpcTargets.All)]
    public void RPC_CharacterRedSelected()
    {
        redGuy.SetActive(true);
       selectedCharacter = 2;
        canvas.SetActive(false);
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_CharacterWendigoSelected()
    {
        wendigo.SetActive(true);
        selectedCharacter = 3;
        canvas.SetActive(false);
    }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;

            Utils.SetRenderLayerInChildren(playerUI, LayerMask.NameToLayer("LocalPlayerUI"));
            Utils.SetRenderLayerInChildren(playerUIFighting, LayerMask.NameToLayer("LocalPlayerUIFighting"));
            Camera.main.gameObject.SetActive(false);
            Debug.Log("Spawned local player");
        }
        else {
            Camera localCamera = GetComponentInChildren<Camera>();
            localCamera.enabled = false;
            playerUI.gameObject.SetActive(false);
            playerUIFighting.gameObject.SetActive(false);
            AudioListener audioListener= GetComponentInChildren<AudioListener>();
            audioListener.enabled = false;
            Debug.Log("Spawned remote player");
            RPC_ChangeToSelectedCharacter();
        }    
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (player == Object.InputAuthority)
        {
            Runner.Despawn(Object);
        }
    }

}
