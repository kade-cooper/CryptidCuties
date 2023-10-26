using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


[CreateAssetMenu]
public class DashAbility : Ability
{
    public float dashVelocity;
    public float alot;
    public int maxSpeed;

    public override void Activate(GameObject thisThing)
    {
        NetworkRunner runner = GameObject.FindObjectOfType<NetworkRunner>();
        var deltaTime = runner.DeltaTime;
        Debug.Log("before activate");
        //CharacterMovementHandler movement = parent.GetComponent<CharacterMovementHandler>();
        NetworkCharacterControllerPrototypeCustom characterCollider = thisThing.GetComponentInParent<Transform>().GetComponentInParent<NetworkCharacterControllerPrototypeCustom>();
        Debug.Log(characterCollider);
        thisThing.GetComponent<CryptidScript>().canBeDamaged = false;
        characterCollider.maxSpeed += dashVelocity;
        characterCollider.acceleration += alot;

        // movement.moveDirection.normalized * dashVelocity;
        //characterCollider.Velocity += new Vector3(0, 0, 0);// movement.moveDirection.normalized * dashVelocity;
    }

    public override void OnEnd(GameObject thisThing)
    {
        NetworkCharacterControllerPrototypeCustom characterCollider = thisThing.GetComponentInParent<Transform>().GetComponentInParent<NetworkCharacterControllerPrototypeCustom>();
        characterCollider.maxSpeed = maxSpeed;
        characterCollider.acceleration = 20;
        thisThing.GetComponent<CryptidScript>().canBeDamaged = true;
    }
}
