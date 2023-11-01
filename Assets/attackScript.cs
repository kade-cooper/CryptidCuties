using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class attackScript : MonoBehaviour
{
    public NetworkString<_32> tagthing { get; set; }
    public playerRomanceHandler prh;
    // Start is called before the first frame update
    void Start()
    {
        if (prh != null)
        {
            tagthing = prh.tagthing;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecieveTag(string tag)
    {
        tagthing = tag;
    }
}
