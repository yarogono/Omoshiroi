using Data;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    /// <summary>
    /// 서버에서 받아온 데이터를 필요할 때마다 빠르게 접근할 수 있도록 Dictionary 형태로 가공한다.
    /// </summary>
    /// <returns></returns>
    Dictionary<Key, Value> MakeDict();
}

public class DataManager : CustomSingleton<DataManager>
{
    public Dictionary<int, WeaponItem> WeaponItemDict { get; private set; } = new Dictionary<int, WeaponItem>();
    public Dictionary<int, FBInventory> FBInventoryDict { get; private set; } = new Dictionary<int, FBInventory>();

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
