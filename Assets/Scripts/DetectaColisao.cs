using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectaColisao : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.transform.parent.GetComponent<PlataformaQueCaii>().ColisaoDetectada(this);
    }
}
