using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuInicial : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicaDoNaruto;
    [SerializeField]
    private AudioSource musicaDoJogo;
    public CanvasGroup grupoDoCanvas;
    public float tempoDeTransicao;

    void Start()
    {
        this.musicaDoNaruto.Play();
        this.musicaDoJogo.enabled = false;
        Invoke("ExecutaTransicao", 3f);
        
    }

    private void ExecutaTransicao()
    {
        StartCoroutine(TransicaoDeCena(1f, 0f, "FadeOut"));
    }

    private IEnumerator TransicaoDeCena(float tempoInicio, float tempoFinal, string transicao)
    {
        float velocidade = (tempoFinal - tempoInicio) / tempoDeTransicao;
        grupoDoCanvas.alpha = tempoInicio;

        if(transicao == "FadeIn")
        {
            while(grupoDoCanvas.alpha < tempoFinal)
            {
                grupoDoCanvas.alpha += velocidade * Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while (grupoDoCanvas.alpha > tempoFinal)
            {
                grupoDoCanvas.alpha += velocidade * Time.deltaTime;
                yield return null;
            }
        }

        grupoDoCanvas.alpha = tempoFinal;
    }

    void Update()
    {
        
    }
}
