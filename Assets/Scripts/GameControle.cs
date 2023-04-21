using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControle : MonoBehaviour
{
    public Text pontuacaoAtual;
    [SerializeField]
    private Text totalPointsAcquired;
    public int pontuacaoTotal;
    private int playerTotalPoints;
    public GameObject fimDeJogo;
    public BarraDeVida barraDeVida;
    public Heroi heroi;
    public GameObject menuEsc;
    public GameObject menuDeOpcoes;
    public GameObject menuPrincipal;
    public bool estaPausado;
    private bool menuDeOpcoesAberto;

    public static GameControle instance;

    void Start()
    {
        instance = this;
        LoadGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            //if (this.menuPrincipal != null)
            //{
            //    if (this.menuDeOpcoesAberto)
            //    {
            //        FecharOpcoes();
            //        return;
            //    }

            //    return;
            //}

            if (estaPausado)
            {
                if (!menuDeOpcoes.activeSelf)
                {
                    DespausarGame();
                }
            } else
            {
                PausarGame();
            }
        }
    }

    public void PausarGame()
    {
        Time.timeScale = 0;
        this.menuEsc.SetActive(true);
        estaPausado = true;
    }

    public void DespausarGame()
    {
        Time.timeScale = 1;
        this.menuEsc.SetActive(false);
        estaPausado = false;
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

    public void ContinueGame()
    {
        LoadGame();
        Time.timeScale = 1;
        SceneManager.LoadScene("0_TelaDeFases");
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

    public void AbrirOpcoes()
    {
        //this.menuEsc.SetActive(false);
        this.menuDeOpcoesAberto = true;
        this.menuDeOpcoes.SetActive(true);
    }

    public void FecharOpcoes()
    {
        this.menuEsc.SetActive(true);
        this.menuDeOpcoesAberto = false;
        this.menuDeOpcoes.SetActive(false);
    }

    public void SairDoJogo()
    {
        SaveGame();
        Application.Quit();
    }

    public void IrParaOMenuPrincipal()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("0_Menu");
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame(this);
    }

    private void LoadGame()
    {
        Debug.Log("Entrou aqui1");
        GameData data = SaveSystem.LoadGame();

        if (data == null) {
            return;
        }

        this.playerTotalPoints = data.playerTotalPoints;

        if (this.playerTotalPoints >= 0 && this.totalPointsAcquired != null)
        {
            this.totalPointsAcquired.text = this.playerTotalPoints.ToString();
        }
    }

    public void UpdateTotalScore()
    {
        this.playerTotalPoints += this.pontuacaoTotal;

        if (this.playerTotalPoints >= 0 && this.totalPointsAcquired != null)
        {
            this.totalPointsAcquired.text = this.playerTotalPoints.ToString();
        }
    }

    public int GetPlayerTotalPoints()
    {
        return this.playerTotalPoints;
    }

    public void FinishedStage() {
        UpdateTotalScore();
        SaveGame();
        this.pontuacaoTotal = 0;
        CarregaProximaFase("0_TelaDeFases");
    }
}
