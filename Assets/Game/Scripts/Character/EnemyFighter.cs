using UnityEngine;

public class EnemyFighter : Fighter
{
    [Header("Referências")]
    [SerializeField] private Transform player;
    [SerializeField] private PowerAttackHandler powerHandler;

    [Header("Movimento")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float attackDistance = 2f;          
    [SerializeField] private float gravity = -9.81f;

    [Header("Power attack")]
    [SerializeField] private float powerAttackRange = 6f;

    /* ----------------- IA – probabilidades & timers ----------------- */
    private float powerProb   = 0.30f;   // pp 30 %
    private const float powerInc = 0.10f;
    private float powerTimer  = 20f;

    private float bareProb    = 0.60f;   // bp 60 %
    private const float bareInc  = 0.05f;
    private float bareTimer   = 20f;

    /* ----------------- cooldowns ----------------- */
    private float strikeCooldown = 5f;   
    private float strikeCooldownTimer;
    private int   strikeToggle;          
    private float decisionTimer;        

    private float velocityY;

    protected override void ReadInput() { }   

    /* ----------------- Movimento ----------------- */
    protected override void HandleMovement()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0f;
        float dist = dir.magnitude;

        if (dir != Vector3.zero)
            transform.forward = dir.normalized;

        Vector3 move = dist > attackDistance + 0.5f ? dir.normalized * moveSpeed : Vector3.zero;

        velocityY = controller.isGrounded ? -2f : velocityY + gravity * Time.deltaTime;
        move.y = velocityY;

        Vector3 prev = transform.position;
        controller.Move(move * Time.deltaTime);

        animator.SetBool("IsWalking",
            (transform.position - prev).magnitude > 0.01f);
    }

    /* ----------------- Ataques ----------------- */
    protected override void HandleAttacks()
    {
        powerTimer -= Time.deltaTime;
        if (powerTimer <= 0f)
        {
            powerProb = Mathf.Clamp01(powerProb + powerInc);
            powerTimer = 20f;
        }

        bareTimer -= Time.deltaTime;
        if (bareTimer <= 0f)
        {
            bareProb = Mathf.Clamp01(bareProb + bareInc);
            bareTimer = 20f;
        }

        float dist = Vector3.Distance(transform.position, player.position);

        /* ---------- Power Attack ---------- */
        if (dist <= powerAttackRange &&
            powerHandler != null &&
            powerHandler.CanUse &&
            Random.value < powerProb)
        {
            powerHandler.ExecutePower();
            return;                             
        }

        /* ---------- Strikes especiais (4‑5) ---------- */
        if (strikeCooldownTimer > 0f)
            strikeCooldownTimer -= Time.deltaTime;

        if (dist <= attackDistance + 1f && strikeCooldownTimer <= 0f)
        {
            attackHandler.ExecuteStrike(strikeToggle + 4);   
            strikeToggle = 1 - strikeToggle;
            strikeCooldownTimer = strikeCooldown;
            return;
        }

        /* ---------- Strikes normais (0‑3) ---------- */
        if (dist <= attackDistance && !animator.GetBool("IsWalking"))
        {
            decisionTimer -= Time.deltaTime;

            if (decisionTimer <= 0f)
            {
                decisionTimer = Random.Range(1f, 2f);

                if (Random.value < bareProb)                
                {
                    int idx = Random.Range(0, 4);           
                    attackHandler.ExecuteStrike(idx);
                }
            }
        }
    }
}
