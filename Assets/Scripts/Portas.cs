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

    void Start()
    {
        this.animacaoDasPortas = GetComponent<Animator>();
    }



    private void ChamarFase()
    {
         GameControle.instance.CarregaProximaFase(this.nivelDaPorta);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            this.somPortaAbrindo.Play();
            this.animacaoDasPortas.SetBool("abrindo", true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown("w"))
            {
                Debug.Log("sim");
                this.player.SetActive(false);
                this.player.GetComponent<BoxCollider2D>().enabled = false;
                Invoke("ChamarFase", 0.5f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.somPortaAbrindo.Play();
            this.animacaoDasPortas.SetBool("abrindo", false);
        }
    }
}
