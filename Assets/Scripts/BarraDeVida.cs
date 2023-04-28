using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraDeVida : MonoBehaviour
{
    public int valorMaximoDeVida = 10;
    public int vidaAtual = 10;
    public GameObject positionOne;
    public GameObject positionTwo;
    public GameObject positionTree;
    public GameObject positionFour;
    public GameObject positionFive;
    public GameObject positionSix;
    public GameObject positionSeven;
    public GameObject positionEight;
    public GameObject positionNine;
    public GameObject positionTen;
    public static BarraDeVida instance;

    void Start()
    {
        this.vidaAtual = this.valorMaximoDeVida;
        this.positionOne.SetActive(true);
        this.positionTwo.SetActive(true);
        this.positionTree.SetActive(true);
        this.positionFour.SetActive(true);
        this.positionFive.SetActive(true);
        this.positionSix.SetActive(true);
        this.positionSeven.SetActive(true);
        this.positionEight.SetActive(true);
        this.positionNine.SetActive(true);
        this.positionTen.SetActive(true);
        instance = this;
    }

    public void AtualizaVida()
    {
        this.positionOne.SetActive(false);
        this.positionTwo.SetActive(false);
        this.positionTree.SetActive(false);
        this.positionFour.SetActive(false);
        this.positionFive.SetActive(false);
        this.positionSix.SetActive(false);
        this.positionSeven.SetActive(false);
        this.positionEight.SetActive(false);
        this.positionNine.SetActive(false);
        this.positionTen.SetActive(false);

        if (this.vidaAtual == 0)
        {
            return;
        }

        if (this.vidaAtual >= 1)
        {
            this.positionOne.SetActive(true);
        }

        if (this.vidaAtual >= 2)
        {
            this.positionTwo.SetActive(true);
        }

        if (this.vidaAtual >= 3)
        {
            this.positionTree.SetActive(true);
        }

        if (this.vidaAtual >= 4)
        {
            this.positionFour.SetActive(true);
        }

        if (this.vidaAtual >= 5)
        {
            this.positionFive.SetActive(true);
        }

        if (this.vidaAtual >= 6)
        {
            this.positionSix.SetActive(true);
        }

        if (this.vidaAtual >= 7)
        {
            this.positionSeven.SetActive(true);
        }

        if (this.vidaAtual >= 8)
        {
            this.positionEight.SetActive(true);
        }

        if (this.vidaAtual >= 9)
        {
            this.positionNine.SetActive(true);
        }

        if (this.vidaAtual >= 10)
        {
            this.positionTen.SetActive(true);
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
