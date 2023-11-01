using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class trapScript : NetworkBehaviour
{

    public Vector3 tempLocation;
    // Start is called before the first frame update
    void Start()
    {
        RPC_SendLocation();
        this.transform.position = tempLocation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SendLocation()
    {
        tempLocation =  this.transform.position;
    }
}
