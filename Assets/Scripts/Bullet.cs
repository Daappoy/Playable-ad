using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Tooltip("Seconds before the projectile self-destructs")]
    public float lifeTime = 2f;
    [Tooltip("Damage dealt to enemy on hit")]
    public int damage = 25;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == null) { Destroy(gameObject); return; }

        Enemy enemy = collision.collider.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}