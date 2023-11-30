using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class animScript : NetworkBehaviour
{

    public Animator animator;
    public CharacterInputHandler cih;
    [Networked] public NetworkBool isMoving { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cih.moveInputVector.x > 0 || cih.moveInputVector.y > 0 || cih.moveInputVector.x < 0 || cih.moveInputVector.y < 0)
        {
            Debug.Log("is Moving");
            animator.Play("Move");
            //AnimatorControllerParameter[] parameters = animator.parameters;
           // foreach(AnimatorControllerParameter parameter in parameters)
           // {
           //     if(parameter.name == "isMoving")
            //    {
           //         parameter.Equals(true);
                    
            //    }
           // }
        }
    }
}
