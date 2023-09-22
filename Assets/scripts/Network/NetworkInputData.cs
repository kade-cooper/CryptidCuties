using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

//kade's branch

public struct NetworkInputData : INetworkInput
{
    public Vector2 movementInput;
    public float rotationInput;
    public NetworkBool isJumpPressed;
}
