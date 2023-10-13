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
        runner.Spawn(trapPrefab);
    }

    public override void OnEnd(GameObject thisThing)
    {

    }
}
