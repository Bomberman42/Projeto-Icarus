using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControle : MonoBehaviour
{
    public Text pontuacaoAtual;
    private int pontuacaoTotal;
    public GameObject fimDeJogo;
    public BarraDeVida barraDeVida;
    public Heroi heroi;
    public GameObject menuEsc;

    public static GameControle instance;

    void Start()
    {
      instance = this;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            this.menuEsc.SetActive(true);
        }

        while (Input.GetButtonDown("Cancel") == true) { this.menuEsc.SetActive(false); }
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
    public int RetornaPontuacaoAtual()
    {
        return this.pontuacaoTotal;
    }

    public void ResetarCena()
    {
        Time.timeScale = 1;
        Scene cenaAtual = SceneManager.GetActiveScene();
        SceneManager.LoadScene(cenaAtual.name);
    }

    public void CarregaProximaFase(string nomeDaProximaCena)
    {
        SceneManager.LoadScene(nomeDaProximaCena);
    }

    public void DanoDoHeroi(int valorDoDanoAoHeroi, float valorDaForcaParaEmpurrarHeroi)
    {
        if (this.heroi.EstaTomandoDando)
        {
            return;
        }

        int vidaAtual = this.barraDeVida.RemoveVida(valorDoDanoAoHeroi);

        if (vidaAtual == 0)
        {
            FimDeJogo();
            return;
        }

        this.heroi.SofreuDano(valorDaForcaParaEmpurrarHeroi);
    }

    public void SairDoJogo()
    {
        Debug.Log("Ação de sair do jogo");
        Application.Quit();
    }
}
