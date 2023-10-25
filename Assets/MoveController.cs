using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    Animator animator;

    int isWalkingHash;
    int isRunningHash;

    bool isMove;

    bool forwardPressed;
    bool backPressed;
    bool leftPressed;
    bool rightPressed;

    bool runPressed;

    void Start()
    {
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    void Update()
    {
        handleKeyboard();
        handleRotation();
        handleMovement();
    }

    void handleKeyboard()
    {
        forwardPressed = Input.GetKey("w");
        backPressed = Input.GetKey("s");
        leftPressed = Input.GetKey("a");
        rightPressed = Input.GetKey("d");
        runPressed = Input.GetKey("left shift");

        isMove = forwardPressed || backPressed || leftPressed || rightPressed;
    }

    void handleMovement()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        if (!isWalking && isMove)
        {
            animator.SetBool(isWalkingHash, true);
        }
        if (isWalking && !isMove)
        {
            animator.SetBool(isWalkingHash, false);
        }
        if (!isRunning && isMove && runPressed)
        {
            animator.SetBool(isRunningHash, true);
        }
        if (isRunning && (!isMove || !runPressed))
        {
            animator.SetBool(isRunningHash, false);
        }
    }

    void handleRotation(){
        Vector2 currentMovement = new Vector2(0,0);

        if(forwardPressed && !backPressed){
            currentMovement.y = 1;
        }
        if(backPressed && !forwardPressed){
            currentMovement.y = -1;
        }
        if(leftPressed && !rightPressed){
            currentMovement.x = -1;
        }
        if(rightPressed && !leftPressed){
            currentMovement.x = 1;
        }

        Vector3 currentPosition = transform.position;

        Vector3 newPosition = new Vector3(currentMovement.x, 0, currentMovement.y);

        Vector3 positionToLookAt = currentPosition + newPosition;

        transform.LookAt(positionToLookAt);
    }
}
