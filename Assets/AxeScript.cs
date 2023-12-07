using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class AxeScript : NetworkBehaviour
{
    [Networked] public int thisLayer { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        if (HasStateAuthority)
        {
            RPC_SendLayer(this.gameObject.layer);
        }
        this.gameObject.layer = thisLayer;
        StartCoroutine(Retry());
        //this.gameObject.layer = tempLayer;
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
