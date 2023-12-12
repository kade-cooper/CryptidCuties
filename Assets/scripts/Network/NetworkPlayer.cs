using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
//using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

//kade's branch

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{

    public GameObject canvas;
    public GameObject Draguar;
    // ^ this is replacement for blueGuy
    public GameObject Jack;
    // ^ this is replacement for redGuy
    public GameObject wendigo;
    public GameObject elSilbon;
    public GameObject ghost;
    public Color clearwhite;
    public Vector3 spawnpoint;

    [Networked(OnChanged = nameof(sendSelection))]
    public int selectedCharacter { get; set; }


    public KeyCode Draguar4_attk;
    public KeyCode Draguar4_RomanceAttk;
    public KeyCode Draguar4_Ability1;
    //^ this is replacement for blueGuy


    public KeyCode Jack1_attk;
    public KeyCode Jack1_RomanceAttk;
    public KeyCode Jack1_Ability1;
    //^this is replacement for redGuy

    public KeyCode Wendigo3_attk;
    public KeyCode Wendigo3_RomanceAttk;
    public KeyCode Wendigo3_Ability1;

    public KeyCode elSilbon2_attk;
    public KeyCode elSilbon2_RomanceAttk;
    public KeyCode elSilbon2_Ability1;

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
            this.GetComponent<CharacterInputHandler>().cryptidSelected(Jack1_attk, Jack1_RomanceAttk, Jack1_Ability1);
        }
        else if(selectedCharacter == 2)
        {
            this.GetComponent<CharacterInputHandler>().cryptidSelected(elSilbon2_attk, elSilbon2_RomanceAttk, elSilbon2_Ability1);
        }
        else if (selectedCharacter == 3)
        {
            this.GetComponent<CharacterInputHandler>().cryptidSelected(Wendigo3_attk, Wendigo3_RomanceAttk, Wendigo3_Ability1);
        }
        else if (selectedCharacter == 4)
        {
            this.GetComponent<CharacterInputHandler>().cryptidSelected(Draguar4_attk, Draguar4_RomanceAttk, Draguar4_Ability1);
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
                // blueGuy.SetActive(true);
                Jack.SetActive(true);
                ghost.GetComponent<SpriteRenderer>().color = Color.clear;
            }
            else if(selectedCharacter == 2)
            {
                // redGuy.SetActive(true);
                elSilbon.SetActive(true);
                ghost.GetComponent<SpriteRenderer>().color = Color.clear;
            }
            else if (selectedCharacter == 3)
            {
                wendigo.SetActive(true);
                ghost.GetComponent<SpriteRenderer>().color = Color.clear;
            }
            else if (selectedCharacter == 4)
            {
                Draguar.SetActive(true);
                ghost.GetComponent<SpriteRenderer>().color = Color.clear;
            }
            else if (selectedCharacter == 0)
            {
                Jack.SetActive(false);
                Draguar.SetActive(false);
                wendigo.SetActive(false);
                elSilbon.SetActive(false);
                ghost.GetComponent<SpriteRenderer>().color = clearwhite;
            }
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_CharacterBlueSelected()
    {
        ghost.GetComponent<SpriteRenderer>().color = Color.clear;
        //blueGuy.SetActive(true);
        Jack.SetActive(true);
        selectedCharacter = 1;
        canvas.SetActive(false);
        this.gameObject.GetComponent<CharacterController>().Move(new Vector3(0, 0, -100));
        this.gameObject.GetComponent<CharacterController>().Move(getRespawnVector());
        this.gameObject.GetComponent<CharacterController>().Move(new Vector3(0, 0, 100));

    }

    [Rpc(RpcSources.InputAuthority,RpcTargets.All)]
    public void RPC_CharacterRedSelected()
    {
        ghost.GetComponent<SpriteRenderer>().color = Color.clear;
        //redGuy.SetActive(true);
        elSilbon.SetActive(true);
       selectedCharacter = 2;
        canvas.SetActive(false);
        this.gameObject.GetComponent<CharacterController>().Move(new Vector3(0, 0, -100));
        this.gameObject.GetComponent<CharacterController>().Move(getRespawnVector());
        this.gameObject.GetComponent<CharacterController>().Move(new Vector3(0, 0, 100));
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_CharacterWendigoSelected()
    {
        ghost.GetComponent<SpriteRenderer>().color = Color.clear;
        wendigo.SetActive(true);
        selectedCharacter = 3;
        canvas.SetActive(false);
        this.gameObject.GetComponent<CharacterController>().Move(new Vector3(0, 0, -100));
        this.gameObject.GetComponent<CharacterController>().Move(getRespawnVector());
        this.gameObject.GetComponent<CharacterController>().Move(new Vector3(0, 0, 100));
    }


    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_CharacterDragurSelected()
    {
        ghost.GetComponent<SpriteRenderer>().color = Color.clear;
        Draguar.SetActive(true);
        selectedCharacter = 4;
        canvas.SetActive(false);
        this.gameObject.GetComponent<CharacterController>().Move(new Vector3(0, 0, -100));
        this.gameObject.GetComponent<CharacterController>().Move(getRespawnVector());
        this.gameObject.GetComponent<CharacterController>().Move(new Vector3(0, 0, 100));
    }

    Vector3 getRespawnVector()
    {
        return spawnpoint - this.gameObject.transform.position + new Vector3(0, 0, -100);
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
