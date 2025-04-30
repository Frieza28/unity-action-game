using UnityEngine;

public class PlayerFighter : Fighter
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector3 moveDir;

    protected override void ReadInput()
    {
        Vector2 inputVec = input.MoveVector;
        moveDir = new Vector3(inputVec.x, 0f, inputVec.y);
    }

    protected override void HandleMovement()
    {
        float originalY = transform.position.y;

        if (moveDir.sqrMagnitude > 0.01f)
        {
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
            transform.forward = moveDir.normalized;
        }

        Vector3 fix = transform.position;
        fix.y = originalY;
        transform.position = fix;

        //if (!controller.isGrounded)
        //    controller.Move(Physics.gravity * Time.deltaTime);
        //    Debug.Log(controller.isGrounded);       
    }
}
