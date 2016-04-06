using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(ThirdPersonCharacterLook))]
public class ThirdPersonUserControl : MonoBehaviour
{
    private ThirdPersonCharacterLook m_Character; // A reference to the ThirdPersonCharacter on the object
    private bool m_Jump;
    private float m_LookAngle;
    private float attackTimer;

    private void Start()
    {
        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<ThirdPersonCharacterLook>();
        m_LookAngle = 0;
    }

    private void Update()
    {
        if(attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        if (!m_Jump)
        {
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }
        if (Input.GetKeyDown(KeyCode.E) && attackTimer <= 0)
        {
            print(PlayerManager.playerManager.attackDelay);
            attackTimer = PlayerManager.playerManager.attackDelay;
            Attack();
        }
    }

    public void Attack()
    {
        m_Character.Attack(delegate { AttackCallback(); });   //calls the attack damage calculations at the proper point in the swing
    }

    public void AttackCallback()
    {
        RaycastHit ray;
        if (Physics.Raycast(transform.position + transform.up * .5f, transform.forward, out ray, PlayerManager.playerManager.attackDistance))
        {
            ray.collider.gameObject.SendMessage("TakeDamage", PlayerManager.playerManager.attackDamage, SendMessageOptions.DontRequireReceiver);
        }

        #if UNITY_EDITOR
        // helper to visualise the ray in the scene view
        Debug.DrawLine(transform.position + transform.up * .5f, transform.position + transform.forward * PlayerManager.playerManager.attackDistance + transform.up * .5f, Color.white, 1f);
        #endif
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // read inputs
        float hMove = CrossPlatformInputManager.GetAxis("Horizontal");
        float vMove = CrossPlatformInputManager.GetAxis("Vertical");
        float hLook = CrossPlatformInputManager.GetAxis("HorizontalLook");
        float vLook = CrossPlatformInputManager.GetAxis("VerticalLook");

        if (hLook != 0 || vLook != 0)
        {
            m_LookAngle = Mathf.Atan2(hLook, vLook);
        }

        // pass all parameters to the character control script
        m_Character.Move(new Vector2(hMove, vMove).normalized, m_LookAngle, m_Jump);
        m_Jump = false;
    }
}