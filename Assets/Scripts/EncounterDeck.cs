using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deck", menuName = "SGJ/Encounter Deck", order = 0)]
public class EncounterDeck : EncounterSource
{
	public override Encounter Get()
	{
		if(currentDeck == null || currentDeck.Count == 0)
		{
			Reset();
		}
		var source = currentDeck[0];
		currentDeck.RemoveAt(0);
		return source.Get();
	}
	public override void Reset()
	{
		currentDeck = new List<EncounterSource>(deck);
		if(shuffle)
		{
			for(int i = 0; i < currentDeck.Count; i++)
			{
				int j = Random.Range(0, currentDeck.Count);
				var swap = currentDeck[j];
				currentDeck[j] = currentDeck[i];
				currentDeck[i] = swap;
			}
		}
	}

	public EncounterSource[] deck;
	public bool shuffle;
	private List<EncounterSource> currentDeck;
}
