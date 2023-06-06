using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class ButtonAction : MonoBehaviour
{

    [SerializeField]
    private GameObject turnOnObject;
    private bool isActive;
    private Animator anim;

    private void Start()
    {
        this.anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.isActive)
        {
            return;
        }

        if (collision.gameObject.name == "FeetCollider")
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Box"))
        {
            float width = collision.gameObject.CompareTag("Player") ? 0.2f : 0.29f;
            float heightUp = collision.gameObject.CompareTag("Player") ? 0.2f : 0.1f;
            Vector2 colliderVelocity;
            if (collision.gameObject.name == "FeetCollider")
            {
                colliderVelocity = collision.gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity;
            }
            else
            {
                colliderVelocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
            }

            Transform colliderTransform = collision.gameObject.transform;
            collision.gameObject.transform.position = new Vector2(colliderTransform.position.x + (colliderVelocity.x > 0 ? width : -width), colliderTransform.position.y + heightUp);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (this.isActive)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Box"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                this.playerObject = collision.gameObject;
            }

            if (collision.gameObject.CompareTag("Box"))
            {
                this.boxObject = collision.gameObject;
            }

            this.anim.SetBool("active", true);
            this.isActive = true;
            if (this.turnOnObject.name == "Fan")
            {
                this.turnOnObject.GetComponent<Ventilador>().EnableEffector();
            }
        }
    }

    private GameObject boxObject;
    private GameObject playerObject;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!this.isActive)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            this.playerObject = null;
        }

        if (collision.gameObject.CompareTag("Box"))
        {
            this.boxObject = null;
        }

        if (this.playerObject == null && this.boxObject == null)
        {
            this.anim.SetBool("active", false);
            this.isActive = false;
            if (this.turnOnObject.name == "Fan")
            {
                this.turnOnObject.GetComponent<Ventilador>().DisableEffector();
            }
        }
    }
}
