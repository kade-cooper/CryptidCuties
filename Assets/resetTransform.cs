using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetTransform : MonoBehaviour
{
    public Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = pos;
    }
}
