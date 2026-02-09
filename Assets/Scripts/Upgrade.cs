using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.UpgradeWeapon();
                GameManager.Instance.UpgradeIsOnWorld = false;
                Destroy(gameObject);
            }
        }
    }
}
