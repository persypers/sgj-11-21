using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceConfig", menuName = "SGJ/ResourceConfig", order = 0)]
public class ResourceConfig : ScriptableObject
{
	public int maxCardsOnHand = 8;
	public int maxInnocence = 10;
	public int baseSanity = 30;
	public float minBarkTime = 4f;
	public bool foolModeEnabled = false;
	public bool allowDraw = false;
	public bool showRandomCardsOnFail = false;
	public bool rightClickDiscards = true;
	public bool skipCutscenes = false;
	public bool skipIntro = false;
	public int firstDeal = 6;
	[System.Serializable]
	public class PredicateIconDict : SerializableDictionary<Predicates.Enum, GameObject> {}
	public PredicateIconDict predicateIcons;

	public Card.Value cardValueOrder;
	public Sprite[] redValueIcons;
	public Sprite[] blackValueIcons;
	public Card.Mast cardMastOrder;
	public Sprite[] mastIcons;
	public GameObject captainPlaceholder;

	public Sprite[] spades;
	public Sprite[] clubs;
	public Sprite[] diamonds;
	public Sprite[] hearts;

	public Sprite GetCardIcon(Card.Value value, Card.Mast mast)
	{
		var sprites = mast == Card.Mast.Clubs ? clubs : mast == Card.Mast.Diamonds ? diamonds : mast == Card.Mast.Spades ? spades : hearts;
		return sprites[(int)value];
	}
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
