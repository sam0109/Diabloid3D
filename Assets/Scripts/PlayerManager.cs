using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
#pragma warning disable 0649

public class PlayerManager : CharacterManager
{
    public static PlayerManager playerManager;

    float m_LookAngle;
    AbilityManager abilityManager;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        playerManager = this;
        m_LookAngle = 0;
        abilityManager = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<AbilityManager>(includeInactive: true);

        Inventory.inventory.inventoryChanged += UpdateItemStats;
    }

    public override void Update()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.E) && attackTimer <= 0)
        {
            attackTimer = attackDelay;
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }
    
    private void Interact()
    {
        RaycastHit ray;
        if (Physics.Raycast(transform.position + transform.up * .5f, transform.forward, out ray, 1))
        {
            ray.collider.gameObject.SendMessage("Interacted", SendMessageOptions.DontRequireReceiver);
            print("Interacting with " + ray.collider.gameObject.name);
        }
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
        Move(new Vector2(hMove, vMove).normalized, m_LookAngle);
    }

    public void UpdateItemStats(bool paperDollChanged)
    {
        if (paperDollChanged)
        {

            agilityMod = 0;
            intMod = 0;
            strengthMod = 0;
            armor = 0;
            attackSpeed = 150;
            attackDistance = 1;
            weaponDamage = 0;

            bool weaponEquipped = false;
            for (int i = 0; i < Inventory.inventory.paperDoll.Length; i++)
            {
                Item temp = Inventory.inventory.GetItemInSlot(i);
                switch (temp.itemType)
                {
                    case (ItemType.Bow):
                    case (ItemType.Axe):
                    case (ItemType.Hammer):
                    case (ItemType.Sword):
                        if (weaponEquipped)
                        {
                            attackDistance = Mathf.Min(attackDistance, temp.range);
                            attackSpeed = attackSpeed + temp.speed;
                            weaponDamage = (temp.damage + weaponDamage) / 4; //divide by 2 to average, divide by 2 more to account for extra speed.
                        }
                        else
                        {
                            attackDistance = temp.range;
                            attackSpeed = temp.speed;
                            weaponDamage = temp.damage;
                        }
                        weaponEquipped = true;
                        break;
                    case (ItemType.Body):
                    case (ItemType.Feet):
                    case (ItemType.Hands):
                    case (ItemType.Helmet):
                    case (ItemType.Ring):
                    case (ItemType.Shield):
                        armor += temp.armor;
                        agilityMod += temp.agility;
                        intMod += temp.intelligence;
                        strengthMod += temp.strength;
                        break;
                    case (ItemType.None):
                        if (temp.name != "")
                        {
                            Debug.Log("No item type! Cannot set equipped stats.");
                        }
                        break;
                    default:
                        Debug.Log("Unrecognized item type equipped");
                        break;
                }
            }
            RecalculateStats(false);
        }
    }
}