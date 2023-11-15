using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationScript : MonoBehaviour
{
    private Animator Animator;
    
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        
        Animator.SetBool("Run", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            Animator.SetBool("Run", true);
        }
        
        if (Input.GetKeyUp("w"))
        {
            Animator.SetBool("Run", false);
        }
    }
}
