using UnityEngine;

public abstract class Fighter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected AttackHandler attackHandler;
    [SerializeField] protected CharacterController controller;

    protected IInputProvider input;

    private void Update()
    {
        ReadInput();          // 1. Gather commands (hook)
        HandleMovement();     // 2. Move/rotate
        HandleAttacks();      // 3. Fire strikes / powers
        HandleCooldowns();    // 4. Tick timers & state
    }

    protected abstract void ReadInput();
    protected abstract void HandleMovement();

    protected virtual void HandleAttacks()
    {
        if (!attackHandler.Ready) return;
        if (input.StrikePressed) attackHandler.ExecuteStrike(input.StrikeIndex);
        if (input.PowerPressed) attackHandler.ExecutePower();
    }

    protected virtual void HandleCooldowns() => attackHandler.Tick(Time.deltaTime);
}
