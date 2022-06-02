using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Locale", menuName = "SGJ/Locale", order = 0)]
public class Locale : ScriptableObject
{
	[System.Serializable]
	public class StringTable : SerializableDictionary<string, string> {}
	public StringTable strings;
}
