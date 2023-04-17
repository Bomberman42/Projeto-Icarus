using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{

    private Rigidbody2D fisicaDoInimigo;
    private Animator animacaoDoInimigo;
    private bool colidir;

    public Transform colisorDaDireita;
    public Transform colisorDaEsquerda;
    public LayerMask camadas;
    public Transform pontoDaCabeca;
    public BoxCollider2D boxCollider2D;

    public int totalDanoPorAtaque;
    public int totalDeVida;
    public int velocidade;



    void Start()
    {
        this.fisicaDoInimigo = GetComponent<Rigidbody2D>();
        this.animacaoDoInimigo = GetComponent <Animator>();
    }


    void Update()
    {
        this.fisicaDoInimigo.velocity = new Vector2(this.velocidade, this.fisicaDoInimigo.velocity.y);
        this.colidir = Physics2D.Linecast(this.colisorDaDireita.position, this.colisorDaEsquerda.position, this.camadas);
        Debug.DrawRay(new Vector2(this.colisorDaDireita.position.x, this.colisorDaDireita.position.y), Vector2.left ,Color.red);
        this.animacaoDoInimigo.SetBool("correr", true);

        if (colidir)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            this.velocidade = -this.velocidade;
        }
    }

    private void OnCollisionEnter2D(Collision2D colisor)
    {
        if (colisor.gameObject.tag == "Player")
        {
            float altura = colisor.contacts[0].point.y - this.pontoDaCabeca.position.y;

            if (altura > 0)
            {
                Debug.Log("sim");
                colisor.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                velocidade = 0;
                this.animacaoDoInimigo.SetTrigger("hit");
                boxCollider2D.enabled = false;

            }
        }
    }
}
