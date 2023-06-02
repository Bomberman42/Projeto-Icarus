using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineAttack : Enemy
{
    [SerializeField]
    private float shootingTime = 0.30f;
    private float timer;

    void Start()
    {
        StartEnemy();
    }


    void Update()
    {
        Transform playerPosition = FindPlayer();
        AttackingThePlayer();
        this.timer += Time.deltaTime;
        if(playerPosition == null)
        {
            this.timer = 0;
        }
        else
        {
            this.timer = InvokeAttack(this.timer, this.shootingTime);
        }
    }
}
