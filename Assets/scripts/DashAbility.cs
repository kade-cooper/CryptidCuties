using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Fusion;


[CreateAssetMenu]
public class DashAbility : Ability
{
    
    private void Awake()
    {
       
    }
    public float dashVelocity;
    public override void Activate(GameObject parent)
    {
        CharacterMovementHandler movement = parent.GetComponent<CharacterMovementHandler>();
        NetworkCharacterControllerPrototypeCustom characterCollider = parent.GetComponentInParent<Transform>().GetComponentInParent<NetworkCharacterControllerPrototypeCustom>();

        characterCollider.Velocity += characterCollider.Velocity * dashVelocity;// movement.moveDirection.normalized * dashVelocity;
    }
}
