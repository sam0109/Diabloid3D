using UnityEngine;
using System.Collections.Generic;

public enum WindowType {PaperDoll_Inventory, Character, Loot, Shop};

public class WindowManager : MonoBehaviour 
{
    public static WindowManager windowManager;
    public GameObject closeButton;
    private Window[] openWindows = {null, null};

    void Awake()
    {
        windowManager = this;
    }

    void windowStatusChange(){
        bool windowOpen = false;
        foreach (Window window in openWindows){
            if(window != null){
                windowOpen = true;
            }
        }
        if(windowOpen)
        {
            closeButton.SetActive(true);
        }
        else
        {
            closeButton.SetActive(false);
        }
    }

    public void exitAllWindows(){
        for(int i = 0; i < openWindows.Length; i++){
            if(openWindows[i] != null){
                openWindows[i].close();
                openWindows[i] = null;
            }
        }
        windowStatusChange();
    }

    public void toggleWindow(WindowType type, WindowDataSource dataSource = null) //TODO: very ugly, must fix
    {
        switch (type)
        {
            case (WindowType.Loot):
                if(dataSource == null){
                    Debug.LogWarning("Loot windows need a data source to instantiate");
                    return;
                }
                if(openWindows[0] != null){
                    WindowType winType = openWindows[0].myType;
                    openWindows[0].close();
                    openWindows[0] = null;
                    if(winType != WindowType.Loot){
                        MakeWindow(Prefabs.prefabs.loot, 0, dataSource, type);
                    }
                }
                else
                {
                    MakeWindow(Prefabs.prefabs.loot, 0, dataSource, type);
                }
                break;
            case (WindowType.PaperDoll_Inventory):
                if(openWindows[1] != null){
                    WindowType winType = openWindows[1].myType;
                    openWindows[1].close();
                    openWindows[1] = null;
                    if(winType != WindowType.PaperDoll_Inventory){
                        MakeWindow(Prefabs.prefabs.inventory, 1, null, type);
                    }
                }
                else
                {
                    MakeWindow(Prefabs.prefabs.inventory, 1, null, type);
                }
                break;
            case (WindowType.Shop):
                if(dataSource == null){
                    Debug.LogWarning("Shop windows need a data source to instantiate");
                    return;
                }
                if(openWindows[0] != null){
                    WindowType winType = openWindows[0].myType;
                    openWindows[0].close();
                    openWindows[0] = null;
                    if(winType != WindowType.Shop){
                        MakeWindow(Prefabs.prefabs.shop, 0, dataSource, type);
                    }
                }
                else
                {
                    MakeWindow(Prefabs.prefabs.shop, 0, dataSource, type);
                }
                break;
            case (WindowType.Character):
                if(openWindows[1] != null){
                    WindowType winType = openWindows[1].myType;
                    openWindows[1].close();
                    openWindows[1] = null;
                    if(winType != WindowType.Character){
                        MakeWindow(Prefabs.prefabs.character, 1, null, type);
                    }
                }
                else
                {
                    MakeWindow(Prefabs.prefabs.character, 1, null, type);
                }
                break;
            default:
                Debug.Log("Unknown WindowType");
                break;
        }
        windowStatusChange();
    }

    public void toggleWindowWithDataSource(WindowDataSource dataSource)   
    {
        toggleWindow(Utilities.dataSourceToWindowType(dataSource.myType), dataSource);
    }

    public void toggleWindow(int windowType)   
    {
        toggleWindow((WindowType)windowType);
    }

    private void MakeWindow(GameObject windowPrefab, int side, WindowDataSource dataSource, WindowType myType)  //0 for left, 1 for right
    {
        GameObject UI = GameObject.FindGameObjectWithTag("UI");
        GameObject instantiatedWindow = Instantiate(windowPrefab);
        openWindows[side] = instantiatedWindow.GetComponent<Window>();
        openWindows[side].myType = myType;
        instantiatedWindow.transform.SetParent(UI.transform, false);
        WindowComponent[] components = instantiatedWindow.GetComponentsInChildren<WindowComponent>();
        foreach(WindowComponent component in components)
        {
            if (component.myDataSource == null)
            {
                component.myDataSource = dataSource;
            }
            component.InitializeComponent();
        }
    }
}
