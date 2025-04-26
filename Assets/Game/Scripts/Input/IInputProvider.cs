using UnityEngine;

public interface IInputProvider
{
    bool StrikePressed { get; }
    int StrikeIndex { get; }   // 0-3 (punch L/R, kick L/R…)
    bool PowerPressed { get; }
    Vector2 MoveVector { get; }
}
