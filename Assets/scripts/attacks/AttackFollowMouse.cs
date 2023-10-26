using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class AttackFollowMouse : NetworkBehaviour
{
    public GameObject playerRef;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void FixedUpdateNetwork()
    {
        this.transform.rotation = (Quaternion.Euler(new Vector3(Input.mousePosition.x,Input.mousePosition.y,Input.mousePosition.z).normalized));
    }
}
