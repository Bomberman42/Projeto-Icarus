using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{

    private Rigidbody2D fisicaDoInimigo;
    private Animator animacaoDoInimigo;
    private bool colidir;
    private float velocidadeDeInicio;

    public Transform colisorDaDireita;
    public Transform colisorDaEsquerda;
    public LayerMask camadas;
    public Transform pontoDaCabeca;
    public BoxCollider2D boxCollider2D;

    public int totalDanoPorAtaque;
    public int totalDeVida;
    public float velocidade;



    void Start()
    {
        this.fisicaDoInimigo = GetComponent<Rigidbody2D>();
        this.animacaoDoInimigo = GetComponent<Animator>();
        this.velocidadeDeInicio = this.velocidade;
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
            float altura = colisor.gameObject.transform.position.y - this.pontoDaCabeca.position.y;

            Debug.Log(altura);
            Debug.Log("posição Y do player:");
            Debug.Log(colisor.gameObject.transform.position.y);
            Debug.Log("posição Y da cabeça:");
            Debug.Log(this.pontoDaCabeca.position.y);

            if (altura > 0)
            {
                Debug.Log("sim");
                colisor.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                AlteraVelocidade(0);

                this.totalDeVida--;

                if (this.totalDeVida > 0)
                {
                    this.animacaoDoInimigo.SetTrigger("dano");
                    Invoke("ResetaVelocidade", 0.35f);
                    return;
                }

                this.animacaoDoInimigo.SetTrigger("morte");
                boxCollider2D.enabled = false;
                //Isto faz com que o boneco não tenha fisica
                this.fisicaDoInimigo.bodyType = RigidbodyType2D.Kinematic;
                Destroy(gameObject, 0.33f);
            }
        }
    }

    private void AlteraVelocidade(float novoValor)
    {
        this.velocidade = novoValor;
    }

    private void ResetaVelocidade()
    {
        this.velocidade = transform.localScale.x >= 0f ? this.velocidadeDeInicio : -this.velocidadeDeInicio;
    }
}
