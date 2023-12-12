using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class syncLayer : NetworkBehaviour
{

    [Networked] public int thisLayer { get; set; }

    public GameObject other;


    // Start is called before the first frame update
    void Start()
    {
        if(HasStateAuthority)
            thisLayer = this.gameObject.layer;
        this.gameObject.layer = thisLayer;
        //this.gameObject.layer = tempLayer;
    }

    // Update is called once per frame
    void Update()
    {
        if(HasStateAuthority)
            thisLayer = this.gameObject.layer;
        this.gameObject.layer = thisLayer;
    }

}
