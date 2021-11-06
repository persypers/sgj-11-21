using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceConfig", menuName = "SGJ/ResourceConfig", order = 0)]
public class ResourceConfig : ScriptableObject
{
	public int maxCardsOnHand = 8;
	[System.Serializable]
	public class PredicateIconDict : SerializableDictionary<Predicates.Enum, GameObject> {}
	public PredicateIconDict predicateIcons;

	public Card.Value cardValueOrder;
	public Sprite[] redValueIcons;
	public Sprite[] blackValueIcons;
	public Card.Mast cardMastOrder;
	public Sprite[] mastIcons;

	public Sprite GetValueIcon(Card.Value value, Card.Color color = Card.Color.Default)
	{
		var sprites = color == Card.Color.Red ? redValueIcons : blackValueIcons;
		return sprites[(int)value];
	}

	public Sprite GetMastIcon(Card.Mast mast)
	{
		return mastIcons[(int)mast];
	}

	public MiscIcon.Type miscIconOrder;
	public Sprite[] miscIcons;
}
