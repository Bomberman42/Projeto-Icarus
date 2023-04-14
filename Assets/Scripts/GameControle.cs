using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControle : MonoBehaviour
{
    public Text pontuacaoAtual;
    private int pontuacaoTotal;
    private int vida;

    public static GameControle instance;

    void Start()
    {
      instance = this;
        
    }

    public void AtualizaPontuacaoAtual(int pontoFruta)
    {
        this.pontuacaoTotal += pontoFruta;
        this.pontuacaoAtual.text = pontuacaoTotal.ToString();
    }
}
