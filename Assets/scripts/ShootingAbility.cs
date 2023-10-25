using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

[CreateAssetMenu]
public class ShootingAbility : Ability
{
    public GameObject BulletTransform;
    public float bulletVelocity;
    public Transform spawnBullet; //may need to change this to rotate point or whatever the actual name is of object being used in inspector.
    public LayerMask ground;

    public override void Activate(GameObject thisThing)
    {

    }

}
