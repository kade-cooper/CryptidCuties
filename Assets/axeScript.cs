using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class axeScript : NetworkBehaviour
{

    [Networked] public int thisLayer {get; set;}
    public int velocity;
    public int angVelocity;

    // Start is called before the first frame update
    void Start()
    {
        if (HasStateAuthority) {
            RPC_SendLayer(this.gameObject.layer);
        }
        this.gameObject.layer = thisLayer;
        StartCoroutine(Retry());
        //this.gameObject.layer = tempLayer;
        this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = angVelocity;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = this.transform.right * velocity;
    }

    IEnumerator Retry()
    {
        yield return new WaitForSeconds(.1f);
        this.gameObject.layer = thisLayer;
        this.gameObject.GetComponent<PolygonCollider2D>().enabled = true;


    }

    // Update is called once per frame
    void Update()
    {

    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_SendLayer(int layer)
    {
        thisLayer = layer;
    }
}
