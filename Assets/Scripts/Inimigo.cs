using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    private Rigidbody2D fisicaDoInimigo;
    private Animator animacaoDoInimigo;
    private bool colidir;
    private float velocidadeDeInicio;

    [Header("Atributos do Ininmigo")]
    public Transform colisorDaDireita;
    public Transform colisorDaEsquerda;
    public Transform pontoDaCabeca;
    public BoxCollider2D boxCollider2D;
    public LayerMask camadas;

    [Header("Status do Ininmigo")]
    public int totalDanoRecebidoPorAtaque;
    public int totalDeVida;
    public float velocidade;
    public int totalDanoPorAtaque;
    public float valorDaForcaParaEmpurrarHeroi;



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
        Debug.DrawRay(new Vector2(this.colisorDaDireita.position.x, this.colisorDaDireita.position.y), new Vector2(transform.localScale.x > 0 ? 1 : -1, -10), Color.red);
        Debug.DrawRay(new Vector2(this.colisorDaEsquerda.position.x, this.colisorDaEsquerda.position.y), new Vector2(transform.localScale.x > 0 ? 1 : -1, 1), Color.red);

        if (this.velocidade == 0)
        {
            this.animacaoDoInimigo.SetBool("correr", false);
            return;
        }

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
            //float altura = colisor.gameObject.transform.position.y - this.pontoDaCabeca.position.y;
            float altura = colisor.contacts[0].point.y - this.pontoDaCabeca.position.y;

            if (altura > 0)
            {
                // Altera a velocidade do inimigo para iniciar a animação de dano.
                AlteraVelocidade(0);

                // Remove as forças do jogador
                colisor.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                // Empurra o jogador para cima como um impulso por atingir um inimigo.
                colisor.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 8, ForceMode2D.Impulse);

                // Remove um ponto de vida do inimigo
                this.totalDeVida -= this.totalDanoRecebidoPorAtaque;

                if (this.totalDeVida > 0)
                {
                    this.animacaoDoInimigo.SetTrigger("dano");
                    Invoke("ResetaVelocidade", 0.35f);
                    return;
                }

                this.animacaoDoInimigo.SetTrigger("morte");
                boxCollider2D.enabled = false;
                // Isto faz com que o boneco não tenha fisica
                this.fisicaDoInimigo.bodyType = RigidbodyType2D.Kinematic;
                Destroy(gameObject, 0.33f);
            } else
            {
                causarDano();
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

    private void causarDano()
    {
        GameControle.instance.DanoDoHeroi(this.totalDanoPorAtaque, this.valorDaForcaParaEmpurrarHeroi);
    }
}
