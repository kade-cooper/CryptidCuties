using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;

public class AbilityHolder : NetworkBehaviour
{
    public Ability ability;

    [Networked]
    float cooldownTime { get; set; }
    [Networked]
    float activeTime { get; set; }


    [Networked]
    public float fill { get; set; }

    public GameObject thisThing;
    public playerRomanceHandler thisPRH;

    public Image fillImage;

    NetworkRunner runner;

    enum AbilityState
    {
        ready, 
        active, 
        cooldown
    }

    AbilityState state = AbilityState.ready;

    //this is where key selection is selected in inspector for each ability
   // public KeyCode key;

    // Update is called once per frame
    public void Update()
    {

            fillImage.fillAmount = fill;
    }

    public void Start()
    {
        runner = GameObject.FindObjectOfType<NetworkRunner>();
    }
    public override void FixedUpdateNetwork()
    {
        //this is where ability will be activated or set to cooldown depending on stage of key being pressed.
        switch (state)
        {
            case AbilityState.ready:
                if (GetInput(out NetworkInputData networkInputData))
                {
                    if (networkInputData.isAbility1)
                    {
                        //Debug.Log(key + " down");
                        ability.Activate(thisThing, thisPRH);
                        Debug.Log("did activate");
                        activeTime = ability.activeTime;
                        state = AbilityState.active;
                    }
                    //Activate
                }
                break;
            case AbilityState.active:
                if(activeTime >= 0)
                {
                    activeTime -= runner.DeltaTime;
                }
                else
                {
                    ability.OnEnd(thisThing, thisPRH);
                    cooldownTime = ability.cooldownTime;
                    
                    state = AbilityState.cooldown;
                    
                }
                break;
            case AbilityState.cooldown:
                if(cooldownTime >= 0)
                {
                    cooldownTime -= runner.DeltaTime;
                    //if (HasStateAuthority)
                    //{
                        fill = cooldownTime / ability.cooldownTime;
                        fill = cooldownTime / ability.cooldownTime;
                    //}
                    /*
                    else
                    {
                        fill = cooldownTime / ManualSetCT;
                    }
                    */
                }
                else
                {
                    state = AbilityState.ready;
                    
                }
                break;

        }
    }
}
