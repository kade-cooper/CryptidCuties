using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public new string name;
    public float cooldownTime;
    public float activeTime;

    //this is where abilities will be activated
    public virtual void Activate(GameObject parent, playerRomanceHandler prh)
    {
        Debug.Log("not being overridden");
    }

    public virtual void OnEnd(GameObject thisThing, playerRomanceHandler prh)
    {
        Debug.Log("not being overriden");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
