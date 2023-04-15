using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraDeVida : MonoBehaviour
{
    private Animator animacaoDaVida;
    private int valorMaximoDeVida = 4;
    private int vidaAtual = 4;

    void Start()
    {
        this.animacaoDaVida = GetComponent<Animator>();
    }

    public void AtualizaVida()
    {
        Debug.Log(this.vidaAtual);

        int death = 0;
        int onelife = 1;
        int halflife = 2;
        int almostlife = 3;
        int fulllife = 4;

        if (this.vidaAtual == death)
        {
            this.animacaoDaVida.SetBool("death", true);
            GameControle.instance.FimDeJogo();
            return;
        }

        if (this.vidaAtual == onelife)
        {
            this.animacaoDaVida.SetBool("onelife", true);
            return;
        }

        if (this.vidaAtual == halflife)
        {
            this.animacaoDaVida.SetBool("halflife", true);
            return;
        }

        if (this.vidaAtual == almostlife)
        {
            this.animacaoDaVida.SetBool("almostlife", true);
            return;
        }

        if (this.vidaAtual == fulllife)
        {
            this.animacaoDaVida.SetBool("fulllife", true);
            return;
        }
    }

    public int AdicionaVida(int valorParaAdicionar)
    {
        this.vidaAtual += valorParaAdicionar;
        if (this.vidaAtual > this.valorMaximoDeVida)
        {
            this.vidaAtual = this.valorMaximoDeVida;
        }

        this.AtualizaVida();

        return this.vidaAtual;
    }

    public int RemoveVida(int valorParaRemover)
    {

        //this.animacaoDaVida.SetBool("death", false);
        //this.animacaoDaVida.SetBool("onelife", false);
        //this.animacaoDaVida.SetBool("halflife", false);
        //this.animacaoDaVida.SetBool("fulllife", false);
        //this.animacaoDaVida.SetBool("almostlife", false);

        this.vidaAtual -= valorParaRemover;
        if (this.vidaAtual < 0)
        {
            this.vidaAtual = 0;
        }

        this.AtualizaVida();

        return this.vidaAtual;
    }
}
