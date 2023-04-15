using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaQueCaii : MonoBehaviour
{
    public float tempoDeQueda;


    private TargetJoint2D gatilho;
    private int indexLayerFimDeJogo = 8;
    private BoxCollider2D colisaoDaPlataforma;

    void Start()
    {
       this.colisaoDaPlataforma = GetComponent<BoxCollider2D>();
       this.gatilho = GetComponent<TargetJoint2D>();   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Invoke("QuedaDaPlataforma", tempoDeQueda);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        Debug.Log(collider.gameObject.tag);

        if(collider.gameObject.layer == indexLayerFimDeJogo)
        {
            Destroy(gameObject);
        }
    }

    private void QuedaDaPlataforma()
    {
        this.gatilho.enabled = false;
        this.colisaoDaPlataforma.isTrigger = true;
    }

}
