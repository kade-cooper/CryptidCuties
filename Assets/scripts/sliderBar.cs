using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class sliderBar : MonoBehaviour
{

    [Networked]
    public float percentFull { get; set; }

    public float maxSize = 4;
    public bool isR;
    // Start is called before the first frame update
    void Start()
    {
        if (isR)
            percentFull = 0;
        else
            percentFull = 1;
        this.transform.localScale = new Vector3(percentFull * maxSize, 1, 1);
    }

    // Update is called once per frame

    public void onchange(float valueToChagneBy, float maxOfValue, int multiplier)
    {
        float percentToChange = valueToChagneBy / maxOfValue;
        percentFull -= multiplier*percentToChange;
        this.transform.localScale = new Vector3(percentFull*maxSize,1,1);
    }

    public void changeTo(float percentTo)
    {
        this.transform.localScale = new Vector3(percentTo * maxSize, 1, 1);
    }

    public void onfull()
    {
        percentFull = 1;
        this.transform.localScale = new Vector3(percentFull *maxSize, 1, 1);
    }


}
