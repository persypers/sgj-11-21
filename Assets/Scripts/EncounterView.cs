using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterView : Fancy.MonoSingleton<EncounterView>
{
	public TMPro.TMP_Text label;
	public BlameView[] blameViews;
}
