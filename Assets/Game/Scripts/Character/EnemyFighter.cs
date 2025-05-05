using UnityEngine;

public class EnemyFighter : Fighter
{
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float attackDistance = 2f;
    [SerializeField] private float decisionCooldown = 2f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float powerAttackRange = 6f;
    [SerializeField] private PowerAttackHandler powerHandler;

    private float velocityY = 0f;
    private float decisionTimer = 0f;
    private int strikeToggle = 0;
    private float strikeCooldown = 5f;
    private float strikeCooldownTimer = 0f;

    protected override void ReadInput()
    {
        // Não lê input real — controlado por IA
    }

    protected override void HandleMovement()
    {
        Vector3 dirToPlayer = player.position - transform.position;
        dirToPlayer.y = 0f;
        float distance = dirToPlayer.magnitude;

        if (dirToPlayer != Vector3.zero)
            transform.forward = dirToPlayer.normalized;

        Vector3 move = Vector3.zero;

        if (distance > attackDistance + 0.5f)
        {
            move = dirToPlayer.normalized * moveSpeed;
        }

        if (controller.isGrounded)
            velocityY = -2f;
        else
            velocityY += gravity * Time.deltaTime;

        move.y = velocityY;

        Vector3 previousPosition = transform.position;
        controller.Move(move * Time.deltaTime);
        Vector3 actualMovement = transform.position - previousPosition;
        actualMovement.y = 0f;

        animator.SetBool("IsWalking", actualMovement.magnitude > 0.01f);
    }

    protected override void HandleAttacks()
    {
        Vector3 dirToPlayer = player.position - transform.position;
        dirToPlayer.y = 0f;
        float distance = dirToPlayer.magnitude;

        if (strikeCooldownTimer > 0f)
            strikeCooldownTimer -= Time.deltaTime;

        // Power Attack (raio vermelho)
        if (distance <= powerAttackRange && powerHandler != null && powerHandler.CanUse)
        {
            powerHandler.ExecutePower();
            return; // Não ataca com outros strikes nesse frame
        }

        // Strike alternado (ButterflyTwirl e HurricaneKick = slots 4 e 5)
        if (distance <= attackDistance + 1f && strikeCooldownTimer <= 0f)
        {
            attackHandler.ExecuteStrike(strikeToggle + 4); // 4 ou 5
            strikeToggle = 1 - strikeToggle;
            strikeCooldownTimer = strikeCooldown;
            return;
        }

        // Ataques normais (socos/pontapés)
        if (distance <= attackDistance && !animator.GetBool("IsWalking"))
        {
            decisionTimer -= Time.deltaTime;

            if (decisionTimer <= 0f)
            {
                decisionTimer = Random.Range(1f, decisionCooldown);
                int strikeIndex = Random.Range(0, 4);
                attackHandler.ExecuteStrike(strikeIndex);
            }
        }
    }
}
