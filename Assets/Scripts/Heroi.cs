using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Threading.Tasks;
using UnityEditor.VersionControl;
using Microsoft.Unity.VisualStudio.Editor;
using System.IO;

public class Heroi : MonoBehaviour
{
    Rigidbody2D fisicaDoHeroi;
    [SerializeField]
    private float velocidade = 7;
    public float alturaPulo;
    private bool tocandoOChao = true;

    private bool estaPulando;
    private bool puloDuplo;
    private int indexLayerGround = 6;
    private int indexLayerParede = 7;
    private Animator animator;
    private Vector3 olhandoParaDireita = new Vector3(0f, 0f, 0f);
    private Vector3 olhandoParaEsquerda = new Vector3(0f, 180f, 0f);
    public AudioSource audioPulo;
    private bool estaBatendoNaParede;
    public BoxCollider2D colisorDoChao;
    private int indexLayerFimDeJogo = 8;


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

        this.fisicaDoHeroi.velocity = new Vector2(direction * this.velocidade, this.fisicaDoHeroi.velocity.y);
        this.transform.eulerAngles = direction > 0f ? this.olhandoParaDireita : this.olhandoParaEsquerda;
        this.animator.SetBool("correr", true);
    }

    private void Pular()
    {

        if (Input.GetKeyDown("space"))
        {
            if(!this.estaPulando)
            {
                this.fisicaDoHeroi.velocity = new Vector2(this.fisicaDoHeroi.velocity.x, this.alturaPulo);
                this.puloDuplo = true;
                this.animator.SetBool("pular", true);
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
            this.animator.SetBool("pular", false);
            //this.estaBatendoNaParede = false;
        }

        //if (collision.gameObject.layer == this.indexLayerParede && this.tocandoOChao == false)
        //{
        //    this.fisicaDoHeroi.velocity = new Vector2(this.fisicaDoHeroi.position.x, this.fisicaDoHeroi.position.y - 2);
        //    this.estaBatendoNaParede = true;
        //}

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

}