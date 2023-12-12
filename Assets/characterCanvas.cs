using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterCanvas : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!HasInputAuthority)
            this.gameObject.SetActive(false);
    }
}
