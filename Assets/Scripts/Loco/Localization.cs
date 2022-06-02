using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "Localization", menuName = "SGJ/Localization", order = 0)]
public class Localization : ScriptableObject
{
	[System.Serializable]
	public class Locales : SerializableDictionary<string, Locale> {}
	public Locales locales;
	private Locale activeLocale;
	public static string LocaleKey {get; private set;}

	public static string Localize(string term)
	{
		string result = term;

		Global global = Global.Instance;
		if(!global) return result;

		Localization loco = global.config.localization;
		loco.activeLocale.strings.TryGetValue(term, out result);
		return result;
	}

	public static void SwitchLocale(string loc)
	{
		Global global = Global.Instance;
		if(!global) return;

		Localization loco = global.config.localization;
		LocaleKey = loc;
		loco.activeLocale = loco.locales[loc];
	}

	public void OnEnable()
	{
		if(activeLocale == null) activeLocale = locales.Values.GetEnumerator().Current;
	}

	public void LoadFromCsv(string data)
	{
		StringReader reader = new StringReader(data);
		var strings = SplitCsv(reader.ReadLine());
		if(strings.Length != locales.Count + 1) {
			Debug.LogError("Mismatch Localization and CSV configuration: " + strings.Length + " vs " + (locales.Count+1));
			return;
		}
		Locale[] locArray = new Locale[locales.Count];
		for(int i = 1; i < strings.Length; i++)
		{
			locArray[i - 1] = locales[strings[i]];
			locArray[i - 1].strings.Clear();
		}
		string str = reader.ReadLine();
		while(str != null)
		{
			strings = SplitCsv(str);
			for(int i = 0; i < locArray.Length; i++)
			{
				string key = strings[0];
				string value = i+1 < strings.Length ? strings[i+1] : null;
				locArray[i].strings[key] = value == "" ? null : value;
			}
			str = reader.ReadLine();
		}
	}

	private static string[] SplitCsv(string data)
	{
		List<string> result = new List<string>();
		bool firstChar = true;
		bool quotes = false;
		Debug.Log("SplitCsv: " + data);
		StringBuilder str = new StringBuilder();
		for(int j = 0; j < data.Length; j++)
		{
			char c = data[j];
			char next = data.Length > j+1 ? data[j+1] : ',';
			if(c == '"')
			{
				if(firstChar) {
					quotes = true;
				} else if(next == '"'){
					str.Append('"');				// text quotes are doubled
					j++;
				} else if(next == ',' && quotes) {
					quotes = false;
					result.Add(str.ToString());
					str.Clear();
					j++;
					firstChar = true;
					continue;
				} else {
					Debug.LogWarning("Bad CSV string: " + data);
				}
			} else if(c == ',' && !quotes){
				result.Add(str.ToString());
				str.Clear();
				firstChar = true;
				continue;
			} else {
				str.Append(c);
			}
			firstChar = false;
		}
		if(!firstChar) {
			result.Add(str.ToString());
			str.Clear();
		}
		for (int i = 0; i < result.Count; i++)
		{
			Debug.Log(result[i]);
		}
		return result.ToArray();
	}

	public string SaveToCsv()
	{
		StringBuilder str = new StringBuilder("key", 4096);
		int locCount = locales.Keys.Count;
		Dictionary<string, string[]> table = new Dictionary<string, string[]>();
		int locIndex = 0;
		foreach(var kvLoc in locales)
		{
			str.Append(",");
			foreach(var kv in kvLoc.Value.strings)
			{
				string key = kv.Key;
				string[] values = null;
				if(!table.TryGetValue(key, out values))
				{
					values = table[key] = new string[locCount];
				}
				values[locIndex] = kv.Value;
			}
			str.Append(kvLoc.Key);
			locIndex++;
		}

		foreach(var kv in table)
		{
			str.Append("\n");
			str.Append("\"");
			str.Append(kv.Key);
			str.Append("\"");
			var values = kv.Value;
			for(int i = 0; i < values.Length; i++)
			{
				str.Append(",");
				if(values[i] != null) {
					str.Append("\"");
					str.Append(values[i].Replace("\"", "\"\""));
					str.Append("\"");
				}
			}
		}
		return str.ToString();
	}
}
