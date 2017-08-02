﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
#pragma warning disable 0649

public class PlayerManager : CharacterManager
{
    public static PlayerManager playerManager;

    float m_LookAngle;

    void Awake()
    {
        playerManager = this;
    }

    // Use this for initialization
    new void Start()
    {
        UpdateItemStats(0);
        base.Start();
        m_LookAngle = 0;
        ItemManager.manager.playerDoll.slotChanged.AddListener(UpdateItemStats);
    }

    public override void Update()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    private void Interact() //may want to remove
    {
        {
            RaycastHit ray;
            if (Physics.Raycast(transform.position + transform.up * .5f, transform.forward, out ray, 1))
            {
                ray.collider.gameObject.SendMessageUpwards("Interacted", SendMessageOptions.DontRequireReceiver);
                print("Interacting with " + ray.collider.gameObject.name);
            }
        }
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // read inputs
        float hMove = CrossPlatformInputManager.GetAxis("Horizontal");
        float vMove = CrossPlatformInputManager.GetAxis("Vertical");

        // pass all parameters to the character control script
        Move(new Vector2(hMove, vMove).normalized, Mathf.Atan2(hMove, vMove));
    }

    public void UpdateItemStats(int slot)
    {
        agilityMod = 0;
        intMod = 0;
        strengthMod = 0;
        armor = 0;
        attackSpeed = 150;
        weaponReach = 0;
        weaponDamageMod = 0;

        bool weaponEquipped = false;
        for (int i = 0; i < ItemManager.manager.playerDoll.ItemCount(); i++)
        {
            Item temp = ItemManager.manager.playerDoll.GetItemInSlot(i);
            switch (temp.itemType)
            {
                case (ItemType.Bow):
                case (ItemType.Axe):
                case (ItemType.Hammer):
                case (ItemType.Sword):
                    if (weaponEquipped)
                    {
                        weaponReach = Mathf.Min(weaponReach, temp.range);
                        attackSpeed = attackSpeed + temp.speed;
                        weaponDamageMod = (temp.damage + weaponDamageMod) / 4; //divide by 2 to average, divide by 2 more to account for extra speed.
                    }
                    else
                    {
                        weaponReach = temp.range;
                        attackSpeed = temp.speed;
                        weaponDamageMod = temp.damage;
                        weaponEquipped = true;
                    }
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

    void OnDestroy()
    {
        ItemManager.manager.playerDoll.slotChanged.RemoveListener(UpdateItemStats);
    }
}