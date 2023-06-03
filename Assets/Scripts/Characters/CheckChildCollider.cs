using UnityEngine;

public class CheckChildCollider : MonoBehaviour
{
    [SerializeField]
    private bool takesDamage;

    private void OnCollisionEnter2D(Collision2D colisor)
    {
        if (colisor.gameObject.tag == "Player")
        {
            if (this.takesDamage)
            {
                this.transform.parent.GetComponent<Enemy>().TakesDamage(colisor.gameObject);
            }
        }
    }
}
