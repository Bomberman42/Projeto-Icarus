using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caixas : MonoBehaviour
{
    public float impacto;
    public bool forcaParaCima;
    public int vidaDaCaixa;
    public Animator animacaoDaCaixa;
    public GameObject coletada;

    void Update()
    {
        if (this.vidaDaCaixa <= 0)
        {
            this.coletada.SetActive(true);
            transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            transform.parent.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(transform.parent.gameObject, 0.40f);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            this.animacaoDaCaixa.SetTrigger("dano");
            Vector2 posicao = new Vector2(0f, this.forcaParaCima ? this.impacto: - this.impacto);
            this.vidaDaCaixa--;
            collider.gameObject.GetComponent<Rigidbody2D>().AddForce(posicao, ForceMode2D.Impulse);
        }
    }
}
