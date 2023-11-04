using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


[CreateAssetMenu]
public class TrapAbility : Ability
{


    public GameObject trapPrefab;
    public playerRomanceHandler thisPRH;
    public playerRomanceHandler tempPRH;

    public override void Activate(GameObject thisThing, playerRomanceHandler PRH)
    {
        thisPRH = PRH;
        RPC_GetPRH();
        thisPRH = tempPRH;
        NetworkRunner runner = GameObject.FindObjectOfType<NetworkRunner>();
        NetworkObject thing = runner.Spawn(trapPrefab, thisThing.transform.position);
        thing.gameObject.layer = thisThing.gameObject.layer;
        thing.GetComponent<attackScript>().prh = thisPRH;
    }

    public void RPC_GetPRH()
    {
        tempPRH = thisPRH;
    }

    public override void OnEnd(GameObject thisThing, playerRomanceHandler PRH)
    {

    }
}
