using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Encounter", menuName = "SGJ/Encounter", order = 0)]
public class Encounter : ScriptableObject
{
	[System.Serializable]
	public class Blame
	{
		public Predicates.Enum check;
		[TextArea]
		public string text;
		public Effect effect;
	}

	[System.Serializable]
	public class Effect
	{
		public enum Type
		{
			Damage,
			Heal,
			Draw,
			Discard,
			Exchange
		}
		public Type type;
		public int magnitude;
	}

	[TextArea]
	public string text;

	public Blame[] blames;
}
