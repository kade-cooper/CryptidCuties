using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

[CreateAssetMenu]
public class AxeProjectileAbility : Ability
{
    public GameObject axeProjectilePrefab;
    public float axeProjectileVelocity;
    public int maxSpeed;

    public override void Activate(GameObject thisThing, playerRomanceHandler PRH)
    {
        NetworkRunner runner = GameObject.FindObjectOfType<NetworkRunner>();
        NetworkObject thing = runner.Spawn(axeProjectilePrefab, thisThing.transform.position);
        thing.gameObject.layer = thisThing.gameObject.layer;
        thing.GetComponent<attackScript>().prh = PRH;
        RPC_AdjustAxeProjectile(thing, thisThing, PRH);
        thing.gameObject.GetComponent<PolygonCollider2D>().enabled = true;

    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_AdjustAxeProjectile(NetworkObject axeProjectile, GameObject sender, playerRomanceHandler senderPRH)
    {
        axeProjectile.gameObject.layer = sender.gameObject.layer;
        //thing.GetComponent<Renderer>().material.color = Color.green;
        axeProjectile.GetComponent<attackScript>().prh = senderPRH;
        axeProjectile.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
    }

    public override void OnEnd(GameObject thisThing, playerRomanceHandler PRH)
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}