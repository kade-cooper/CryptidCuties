using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CharacterInputHandler : MonoBehaviour
{
    Vector2 moveInputVector = Vector2.zero;

    public NetworkBool wasAttacking;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInputVector.x = Input.GetAxis("Horizontal");
        moveInputVector.y = Input.GetAxis("Vertical");

        if(Input.GetButtonDown("Fire1"))
        wasAttacking = true;
    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData();

        //Move data
        networkInputData.movementInput = moveInputVector;

        //jump data

        //attack data
        networkInputData.isAttacking = wasAttacking;

        //Health data
      //  networkInputData.Health = Health;

        //Reset vairables now that we have read their states
      //  isAttacking = false;

        return networkInputData;
    }

}
