using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D fisicaDoInimigo;
    private Animator animacaoDoInimigo;
    private bool colidir;
    private float velocidadeDeInicio;
    private SpriteRenderer sprite;

    [Header("Atributos do Ininmigo")]
    [SerializeField]
    private Transform colisorDaDireita;
    [SerializeField]
    private Transform colisorDaEsquerda;
    [SerializeField]
    private Transform visionTransform;
    [SerializeField]
    private Transform pontoDaCabeca;
    [SerializeField]
    private BoxCollider2D boxCollider2D;
    [SerializeField]
    private LayerMask camadas;
    [SerializeField]
    private float damageTime = 0.45f;
    [SerializeField]
    private float deathTime = 0.45f;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private GameObject projectileArea;
    private float timer;
    [SerializeField]
    private float projectileSpeed;
    [SerializeField]
    private int projectileDamage;
    private bool isDying;


    [Header("Campo de Visão")]
    [SerializeField]
    private float radiusOfVision;
    [SerializeField]
    private LayerMask layerToAttack;
    private Transform enemyTarget;
    [SerializeField]
    private LayerMask layerGround;
    [SerializeField]
    private float rangeDown;

    [Header("Status do Ininmigo")]
    public int totalDanoRecebidoPorAtaque;
    public int totalDeVida;
    public float velocidade;
    public int totalDanoPorAtaque;
    public float valorDaForcaParaEmpurrarHeroi;

    private BoxCollider2D boxCollider;

    private bool isTakingDamage;

    public void StartEnemy()
    {
        this.fisicaDoInimigo = GetComponent<Rigidbody2D>();
        this.animacaoDoInimigo = GetComponent<Animator>();
        this.velocidadeDeInicio = this.velocidade;
        this.sprite = GetComponent<SpriteRenderer>();
        this.boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionStay2D(Collision2D colisor)
    {
        if (colisor.gameObject.tag == "Player")
        {
            // Se o inimigo Não estiver sofrendo dano, então pode causar dano no player
            if (!this.isTakingDamage)
            {
                CausarDano();
            }
        }
    }

    public void TakesDamage(GameObject colisorGameObject)
    {
        this.isTakingDamage = true;

        // Altera a velocidade do inimigo para iniciar a animação de dano.
        AlteraVelocidade(0);

        // Remove as forças do jogador
        colisorGameObject.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // Empurra o jogador para cima como um impulso por atingir um inimigo.
        colisorGameObject.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 8, ForceMode2D.Impulse);

        // Remove um ponto de vida do inimigo
        this.totalDeVida -= this.totalDanoRecebidoPorAtaque;

        if (this.totalDeVida > 0)
        {
            this.animacaoDoInimigo.SetTrigger("hit");
            Invoke("ResetaVelocidade", this.damageTime);
        }
        else
        {
            this.isDying = true;
            this.animacaoDoInimigo.SetTrigger("death");
            boxCollider2D.enabled = false;
            // Isto faz com que o boneco não tenha fisica
            this.fisicaDoInimigo.bodyType = RigidbodyType2D.Kinematic;
            Destroy(this.gameObject, this.deathTime);
        }

        Invoke("ResetIsTakingDamage", 0.10f);
    }

    private void ResetIsTakingDamage()
    {
        this.isTakingDamage = false;
    }

    private void AlteraVelocidade(float novoValor)
    {
        this.velocidade = novoValor;

        if (novoValor == 0)
        {
            this.fisicaDoInimigo.velocity = Vector2.zero;
        }
    }

    private void ResetaVelocidade()
    {
        this.velocidade = transform.localScale.x >= 0f ? this.velocidadeDeInicio : -this.velocidadeDeInicio;
    }

    private void CausarDano()
    {
        GameControle.instance.DanoDoHeroi(this.totalDanoPorAtaque, this.valorDaForcaParaEmpurrarHeroi);
    }

    public void PatrolMovement()
    {
        if(this.isDying)
        {
            return;
        }

        Vector2 directionToRun = new Vector2(this.velocidade, this.fisicaDoInimigo.velocity.y);
        this.colidir = Physics2D.Linecast(this.colisorDaDireita.position, this.colisorDaEsquerda.position, this.camadas);
        RaycastHit2D eyeCollider = this.EnemyEyeCollider();
        RaycastHit2D feetCollider = this.EnemyFeetCollider();
        RaycastHit2D colliderDown = this.EnemyDownCollider();

        if (!colliderDown)
        {
            this.colidir = true;
        }

        // Se tiver tocado o a parte de baixo do colisor mas não a parte de cima...
        if (colliderDown && !eyeCollider && feetCollider)
        {
            //this.fisicaDoInimigo.velocity = new Vector2((this.velocidade / 2), 5);
            //return;
        }

        if (this.velocidade == 0)
        {
            this.animacaoDoInimigo.SetBool("correr", false);
            return;
        }

        // Se não tiver nenhum alvo marcado não deve voltar a patrulhar.
        if (this.enemyTarget != null)
        {
            Vector2 currentPosition = this.transform.position;
            Vector2 targetPosition = this.enemyTarget.position;

            float distance = Vector2.Distance(currentPosition, targetPosition);

            if (distance >= 0)
            {
                Vector2 directionBetweenTarget = targetPosition - currentPosition;
                directionBetweenTarget = directionBetweenTarget.normalized;

                Debug.Log((directionBetweenTarget.x * (this.velocidade + 2)));

                directionToRun = new Vector2((directionBetweenTarget.x * (this.velocidade > 0 ? this.velocidade + 2 : (-this.velocidade) + 2)), this.transform.position.y);
            }
        }

        this.fisicaDoInimigo.velocity = directionToRun;
        this.animacaoDoInimigo.SetBool("correr", true);

        if (this.enemyTarget != null && !this.colidir)
        {
            if (this.fisicaDoInimigo.velocity.x > 0)
            {
                transform.localScale = new Vector2(transform.localScale.x * (transform.localScale.x < 0 ? -1f : 1f), transform.localScale.y);
                this.velocidade = this.velocidade * (this.velocidade < 0 ? -1f : 1f);
            }
            else
            {
                transform.localScale = new Vector2(transform.localScale.x * (transform.localScale.x > 0 ? -1f : 1f), transform.localScale.y);
                this.velocidade = this.velocidade * (this.velocidade > 0 ? -1f : 1f);
            }
        }

        if (this.colidir)
        {
            if (this.enemyTarget != null)
            {
                this.animacaoDoInimigo.SetBool("correr", false);
            }
            else
            {
                transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
                this.velocidade = -this.velocidade;
            }
        }
    }

    private RaycastHit2D EnemyEyeCollider()
    {
        float direction = this.transform.localScale.x > 0 ? 1 : -1;
        RaycastHit2D eyeCollider = Physics2D.Linecast(this.colisorDaDireita.position, new Vector2(this.colisorDaDireita.position.x + (0.3f * direction), this.colisorDaDireita.position.y), this.camadas);
        return eyeCollider;
    }

    private RaycastHit2D EnemyFeetCollider()
    {
        float direction = this.transform.localScale.x > 0 ? 1 : -1;
        RaycastHit2D feetCollider = Physics2D.Linecast(this.colisorDaEsquerda.position, new Vector2(this.colisorDaEsquerda.position.x + (0.1f * direction), this.colisorDaEsquerda.position.y), this.camadas);
        return feetCollider;
    }

    private RaycastHit2D EnemyDownCollider()
    {
        float direction = this.transform.localScale.x > 0 ? 1 : -1;
        RaycastHit2D colliderDown = Physics2D.Linecast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x + (direction == 1 ? 0.4f : -0.4f), this.transform.position.y + this.rangeDown), this.layerGround);
        return colliderDown;
    }

    // Função para desenhar o gizmo na tela ao redor do inimigo.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(this.transform.position, this.radiusOfVision);

        if (this.enemyTarget != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.visionTransform.position, this.enemyTarget.position);
        }

        //// Desenha a linha entre os dois pontos para detectar a colisão
        //Gizmos.DrawLine(this.colisorDaDireita.position, this.colisorDaEsquerda.position);

        float direction = this.transform.localScale.x > 0 ? 1 : -1;

        // Desenha na tela duas linhas para detectar se o inimigo pode pular algum bloco.
        Gizmos.DrawLine(this.colisorDaDireita.position, new Vector2(this.colisorDaDireita.position.x + (0.3f * direction), this.colisorDaDireita.position.y));
        Gizmos.DrawLine(this.colisorDaEsquerda.position, new Vector2(this.colisorDaEsquerda.position.x + (0.1f * direction), this.colisorDaEsquerda.position.y));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x + (direction == 1 ? 0.4f : -0.4f), this.transform.position.y + this.rangeDown));

        ////// Desenha na tela duas linhas para detectar se o player entrou no campo de visão.
        ////Gizmos.color = Color.red;
        ////Gizmos.DrawLine(this.colisorDaDireita.position, new Vector2(this.colisorDaDireita.position.x + (direction * 3f), this.colisorDaDireita.position.y + 2));
        ////Gizmos.color = Color.red;
        ////Gizmos.DrawLine(this.colisorDaEsquerda.position, new Vector2(this.colisorDaEsquerda.position.x + (direction * 3f), this.colisorDaEsquerda.position.y));

        ////Gizmos.DrawWireSphere(new Vector2(this.colisorDaDireita.position.x + (direction * 1.8f), this.colisorDaDireita.position.y), this.radiusOfVision);

        ////Debug.DrawRay(new Vector2(this.colisorDaDireita.position.x, this.colisorDaDireita.position.y), new Vector2(this.transform.localScale.x > 0 ? 1 : -1, -10), Color.red);
        ////Debug.DrawRay(new Vector2(this.colisorDaEsquerda.position.x, this.colisorDaEsquerda.position.y), new Vector2(this.transform.localScale.x > 0 ? 1 : -1, 1), Color.red
    }

    public Transform FindPlayer()
    {
        if (this.isDying)
        {
            return null;
        }

        float direction = this.transform.localScale.x > 0 ? 1 : -1;
        Collider2D collider = Physics2D.OverlapCircle(this.transform.position, this.radiusOfVision, this.layerToAttack);
        //Debug.Log(collider);

        if (collider != null)
        {
            //this.enemyTarget = collider.transform;

            Vector2 startPosition = this.visionTransform.position;
            Vector2 targetPosition = collider.transform.position;
            Vector2 directionBetweenTarget = targetPosition - startPosition;
            directionBetweenTarget = directionBetweenTarget.normalized;

            RaycastHit2D hit = Physics2D.Raycast(startPosition, directionBetweenTarget);

            //Debug.Log(hit.transform.tag);
            // Encontrou algum objeto.
            if (hit.transform != null)
            {
                //Debug.Log(hit.collider.gameObject.tag);
                if (hit.transform.CompareTag("Player"))
                {
                    this.enemyTarget = hit.transform;
                }
                else
                {
                    this.enemyTarget = null;
                }
            }
            else
            {
                this.enemyTarget = null;
            }
        }
        else
        {
            this.enemyTarget = null;
        }

        return this.enemyTarget;
    }

    public void AttackingThePlayer()
    {
        if (this.isDying)
        {
            return;
        }

        // Se não tiver nenhum alvo marcado deve voltar a patrulhar.
        if (this.enemyTarget == null)
        {
            return;
        }

        // Se não tiver nenhum alvo marcado deve voltar a patrulhar.
        if (this.enemyTarget != null)
        {
            Vector2 currentPosition = this.transform.position;
            Vector2 targetPosition = this.enemyTarget.position;

            float distance = Vector2.Distance(currentPosition, targetPosition);

            if (distance >= 0)
            {
                Vector2 directionBetweenTarget = targetPosition - currentPosition;
                directionBetweenTarget = directionBetweenTarget.normalized;
                this.sprite.flipX = directionBetweenTarget.x > 0;
            }
        }

    }

    public float InvokeAttack(float timer, float shootingTime)
    {
        if (this.isDying)
        {
            return 0;
        }

        if (timer < shootingTime)
        {
            return timer;
        }
        float direction = this.transform.localScale.x > 0 ? 1 : -1;
        this.animacaoDoInimigo.SetTrigger("attack");
        GameObject projectile = Instantiate(this.projectile, this.projectileArea.transform.position, this.projectile.transform.rotation);
        projectile.transform.parent = null;
        projectile.GetComponent<Projectile>().projectileDamage = this.projectileDamage;
        projectile.GetComponent<Projectile>().projectileSpeed = this.sprite.flipX ? this.projectileSpeed : - this.projectileSpeed;
        return 0;
    }








































    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.transform.CompareTag("Enemy"))
    //    {
    //        this.fisicaDoInimigo.bodyType = RigidbodyType2D.Kinematic;
    //        //this.boxCollider.isTrigger = true;
    //        collision.collider.enabled = false;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.transform.CompareTag("Enemy"))
    //    {
    //        this.fisicaDoInimigo.bodyType = RigidbodyType2D.Dynamic;
    //        //this.boxCollider.isTrigger = false;
    //        collision.collider.enabled = true;
    //    }
    //}

    private void MovementOfAttackingTheEnemy()
    {
        // Se não tiver nenhum alvo marcado não deve voltar a patrulhar.
        if (this.enemyTarget == null)
        {
            return;
        }

        Vector2 directionToRun = new Vector2(this.velocidade, this.fisicaDoInimigo.velocity.y);
        this.colidir = Physics2D.Linecast(this.colisorDaDireita.position, this.colisorDaEsquerda.position, this.camadas);

        float direction = this.transform.localScale.x > 0 ? 1 : -1;
        RaycastHit2D eyeCollider = Physics2D.Linecast(this.colisorDaDireita.position, new Vector2(this.colisorDaDireita.position.x + (0.3f * direction), this.colisorDaDireita.position.y), this.camadas);
        RaycastHit2D feetCollider = Physics2D.Linecast(this.colisorDaEsquerda.position, new Vector2(this.colisorDaEsquerda.position.x + (0.1f * direction), this.colisorDaEsquerda.position.y), this.camadas);
        //RaycastHit2D colliderDown = Physics2D.Linecast(new Vector2(this.colisorDaEsquerda.position.x + (0.1f * direction), this.colisorDaEsquerda.position.y), new Vector2(this.colisorDaEsquerda.position.x + (0.1f * direction), this.colisorDaEsquerda.position.y + -0.8f));

        //if (!colliderDown)
        //{

        //}

        // Se tiver tocado o a parte de baixo do colisor mas não a parte de cima...
        if (!eyeCollider && feetCollider)
        {
            this.fisicaDoInimigo.velocity = new Vector2((this.velocidade / 2), 5);
            return;
        }

        if (this.velocidade == 0)
        {
            this.animacaoDoInimigo.SetBool("correr", false);
            return;
        }

        // Se não tiver nenhum alvo marcado não deve voltar a patrulhar.
        if (this.enemyTarget != null)
        {
            Vector2 currentPosition = this.transform.position;
            Vector2 targetPosition = this.enemyTarget.position;

            float distance = Vector2.Distance(currentPosition, targetPosition);

            //Debug.Log("distance" + distance);

            if (distance >= 0)
            {
                Vector2 directionBetweenTarget = targetPosition - currentPosition;
                directionBetweenTarget = directionBetweenTarget.normalized;

                directionToRun = new Vector2((directionBetweenTarget * (this.velocidade + 2)).x, this.transform.position.y);
            }

            if (this.fisicaDoInimigo.velocity.x > 0)
            {
                this.sprite.flipX = false;
                this.velocidade = this.velocidade * 1;
            }
            else
            {
                this.sprite.flipX = true;
                this.velocidade = -this.velocidade;
            }
        }

        this.fisicaDoInimigo.velocity = directionToRun;
        this.animacaoDoInimigo.SetBool("correr", true);

        if (colidir && this.enemyTarget == null)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            this.velocidade = -this.velocidade;
        }
    }
}
