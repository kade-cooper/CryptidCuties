using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{

    public GameObject canvas;
    public GameObject blueGuy;
    public GameObject redGuy;

    public Transform playerUI;
    public static NetworkPlayer Local { get; set; }
    void Start()
    {
        
    }
    public void CharacterBlueSelected()
    {
        blueGuy.SetActive(true);
        canvas.SetActive(false);
    }
    public void CharacterRedSelected()
    {
        redGuy.SetActive(true);
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

            AudioListener audioListener= GetComponentInChildren<AudioListener>();
            audioListener.enabled = false;
            Debug.Log("Spawned remote player");
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
