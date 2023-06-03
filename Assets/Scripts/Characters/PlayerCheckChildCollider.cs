using UnityEngine;

public class PlayerCheckChildCollider : MonoBehaviour
{
    private int indexLayerGround = 6;
    private int indexLayerBox = 15;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.name == "FeetCollider")
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                this.transform.parent.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                this.transform.parent.GetComponent<Heroi>().CollideOnEnemyHead();
            }

            if (
                collision.gameObject.layer == this.indexLayerGround ||
                collision.gameObject.layer == this.indexLayerBox
            )
            {
                this.transform.parent.GetComponent<Heroi>().CollideOnGround();
            }
        }
    }
}
