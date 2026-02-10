using UnityEngine;

public class StateMachine : MonoBehaviour
{
    
    //state machine for enemy AI
    public enum State
    {
        Idle,
        Chasing
    }
    [Header("State Machine")]
    public State currentState = State.Idle;
    public Enemy enemyData;
    [Header("Components")]
    public Transform player;

    void Start()
    {
        FindPlayer();
    }

    private void OnEnable()
    {
        if (player == null)
        {
            FindPlayer();
        }
    }
    
    private void FindPlayer()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
                currentState = State.Chasing;
            }
        }
    }
    void Update()
    {
        if (player == null)
        {
            FindPlayer();
        }
        
        switch (currentState)
        {
            case State.Idle:
                if (player != null)
                {
                    currentState = State.Chasing;
                }
                break;
            case State.Chasing:
                ChasePlayer();
                break;
        }
    }
    
    private void ChasePlayer()
    {
        if (player == null) return;
        // Move towards the player
        transform.position = Vector3.MoveTowards(transform.position, player.position, enemyData.speed * Time.deltaTime);
    }
}