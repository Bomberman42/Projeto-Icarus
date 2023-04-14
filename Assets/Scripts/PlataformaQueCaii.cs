using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaQueCaii : MonoBehaviour
{
    public float tempoDeQueda;

    private TargetJoint2D gatilho;
    private BoxCollider2D colisaoDaPlataforma;
    private int indexLayerFimDeJogo = 8;

    void Start()
    {
       this.gatilho = GetComponent<TargetJoint2D>();
       this.colisaoDaPlataforma = GetComponent<BoxCollider2D>();       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Invoke("Falling", tempoDeQueda);
        }

        if(collision.gameObject.layer ==)
    }

}
