using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Inventory;
using ServerCore;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using Google.Protobuf.Collections;

/// <summary>
/// 테스트가 용이하도록 임의대로 아이템 스폰을 자체적인 SO 로 관리하도록 해 두었음.
/// </summary>
public class FarmingBox : BattleFieldObject, ILootable, IInteractable
{
    [SerializeField] private int inventorySize;
    [SerializeField] private InventorySO inventorySO;
    [SerializeField] private InventoryController_FB inventoryController;
    private Collider farminBoxCollider;

    public Dictionary<int, InventoryItem> ItemList { get; private set; }
    public int InventorySize { get { return inventorySize; } private set { inventorySize = value; } }

    public Action OnOpened;
    public Action OnClosed;

    // Start is called before the first frame update
    private void Start()
    {
        ItemList = new Dictionary<int, InventoryItem>();

        Dictionary<int, InventoryItem> items = inventorySO.GetCurrentInventoryState();

        foreach (KeyValuePair<int, InventoryItem> item in items)
        {
            ItemList.Add(item.Key, item.Value);
        }

        inventoryController = GetComponent<InventoryController_FB>();
        farminBoxCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update() { }

    /// <summary>
    /// 이 보관함의 인벤토리에서 아이템을 가져갈 때 마다 호출 될 예정.
    /// </summary>
    public void Loot() { }

    public void Interact() { }

    public void SetItemList(Dictionary<int, InventoryItem> items)
    {
        ItemList = items;
    }
    private void OnMouseDown()
    {
        if (/*Vector3.Distance(this.transform.position, 플레이어.transform.position) < 20*/ true)
        {
            SendFarmingBoxOpen();
        }
    }

    /// <summary>
    /// 서버에서 받아온 [farmingBoxID, isOpen, farmingBoxInventory] 데이터를 이용해 작업을 수행한다.
    /// 해당하는 파밍박스를 식별하고, isOpen 이 true 라면 파밍박스를 열 수 없음을 알리고 종료, false 라면 파밍박스 인벤토리를 데이터대로 갱신해주면 된다. 
    /// </summary>
    public void OpenBox(S_FarmingBoxOpen FBData)
    {
        if (FBData.IsOpen)
        {
            //상자를 열 수 없는 상태라는 것을 알려주는 연출에 대한 내용.
            Debug.Log($"{FBData.FarmingBoxId} 를 누군가가 뒤져 보는 중이므로 열 수 없습니다.");
            return;
        }

        SetFBItems(FBData);
        inventoryController.OpenInventoryUI();
    }

    private void SetFBItems(S_FarmingBoxOpen FBData)
    {
        Debug.Log("파밍박스 인벤토리 데이터 받아옴");
        RepeatedField<FarmingBoxItem> items = FBData.Items;
        InventoryItem inventoryItem;

        for (int i = 0; i < items.Count; i++)
        {
            //ItemID 로 BaseItem 을 구하고, 파밍박스 인벤토리에 추가한다.

        }
        //BaseItem item;
        //DataManager dataManager = DataManager.Instance;
        //Data.ItemData itemData = dataManager.;

        //for()

        //switch (itemData.id)
        //{
        //    case eItemType.Good:
        //        {
        //            break;
        //        }
        //    case eItemType.Good:
        //        {
        //            break;
        //        }
        //    case eItemType.Good:
        //        {
        //            break;
        //        }
        //    case eItemType.Good:
        //        {
        //            break;
        //        }
        //    case eItemType.Good:
        //        {
        //            break;
        //        }
        //}

        //if (dataManager.WeaponItemDict.ContainsKey(itemId)) { itemData = dataManager.WeaponItemDict[itemId]; }


        //return item;
    }

    /// <summary>
    /// 현재 FarmingBox 의 object ID 를 함께 보내게 될 텐데, 이걸 어떻게 알 수 있는가?
    /// </summary>
    /// <param name="items"></param>
    private void SendFarmingBoxOpen()
    {
        C_FarmingBoxOpen fbOpen = new C_FarmingBoxOpen();
        fbOpen.FarmingBoxId = 0;
    }

    public void SendFarmingBoxClose()
    {
        C_FarmingBoxClose fbClose = new C_FarmingBoxClose();
        fbClose.FarmingBoxId = this.gameObject.GetInstanceID(); fbClose.PlayerId = 0; //C_FarmingBoxClose.Items 는 get; 으로 설정되어 있어 set 이 불가능한 상태이다.
    }
}

public partial class PacketHandler
{
    public static void S_FarmingBoxOpenHandler(PacketSession session, IMessage packet)
    {
        S_FarmingBoxOpen FBPacket = packet as S_FarmingBoxOpen;

        GameObject gameObject = ObjectManager.Instance.FindById(FBPacket.FarmingBoxId);

        if(gameObject == null)
        {
            return;
        }

        FarmingBox farmingBox = gameObject.GetComponent<FarmingBox>();

        farmingBox.OpenBox(FBPacket);
    }

    public static void S_FarmingBoxSpawnHandler(PacketSession session, IMessage packet)
    {
        S_FarmingBoxSpawn FBPacket = packet as S_FarmingBoxSpawn;

        RepeatedField<ObjectInfo> FBSpawnInfos = FBPacket.BoxInfos;

        GameObject farmingBox;

        foreach (ObjectInfo fb in FBSpawnInfos)
        {
            //원래는 Instantiate 할 것이 아니라, ObjectPool 에서 관리하면서 SetActive 설정하는 식이 되어야 할 것임.
            //현재는 테스트용으로 별개의 Spawner 를 두고서 Instantiate 하도록 만듦.
            //ObejctID 를 별도로 설정할 수 있는지 모르겠음. 클라이언트 측 객체에도 ObjectID 에 대응되는 멤버변수가 필요하다 생각됨.

            //farmingBox = ObjectManager.Instance.FindById(fb.ObjectId);

            //if (farmingBox == null) { return; }
            //if (farmingBox.GetComponent<FarmingBox>() == null) { return; }

            //farmingBox.transform.position = new Vector3(fb.PosInfo.PosX, fb.PosInfo.PosY, fb.PosInfo.PosZ);

            //farmingBox.SetActive(true);

            FarmingBoxSpawner.instance.SpawnFarmingBox(new Vector3(fb.PosInfo.PosX, fb.PosInfo.PosY, fb.PosInfo.PosZ));
        }
    }
}
