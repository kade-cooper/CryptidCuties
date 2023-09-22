using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

//kade's branch

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{

    public GameObject canvas;
    public GameObject blueGuy;
    public GameObject redGuy;

    [Networked]
    [HideInInspector]
    public int selectedCharacter { get; set; }

    public Transform playerUI;
    public static NetworkPlayer Local { get; set; }
    void Start()
    {
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_ChangeToSelectedCharacter()
    {
        Debug.Log(Object.HasInputAuthority);
        Debug.Log(selectedCharacter);
        if (selectedCharacter != null)
        {
            if (selectedCharacter == 1)
            {
                blueGuy.SetActive(true);
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

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;

            Utils.SetRenderLayerInChildren(playerUI, LayerMask.NameToLayer("LocalPlayerUI"));
            Camera.main.gameObject.SetActive(false);
            Debug.Log("Spawned local player");
        }
        else {
            Camera localCamera = GetComponentInChildren<Camera>();
            localCamera.enabled = false;
            playerUI.gameObject.SetActive(false);
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
