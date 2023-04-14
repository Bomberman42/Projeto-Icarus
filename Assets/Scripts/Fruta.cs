using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruta : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    public GameObject prefabColetada;
    private AudioSource audioSource;
    public int valorDaFruta;

    void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.circleCollider = GetComponent<CircleCollider2D>();
        this.audioSource = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            this.prefabColetada.SetActive(true);
            this.circleCollider.enabled = false;
            this.spriteRenderer.enabled = false;
            this.audioSource.Play();

            GameControle.instance.AtualizaPontuacaoAtual(valorDaFruta);

            Destroy(this.gameObject, 0.5f);
        }
    }

}
