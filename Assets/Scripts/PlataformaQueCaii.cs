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

    void Start()
    {
        this.colisaoDaPlataforma = GetComponent<BoxCollider2D>();
        this.gatilho = GetComponent<TargetJoint2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == indexLayerFimDeJogo)
        {
            Destroy(gameObject);
        }
    }

    private void QuedaDaPlataforma()
    {
        this.gatilho.enabled = false;
        this.colisaoDaPlataforma.isTrigger = true;
        this.removedorDeAtritoDireita.isTrigger = true;
        this.removedorDeAtritoEsquerda.isTrigger = true;
    }

    public void ColisaoDetectada(DetectaColisao scriptDoComponenteFilho)
    {
        Invoke("QuedaDaPlataforma", tempoDeQueda);
    }
}
