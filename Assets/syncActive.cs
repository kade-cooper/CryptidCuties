using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class syncActive : NetworkBehaviour
{
    [Networked]
    private bool isActive { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(HasStateAuthority)
            isActive = this.gameObject.activeSelf;
        this.gameObject.SetActive(isActive);
    }
}
