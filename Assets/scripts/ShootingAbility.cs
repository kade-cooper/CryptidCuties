using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

[CreateAssetMenu]
public class ShootingAbility : Ability
{
   // public GameObject BulletTransform;
   // public float bulletVelocity;
    public Transform BulletTransform; 
   // public LayerMask ground;
    public Vector2 lookDirection { get; set; }
    public float lookAngle;
    public GameObject Bullet;
    public float bulletSpeed;

    public void FixedUpdate()
    {
         lookDirection = Camera.main.WorldToScreenPoint(Input.mousePosition);
         lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
         BulletTransform.rotation = Quaternion.Euler(0, 0, 1);

    }

    public override void Activate(GameObject thisThing)
    {
        // lookDirection = Camera.main.WorldToScreenPoint(Input.mousePosition);
        // lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        //BulletTransform.rotation = Quaternion.Euler(0, 0, 1);
        GameObject bulletClone = Instantiate(Bullet);
        bulletClone.transform.position = BulletTransform.position;
        bulletClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);

        bulletClone.GetComponent<Rigidbody2D>().velocity = BulletTransform.right * bulletSpeed;

    }

}
