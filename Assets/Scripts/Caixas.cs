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
    public float tempoDeMorte;
    public GameObject loot;
    public int quantidadeDeLoot = 1;
    private bool destruindoObjeto;

    void Update()
    {
        if (this.vidaDaCaixa <= 0 && !this.destruindoObjeto)
        {
            this.destruindoObjeto = true;
            this.coletada.SetActive(true);
            transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            transform.parent.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            if(this.loot != null)
            {
                for(int index = 0; index < quantidadeDeLoot; index++)
                {
                    Instantiate(this.loot, new Vector3(Random.Range(transform.parent.position.x +0.3f, transform.parent.position.x -0.3f), Random.Range(transform.parent.position.y +0.3f, transform.parent.position.y -0.3f)), transform.parent.rotation);
                }
            }

            Destroy(transform.parent.gameObject, this.tempoDeMorte);
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
