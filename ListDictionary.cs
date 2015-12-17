using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListDictionary<TKey, TValue>
{
	private Dictionary<TKey, List<TValue>> dic;
	
	public ListDictionary()
	{
		dic = new Dictionary<TKey, List<TValue>>();
	}
	
	public TValue GetRandomObject(TKey key)
	{
		int index = UnityEngine.Random.Range(0, dic[key].Count);
		return dic[key][index];
	}
	
	public int GetKeyCount()
	{
		return dic.Count;
	}
	
	public int GetValueCount(TKey key)
	{
		if (!dic.ContainsKey(key))
			return 0;
		return dic[key].Count;
	}
	
	public List<TValue> this[TKey key] { get { return dic[key]; } }
	
	public void Add(TKey key, TValue value)
	{
		if (!dic.ContainsKey(key))
			dic.Add(key, new List<TValue>());
		
		dic[key].Add(value);
	}
	
	public bool ContainsKey(TKey key)
	{
		return dic.ContainsKey(key);
	}
	
	public bool ContainsValue(TKey key, TValue value)
	{
		if (!dic.ContainsKey(key))
			return false;
		
		return dic[key].Contains(value);
	}
	
	public bool ContainsValue(TValue value)
	{
		return !FindKey(value).Equals(default(TKey));
	}
	
	public void Clear()
	{
		Dictionary<TKey, List<TValue>>.KeyCollection keys = dic.Keys;
		foreach (TKey _key in keys)
		{
			dic[_key].Clear();
		}
		dic.Clear();
	}
	
	public TKey FindKey(TValue value)
	{
		Dictionary<TKey, List<TValue>>.KeyCollection keys = dic.Keys;
		foreach (TKey _key in keys)
		{
			foreach (TValue _value in dic[_key])
			{
				if (value.Equals(_value))
					return _key;
			}
		}
		
		return default(TKey);
	}
	
	public void Remove(TKey key)
	{
		dic[key].Clear();
		dic.Remove(key);
	}
	
	public void Remove(TKey key, TValue value)
	{
		dic[key].Remove(value);
	}
	
	public void RemoveAllValue(TValue value)
	{
		Dictionary<TKey, List<TValue>>.KeyCollection keys = dic.Keys;
		foreach (TKey _key in keys)
		{
			if (dic[_key].Contains(value))
				dic[_key].Remove(value);
		}
	}
}
