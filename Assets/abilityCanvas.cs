using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;

public class abilityCanvas : NetworkBehaviour
{

    public Color invis;
    // Start is called before the first frame update
    void Start()
    {
        if (!HasInputAuthority && !HasStateAuthority)
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (!HasInputAuthority && !HasStateAuthority)
        {
            this.gameObject.SetActive(false);
        }
        else if (HasStateAuthority && !HasInputAuthority)
        {
            foreach(Transform t in this.transform)
            {
                t.GetComponent<Image>().color = invis;
            }
            
        }
    }
}
