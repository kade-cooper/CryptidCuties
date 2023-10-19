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

    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData();

        //Move data
        networkInputData.movementInput = moveInputVector;

        //jump data

        //attack data

        //Health data
      //  networkInputData.Health = Health;

        //Reset vairables now that we have read their states
      //  isAttacking = false;

        return networkInputData;
    }

}
