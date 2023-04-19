using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControle : MonoBehaviour
{
    [SerializeField]
    private AudioSource musica;
    public Text pontuacaoAtual;
    private int pontuacaoTotal;
    public GameObject fimDeJogo;
    public BarraDeVida barraDeVida;
    public Heroi heroi;
    public GameObject menuEsc;
    public GameObject menuDeOpcoes;
    public GameObject menuPrincipal;
    public  bool estaPausado;
    private bool menuDeOpcoesAberto;

    public static GameControle instance;

    void Start()
    {
      instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if(estaPausado)
            {
                if (this.menuDeOpcoesAberto)
                {
                    FecharOpcoes();
                    return;
                }
                DespausarGame();
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
        this.menuEsc.SetActive(false);
        this.menuPrincipal.SetActive(false);
        this.menuDeOpcoesAberto = true;
        this.menuDeOpcoes.SetActive(true);
    }

    public void FecharOpcoes()
    {
        this.menuEsc.SetActive(true);
        this.menuPrincipal.SetActive(true);
        this.menuDeOpcoesAberto = false;
        this.menuDeOpcoes.SetActive(false);
    }

    public void SairDoJogo()
    {
        Debug.Log("Ação de sair do jogo");
        Application.Quit();
    }

    public void IrParaOMenuPrincipal()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("0_Menu");
    }

    public void VolumeDaMusica(float mixer)
    {
        this.musica.volume = mixer;
    }

}
