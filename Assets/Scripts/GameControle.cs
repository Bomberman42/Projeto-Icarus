using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControle : MonoBehaviour
{
    [SerializeField]
    private Text totalPointsAcquired;
    [SerializeField]
    private GameObject menuDeOpcoes;
    [SerializeField]
    private GameObject timing;
    [SerializeField]
    private GameObject lifeBar;
    [SerializeField]
    private GameObject pointsBar;

    public Text pontuacaoAtual;
    public int pontuacaoTotal;
    private int playerTotalPoints;
    public GameObject fimDeJogo;
    public BarraDeVida barraDeVida;
    public Heroi heroi;
    public GameObject menuEsc;

    public GameObject menuPrincipal;
    public bool estaPausado;

    public static GameControle instance;

    void Start()
    {
        instance = this;
        LoadGame();
        if (SceneManager.GetActiveScene().name == "0_TelaDeFases")
        {
            this.timing.SetActive(false);
            this.lifeBar.SetActive(false);
            this.pointsBar.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
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
        StringVariables.sceneToLoad = nomeDaProximaCena;
        SceneManager.LoadScene("LoadScene");
        Timer.stopTimer = true;
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
        this.menuDeOpcoes.SetActive(true);
    }

    public void FecharOpcoes()
    {
        this.menuEsc.SetActive(true);
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
        Debug.Log("player total points " + playerTotalPoints);
        this.playerTotalPoints += this.pontuacaoTotal;
        Debug.Log("total points " + pontuacaoTotal);
        Debug.Log("total points adquirido " + totalPointsAcquired);

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
