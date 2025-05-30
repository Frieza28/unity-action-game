using UnityEngine;

public abstract class Fighter : MonoBehaviour
{
    [Header("References")]
    protected Animator animator;
    protected AttackHandler attackHandler;
    protected CharacterController controller;

    protected IInputProvider input;

    protected void Awake()
    {
        if (animator == null) animator = GetComponent<Animator>();
        if (attackHandler == null) attackHandler = GetComponent<AttackHandler>();
        attackHandler.Init(animator);
        if (controller == null) controller = GetComponent<CharacterController>();
        if (input == null) input = GetComponent<IInputProvider>();
    }
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
        if (input.PunchPressed) attackHandler.ExecuteStrike(0);
        if (input.KickPressed) attackHandler.ExecuteStrike(1);
        if (input.PowerPressed) attackHandler.ExecutePower();
    }

    protected virtual void HandleCooldowns() => attackHandler.Tick(Time.deltaTime);

    protected virtual void HandleCombat() { }
}
