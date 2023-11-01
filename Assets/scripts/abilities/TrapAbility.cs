using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


[CreateAssetMenu]
public class TrapAbility : Ability
{


    public GameObject trapPrefab;

    public override void Activate(GameObject trapPrefab)
    {
        NetworkRunner runner = GameObject.FindObjectOfType<NetworkRunner>();
        NetworkObject thing = runner.Spawn(trapPrefab, trapPrefab.transform.position);
        thing.gameObject.layer = trapPrefab.gameObject.layer;
        
    }

    public override void OnEnd(GameObject thisThing)
    {

    }
}
