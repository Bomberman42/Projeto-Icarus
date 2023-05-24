using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Heroi : MonoBehaviour
{
    Rigidbody2D fisicaDoHeroi;
    [SerializeField]
    private float velocidade = 7;
    public float alturaPulo;
    private bool tocandoOChao = true;

    // Forma para exportar o valor de uma variável sem que outras classes possam mudar o valor, apenas para ler o valor 
    public bool EstaTomandoDando { get; private set; }

    private bool bloqueiaPulo;
    private bool estaPulando;
    private bool puloDuplo;
    private int indexLayerGround = 6;
    private int indexLayerVentilador = 11;
    private Animator animator;
    private Vector3 olhandoParaDireita = new Vector3(0f, 0f, 0f);
    private Vector3 olhandoParaEsquerda = new Vector3(0f, 180f, 0f);
    public AudioSource audioPulo;
    public int playerLife = 4;

    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        this.fisicaDoHeroi = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameControle.instance.estaPausado)
        {
            return;
        }

        Movimentar();
        Pular();
        Cair();
    }

    private void Movimentar()
    {
        float direction = Input.GetAxis("Horizontal");

        if (direction == 0f)
        {
            this.animator.SetBool("correr", false);
            return;
        }

        this.transform.eulerAngles = direction > 0f ? this.olhandoParaDireita : this.olhandoParaEsquerda;

        // Se o heroi esta sofrendo dano, não deve se movimentar por alungs segundos.
        if (this.EstaTomandoDando)
        {
            return;
        }

        this.fisicaDoHeroi.velocity = new Vector2(direction * this.velocidade, this.fisicaDoHeroi.velocity.y);
        this.animator.SetBool("correr", true);
    }

    private void Pular()
    {

        if (this.bloqueiaPulo == true)
        {
            return;
        }

        if (Input.GetKeyDown("space"))
        {
            if (!this.estaPulando)
            {
                this.animator.SetBool("pular", true);
                this.fisicaDoHeroi.velocity = new Vector2(this.fisicaDoHeroi.velocity.x, this.alturaPulo);
                this.puloDuplo = true;
                this.audioPulo.Play();
            }
            else
            {
                if (this.puloDuplo)
                {
                    this.audioPulo.Stop();
                    this.fisicaDoHeroi.velocity = new Vector2(this.fisicaDoHeroi.velocity.x, (this.alturaPulo));
                    this.audioPulo.Play();
                    this.puloDuplo = false;
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == this.indexLayerGround)
        {
            this.tocandoOChao = true;
            this.estaPulando = false;

            // Deve encerrar a animação de pulo caso o heroi toque no chão.
            this.animator.SetBool("pular", false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == this.indexLayerGround)
        {
            this.tocandoOChao = true;
            this.estaPulando = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == this.indexLayerGround)
        {
            this.estaPulando = true;
            this.tocandoOChao = false;
        }
    }

    private void Cair()
    {
        if (this.tocandoOChao)
        {
            this.animator.SetBool("cair", false);
            return;
        }

        this.animator.SetBool("cair", true);
    }

    public void SofreuDano(float valorDaForcaParaEmpurrarHeroi)
    {
        this.animator.SetBool("dano", true);
        this.EstaTomandoDando = true;
        this.fisicaDoHeroi.velocity = new Vector2(this.fisicaDoHeroi.transform.rotation.y < 0 ? valorDaForcaParaEmpurrarHeroi : (valorDaForcaParaEmpurrarHeroi * -1), this.fisicaDoHeroi.position.y + (valorDaForcaParaEmpurrarHeroi * 2));
        Invoke("ParouDeSofrerDano", 1f);
    }

    private void ParouDeSofrerDano()
    {
        this.animator.SetBool("dano", false);
        this.EstaTomandoDando = false;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.layer == this.indexLayerVentilador)
        {
            this.bloqueiaPulo = true;
            this.puloDuplo = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == this.indexLayerVentilador)
        {
            this.bloqueiaPulo = false;
        }
    }
}