using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class networkFill : NetworkBehaviour
{
    [Networked]
    public float fill { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fill = this.GetComponent<Image>().fillAmount;
        this.GetComponent<Image> ().fillAmount = fill;
    }
}
