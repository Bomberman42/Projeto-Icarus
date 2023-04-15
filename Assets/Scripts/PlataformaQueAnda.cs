using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaQueAnda : MonoBehaviour
{
    private SliderJoint2D movimentarPlataforma;

    void Start()
    {
        this.movimentarPlataforma = GetComponent<SliderJoint2D>();
    }

    private void Movimentar()
    {
        Debug.Log(this.movimentarPlataforma.connectedAnchor.x);

        if (this.transform.position.x * 2 > this.movimentarPlataforma.connectedAnchor.x)
        {

        }
    }

    void Update()
    {
        Movimentar();
    }
}
