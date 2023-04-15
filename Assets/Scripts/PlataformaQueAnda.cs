using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaQueAnda : MonoBehaviour
{
    private SliderJoint2D movimentarPlataforma;
    private JointMotor2D motor2D;
    public Rigidbody2D heroi;

    void Start()
    {
        this.movimentarPlataforma = GetComponent<SliderJoint2D>();
        this.motor2D = this.movimentarPlataforma.motor;
    }

    private void Movimentar()
    {
        if (this.movimentarPlataforma.limitState == JointLimitState2D.UpperLimit)
        {
            this.motor2D.motorSpeed = -1;
        }

        if (this.movimentarPlataforma.limitState == JointLimitState2D.LowerLimit)
        {
            this.motor2D.motorSpeed = 1;
        }

        this.movimentarPlataforma.motor = this.motor2D;
    }

    void Update()
    {
        Movimentar();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = this.transform;
        //collision.gameObject.transform.position = new Vector2(this.transform.position.x, collision.gameObject.transform.position.y);
    }
}