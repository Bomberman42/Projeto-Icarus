using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraDeVida : MonoBehaviour
{
    private Animator animacaoDaVida;
    private int vida = 4;

    void Start()
    {
        this.animacaoDaVida = GetComponent<Animator>();
    }

    public void AtualizaVida(int novoValorDaVida)
    {

        Debug.Log("Att vida");
        this.animacaoDaVida.SetBool("almostlife", true);
    }
}
