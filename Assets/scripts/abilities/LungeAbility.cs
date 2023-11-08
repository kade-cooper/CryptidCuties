using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

[CreateAssetMenu]
public class LungeAbility : Ability
{
    public float lungeVelocity;
    public float alot;
    public GameObject lungeCircle;

    public int maxSpeed;
    public override void Activate(GameObject thisThing, playerRomanceHandler prh)
    {
        NetworkRunner runner = GameObject.FindObjectOfType<NetworkRunner>();
        var deltaTime = runner.DeltaTime;
        Debug.Log("before activate");
        //CharacterMovementHandler movement = parent.GetComponent<CharacterMovementHandler>();
        lungeCircle = thisThing.transform.GetChild(2).gameObject;
        lungeCircle.SetActive(true);
        NetworkCharacterControllerPrototypeCustom characterCollider = thisThing.GetComponentInParent<Transform>().GetComponentInParent<NetworkCharacterControllerPrototypeCustom>();
        CharacterInputHandler inputHandler = thisThing.GetComponentInParent<Transform>().GetComponentInParent<CharacterInputHandler>();
        Debug.Log(characterCollider);

        characterCollider.maxSpeed += lungeVelocity;
        characterCollider.acceleration += alot;
        inputHandler.canInput = false;

        // movement.moveDirection.normalized * dashVelocity;
        //characterCollider.Velocity += new Vector3(0, 0, 0);// movement.moveDirection.normalized * dashVelocity;
    }

    public override void OnEnd(GameObject thisThing, playerRomanceHandler prh)
    {
        NetworkCharacterControllerPrototypeCustom characterCollider = thisThing.GetComponentInParent<Transform>().GetComponentInParent<NetworkCharacterControllerPrototypeCustom>();
        CharacterInputHandler inputHandler = thisThing.GetComponentInParent<Transform>().GetComponentInParent<CharacterInputHandler>();
        characterCollider.maxSpeed = maxSpeed;
        characterCollider.acceleration -= alot;
        if (lungeCircle != null)
        {
            lungeCircle.SetActive(false);
        }
        inputHandler.canInput = true;
    }
}
