using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class AbilityHolder : NetworkBehaviour
{
    public Ability ability;
    float cooldownTime;
    float activeTime;

    public GameObject thisThing;

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
                        ability.Activate(thisThing);
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
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    ability.OnEnd(thisThing);
                    state = AbilityState.cooldown;
                    cooldownTime = ability.cooldownTime;
                }
                break;
            case AbilityState.cooldown:
                if(cooldownTime >= 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.ready;
                    
                }
                break;

        }
    }
}
