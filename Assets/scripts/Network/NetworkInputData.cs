using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public struct NetworkInputData : INetworkInput
{

 
  public Vector2 movementInput;
  public float rotationInput;
  public NetworkBool isJumpPressed;


 
  public NetworkBool isHit;
  public NetworkBool onHit;
  public float RomanceMeter;
  public float RomanceMeterFull;
  public NetworkBool RomanceReady;

  
  public NetworkBool isAttacking;
  public NetworkBool attackLands;

  
}
