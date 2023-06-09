using UnityEngine;

public class CaixaDeFerro : MonoBehaviour
{
    private Rigidbody2D rig;
    public AudioSource soundOfMoving;

    [SerializeField]
    private Transform feetColliderR;
    [SerializeField]
    private Transform feetColliderL;
    [SerializeField]
    private bool colisionIgnore;
    private bool canMove = true;

    [SerializeField]
    private LayerMask layerGround;

    private bool isFall;

    private void Start()
    {
        this.rig = GetComponent<Rigidbody2D>();
    }

    private RaycastHit2D rCollider;
    private RaycastHit2D lCollider;

    private void Update()
    {
        this.rCollider = CheckFeetColliderR();
        this.lCollider = CheckFeetColliderL();

        if (this.isFall)
        {
            //this.rig.velocity = new Vector2(0, this.rig.velocity.y);
        }
    }

    private void OnDrawGizmos()
    {
        float direction = this.transform.localScale.x > 0 ? 1 : -1;

        Gizmos.DrawLine(this.feetColliderR.position, new Vector2(this.feetColliderR.position.x + (0.3f * direction), this.feetColliderR.position.y));
        Gizmos.DrawLine(this.feetColliderL.position, new Vector2(this.feetColliderL.position.x - (0.3f * direction), this.feetColliderL.position.y));
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.rig.bodyType = RigidbodyType2D.Dynamic;
            this.rig.velocity = Vector2.zero;
            this.gameObject.transform.parent = null;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(this.canMove == true)
            {
                return;
            }

            if (collision.gameObject.GetComponent<Heroi>().feetCollider.position.y > this.transform.position.y)
            {
                return;
            }

            float playerPositionX = collision.gameObject.GetComponent<Rigidbody2D>().velocity.x;
            Vector2 boxNewPosition = new Vector2(playerPositionX, this.rig.velocity.y);

            if (!this.rCollider && playerPositionX > 0 && !this.colisionIgnore)
            {
                this.rig.velocity = Vector2.zero;
                this.rig.bodyType = RigidbodyType2D.Kinematic;
                return;
            }

            if (!this.lCollider && playerPositionX < 0 && !this.colisionIgnore)
            {
                this.rig.velocity = Vector2.zero;
                this.rig.bodyType = RigidbodyType2D.Kinematic;
                return;
            }

            this.rig.velocity = boxNewPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            this.canMove = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            this.canMove = false;
        }
    }

    private RaycastHit2D CheckFeetColliderR()
    {
        float direction = this.transform.localScale.x > 0 ? 1 : -1;
        RaycastHit2D rCollider = Physics2D.Linecast(this.feetColliderR.position, new Vector2(this.feetColliderR.position.x + (0.3f * direction), this.feetColliderR.position.y), this.layerGround);
        return rCollider;
    }

    private RaycastHit2D CheckFeetColliderL()
    {
        float direction = this.transform.localScale.x > 0 ? 1 : -1;
        RaycastHit2D lCollider = Physics2D.Linecast(this.feetColliderL.position, new Vector2(this.feetColliderL.position.x - (0.3f * direction), this.feetColliderL.position.y), this.layerGround);
        return lCollider;
    }

    public void isNotCollideOnGround()
    {
        this.rig.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        this.rig.velocity = new Vector2(0, this.rig.velocity.y - 1);
        this.isFall = true;
    }

    public void isCollideOnGround()
    {
        this.rig.constraints = RigidbodyConstraints2D.FreezeRotation;
        this.isFall = false;
    }
}
