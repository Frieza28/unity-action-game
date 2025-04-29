public class PlayerFighter : Fighter
{
    protected override void Awake()
    {
        base.Awake();
        input = GetComponent<IInputProvider>();
    }

    protected override void ReadInput()
    {
    }

    protected override void HandleMovement()
    {
    }
}
