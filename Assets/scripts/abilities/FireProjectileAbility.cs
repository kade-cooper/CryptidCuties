using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

[CreateAssetMenu]
public class FireProjectileAbility : Ability
{
    /*
    public GameObject fireProjectilePrefab;
    public float fireProjectileVelocity;
    public int maxSpeed;

    public override void Activate(GameObject thisThing, playerRomanceHandler PRH)
    {
        NetworkRunner runner = GameObject.FindObjectOfType<NetworkRunner>();
        NetworkObject thing = runner.Spawn(fireProjectilePrefab, thisThing.transform.position);
        thing.gameObject.layer = thisThing.gameObject.layer;
        thing.GetComponent<attackScript>().prh = PRH;
        RPC_AdjustFireProjectile(thing, thisThing, PRH);
        thing.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
    }

    public void RPC_AdjustFireProjectile(NetworkObject fireProjectile, GameObject sender, playerRomanceHandler senderPRH)
    {
        fireProjectile.gameObject.layer = sender.gameObject.layer;
        //thing.GetComponent<Renderer>().material.color = Color.green;
        fireProjectile.GetComponent<attackScript>().prh = senderPRH;
        fireProjectile.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
