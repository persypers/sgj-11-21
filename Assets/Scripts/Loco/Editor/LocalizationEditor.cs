using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomPropertyDrawer(typeof(Localization.Locales))]
public class LocalesPropertyDrawer : SerializableDictionaryPropertyDrawer {}

[CustomPropertyDrawer(typeof(Locale.StringTable))]
public class LocaleStringsPropertyDrawer : SerializableDictionaryPropertyDrawer {}


[CustomEditor(typeof(Localization))]
public class LocalizationEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if(GUILayout.Button("Extract"))
		{
			ExtractFirstLocale();
		}

		if(GUILayout.Button("Export"))
		{
			string path = EditorUtility.SaveFilePanel("Export .csv locales", Application.dataPath, "locales", "csv");
			if(!string.IsNullOrEmpty(path))
			{
				Localization l = serializedObject.targetObject as Localization;
				var data = l.SaveToCsv();
				File.WriteAllText(path, data);
				AssetDatabase.Refresh();
			}
		}

		if(GUILayout.Button("Import"))
		{
			string path = EditorUtility.OpenFilePanel("Export .csv locales", Application.dataPath, "csv");
			if(!string.IsNullOrEmpty(path))
			{
				Localization l = serializedObject.targetObject as Localization;
				l.LoadFromCsv(File.ReadAllText(path));
			}
		}

		if(GUILayout.Button("Clear"))
		{
			Localization l = serializedObject.targetObject as Localization;
			foreach (var kv in l.locales)
			{
				kv.Value.strings.Clear();
				EditorUtility.SetDirty(kv.Value);
			}
		}
	}

	private void ExtractFirstLocale()
	{
		Localization l = serializedObject.targetObject as Localization;
		Locale en = l.locales["en"];
		Locale ru = l.locales["ru"];
		string[] guids = AssetDatabase.FindAssets("t:Encounter");
		for(int i = 0; i < guids.Length; i++)
		{
			Encounter enc = AssetDatabase.LoadAssetAtPath<Encounter>(AssetDatabase.GUIDToAssetPath(guids[i]));
			ExtractEncounterLocale(enc, ru, false);
			ExtractEncounterLocale(enc, en, true);
		}
		EditorUtility.SetDirty(en);
		EditorUtility.SetDirty(ru);
		EditorUtility.SetDirty(l);
	}

	private void ExtractEncounterLocale(Encounter enc, Locale loc, bool isEmpty = false)
	{
		TryAddString(loc, enc.name, enc.text, isEmpty);
		TryAddString(loc, enc.name + "_prebark", enc.preBark, isEmpty);
		TryAddString(loc, enc.name + "_goodbark", enc.goodBark, isEmpty);
		TryAddString(loc, enc.name + "_medbark", enc.mediumBark, isEmpty);
		TryAddString(loc, enc.name + "_badbark", enc.badBark, isEmpty);
		for(int i = 0; i < enc.blames.Length; i++)
		{
			TryAddString(loc, enc.name + "_blame_" + i, enc.blames[i].text, isEmpty);
		}
	}

	private void TryAddString(Locale loc, string key, string value, bool isEmpty)
	{
		if(loc.strings.ContainsKey(key)) {
			Debug.LogError("Duplicate locale key: " + key);
			return;
		}
		loc.strings[key] = isEmpty ? null : value;
	}
}
