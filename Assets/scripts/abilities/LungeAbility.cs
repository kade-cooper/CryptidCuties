using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

[CreateAssetMenu]
public class LungeAbility : Ability
{
    public float lungeVelocity;
    public float alot;
    [Networked]
    public bool lungeBool { get; set; }
    public GameObject lungeCircle;

    public int maxSpeed;
    public void Start()
    {
        lungeBool = false;
    }

    public void Update()
    {
        //lungeCircle.SetActive(lungeBool);
    }
    public override void Activate(GameObject thisThing, playerRomanceHandler prh)
    {
        NetworkRunner runner = GameObject.FindObjectOfType<NetworkRunner>();
        var deltaTime = runner.DeltaTime;
        Debug.Log("before activate");
        //CharacterMovementHandler movement = parent.GetComponent<CharacterMovementHandler>();
        lungeBool = true;
        lungeCircle = thisThing.transform.Find("LungeCircle").gameObject;
        lungeCircle.SetActive(lungeBool);
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
        lungeBool = false;
        if (lungeCircle != null)
        {
            lungeCircle.SetActive(lungeBool);
        }  
        inputHandler.isAbility1Pressed = false;
        inputHandler.canInput = true;

    }
}
