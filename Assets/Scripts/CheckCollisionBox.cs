using UnityEngine;

public class CheckCollisionBox : MonoBehaviour
{

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            this.transform.parent.gameObject.GetComponent<CaixaDeFerro>().isNotCollideOnGround();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            this.transform.parent.gameObject.GetComponent<CaixaDeFerro>().isCollideOnGround();
        }
    }
}
