using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]

public class ThirdPersonCharacterLook : MonoBehaviour
{
    [SerializeField] float m_JumpPower = 8f;
    [Range(1f, 4f)] [SerializeField] float m_GravityMultiplier = 2f;
    [SerializeField] float m_MoveSpeedMultiplier = 1f;
    [SerializeField] float m_AnimSpeedMultiplier = 1f;
    [SerializeField] float m_GroundCheckDistance = 0.2f;
    [SerializeField] float m_TurnSpeed = 0.2f;

    Rigidbody m_Rigidbody;
    Animator m_Animator;
    bool m_IsGrounded;
    float m_OrigGroundCheckDistance;
    float m_Sideways;
    float m_Forwards;
    float m_Look;
    public delegate void AttackCallbackDelegate();
    AttackCallbackDelegate m_callback;


    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();

        m_OrigGroundCheckDistance = m_GroundCheckDistance;
        m_Look = 0;

        m_callback = null;
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

    public void Move(Vector2 move, float lookAngle, bool jump)
    {
        float moveAngle = Mathf.Atan2(move.x, move.y);
        m_Look = Mathf.Deg2Rad * Mathf.LerpAngle(Mathf.Rad2Deg * m_Look, Mathf.Rad2Deg * lookAngle, m_TurnSpeed);

        m_Forwards = Mathf.Clamp(Mathf.Cos(m_Look - moveAngle) * move.magnitude, -1, 1);
        m_Sideways = Mathf.Clamp(Mathf.Sin(moveAngle - m_Look) * move.magnitude, -1, 1);

        transform.rotation = Quaternion.Euler(0, Mathf.Rad2Deg * m_Look, 0);
        m_Rigidbody.AddForce(m_MoveSpeedMultiplier * new Vector3(move.x, 0, move.y));

        CheckGroundStatus();
        // control and velocity handling is different when grounded and airborne:
        if (m_IsGrounded)
        {
            HandleGroundedMovement(jump);
        }
        else
        {
            HandleAirborneMovement();
        }

        // send input and other state parameters to the animator
        UpdateAnimator(move);
    }

    void UpdateAnimator(Vector3 move)
    {
        // update the animator parameters
        m_Animator.SetFloat("Sideways", m_Sideways, 0.1f, Time.deltaTime);
        m_Animator.SetFloat("Forwards", m_Forwards, 0.1f, Time.deltaTime);
        m_Animator.SetBool("OnGround", m_IsGrounded);
        if (!m_IsGrounded)
        {
            m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
        }

        // the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
        // which DOES NOT affect the movement speed, since root motion is disabled.
        if (m_IsGrounded && move.magnitude > 0)
        {
            m_Animator.speed = m_AnimSpeedMultiplier * m_Rigidbody.velocity.magnitude;
        }
        else
        {
            // don't use that while airborne
            m_Animator.speed = 1;
        }
    }

    void HandleAirborneMovement()
    {
        // apply extra gravity from multiplier:
        Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
        m_Rigidbody.AddForce(extraGravityForce);

        m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
    }

    void HandleGroundedMovement(bool jump)
    {
        // check whether conditions are right to allow a jump:
        if (jump && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
        {
            // jump!
            m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower, m_Rigidbody.velocity.z);
            m_IsGrounded = false;
            m_GroundCheckDistance = 0.1f;
        }
    }

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
#if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
        {
            m_IsGrounded = true;
        }
        else
        {
            m_IsGrounded = false;
        }
    }
}