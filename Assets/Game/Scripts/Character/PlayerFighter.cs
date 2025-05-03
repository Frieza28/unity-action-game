using UnityEngine;

public class PlayerFighter : Fighter
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Transform opponent;

    private Vector3 moveDir;
    private bool isJumping = false;
    private float velocityY = 0f;

    protected override void ReadInput()
    {
        Vector2 inputVec = input.MoveVector;
        moveDir = new Vector3(inputVec.x, 0f, inputVec.y);
    }

    protected override void HandleMovement()
    {
        if (controller.isGrounded)
        {
            if (isJumping && velocityY < 0)
            {
                isJumping = false;
                animator.SetBool("IsJumping", false);
            }

            velocityY = -2f;

            // Novo Input System
            if (input.JumpPressed)  
            {
                velocityY = jumpForce;
                isJumping = true;
                animator.SetBool("IsJumping", true);
            }
        }
        else
        {
            velocityY += gravity * Time.deltaTime;
        }

        Vector3 fullMovement = moveDir.normalized * moveSpeed;
        fullMovement.y = velocityY;

        controller.Move(fullMovement * Time.deltaTime);

        if (moveDir.sqrMagnitude > 0.01f)
            transform.forward = moveDir.normalized;

        animator.SetBool("IsWalking", moveDir.sqrMagnitude > 0.01f);

        Vector3 directionToOpponent = opponent.position - transform.position;
        directionToOpponent.y = 0f;
        
        if (directionToOpponent != Vector3.zero)
            transform.forward = directionToOpponent.normalized;
    }

}
