using UnityEngine;

public class Heroi : MonoBehaviour
{
    Rigidbody2D fisicaDoHeroi;
    [SerializeField]
    private float velocidade = 7;
    public float alturaPulo;
    private bool tocandoOChao = true;

    [SerializeField]
    public Transform feetCollider;

    // Forma para exportar o valor de uma vari�vel sem que outras classes possam mudar o valor, apenas para ler o valor 
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
    public AudioSource audioHit;
    public int playerLife = 4;
    [SerializeField]
    private float gravityPlayer = 3f;

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

        // Se o heroi esta sofrendo dano, n�o deve se movimentar por alungs segundos.
        if (this.EstaTomandoDando)
        {
            return;
        }

        if(this.fisicaDoHeroi.gravityScale == 0f)
        {
            return;
        }

        this.fisicaDoHeroi.velocity = new Vector2(direction * this.velocidade, this.fisicaDoHeroi.velocity.y);
        this.animator.SetBool("correr", true);
    }

    public void RemoveGravity()
    {
        this.fisicaDoHeroi.velocity = Vector2.zero;
        this.fisicaDoHeroi.gravityScale = 0f;
    }

    public void ReturnGravity()
    {
        this.fisicaDoHeroi.gravityScale = this.gravityPlayer;
        this.fisicaDoHeroi.velocity = Vector2.zero;
    }

    private void Pular()
    {

        if (this.fisicaDoHeroi.gravityScale == 0f)
        {
            return;
        }

        if (this.bloqueiaPulo == true)
        {
            return;
        }

        if (Input.GetKeyDown("space"))
        {
            if (!this.estaPulando)
            {
                this.fisicaDoHeroi.velocity = new Vector2(this.fisicaDoHeroi.velocity.x, this.alturaPulo);
                this.puloDuplo = true;
                this.estaPulando = true;
                this.audioPulo.Play();
                this.animator.SetBool("cair", false);
                this.animator.SetBool("pular", true);
            }
            else
            {
                if (this.puloDuplo)
                {
                    this.audioPulo.Stop();
                    this.fisicaDoHeroi.velocity = new Vector2(this.fisicaDoHeroi.velocity.x, (this.alturaPulo));
                    this.audioPulo.Play();
                    this.puloDuplo = false;
                    this.animator.SetBool("cair", false);
                    this.animator.SetBool("pular", true);
                }
            }

            Invoke("ResetJumpAnimation", 0.4f);
        }
    }

    public void CollideOnGround()
    {
        if (this.speedOfFall <= -25)
        {
            GameControle.instance.DanoDoHeroi(2, 0);
        }

        this.speedOfFall = 0;
        this.tocandoOChao = true;
        this.estaPulando = false;

        // Deve encerrar a anima��o de pulo caso o heroi toque no ch�o.
        this.animator.SetBool("pular", false);
        this.animator.SetBool("cair", false);
    }

    public void CollideOnEnemyHead()
    {
        this.speedOfFall = 0;
        this.tocandoOChao = false;
        this.estaPulando = false;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //if (collision.gameObject.CompareTag("Enemy"))
    //    //{
    //    //    this.speedOfFall = 0;
    //    //    this.tocandoOChao = false;
    //    //    this.estaPulando = false;
    //    //}

    //    //if (collision.gameObject.layer == this.indexLayerGround)
    //    //{
    //    //    //float feetY = this.feetCollider.transform.position.y;
    //    //    //float groundY = collision.gameObject.transform.position.y;
    //    //    //float centerGroundY = (collision.gameObject.transform.localScale.y / 2);

    //    //    //Debug.Log("Feet Y: " + feetY);
    //    //    //Debug.Log("Ground Y: " + groundY + centerGroundY);
    //    //    //Debug.Log("Resultado: " + (feetY < (groundY + centerGroundY)));

    //    //    //if (!(feetY < (groundY + centerGroundY)) || feetY < groundY)
    //    //    //{
    //    //    //    return;
    //    //    //}

    //    //    // Se a velocidade de queda do heroi for maior que 25 ent�o ele deve receber dando.
    //    //    if (this.speedOfFall <= -25)
    //    //    {
    //    //        GameControle.instance.DanoDoHeroi(2, 0);
    //    //    }

    //    //    this.speedOfFall = 0;
    //    //    this.tocandoOChao = true;
    //    //    this.estaPulando = false;

    //    //    // Deve encerrar a anima��o de pulo caso o heroi toque no ch�o.
    //    //    this.animator.SetBool("pular", false);
    //    //    this.animator.SetBool("cair", false);
    //    //}
    //}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == this.indexLayerGround)
        {
            this.tocandoOChao = true;
            //this.estaPulando = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == this.indexLayerGround)
        {
            //this.estaPulando = true;
            this.tocandoOChao = false;
        }
    }

    float speedOfFall;

    private void Cair()
    {
        if (this.tocandoOChao)
        {
            this.animator.SetBool("cair", false);
            return;
        }

        this.speedOfFall = this.fisicaDoHeroi.velocity.y;

        this.animator.SetBool("cair", true);
    }

    private void ResetJumpAnimation()
    {
        this.animator.SetBool("pular", false);
    }

    public void SofreuDano(float valorDaForcaParaEmpurrarHeroi)
    {
        this.animator.SetBool("pular", false);
        this.fisicaDoHeroi.velocity = new Vector2(this.fisicaDoHeroi.transform.rotation.y < 0 ? valorDaForcaParaEmpurrarHeroi : (valorDaForcaParaEmpurrarHeroi * -1), this.fisicaDoHeroi.position.y + (valorDaForcaParaEmpurrarHeroi * 2));
        this.animator.SetBool("dano", true);
        this.EstaTomandoDando = true;
        this.audioHit.Play();
        Invoke("TurnOffVelocity", 0.5f);
        Invoke("ParouDeSofrerDano", 1f);
    }

    private void TurnOffVelocity()
    {
        this.fisicaDoHeroi.velocity = new Vector2(this.fisicaDoHeroi.velocity.x / 2, this.fisicaDoHeroi.velocity.y);
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