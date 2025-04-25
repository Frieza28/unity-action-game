using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 3f;

    private BigVegasInputActions controls;
    private Vector2 movement;

    private void Awake()
    {
        controls = new BigVegasInputActions();

        // Usa o wrapper .Player para aceder às ações
        controls.Player.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => movement = Vector2.zero;

        controls.Player.PunchLeft.performed += _ => animator.SetTrigger("PunchLeft");
        controls.Player.PunchRight.performed += _ => animator.SetTrigger("PunchRight");
        controls.Player.KickLeft.performed += _ => animator.SetTrigger("KickLeft");
        controls.Player.KickRight.performed += _ => animator.SetTrigger("KickRight");
        controls.Player.PowerAttack.performed += _ => animator.SetTrigger("PowerAttack");
    }

    private void OnEnable() => controls.Player.Enable();
    private void OnDisable() => controls.Player.Disable();

    void Update()
    {
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

        animator.SetFloat("Speed", move.magnitude);
    }
}
