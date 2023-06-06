using UnityEngine;

public class CheckColission : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        this.transform.parent.gameObject.GetComponent<PlataformaFlutuante>().forceUp = true;
    }
}
