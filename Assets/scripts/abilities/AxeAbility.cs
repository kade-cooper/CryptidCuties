using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


[CreateAssetMenu]
public class AxeAbility : Ability
{


    public GameObject axePrefab;
    public GameObject rotateObj;
    //public playerRomanceHandler thisPRH;

    public override void Activate(GameObject thisThing, playerRomanceHandler PRH)
    {
        rotateObj = thisThing.transform.Find("AttackContainer").gameObject;
        //RPC_GetPRH(thisPRH);
        NetworkRunner runner = GameObject.FindObjectOfType<NetworkRunner>();
        NetworkObject thing = runner.Spawn(axePrefab, rotateObj.transform.position, rotateObj.transform.rotation);
        thing.gameObject.layer = 0;
        //thing.GetComponent<Renderer>().material.color = Color.green;
        thing.GetComponent<attackScript>().prh = PRH;
        RPC_AdjustAxe(thing, thisThing, PRH);
        thing.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
    }

    /*
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_GetPRH(playerRomanceHandler sentPRH)
    {
        thisPRH = sentPRH;
    }
    */
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_AdjustAxe(NetworkObject axe, GameObject sender, playerRomanceHandler senderPRH)
    {
        axe.gameObject.layer = sender.gameObject.layer;
        //thing.GetComponent<Renderer>().material.color = Color.green;
        axe.GetComponent<attackScript>().prh = senderPRH;
        axe.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
    }

    public override void OnEnd(GameObject thisThing, playerRomanceHandler PRH)
    {

    }
}
