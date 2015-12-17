using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.IO;

public class FindSprite
{
	[MenuItem("CustomTools/Find sprite names In All Scenes")]
	public static void FindSpriteNamesInAllScene()
	{
		ListDictionary<string, string> spriteNames = new ListDictionary<string, string>();
		
		string[] scenes = (from scene in EditorBuildSettings.scenes where scene.enabled select scene.path).ToArray();
		foreach (string scene in scenes)
		{
			EditorApplication.OpenScene(scene);
			
			Transform[] allTrnasforms = GameObject.FindObjectsOfType<Transform>();
			foreach (Transform transform in allTrnasforms)
			{
				UISprite sprite = transform.GetComponent<UISprite>();
				if (sprite != null && !spriteNames.ContainsValue(scene, sprite.spriteName))
					spriteNames.Add(scene, sprite.spriteName);
			}
		}
		
		string log = string.Empty;
		foreach (string scene in scenes)
		{
			if (!spriteNames.ContainsKey(scene))
				continue;
			
			log += string.Format("Scene : {0}\n", scene);
			
			string spriteNameInScene = string.Empty;
			foreach (string spriteName in spriteNames[scene])
				spriteNameInScene += string.Format("{0}\n", spriteName);
			
			log += string.Format("{0}\n\n", spriteNameInScene);
		}
		
		Debug.LogWarning(log);
	}
}																																																																																																																																																																																																																																																							
