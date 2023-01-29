using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EleanorMoveScript : MonoBehaviour
{
    float lastPos;
    public float offset;
    public Animator animator;
    public Rigidbody body;

    void Start(){
        lastPos = gameObject.transform.position.x;
        offset = 0;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        offset = gameObject.transform.position.x - lastPos;
        if(offset != 0f){
            animator.SetBool("moving", true);
        }
        else
            animator.SetBool("moving", false);
        
        animator.SetFloat("MoveDir", body.velocity.x);
        lastPos = gameObject.transform.position.x;
    }
}
