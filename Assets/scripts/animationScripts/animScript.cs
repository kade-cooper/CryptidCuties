using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animScript : MonoBehaviour
{

    public Animator animator;
    public CharacterInputHandler cih;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cih.moveInputVector.x > 0 || cih.moveInputVector.y > 0)
        {
            AnimatorControllerParameter[] parameters = animator.parameters;
            foreach(AnimatorControllerParameter parameter in parameters)
            {
                if(parameter.name == "isMoving")
                {
                    parameter.Equals(true);
                }
            }
        }
    }
}
