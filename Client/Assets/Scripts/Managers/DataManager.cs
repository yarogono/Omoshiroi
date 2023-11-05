using Data;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager : CustomSingleton<DataManager>
{
    public Dictionary<int, WeaponItem> WeaponItemDict { get; private set; } = new Dictionary<int, WeaponItem>();

    private void Awake()
    {
        WeaponItemDict = LoadJson<Data.WeaponItemData, int, WeaponItem>("WeaponItem").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"Prefabs/Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
