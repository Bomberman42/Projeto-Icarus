using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraDeVida : MonoBehaviour
{
    private Animator animacaoDaVida;
    private int valorMaximoDeVida = 4;
    private int vidaAtual = 4;
    public GameObject fullLifeObject;
    public GameObject almostLifeObject;
    public GameObject halfLifeObject;
    public GameObject oneLifeObject;
    public GameObject deathObject;

    void Start()
    {
        this.animacaoDaVida = GetComponent<Animator>();
        this.fullLifeObject.SetActive(true);
        this.almostLifeObject.SetActive(false);
        this.halfLifeObject.SetActive(false);
        this.oneLifeObject.SetActive(false);
        this.deathObject.SetActive(false);
    }

    public void AtualizaVida()
    {
        int death = 0;
        int onelife = 1;
        int halflife = 2;
        int almostlife = 3;
        int fulllife = 4;

        this.fullLifeObject.SetActive(false);
        this.almostLifeObject.SetActive(false);
        this.halfLifeObject.SetActive(false);
        this.oneLifeObject.SetActive(false);
        this.deathObject.SetActive(false);

        if (this.vidaAtual == death)
        {
            this.deathObject.SetActive(true);
            GameControle.instance.FimDeJogo();
            return;
        }

        if (this.vidaAtual == onelife)
        {
            this.oneLifeObject.SetActive(true);
            return;
        }

        if (this.vidaAtual == halflife)
        {
            this.halfLifeObject.SetActive(true);
            return;
        }

        if (this.vidaAtual == almostlife)
        {
            this.almostLifeObject.SetActive(true);
            return;
        }

        if (this.vidaAtual == fulllife)
        {
            this.fullLifeObject.SetActive( true);
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
        this.vidaAtual -= valorParaRemover;
        if (this.vidaAtual < 0)
        {
            this.vidaAtual = 0;
        }

        this.AtualizaVida();

        return this.vidaAtual;
    }
}
