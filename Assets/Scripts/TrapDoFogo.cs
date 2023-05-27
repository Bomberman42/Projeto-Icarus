using System.Collections.Generic;
using UnityEngine;

public class TrapDoFogo : MonoBehaviour
{
    private SpriteRenderer sr;
    public int valorDoDanoAoHeroi;
    public float valorDaForcaParaEmpurrarHeroi;
    public float tempoDoFoguinhoOn = 3f;
    public float tempoDoFoguinhoOff = 2f;
    public float tempoDoFoguinhoLigando = 4f;
    private GameObject currentInstance;

    [SerializeField]
    private List<GameObject> fireTrapList;

    void Start()
    {
        this.sr = GetComponent<SpriteRenderer>();
        this.sr.enabled = false;

        if (this.tempoDoFoguinhoOff > 0)
        {
            Invoke("FireTrapOff", 0f);
            return;
        }

        if (this.tempoDoFoguinhoLigando > 0)
        {
            Invoke("FireTrapStarting", 0f);
            return;
        }

        if (this.tempoDoFoguinhoOn > 0)
        {
            Invoke("FireTrapOn", 0f);
            return;
        }
    }

    private void FireTrapStarting()
    {
        Destroy(this.currentInstance);
        this.currentInstance = Instantiate(this.fireTrapList[1], this.transform);
        this.currentInstance.name = "FireTrapStarting";
        this.currentInstance.transform.localScale = new Vector2(1f, 1f);

        if (this.tempoDoFoguinhoOn == 0)
        {
            return;
        }

        Invoke("FireTrapOn", this.tempoDoFoguinhoLigando);
    }

    private void FireTrapOn()
    {
        Destroy(this.currentInstance);
        this.currentInstance = Instantiate(this.fireTrapList[2], this.transform);
        this.currentInstance.name = "FireTrapOn";
        this.currentInstance.transform.localScale = new Vector2(1f, 1f);

        if (this.tempoDoFoguinhoOff == 0)
        {
            return;
        }

        Invoke("FireTrapOff", this.tempoDoFoguinhoOn);
    }

    private void FireTrapOff()
    {
        Destroy(this.currentInstance);
        this.currentInstance = Instantiate(this.fireTrapList[0], this.transform);
        this.currentInstance.name = "FireTrapOff";
        this.currentInstance.transform.localScale = new Vector2(1f, 1f);

        if (this.tempoDoFoguinhoLigando == 0)
        {
            return;
        }

        Invoke("FireTrapStarting", this.tempoDoFoguinhoOff);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (this.currentInstance.name == "FireTrapOn")
            {
                GameControle.instance.DanoDoHeroi(this.valorDoDanoAoHeroi, this.valorDaForcaParaEmpurrarHeroi);
            }
        }
    }
}
