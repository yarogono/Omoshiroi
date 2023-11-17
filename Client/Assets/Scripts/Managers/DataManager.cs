using Data;
using System.Collections.Generic;
using UnityEngine;

public enum DataType
{

}

public interface ILoader<Key, Value>
{
	Dictionary<Key, Value> MakeDictionary();
}

public class DataManager : CustomSingleton<DataManager>
{
	public Dictionary<int, Stat> Stats { get; private set; } = new();
	public Dictionary<int, Rune> Runes { get; private set; } = new();
	public Dictionary<int, Weapon> Weapons { get; private set; } = new();
	public Dictionary<int, FarmingBox> FarmingBoxes { get; private set; } = new();

	private void Awake()
	{
		Stats = LoadJson<StatData, int, Stat>("Stat").MakeDictionary();
		Debug.Log($"{Stats[1].MaxHp}");
		// FarmingBoxes = LoadJson<Data.FarmingBoxData, int, FarmingBox>("FarmingBoxInventory").MakeDictionary();
	}

	Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
	{
		TextAsset textAsset = Resources.Load<TextAsset>($"Data/{path}");

		return JsonUtility.FromJson<Loader>(textAsset.text);
	}

	public BaseItem FindItem(int itemId)
	{
		BaseItem item;

		item = Weapons[itemId];
		if (item != null)
		{
			return item;
		}
		item = Runes[itemId];
		if (item != null)
		{
			return item;
		}

		Debug.Log($"ItemID ({itemId}) 는 존재하지 않는 아이템입니다.");
		return null;
	}
}
