using UnityEngine;

public class StarScript : MonoBehaviour
{
    private enum EnumType { Gold, Silver, Bronze }
    [SerializeField]
    private EnumType starType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameControle.instance.FinishedStage();
        }
    }
}
