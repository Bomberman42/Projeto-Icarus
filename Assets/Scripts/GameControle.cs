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
    private List<Level> levels = new List<Level>();

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

    private void SaveLevel()
    {
        SaveSystem.SaveLevelGame(this);
    }

    private void LoadGame()
    {
        GameData data = SaveSystem.LoadGame();

        if (data == null) {
            return;
        }

        this.levels = SaveSystem.LoadLevelGame();

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

    private void UpdateLevelList()
    {

        if(this.levels == null)
        {
            this.levels = new List<Level>();
        }

        StarScript gameObjectStar = FindObjectOfType<StarScript>();
        Level level = new Level();
        level.star = gameObjectStar.GetComponent<StarScript>().TypeStar();
        level.type = SceneManager.GetActiveScene().name;
        level.coins = this.pontuacaoTotal.ToString();
        level.time = this.timing.GetComponent<Timer>().GetTime();
        this.levels.Add(level);

        //int index = this.levels.FindIndex(level => level.type == SceneManager.GetActiveScene().name);
        //if (index >= 0)
        //{

        //}
    }

    public void FinishedStage() {
        ItensColetaveis[] objectsWithScript = FindObjectsOfType<ItensColetaveis>();
        for (int index = 0; index < objectsWithScript.Length; index++)
        {
            if (objectsWithScript[index].GetComponent<ItensColetaveis>().collide)
            {
                AtualizaPontuacaoAtual(objectsWithScript[index].GetComponent<ItensColetaveis>().valorDoIten);
            }
        }
        UpdateTotalScore();
        UpdateLevelList();
        SaveGame();
        SaveLevel();
        this.pontuacaoTotal = 0;
        CarregaProximaFase("0_TelaDeFases");
    }

    public List<Level> GetLevel()
    {
        return this.levels;
    }
}
