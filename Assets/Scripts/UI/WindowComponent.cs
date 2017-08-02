using UnityEngine;

public abstract class WindowComponent : MonoBehaviour {
    public WindowDataSourceType myType;
    public WindowDataSource myDataSource;
    public abstract void InitializeComponent();
    
    public virtual void Awake(){
        switch(myType){
            case(WindowDataSourceType.Loot):
            case(WindowDataSourceType.Shop):
                myDataSource = null;
                break;
            case(WindowDataSourceType.PaperDoll):
                myDataSource = ItemManager.manager.playerDoll;
                break;
            case(WindowDataSourceType.Inventory):
                myDataSource = ItemManager.manager.playerInventory;
                break;
            case(WindowDataSourceType.Character):
                Debug.LogError("Character stats not yet implemented");
                myDataSource = null;
                break;
            case(WindowDataSourceType.None):
                Debug.LogError("None WindowDataSourceType, please initialize before calling parent awake function");
                break;
            default:
                Debug.LogError("Unrecognized WindowDataSourceType");
                break;
        }
    }
}
