using UnityEngine;

public class Inimigo : Enemy
{
    private void Start()
    {
        StartEnemy();
    }

    private void Update()
    {
        PatrolMovement();
    }
}
