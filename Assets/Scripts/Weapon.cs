using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Components")]
    public Player player;
    [Header("Ranged Weapon Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float fireRate = 0.5f;
    private float _nextFireTime = 1f;

    void Update()
    {
        //test tambahan
        if (Time.time >= _nextFireTime)
        {
            FireProjectile();
            _nextFireTime = Time.time + fireRate;
        }
    }

    private void FireProjectile()
    {
        if (projectilePrefab == null || firePoint == null) return;
        AudioManager.Instance.PlaySound(AudioManager.Instance.shootSound);
        if (player.upgraded == false)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            Transform projTransform = projectile.GetComponent<Transform>();
            projTransform.localRotation = Quaternion.Euler(0f, 0f, -90f);
            if (rb != null)
            {
                int facing = (player != null) ? player._facing : 1;
                rb.velocity = new Vector2(facing * projectileSpeed, 0f);
                projTransform.localRotation = Quaternion.Euler(0f, 0f, facing * -90f);
            }
        }
        else
        {
            int facing = (player != null) ? player._facing : 1;

            GameObject projectile1 = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb1 = projectile1.GetComponent<Rigidbody2D>();
            Transform projTransform1 = projectile1.GetComponent<Transform>();
            projTransform1.localRotation = Quaternion.Euler(0f, 0f, -90f);
            if (rb1 != null)
            {
                float angleOffset = 1f; 
                float rad = Mathf.Deg2Rad * angleOffset;
                Vector2 direction1 = new Vector2(Mathf.Cos(rad) * facing, Mathf.Sin(rad));
                rb1.velocity = direction1.normalized * projectileSpeed;
                projTransform1.localRotation = Quaternion.Euler(0f, 0f, facing * (-90f + angleOffset));
            }

            GameObject projectile2 = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb2 = projectile2.GetComponent<Rigidbody2D>();
            Transform projTransform2 = projectile2.GetComponent<Transform>();
            projTransform2.localRotation = Quaternion.Euler(0f, 0f, -90f);
            if (rb2 != null)
            {
                float angleOffset = -1f;
                float rad = Mathf.Deg2Rad * angleOffset;
                Vector2 direction2 = new Vector2(Mathf.Cos(rad) * facing, Mathf.Sin(rad));
                rb2.velocity = direction2.normalized * projectileSpeed;
                projTransform2.localRotation = Quaternion.Euler(0f, 0f, facing * (-90f + angleOffset));
            }
        }
    }
}