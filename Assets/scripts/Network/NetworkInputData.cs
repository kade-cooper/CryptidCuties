using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

//kade's branch

public struct NetworkInputData : INetworkInput
{

 
  public Vector2 movementInput;

    public Vector3 mousepos;


  public Vector2 aimForwardVector;

  public float rotationInput;
  public NetworkBool isJumpPressed;

  public NetworkBool isAbility1;


 
  public NetworkBool isHit;
  public NetworkBool onHit;
  public float RomanceMeter;
  public float RomanceMeterFull;
  public NetworkBool RomanceReady;


  public NetworkBool isMoving;
  public NetworkBool isAttacking;
  public NetworkBool isRomanceAttk;
  public NetworkBool attackLands;
  
}
