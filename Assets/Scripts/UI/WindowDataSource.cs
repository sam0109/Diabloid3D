using System.Collections.Generic;
using UnityEngine;

public enum WindowDataSourceType {None, PaperDoll, Inventory, Character, Loot, Shop};

public abstract class WindowDataSource : MonoBehaviour {
    public WindowDataSourceType myType;
}