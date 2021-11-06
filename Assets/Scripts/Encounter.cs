using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Encounter", menuName = "SGJ/Encounter", order = 0)]
public class Encounter : ScriptableObject
{
	[System.Serializable]
	public class Blame
	{
		public Predicates.Enum predicate;
		public GameObject predicateIconPrefab;
		[TextArea]
		public string text;
		public Effect effect;
		public bool hostile = true;
	}

	[System.Serializable]
	public class Effect
	{
		[System.Serializable]
		public enum Type
		{
			Sanity,
			Redemtion,
			Card,
		}
		public Type type;
		public int magnitude;
	}

	[TextArea]
	public string text;

	public Blame[] blames;

	[TextArea]
	public string preBark;
	[TextArea]
	public string goodBark;
	[TextArea]
	public string mediumBark;
	[TextArea]
	public string badBark;

}
