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

		public class Resolve
		{
			public bool isGood = false;
			public bool predicateSucceed = false;
			public List<Card> hits = null;
			public Effect effect;
		}

		public Resolve GetResolve(List<Card> hand)
		{
			Resolve resolve = new Resolve();
			
			Predicates.Predicate p = Predicates.Table.ContainsKey(predicate) ? Predicates.Table[predicate] : null;
			if(p != null)
			{
				resolve.hits = p(hand);
			}
			resolve.predicateSucceed = p == null || resolve.hits != null;
			resolve.isGood = hostile != resolve.predicateSucceed;

			return resolve;
		}
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

	public enum ResolveType
	{
		Bad,
		Medium,
		Good
	}
	public class Resolve
	{
		public Blame.Resolve[] blameResolves;
		public ResolveType type;
	}

	public Resolve GetResolve(List<Card> hand)
	{
		Resolve resolve = new Resolve();
		int c = blames.Length;
		resolve.blameResolves = new Blame.Resolve[c];
		bool hasBad = false;
		bool hasGood = false;
		for(int i = 0; i < c; i++)
		{
			var r = blames[i].GetResolve(hand);
			hasBad |= !r.isGood;
			hasGood |= r.isGood;
			resolve.blameResolves[i] = r;
		}
		resolve.type = hasGood && !hasBad ? ResolveType.Good : (hasBad && !hasGood ? ResolveType.Bad : ResolveType.Medium);

		return resolve;
	}
}
