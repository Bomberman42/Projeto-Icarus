using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneric : Enemy
{
    void Start()
    {
        StartEnemy();
    }

    void Update()
    {
        PatrolMovement();
    }
}
