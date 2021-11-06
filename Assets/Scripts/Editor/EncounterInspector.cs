using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Encounter))]
public class EncounterInspector : Editor
{
	Encounter enc;
	public void OnEnable()
	{
		enc = serializedObject.targetObject as Encounter;
		Debug.Log("Loaded: " + serializedObject.targetObject.name);

		var scene = EncounterEditableScene.Instance;
		EncounterView view = scene?.view;
		if(enc == null || view == null) return;

		view.Apply(enc);
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		
		serializedObject.Update();

		var scene = EncounterEditableScene.Instance;
		EncounterView view = scene?.view;
		if(enc == null || view == null) return;

		GUILayout.BeginHorizontal();

		if(GUILayout.Button("Create missing predicate icons"))
		{
			for(int i = 0; i < enc.blames.Length; i++)
			{
				if(enc.blames[i].predicateIconPrefab != null) continue;
				string templatePath = AssetDatabase.GetAssetPath(scene.predicatePrefabTemplate);
				string prefabName = enc.name + "_" + (i + 1) + ".prefab";
				string fullPath = scene.predicatePrefabFolder + "/" + prefabName;
				fullPath = AssetDatabase.GenerateUniqueAssetPath(fullPath);
				if(!AssetDatabase.CopyAsset(
					templatePath,
					fullPath))
				{
					Debug.LogWarning("Failed to create " + fullPath);
					continue;
				}
				enc.blames[i].predicateIconPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(fullPath);
				EditorUtility.SetDirty(enc);
			}
		}

		GUILayout.EndHorizontal();

		view.Apply(enc);
	}
}
