using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EncounterSource : ScriptableObject
{
	public abstract Encounter Get();
	public virtual void Reset() {}
}

[CreateAssetMenu(fileName = "Encounter", menuName = "SGJ/Encounter", order = 0)]
public class Encounter : EncounterSource
{
	public override Encounter Get()
	{
		return this;
	}
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
		public bool isHostile = false;

		public int sanityEffect = 0;
		public int innocenceEffect = 0;
		public int draw = 0;
		public int discard = 0;
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
			resolve.isHostile |= blames[i].hostile && r.predicateSucceed;
			if(!r.predicateSucceed) continue;
			if(blames[i].effect.type == Effect.Type.Sanity)
			{
				resolve.sanityEffect += blames[i].effect.magnitude;
			}
			if(blames[i].effect.type == Effect.Type.Redemtion)
			{
				resolve.innocenceEffect += blames[i].effect.magnitude;
			}
			if(blames[i].effect.type == Effect.Type.Card)
			{
				if(blames[i].effect.magnitude < 0)
				{
					resolve.discard += Mathf.Abs(blames[i].effect.magnitude);
				} else {
					resolve.draw += Mathf.Abs(blames[i].effect.magnitude);
				}
			}

		}
		resolve.type = hasGood && !hasBad ? ResolveType.Good : (hasBad && !hasGood ? ResolveType.Bad : ResolveType.Medium);

		return resolve;
	}
}
