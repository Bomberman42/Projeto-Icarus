using UnityEngine;

public class ButtonAction : MonoBehaviour
{

    [SerializeField]
    private GameObject turnOnObject;
    private Animator anim;

    private void Start()
    {
        this.anim = GetComponent<Animator>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.anim.SetBool("active", true);
            if (this.turnOnObject.name == "Fan")
            {
                this.turnOnObject.GetComponent<Ventilador>().EnableEffector();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.anim.SetBool("active", false);
            if (this.turnOnObject.name == "Fan")
            {
                this.turnOnObject.GetComponent<Ventilador>().DisableEffector();
            }
        }
    }
}
