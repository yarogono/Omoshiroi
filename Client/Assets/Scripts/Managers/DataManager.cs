using Data;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    /// <summary>
    /// json 파일의 데이터를 필요할 때마다 빠르게 접근할 수 있도록 Dictionary 형태로 가공한다.
    /// </summary>
    /// <returns></returns>
    Dictionary<Key, Value> MakeDict();
}

/// <summary>
/// 아이템 ID 명명 규칙 : 첫 숫자로 아이템의 종류를 나타낸다. 이는 eItemType 를 참고할 것. 
/// </summary>
public class DataManager : CustomSingleton<DataManager>
{
    public Dictionary<int, WeaponItem> WeaponItemDict { get; private set; } = new Dictionary<int, WeaponItem>();
    public Dictionary<int, FBInventory> FBInventoryDict { get; private set; } = new Dictionary<int, FBInventory>();

    //아래의 Dictionary 들은 검색용으로 만든 임시 자료형이다.
    public Dictionary<int, WeaponItem> MagicItemDict { get; private set; } = new Dictionary<int, WeaponItem>();
    public Dictionary<int, WeaponItem> RuneItemDict { get; private set; } = new Dictionary<int, WeaponItem>();
    public Dictionary<int, WeaponItem> ResourceItemDict { get; private set; } = new Dictionary<int, WeaponItem>();
    public Dictionary<int, WeaponItem> ConsumableItemDict { get; private set; } = new Dictionary<int, WeaponItem>();
    public Dictionary<int, WeaponItem> SkinItemDict { get; private set; } = new Dictionary<int, WeaponItem>();


    private void Awake()
    {
        WeaponItemDict = LoadJson<Data.WeaponItemData, int, WeaponItem>("WeaponItem").MakeDict();
        FBInventoryDict = LoadJson<Data.FarmingBoxInventoryData, int, FBInventory>("FarmingBoxInventory").MakeDict();
    }

    /// <summary>
    /// json 파일의 내용을 읽어온다.
    /// </summary>
    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"Prefabs/Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
