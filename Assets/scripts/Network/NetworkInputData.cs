using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

//kade's branch

public struct NetworkInputData : INetworkInput
{

 
  public Vector2 movementInput;

  public Vector2 aimForwardVector;

  public float rotationInput;
  public NetworkBool isJumpPressed;


 
  public NetworkBool isHit;
  public NetworkBool onHit;
  public float RomanceMeter;
  public float RomanceMeterFull;
  public NetworkBool RomanceReady;

  
  public NetworkBool isAttacking;
  public NetworkBool isRomanceAttk;
  public NetworkBool attackLands;

  
}
