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
    
    [Header("Components")]
    public Transform player;
    //once spawned, enemy will chase player

    void Start()
    {
        currentState = State.Chasing;
        //find player in scene
        FindPlayer();
    }
    
    private void FindPlayer()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }
    void LateUpdate()
    {
        switch (currentState)
        {
            case State.Idle:
                // Do nothing
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
        float step = 2f * Time.deltaTime; // Adjust speed as needed
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);
    }
}