using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]

public class ThirdPersonCharacterLook : MonoBehaviour
{
    [SerializeField] float m_MoveSpeedMultiplier = 1f;
    [SerializeField] float m_AnimSpeedMultiplier = 1f;
    [SerializeField] float m_TurnSpeed = 0.2f;

    Rigidbody m_Rigidbody;
    Animator m_Animator;
    float m_Sideways;
    float m_Forwards;
    float m_Look;
    bool m_Dead;
    public delegate void AttackCallbackDelegate();
    AttackCallbackDelegate m_callback;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Look = 0;
        m_callback = null;
        m_Dead = false;
    }

    public void Die()
    {
        m_Animator.speed = 1;
        m_Animator.SetBool("Dead", true);
        m_Rigidbody.isKinematic = true;
        m_Dead = true;
    }

    public void TakeDamage()
    {
        m_Animator.speed = 1;
        m_Animator.SetTrigger("Hit");
    }

    public void Attack(AttackCallbackDelegate callback)
    {
        m_Animator.SetTrigger("Attack");
        m_callback = callback;
    }

    public void AttackHit()
    {
        if (m_callback != null)
        {
            m_callback();
            m_callback = null;
        }
    }

    public void Move(Vector2 move, float lookAngle)
    {
        if (!m_Dead)
        {
            float moveAngle = Mathf.Atan2(move.x, move.y);
            m_Look = Mathf.Deg2Rad * Mathf.LerpAngle(Mathf.Rad2Deg * m_Look, Mathf.Rad2Deg * lookAngle, m_TurnSpeed);

            m_Forwards = Mathf.Clamp(Mathf.Cos(m_Look - moveAngle) * move.magnitude, -1, 1);
            m_Sideways = Mathf.Clamp(Mathf.Sin(moveAngle - m_Look) * move.magnitude, -1, 1);

            transform.rotation = Quaternion.Euler(0, Mathf.Rad2Deg * m_Look, 0);
            m_Rigidbody.AddForce(m_MoveSpeedMultiplier * new Vector3(move.x, 0, move.y));

            // send input and other state parameters to the animator
            UpdateAnimator(move);
        }
    }

    void UpdateAnimator(Vector3 move)
    {
        // update the animator parameters
        m_Animator.SetFloat("Sideways", m_Sideways, 0.1f, Time.deltaTime);
        m_Animator.SetFloat("Forwards", m_Forwards, 0.1f, Time.deltaTime);

        // the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
        // which DOES NOT affect the movement speed, since root motion is disabled.
        if(m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            m_Animator.speed = PlayerManager.playerManager.attackSpeed / 100f;
        }
        else if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            m_Animator.speed = 1;
        }
        else if (move.magnitude > 0)
        {
            m_Animator.speed = m_AnimSpeedMultiplier * m_Rigidbody.velocity.magnitude;
        }
        else
        {
            m_Animator.speed = 1;
        }
    }
}