using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class IntEvent : UnityEvent<int> { }

public class Enemy : MonoBehaviour
{
    public GameObject healthBarPrefab;
    public HealthBarUI healthBarInstance;
    public Rigidbody2D rb;
    public Character character;
    public int health;
    public float speed;
    public float jumpForce;
    public GameObject upgradePrefab;
    public Animator animator;
    
    [Tooltip("score value")]
    public int scoreValue = 10;
    [Tooltip("Event Invoked when enemy dies. int payload = score amount")]
    public IntEvent onDeath = new IntEvent();
    void Start()
    {
        InitiateData();
        
        if (healthBarPrefab != null)
        {
            healthBarInstance.SetMaxHealth(health);
        }
    }

    private void InitiateData()
    {
        if (character != null)
        {
            health = character.health;
            speed = character.speed;
            jumpForce = character.jumpForce;
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        animator.SetTrigger("Hurt");
        AudioManager.Instance.PlaySound(AudioManager.Instance.hurtSound);

        healthBarInstance.SetHealth(health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        onDeath.Invoke(scoreValue);

        //50% chance to drop a weapon upgrade
        float dropChance = Random.Range(0f, 1f);
        if (dropChance <= 0.5f && !GameManager.Instance.UpgradeIsOnWorld)
        {
            Debug.Log("Dropping weapon upgrade");
            AudioManager.Instance.PlaySound(AudioManager.Instance.coinSound);
            // Instantiate weapon upgrade prefab at enemy position
            if (upgradePrefab != null)
            {
                Instantiate(upgradePrefab, transform.position, Quaternion.identity);
                GameManager.Instance.UpgradeIsOnWorld = true;
            }
        }
        Destroy(gameObject);
    }
}