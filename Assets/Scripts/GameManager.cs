using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Text scoreText;
    private int score = 0;
    public bool UpgradeIsOnWorld = false;
    private Player player;
    public GameObject losePanel;
    public GameObject MovementButtons; 
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        UpdateScoreText();
        
        
         //subscribe to player whenever it is dead
         player = FindObjectOfType<Player>();
         if (player != null)
         {
             player.onPlayerDead.AddListener(GameOver);
         }
    }
    
    public void RegisterEnemy(Enemy enemy)
    {
        if (enemy == null) return;
        // Prevent duplicate subscriptions by removing first, then adding once
        enemy.onDeath.RemoveListener(AddScore);
        enemy.onDeath.AddListener(AddScore);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }
    
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    
    private void GameOver()
    {
        Debug.Log("Player has died. Game Over!");
        losePanel.SetActive(true);
        MovementButtons.SetActive(false);
        // Implement game over logic here (e.g., show game over screen, restart level, etc.)
    }
}