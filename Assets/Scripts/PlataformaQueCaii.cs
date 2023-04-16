using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaQueCaii : MonoBehaviour
{
    public float tempoDeQueda;
    private TargetJoint2D gatilho;
    private int indexLayerFimDeJogo = 8;
    private BoxCollider2D colisaoDaPlataforma;
    public BoxCollider2D removedorDeAtritoDireita;
    public BoxCollider2D removedorDeAtritoEsquerda;
    private Vector3 posicaoInicialDaPlataforma;
    public float tempParaReaparecer;

    void Start()
    {
        this.colisaoDaPlataforma = GetComponent<BoxCollider2D>();
        this.gatilho = GetComponent<TargetJoint2D>();
        this.posicaoInicialDaPlataforma = this.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == indexLayerFimDeJogo)
        {
            // Se a plataforma estiver definido um tempo em que deve reaparecer então devemos montar a plataforma novamente na tela.
            if (this.tempParaReaparecer > 0)
            {
                this.gameObject.SetActive(false);
                this.transform.position = this.posicaoInicialDaPlataforma;

                Invoke("ReaparecerPlataforma", this.tempParaReaparecer);
                return;
            }

            Destroy(gameObject);
        }
    }

    private void ReaparecerPlataforma()
    {
        this.gameObject.SetActive(true);
        this.gatilho.enabled = true;
        this.colisaoDaPlataforma.isTrigger = false;
        this.removedorDeAtritoDireita.isTrigger = false;
        this.removedorDeAtritoEsquerda.isTrigger = false;
    }

    public void ColisaoDetectada(DetectaColisao scriptDoComponenteFilho)
    {
        // Se a plataforma não tiver um tempo de queda definido, ela não deve cair nunca.
        if (this.tempoDeQueda == 0)
        {
            return;
        }

        Invoke("QuedaDaPlataforma", tempoDeQueda);
    }

    private void QuedaDaPlataforma()
    {
        this.gatilho.enabled = false;
        this.colisaoDaPlataforma.isTrigger = true;
        this.removedorDeAtritoDireita.isTrigger = true;
        this.removedorDeAtritoEsquerda.isTrigger = true;
    }
}
