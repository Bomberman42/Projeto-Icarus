using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portas : MonoBehaviour
{
    private Animator animacaoDasPortas;
    public string nomeDaPorta;
    public string nivelDaPorta;
    public float tempoDeAbrirAPorta;
    public GameObject player;
    public AudioSource somPortaTrancada;
    public AudioSource somPortaAbrindo;
    public bool trancaDaPorta;

    void Start()
    {
        this.animacaoDasPortas = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            if(Vector2.Distance(this.transform.position, this.player.transform.position) <= 1f && !this.trancaDaPorta)
            {
                this.player.SetActive(false);
                this.player.GetComponent<BoxCollider2D>().enabled = false;
                Invoke("ChamarFase", 0.5f);
            }
        }
    }

    private void ChamarFase()
    {
         GameControle.instance.CarregaProximaFase(this.nivelDaPorta);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.trancaDaPorta)
        {
            return;
        }

        if(collision.gameObject.tag == "Player")
        {
            this.somPortaAbrindo.Play();
            this.animacaoDasPortas.SetBool("abrindo", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (this.trancaDaPorta)
            {
                return;
            }

            this.somPortaAbrindo.Play();
            this.animacaoDasPortas.SetBool("abrindo", false);
        }
    }
}
