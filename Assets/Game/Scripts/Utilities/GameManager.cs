using UnityEngine;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager I;
    [SerializeField] private TMP_Text gameOverText;
    private bool gameOver;

    void Awake() => I = this;

    public void EndGame(ScoreKeeper winner, ScoreKeeper loser)
    {
        if (gameOver) return;
        gameOver = true;

        foreach (var f in FindObjectsByType<Fighter>(FindObjectsSortMode.None))
            f.enabled = false;

        winner.GetComponent<Animator>()?.SetTrigger("Victory");
        loser .GetComponent<Animator>()?.SetTrigger("Defeat");
        Destroy(loser.gameObject, 3f);

        if (gameOverText != null)
            gameOverText.text = $"{(winner.isPlayer ? "PLAYER" : "ENEMY")} WINS!";
    }
}
