using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoFogo : MonoBehaviour
{

    private GameObject playerGameObject;
    public GameObject fogoAtivando;
    public GameObject fogoOn;
    public GameObject fogoOff;
    public int valorDoDanoAoHeroi;
    public float valorDaForcaParaEmpurrarHeroi;
    public float tempoDoFoguinhoOn = 3f;
    public float tempoDoFoguinhoOff = 2f;
    public float tempoDoFoguinhoLigando = 4f;

    void Start()
    {
        if (this.tempoDoFoguinhoOff > 0)
        {
            Invoke("DesligarTrap", 0f);
            return;
        }

        if (this.tempoDoFoguinhoLigando > 0)
        {
            Invoke("AtivarTrap", 0f);
            return;
        }

        if (this.tempoDoFoguinhoOn > 0)
        {
            Invoke("LigarTrap", 0f);
            return;
        }
    }

    private void AtivarTrap()
    {
        this.fogoOff.SetActive(false);
        this.fogoAtivando.SetActive(true);
        this.fogoOn.SetActive(false);

        if (this.tempoDoFoguinhoOn == 0)
        {
            return;
        }

        Invoke("LigarTrap", this.tempoDoFoguinhoLigando);
    }

    private void LigarTrap()
    {
        this.fogoOn.SetActive(true);
        this.fogoOff.SetActive(false);
        this.fogoAtivando.SetActive(false);

        if (this.tempoDoFoguinhoOff == 0)
        {
            return;
        }

        Invoke("DesligarTrap", this.tempoDoFoguinhoOn);
    }

    private void DesligarTrap()
    {
        this.fogoOff.SetActive(true);
        this.fogoAtivando.SetActive(false);
        this.fogoOn.SetActive(false);

        if (this.tempoDoFoguinhoLigando == 0)
        {
            return;
        }

        Invoke("AtivarTrap", this.tempoDoFoguinhoOff);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && this.fogoOn.activeSelf)
        {
            this.playerGameObject = collision.gameObject;
            bool lookingRight = collision.gameObject.transform.rotation.y >= 0 ? true : false;
            Vector2 playerPosition = collision.gameObject.transform.position;
            //collision.gameObject.transform.position = new Vector2(playerPosition.x + (lookingRight ? -1f:1f), playerPosition.y + 0.3f);
            //collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2((lookingRight ? -6f : 6f), 1f);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2 ((lookingRight ? -0.5f : 0.5f), 0.2f), ForceMode2D.Impulse);
            Invoke("TurnOffVelocity", 0.5f);
            GameControle.instance.DanoDoHeroi(this.valorDoDanoAoHeroi, this.valorDaForcaParaEmpurrarHeroi);
        }
    }

    private void TurnOffVelocity()
    {
        this.playerGameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.playerGameObject.GetComponent<Rigidbody2D>().velocity.y);

    }
}
