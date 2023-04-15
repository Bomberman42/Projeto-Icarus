using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControle : MonoBehaviour
{
    public Text pontuacaoAtual;
    private int pontuacaoTotal;
    private int vida;
    public GameObject fimDeJogo;
    

    public static GameControle instance;

    void Start()
    {
      instance = this;
        
    }

    public void FimDeJogo()
    {
        Time.timeScale = 0;
        this.fimDeJogo.SetActive(true);
    }

    public void AtualizaPontuacaoAtual(int pontoFruta)
    {
        this.pontuacaoTotal += pontoFruta;
        this.pontuacaoAtual.text = pontuacaoTotal.ToString();
    }

    public void ResetarCena()
    {
        Time.timeScale = 1;
        Scene cenaAtual = SceneManager.GetActiveScene();
        SceneManager.LoadScene(cenaAtual.name);
    }
}
