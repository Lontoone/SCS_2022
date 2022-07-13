using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AnimationTest : MonoBehaviour
{
    public Animator Character_Animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InputChange()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            Character_Animator.SetBool("walk",true);

        }
        else if(Input.GetKeyUp(KeyCode.W))
        {
            Character_Animator.SetBool("walk",false);

        }
        else if(Input.GetMouseButtonUp(0))
        {
            Character_Animator.SetBool("Attack",false);

        }
        else if(Input.GetKeyUp(KeyCode.D))
        {
            Character_Animator.SetBool("Dead",true);

        }
    }
}
