using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidaNivelAtual : MonoBehaviour
{
    public int valorMaximoDeFrutas;
    public string nomeDaProximaCena;

    private void Update()
    {
        if (valorMaximoDeFrutas == 0 || nomeDaProximaCena == "") {
            return;
        }

        int pontuacaoAtual = GameControle.instance.RetornaPontuacaoAtual();

        if (pontuacaoAtual == valorMaximoDeFrutas)
        {
            GameControle.instance.CarregaProximaFase(nomeDaProximaCena);
        }
    }
}
