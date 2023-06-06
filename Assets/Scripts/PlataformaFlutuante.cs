using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaFlutuante : MonoBehaviour
{
    private float speed;
    private SliderJoint2D movimentarPlataforma;
    private JointMotor2D motor2D;
    public bool forceUp;
    public bool temEspinhos;
    public GameObject espinhos;

    void Start()
    {
        this.movimentarPlataforma = GetComponent<SliderJoint2D>();
        this.motor2D = this.movimentarPlataforma.motor;
        this.speed = this.motor2D.motorSpeed;

        if (this.temEspinhos)
        {
            InvokeRepeating("AtualizaEstadoDosEspinhos", 3.0f, 3.0f);
        }
    }

    private void Movimentar()
    {
        if (this.movimentarPlataforma.limitState == JointLimitState2D.UpperLimit || this.forceUp)
        {
            this.forceUp = false;
            this.motor2D.motorSpeed = -this.speed;
        }

        if (this.movimentarPlataforma.limitState == JointLimitState2D.LowerLimit)
        {
            this.motor2D.motorSpeed = this.speed;
        }

        this.movimentarPlataforma.motor = this.motor2D;
    }

    void Update()
    {
        Movimentar();
    }

    private void AtualizaEstadoDosEspinhos()
    {
        this.espinhos.SetActive(!this.espinhos.activeSelf);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = this.transform;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = null;
    }
}