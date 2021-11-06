using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterView : Fancy.MonoSingleton<EncounterView>
{
	public TMPro.TMP_Text label;
	public BlameView[] blameViews;

	public void Apply(Encounter encounter)
	{
		label.text = encounter.text;
		var blames = encounter.blames;
		for(int i = 0; i < blameViews.Length; i++)
		{
			blameViews[i].gameObject.SetActive(blames.Length > i);
			if(blames.Length > i)
			{
				blameViews[i].Apply(blames[i]);
			} else {
				blameViews[i].RemovePredicateIcon();
			}
		}
	}
}
