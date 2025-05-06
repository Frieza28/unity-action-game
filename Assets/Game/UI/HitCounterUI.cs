using TMPro;
using UnityEngine;

public class HitCounterUI : MonoBehaviour
{
    [SerializeField] private ScoreKeeper player;  
    [SerializeField] private ScoreKeeper enemy;
    [SerializeField] private TMP_Text     playerText;
    [SerializeField] private TMP_Text     enemyText;

    void Update()
    {
        playerText.text = $"PLAYER HITS: {player.points}/10";
        enemyText.text  = $"ENEMY HITS:  {enemy.points}/10";
    }
}
