using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

public class Inventory : ItemHolder
{
    new void Start()
    {
        items[9] = Database.myDatabase.GetItem("Sword3");  //temporary item maker for testing
        items[10] = Database.myDatabase.GetItem("Sword9");  //temporary item maker for testing
        items[11] = Database.myDatabase.GetItem("Helmet1");  //temporary item maker for testing
        base.Start();
    }
}