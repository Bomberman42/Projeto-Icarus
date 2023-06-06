using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItensColetaveis : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    private AudioSource audioSource;
    [SerializeField]
    private AudioSource coinSound;
    [SerializeField]
    private float coinSpeed = 5f;
    private Animator animador;
    private Heroi player;
    public GameObject prefabColetada;
    public int valorDoIten;
    private bool coletada;
    public bool collide;
    private bool coinTrigger;
    private bool canCollect;
    private float variation;
    private float curentSpeed;
    

    void Start()
    {
        this.curentSpeed = coinSpeed;
        this.animador = GetComponent<Animator>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.circleCollider = GetComponent<CircleCollider2D>();
        this.audioSource = GetComponent<AudioSource>();
        this.player = GameObject.FindObjectOfType<Heroi>();
    }

    private void Update()
    {
        if(this.coinTrigger)
        {
            Vector2 playerPosition = new Vector2 (this.player.transform.position.x, this.player.transform.position.y + this.variation);
            transform.position = Vector2.Lerp(transform.position, playerPosition, Time.deltaTime * this.curentSpeed);
            this.curentSpeed += Random.Range(0, 0.03f); 
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            this.collide = true;

            if (gameObject.CompareTag("Moeda"))
            {
                if (this.coinTrigger == false)
                {
                    this.variation = Random.Range(-0.3f, 0.3f);
                    this.coinSound.Play();
                    transform.position = new Vector2(transform.position.x, transform.position.y + 1f);
          
                    Invoke("ChangeCoinTrigger", 0.1f);
                    Invoke("ActivateCoinCollectionTrigger", 0.3f);
                }
                else
                {
                    if (!this.canCollect)
                    {
                        return;
                    }

                    this.coinTrigger = false;
                    this.prefabColetada.SetActive(true);
                    this.circleCollider.enabled = false;
                    this.spriteRenderer.enabled = false;
                    GameControle.instance.AtualizaPontuacaoAtual(valorDoIten);
                    Destroy(this.gameObject, 0.2f);
                }
                return;
            }

            this.prefabColetada.SetActive(true);
            this.circleCollider.enabled = false;
            this.spriteRenderer.enabled = false;
            this.audioSource.Play();

            GameControle.instance.AtualizaPontuacaoAtual(valorDoIten);

            Destroy(this.gameObject, 0.5f);
        }
    }

    private void ActivateCoinCollectionTrigger()
    {
        this.canCollect = true;
    }

    private void ChangeCoinTrigger()
    {
        this.coinTrigger = true;
    }
}
