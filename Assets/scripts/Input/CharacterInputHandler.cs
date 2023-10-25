using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

//kade's branch

public class CharacterInputHandler : MonoBehaviour
{
    Vector2 moveInputVector = Vector2.zero;

    public bool canInput=true;

    public CharacterMovementHandler characterMovementHandler;


    bool isAttack1Pressed = false;
    bool isRomanceAttkPressed = false;
    bool isAbility1Pressed = false;



    public KeyCode keyAttk1;
    public KeyCode keyRomanceAttk;
    public KeyCode keyAbility1;



    // Start is called before the first frame update
    void Start()
    {
        
    }
    //sets the key codes to the selected crypids key codes
    public void cryptidSelected(KeyCode attk1, KeyCode keyRA, KeyCode keyA1)
    {
        keyAttk1 = attk1;
        keyRomanceAttk = keyRA;
        keyAbility1 = keyA1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!characterMovementHandler.Object.HasInputAuthority || !canInput)
            return;

        moveInputVector.x = Input.GetAxis("Horizontal");
        moveInputVector.y = Input.GetAxis("Vertical");


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
        networkInputData.movementInput = moveInputVector;

        networkInputData.isAttacking = isAttack1Pressed;
        networkInputData.isRomanceAttk = isRomanceAttkPressed;
        networkInputData.isAbility1 = isAbility1Pressed;
        //jump data

        //attack data

        //Health data
        //  networkInputData.Health = Health;

        //Reset vairables now that we have read their states
        //  isAttacking = false;

        return networkInputData;
    }

}
