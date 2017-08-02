using UnityEngine;
using System.Collections;

public class EquipmentManager : MonoBehaviour
{
    public Transform head;
    private GameObject currentHead;
    public Transform lBracer;
    private GameObject currentLBracer;
    public Transform rBracer;
    private GameObject currentRBracer;
    public Transform lShoulder;
    private GameObject currentLShoulder;
    public Transform rShoulder;
    private GameObject currentRShoulder;
    public Transform lWep;
    private GameObject currentLWep;
    public Transform rWep;
    private GameObject currentRWep;
    public GameObject body;

    public void Start()
    {
        ItemManager.manager.playerDoll.slotChanged.AddListener(UpdateModels);
    }

    public void UpdateModels(int slotsChanged)
    {
        Destroy(currentHead);
        Destroy(currentLBracer);
        Destroy(currentRBracer);
        Destroy(currentLWep);
        Destroy(currentRWep);
        Destroy(currentLShoulder);
        Destroy(currentRShoulder);
        for (int i = 0; i < ItemManager.manager.playerDoll.ItemCount(); i++)
        {
            Item temp = ItemManager.manager.playerDoll.GetItemInSlot(i);
            if (temp.name != "")
            {
                for (int j = 0; j < Database.EquipPointLookup[(int)temp.equipSlot].Count; j++)
                {
                    switch (Database.EquipPointLookup[(int)temp.equipSlot][j])
                    {
                        case EquipPoint.Head:
                            if (currentHead)
                            {
                                Destroy(currentHead);
                            }

                            currentHead = (GameObject)Instantiate(temp.modelPrefabs[j], Vector3.zero, Quaternion.identity);
                            currentHead.transform.SetParent(head, false);
                            break;
                        case EquipPoint.LBracer:
                            if (currentLBracer)
                            {
                                Destroy(currentLBracer);
                            }

                            currentLBracer = (GameObject)Instantiate(temp.modelPrefabs[j], Vector3.zero, Quaternion.identity);
                            currentLBracer.transform.SetParent(lBracer, false);
                            break;
                        case EquipPoint.RBracer:
                            if (currentRBracer)
                            {
                                Destroy(currentRBracer);
                            }

                            currentRBracer = (GameObject)Instantiate(temp.modelPrefabs[j], Vector3.zero, Quaternion.identity);
                            currentRBracer.transform.SetParent(rBracer, false);
                            break;
                        case EquipPoint.LShoulder:
                            if (currentLShoulder)
                            {
                                Destroy(currentLShoulder);
                            }

                            currentLShoulder = (GameObject)Instantiate(temp.modelPrefabs[j], Vector3.zero, Quaternion.identity);
                            currentLShoulder.transform.SetParent(lShoulder, false);
                            break;
                        case EquipPoint.RShoulder:
                            if (currentRShoulder)
                            {
                                Destroy(currentRShoulder);
                            }

                            currentRShoulder = (GameObject)Instantiate(temp.modelPrefabs[j], Vector3.zero, Quaternion.identity);
                            currentRShoulder.transform.SetParent(rShoulder, false);
                            break;
                        case EquipPoint.LWep:
                            if (currentLWep)
                            {
                                Destroy(currentLWep);
                            }

                            currentLWep = (GameObject)Instantiate(temp.modelPrefabs[j], Vector3.zero, Quaternion.identity);
                            currentLWep.transform.SetParent(lWep, false);
                            break;
                        case EquipPoint.RWep:
                            if (currentRWep)
                            {
                                Destroy(currentRWep);
                            }

                            currentRWep = (GameObject)Instantiate(temp.modelPrefabs[j], Vector3.zero, Quaternion.identity);
                            currentRWep.transform.SetParent(rWep, false);
                            break;
                        case EquipPoint.Body:
                            body.GetComponent<SkinnedMeshRenderer>().sharedMesh = temp.modelPrefabs[j].GetComponent<SkinnedMeshRenderer>().sharedMesh;
                            break;
                        default:
                            Debug.Log("Unknown attachment point");
                            break;
                    }
                }
            }
        }
    }
    void OnDestroy()
    {
        ItemManager.manager.playerDoll.slotChanged.RemoveListener(UpdateModels);
    }
}