using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

//kade's branch

public class CharacterInputHandler : MonoBehaviour
{
    Vector2 moveInputVector = Vector2.zero;


    bool isAttack1Pressed = false;
    bool isRomanceAttkPressed = false;

    public bool canInput=true;

    public CharacterMovementHandler characterMovementHandler;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!characterMovementHandler.Object.HasInputAuthority || !canInput)
            return;

        moveInputVector.x = Input.GetAxis("Horizontal");
        moveInputVector.y = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Fire1"))
            isAttack1Pressed = true;
        else if (Input.GetButtonUp("Fire1"))
            isAttack1Pressed = false;


        if (Input.GetButtonDown("Fire2"))
            isRomanceAttkPressed = true;
        else if (Input.GetButtonUp("Fire2"))
            isRomanceAttkPressed = false;
    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData();

        //Move data
        networkInputData.movementInput = moveInputVector;

        //jump data

        //attack data
        networkInputData.isAttacking = isAttack1Pressed;
        networkInputData.isRomanceAttk = isRomanceAttkPressed;

        //Health data
      //  networkInputData.Health = Health;

        //Reset vairables now that we have read their states
      //  isAttacking = false;

        return networkInputData;
    }

}
