using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int damage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.transform.name);
        var health = collision.gameObject.GetComponent<HealthBase>();
        if (health != null)
        {
            health.Damage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            Destroy(gameObject); // Destroi o inimigo
        }
    }
}
