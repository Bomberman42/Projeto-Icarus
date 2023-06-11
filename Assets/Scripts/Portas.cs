using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portas : MonoBehaviour
{
    private List<Level> levels;
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
        this.trancaDaPorta = true;
        this.levels = GameControle.instance.GetLevel();
        this.animacaoDasPortas = GetComponent<Animator>();
        int indexOf = this.nivelDaPorta.IndexOf("_");
        int numberDoor = int.Parse(this.nivelDaPorta.Substring(indexOf + 1));
        int index = this.levels.FindIndex(level => level.type == "lvl_" + (numberDoor - 1));
        if (index >= 0 || (numberDoor - 1) == 0)
        {
            this.trancaDaPorta = false;
        }
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

            if (this.trancaDaPorta)
            {
                this.somPortaTrancada.Play();
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
                this.somPortaTrancada.Play();
                return;
            }

            this.somPortaAbrindo.Play();
            this.animacaoDasPortas.SetBool("abrindo", false);
        }
    }
}
