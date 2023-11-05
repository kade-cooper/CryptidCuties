using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


[CreateAssetMenu]
public class TrapAbility : Ability
{


    public GameObject trapPrefab;
    public playerRomanceHandler thisPRH;

    public override void Activate(GameObject thisThing, playerRomanceHandler PRH)
    {
        thisPRH = PRH;
        //RPC_GetPRH(thisPRH);
        NetworkRunner runner = GameObject.FindObjectOfType<NetworkRunner>();
        NetworkObject thing = runner.Spawn(trapPrefab, thisThing.transform.position);
        thing.gameObject.layer = thisThing.gameObject.layer;
        //thing.GetComponent<Renderer>().material.color = Color.green;
        thing.GetComponent<attackScript>().prh = thisPRH;
        RPC_AdjustTrap(thing, thisThing, thisPRH);
        thing.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
    }


    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_GetPRH(playerRomanceHandler sentPRH)
    {
        thisPRH = sentPRH;
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_AdjustTrap(NetworkObject trap, GameObject sender, playerRomanceHandler senderPRH)
    {
        trap.gameObject.layer = sender.gameObject.layer;
        //thing.GetComponent<Renderer>().material.color = Color.green;
        trap.GetComponent<attackScript>().prh = senderPRH;
        trap.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
    }

    public override void OnEnd(GameObject thisThing, playerRomanceHandler PRH)
    {

    }
}
