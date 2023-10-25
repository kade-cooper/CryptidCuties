using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

//kade's branch

public class CryptidInputHandler : MonoBehaviour
{


    bool isAttack1Pressed = false;
    bool isRomanceAttkPressed = false;
    bool isAbility1Pressed = false;

    public bool canInput = true;

    public CharacterMovementHandler characterMovementHandler;


    public KeyCode keyAttk1;
    public KeyCode keyRomanceAttk;
    public KeyCode keyAbility1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!characterMovementHandler.Object.HasInputAuthority || !canInput)
            return;

        if (Input.GetKeyDown(keyAttk1))
            isAttack1Pressed = true;
        else if (Input.GetKeyUp(keyAttk1))
            isAttack1Pressed = false;


        if (Input.GetKeyDown(keyRomanceAttk))
            isRomanceAttkPressed = true;
        else if (Input.GetKeyUp(keyRomanceAttk))
            isRomanceAttkPressed = false;

        if (Input.GetKeyDown(keyAbility1))
            isAbility1Pressed = true;
        else if (Input.GetKeyUp(keyAbility1))
            isAbility1Pressed = false;
    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData();

        //Move data
        //jump data

        //attack data
        networkInputData.isAttacking = isAttack1Pressed;
        networkInputData.isRomanceAttk = isRomanceAttkPressed;
        networkInputData.isAbility1 = isAbility1Pressed;

        //Health data
        //  networkInputData.Health = Health;

        //Reset vairables now that we have read their states
        //  isAttacking = false;
        return networkInputData;
    }

}
