using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


[CreateAssetMenu]
public class TrapAbility : Ability
{


    public GameObject trapPrefab;

    public override void Activate(GameObject thisThing)
    {
        NetworkRunner runner = GameObject.FindObjectOfType<NetworkRunner>();
        NetworkObject thing = runner.Spawn(trapPrefab, thisThing.transform.position);
        thing.gameObject.layer = thisThing.gameObject.layer;
        
    }

    public override void OnEnd(GameObject thisThing)
    {

    }
}
