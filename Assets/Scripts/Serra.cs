using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serra : MonoBehaviour
{
    public float velocidade;
    public float tempoDeMovimento;
    private SliderJoint2D movimentarObjeto;
    private JointMotor2D motor2D;
    private float velocidadeDoMotor;

    private bool direcaoParaDireita = true;
    private float temporizador;

    private void Start()
    {
        this.movimentarObjeto = GetComponent<SliderJoint2D>();
        this.motor2D = this.movimentarObjeto.motor;
        this.velocidadeDoMotor = this.motor2D.motorSpeed;
    }

    private void Update()
    {
        Movimentar();
    }
    private void Movimentar()
    {
        if (this.movimentarObjeto.limitState == JointLimitState2D.UpperLimit)
        {
            this.motor2D.motorSpeed = -this.velocidadeDoMotor;
        }

        if (this.movimentarObjeto.limitState == JointLimitState2D.LowerLimit)
        {
            this.motor2D.motorSpeed = this.velocidadeDoMotor;
        }

        this.movimentarObjeto.motor = this.motor2D;
    }

    private void MovimentaSerraManualmente()
    {
        if (direcaoParaDireita)
        {
            transform.Translate(Vector2.right * velocidade * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * velocidade * Time.deltaTime);
        }

        temporizador += Time.deltaTime;

        if(temporizador >= tempoDeMovimento)
        {
            direcaoParaDireita = !direcaoParaDireita;
            temporizador = 0f;
        }
        
    }
}
