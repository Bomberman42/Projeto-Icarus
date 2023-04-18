using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portas : MonoBehaviour
{
    private Animator animacaoDasPortas;
    public string nomeDaPorta;
    public string nivelDaPorta;
    public float tempoDeAbrirAPorta;

    void Start()
    {
        this.animacaoDasPortas = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            this.tempoDeAbrirAPorta = 4f;
            this.animacaoDasPortas.SetBool("abrindo", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.animacaoDasPortas.SetBool("abrindo", false);
        }
    }

}
