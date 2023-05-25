using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CaixaDeFerro : MonoBehaviour
{
    public AudioSource soundOfMoving;

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.transform.parent = null;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
