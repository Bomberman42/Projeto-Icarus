using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CausaDanoNoHeroi : MonoBehaviour
{
    public int valorDoDanoAoHeroi;
    public float valorDaForcaParaEmpurrarHeroi;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameControle.instance.DanoDoHeroi(this.valorDoDanoAoHeroi, this.valorDaForcaParaEmpurrarHeroi);
        }
    }
}
