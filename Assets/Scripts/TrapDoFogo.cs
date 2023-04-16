using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoFogo : MonoBehaviour
{

    public GameObject fogoAtivando;
    public GameObject fogoOn;
    public GameObject fogoOff;
    public float tempoDoFoguinhoOn = 3f;
    public float tempoDoFoguinhoOff = 2f;
    public float tempoDoFoguinhoLigando = 4f;

    void Start()
    {
        Invoke("DesligarTrap", 2f);
    }

    private void AtivarTrap()
    {
        this.fogoOff.SetActive(false);
        this.fogoAtivando.SetActive(true);
        this.fogoOn.SetActive(false);
        Invoke("LigarTrap", this.tempoDoFoguinhoLigando);
    }

    private void LigarTrap()
    {
        this.fogoOn.SetActive(true);
        this.fogoOff.SetActive(false);
        this.fogoAtivando.SetActive(false);
        Invoke("DesligarTrap", this.tempoDoFoguinhoOn);
    }

    private void DesligarTrap()
    {
        this.fogoOff.SetActive(true);
        this.fogoAtivando.SetActive(false);
        this.fogoOn.SetActive(false);
        Invoke("AtivarTrap", this.tempoDoFoguinhoOff);
    }
}
