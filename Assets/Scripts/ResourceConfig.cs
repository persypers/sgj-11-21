using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceConfig", menuName = "SGJ/ResourceConfig", order = 0)]
public class ResourceConfig : ScriptableObject
{
	[System.Serializable]
	public class PredicateIconDict : SerializableDictionary<Predicates.Enum, GameObject> {}
	public PredicateIconDict predicateIcons;
}
