using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItensColetaveis : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    private AudioSource audioSource;
    private Animator animador;
    private Heroi player;
    public GameObject prefabColetada;
    public int valorDoIten;
    private bool coletada;
    private bool coinTrigger;
    

    void Start()
    {
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
            transform.position = Vector2.Lerp(transform.position, this.player.transform.position, Time.deltaTime * 5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            if (gameObject.CompareTag("Moeda"))
            {

                if (this.coinTrigger == false)
                {
                    transform.position = new Vector2(transform.position.x, transform.position.y + 1f);
                    this.coinTrigger = true;
                }
                else
                {
                    this.coinTrigger = false;
                    this.prefabColetada.SetActive(true);
                    this.circleCollider.enabled = false;
                    this.spriteRenderer.enabled = false;
                    GameControle.instance.AtualizaPontuacaoAtual(valorDoIten);
                    Destroy(gameObject, 0.2f);
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
}
