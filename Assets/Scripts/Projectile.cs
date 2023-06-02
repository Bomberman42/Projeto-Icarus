using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public float projectileSpeed;
    public int projectileDamage;

    void Start()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        this.rigidbody.velocity = new Vector2(this.projectileSpeed, rigidbody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameControle.instance.DanoDoHeroi(this.projectileDamage, 4f);
            Destroy(this.gameObject);
            return;
        }
        Destroy(this.gameObject, 0.2f);
    }
}
