#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.IO;

public class CustomMenus
{
	#if UNITY_EDITOR
	[MenuItem("CustomTools/Find all sprite that refered prefab")]
	public static void FindRefered Prefabs()
	{
		
		const string fixedPrefabPath = "/Prefabs/";
		const string prefabPostFix = "*.prefab";
		const string absoultePath = "FilePath"
			
		ListDictionary<string, string> spriteNames = new ListDictionary<string, string>();
		
		string[] prefabPathes = Directory.GetFiles(Application.dataPath + fixedPrefabPath, prefabPostFix, SearchOption.AllDirectories);
		
		
		foreach (string prefabPath in prefabPathes)
		{
			string prefabPath1 = prefabPath.Replace("\\", "/");
			prefabPath1 = prefabPath1.Replace(absoultePath, "");
			
			GameObject prefab = AssetDatabase.LoadAssetAtPath(prefabPath1, typeof(GameObject)) as GameObject;
			
			Transform[] allTransForms = prefab.GetComponentsInChildren<Transform>(true);
			
			foreach (Transform transform in allTransForms)
			{
				UISprite sprite = transform.GetComponent<UISprite>();
				if (sprite != null && !spriteNames.ContainsValue(prefabPath, sprite.spriteName))
				{
					spriteNames.Add(prefabPath, sprite.spriteName);
				}
			}
			
			string log = string.Empty;
			
			foreach (var prefabName in prefabPathes)
			{
				if (!spriteNames.ContainsKey(prefabName))
					continue;
				
				log += string.Format("PrefabName : {0}\n", prefabName);
				
				string prefabNameInPrefabPathes = string.Empty;
				
				foreach (string spriteName in spriteNames[prefabName])
					prefabNameInPrefabPathes += string.Format("{0}\n", spriteName);
				
				log += string.Format("{0}\n\n", prefabNameInPrefabPathes);
			}
			
			Debug.LogWarning(log);
		}
	}
	
	private static IEnumerable<T> GetAllComponents<T>()
		where T : Component
	{
		IEnumerable<Transform> roots = from root in GameObject.FindObjectsOfType<Transform>()
			where root.parent == null
				select root;
		
		return roots.SelectMany<Transform, T>((trans) => { return trans.GetComponentsInChildren<T>(includeInactive: true); });
	}
	#endif
}																																																																																																																																																																																																																																																							
