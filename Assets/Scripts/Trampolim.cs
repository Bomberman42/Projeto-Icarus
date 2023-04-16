using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolim : MonoBehaviour
{

    public float forcaDoPulo;
    private Animator animcacaoDoTrampolim;

    void Start()
    {
        this.animcacaoDoTrampolim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            this.animcacaoDoTrampolim.SetTrigger("pulo");
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, forcaDoPulo), ForceMode2D.Impulse);
        }
    }
}
