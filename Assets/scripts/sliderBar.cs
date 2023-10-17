using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class sliderBar : MonoBehaviour
{

    public float percentFull = 1;
    public float maxSize = 4;
    // Start is called before the first frame update
    void Start()
    {
        //percentFull = 1;
    }

    // Update is called once per frame

    public void onchange(float valueToChagneBy, float maxOfValue)
    {
        float percentToChange = valueToChagneBy / maxOfValue;
        percentFull -= percentToChange;
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
