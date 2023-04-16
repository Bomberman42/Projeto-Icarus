using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraDeVida : MonoBehaviour
{
    public int valorMaximoDeVida = 4;
    private int vidaAtual = 4;
    public GameObject fullLifeObject;
    public GameObject almostLifeObject;
    public GameObject halfLifeObject;
    public GameObject oneLifeObject;
    public GameObject deathObject;

    void Start()
    {
        this.vidaAtual = this.valorMaximoDeVida;
        this.fullLifeObject.SetActive(true);
        this.almostLifeObject.SetActive(false);
        this.halfLifeObject.SetActive(false);
        this.oneLifeObject.SetActive(false);
        this.deathObject.SetActive(false);
    }

    public void AtualizaVida()
    {
        this.fullLifeObject.SetActive(false);
        this.almostLifeObject.SetActive(false);
        this.halfLifeObject.SetActive(false);
        this.oneLifeObject.SetActive(false);
        this.deathObject.SetActive(false);

        if (this.vidaAtual == 0)
        {
            this.deathObject.SetActive(true);
            return;
        }

        if (this.vidaAtual == 1)
        {
            this.oneLifeObject.SetActive(true);
            return;
        }

        if (this.vidaAtual == 2)
        {
            this.halfLifeObject.SetActive(true);
            return;
        }

        if (this.vidaAtual == 3)
        {
            this.almostLifeObject.SetActive(true);
            return;
        }

        if (this.vidaAtual == 4)
        {
            this.fullLifeObject.SetActive(true);
            return;
        }

        this.fullLifeObject.SetActive(true);
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
