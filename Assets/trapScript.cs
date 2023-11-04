using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class trapScript : NetworkBehaviour
{

    public string tempLayer;
    // Start is called before the first frame update
    void Start()
    {
        //RPC_SendLayer();
        //this.gameObject.layer = LayerMask.NameToLayer(tempLayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SendLayer()
    {
        tempLayer = this.gameObject.layer.ToString();
    }
}
