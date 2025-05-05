using UnityEngine;

public interface IInputProvider
{
    Vector2 MoveVector { get; }
    bool PunchPressed { get; }
    bool KickPressed { get; }
    bool PowerPressed { get; }
    bool JumpPressed { get; }
    bool StrikePressed { get; }
}
