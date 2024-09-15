using UnityEngine;

public class RumJump : MonoBehaviour
{
     [SerializeField] private Animator heroiAnim;

    void Update()
    {
        
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Space))
        {
           heroiAnim.SetBool("RumJump", true);
        }
        else
        {
            heroiAnim.SetBool("RumJump", false);
        }
    }
}
