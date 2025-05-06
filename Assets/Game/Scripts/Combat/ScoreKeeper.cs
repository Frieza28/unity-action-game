using UnityEngine;
using System.Linq;

public class ScoreKeeper : MonoBehaviour
{
    public bool isPlayer;
    public int  points;

    public void AddPoints(int amount)
    {
        points += amount;
        if (points >= 10)
        {
            ScoreKeeper loser = FindObjectsByType<ScoreKeeper>(FindObjectsSortMode.None)
                                .First(sk => sk != this);
            GameManager.I.EndGame(this, loser);
        }
    }
}
