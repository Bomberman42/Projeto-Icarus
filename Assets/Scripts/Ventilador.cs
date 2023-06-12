using UnityEngine;

public class Ventilador : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particles;
    [SerializeField]
    private float particlesGravity = -0.28f;
    [SerializeField]
    private float particlesStarSpeed = 1.66f;
    private AreaEffector2D effector;
    [SerializeField]
    private bool effectorActive;
    [SerializeField]
    private GameObject airParticle;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private GameObject recivePlayer;
    [Header("Configurações do ventilador vortéx")]
    [SerializeField]
    private Transform fanCenter;
    [SerializeField]
    private bool isVortex;
    private bool playerTargget;

    void Start()
    {
        if(this.particles != null)
        {
            this.particles.gravityModifier = this.particlesGravity;
            this.particles.startSpeed = this.particlesStarSpeed;
        }

        this.boxCollider = GetComponent<BoxCollider2D>();
        this.anim = GetComponent<Animator>();
        this.effector = GetComponent<AreaEffector2D>();
        DisableEffector();
        if(effectorActive)
        {
            EnableEffector();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.name != "FeetCollider" && this.isVortex)
        {
            this.recivePlayer = collision.gameObject;
            Invoke("RemoveGravityPlayer", 0.4f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.name != "FeetCollider" && this.isVortex)
        {
            ReturnGravityPlayer();
            this.recivePlayer = null;
        }
    }

    private void Update()
    {
        if(this.isVortex && this.recivePlayer != null && this.playerTargget == true)
        {
            Vector2 centerPosition = new Vector2(this.recivePlayer.transform.position.x, this.fanCenter.transform.position.y);
            this.recivePlayer.transform.position = Vector2.Lerp(this.recivePlayer.transform.position, centerPosition, Time.deltaTime);
        }
    }

    private void RemoveGravityPlayer()
    {
        if(this.recivePlayer == null)
        {
            return;
        }

        this.recivePlayer.GetComponent<Heroi>().RemoveGravity();
        this.playerTargget = true;
    }

    private void ReturnGravityPlayer()
    {
        if (this.recivePlayer == null)
        {
            return;
        }

        this.recivePlayer.GetComponent<Heroi>().ReturnGravity();
        this.playerTargget = false;
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
