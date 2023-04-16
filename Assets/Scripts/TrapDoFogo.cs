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
}
