using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAnimationStateController : MonoBehaviour
{
    public Animator animator;
    private float playerMovementState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerMovementState = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().moveDirection;
        if (playerMovementState > 0 || playerMovementState < 0)
        {
            animator.SetBool("isWalking", true);
        } else
        {
            animator.SetBool("isWalking", false);
        }
        //Debug.Log(playerMovementState);
    }
}
