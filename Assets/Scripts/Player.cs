// csharp
using System;
using UnityEngine;

public class DeadEvent : UnityEngine.Events.UnityEvent { }

public class Player : MonoBehaviour
{
    [Header("Components")]
    public Character characterData;
    [SerializeField] private Rigidbody2D _rb2d;
    [SerializeField] private Collider2D _collider2d;
    public bool upgraded = false;
    [SerializeField] private int moveInput = 0;
    public DeadEvent onPlayerDead = new DeadEvent();
    public Animator playerAnimator;

    [Header("Movement Settings")]
    public int health;
    public float speed;
    public float jumpForce;
    public bool isGrounded = false;

    public LayerMask groundLayer;
    public float groundCheckDistance = 0.1f;

    [Header("Input")]
    public float inputThreshold = 0.1f;

    [SerializeField] public int _facing = 1;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _collider2d = GetComponent<Collider2D>();

        if (characterData != null)
        {
            health = characterData.health;
            speed = characterData.speed;
            jumpForce = characterData.jumpForce;
        }

        _facing = transform.localScale.x >= 0f ? 1 : -1;
    }

    void Update()
    {
        GroundCheck();

        float x = moveInput != 0 ? moveInput * speed : 0f;
        _rb2d.velocity = new Vector2(x, _rb2d.velocity.y);
        playerAnimator.SetInteger("MoveInput", moveInput);

        if (moveInput != 0 && moveInput != _facing)
        {
            transform.localScale = new Vector3(moveInput, 1, 1);
            _facing = moveInput;
        }
    }

    private void GroundCheck()
    {
        if (_collider2d == null) { isGrounded = false; return; }

        Vector2 origin = new Vector2(_collider2d.bounds.center.x, _collider2d.bounds.min.y + 0.01f);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, groundCheckDistance, groundLayer);
        Debug.DrawRay(origin, Vector2.down * groundCheckDistance, hit.collider ? Color.green : Color.red);
        isGrounded = hit.collider != null;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            _rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    public void SetMove(int dir) { moveInput = dir; }
    public void StopMove() { moveInput = 0; }

    private void OnDrawGizmosSelected()
    {
        if (_collider2d == null) return;
        Gizmos.color = Color.yellow;
        Vector2 origin = new Vector2(_collider2d.bounds.center.x, _collider2d.bounds.min.y + 0.01f);
        Gizmos.DrawLine(origin, origin + Vector2.down * groundCheckDistance);
    }

    public void UpgradeWeapon()
    {
        upgraded = true;
        Debug.Log("Player weapon upgraded!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("enemy"))
        {
            PlayerDead();
        }
    }

    public void PlayerDead()
    {
        moveInput = 0;
        upgraded = false;
        onPlayerDead.Invoke();
        Debug.Log("player dies.");
        //destroy player gameobject
        Destroy(gameObject);
    }
}
