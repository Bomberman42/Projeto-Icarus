public class EnemyCoguman : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        StartEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        PatrolMovement();
    }
}
