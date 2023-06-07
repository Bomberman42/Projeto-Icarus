using UnityEngine;

public class StarScript : MonoBehaviour
{
    private enum EnumType { Gold, Silver, Bronze }
    [SerializeField]
    private EnumType starType;
    private bool playerCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !this.playerCollider)
        {
            this.playerCollider = true;
            GameControle.instance.FinishedStage();
        }
    }

    public string TypeStar()
    {
        return this.starType.ToString();
    }
}
