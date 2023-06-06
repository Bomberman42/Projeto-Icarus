using UnityEngine;

public class Ventilador : MonoBehaviour
{
    private AreaEffector2D effector;
    [SerializeField]
    private bool effectorActive;
    [SerializeField]
    private GameObject airParticle;
    private Animator anim;
    private BoxCollider2D boxCollider;

    void Start()
    {
        this.boxCollider = GetComponent<BoxCollider2D>();
        this.anim = GetComponent<Animator>();
        this.effector = GetComponent<AreaEffector2D>();
        DisableEffector();
        if(effectorActive)
        {
            EnableEffector();
        }
    }

    public void EnableEffector()
    {
        this.boxCollider.enabled = true;
        this.effector.enabled = true;
        this.anim.SetBool("active", true);
        this.airParticle.SetActive(true);
    }

    public void DisableEffector()
    {
        this.boxCollider.enabled = false;
        this.effector.enabled = false;
        this.anim.SetBool("active", false);
        this.airParticle.SetActive(false);
    }
}
