using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rig;
    public float projectileSpeed;
    public int projectileDamage;

    void Start()
    {
        this.rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        this.rig.velocity = new Vector2(this.projectileSpeed, this.rig.velocity.y);
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
