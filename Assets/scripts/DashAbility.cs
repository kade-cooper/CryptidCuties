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
        CapsuleCollider2D characterCollider = parent.GetComponent<CapsuleCollider2D>();

       // CapsuleCollider2D.velocity = movement.moveDirection.normalized * dashVelocity;
    }
}
