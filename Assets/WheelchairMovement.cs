using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelchairMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public float rotationSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            animator.enabled=true;
            //animator.SetBool("mover",true);
        }else{
            animator.enabled=false;
            //animator.SetBool("mover",false);
        }
        
        if(Input.GetKey(KeyCode.A)){
            //animator.enabled=true;
            //animator.SetBool("mover",true);
            transform.Rotate(0,rotationSpeed*Time.deltaTime*-1,0);
        }
        else if(Input.GetKey(KeyCode.D)){
            //animator.enabled=true;
            //animator.SetBool("mover",true);
            transform.Rotate(0,rotationSpeed*Time.deltaTime,0);
        }
    }
}
