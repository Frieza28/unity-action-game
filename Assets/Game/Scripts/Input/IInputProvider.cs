using UnityEngine;

public interface IInputProvider
{
    Vector2 MoveVector { get; }
    bool PunchPressed { get; }
    bool KickPressed { get; }
    bool StrikeCPressed { get; }
    bool StrikeDPressed { get; }
    bool PowerPressed { get; }
}
