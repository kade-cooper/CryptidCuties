using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class AttackFollowMouse : NetworkBehaviour
{
    public GameObject playerRef;
    public Vector3 upward;
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void FixedUpdateNetwork()
    {
        /*
        //Vector3 rotation = new Vector3(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"),0);
        Vector2 rotation = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //float angle = Vector3.Angle(playerRef.transform.position, rotation);
        Vector2 playerPos = new Vector2(this.transform.position.x, this.transform.position.y);
        Vector2 heading = rotation - playerPos;
        Vector2 frontOfPlayer = new Vector2(playerPos.x+1, playerPos.y) - playerPos;
        float angle = Vector2.Angle(heading, frontOfPlayer);
        float sign = Mathf.Sign(Vector3.Dot(heading.normalized, Vector3.Cross(heading, frontOfPlayer)));
        Debug.Log("angle"+angle);
        //float rotation = (Input.GetAxisRaw("Mouse X")/ Input.GetAxisRaw("Mouse Y"));
        //this.transform.rotation = (Quaternion.AxisAngle(upward,angle));
        this.transform.eulerAngles =new Vector3(0,0,angle*sign);
        //this.transform.rotation = (Quaternion.Euler(new Vector3(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"),0))); ;
        */
        if (Input.mousePosition.x - (this.transform.position.x+offset) < 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
