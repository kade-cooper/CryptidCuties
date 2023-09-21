using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkCSelect : NetworkBehaviour, IPlayerLeft
{
    public static NetworkCSelect Local { get; set; }
    void Start()
    {

    }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;
            Debug.Log("Spawned local character select screen");
        }
        else
        {
            Debug.Log("Spawned remote character select screen");
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
