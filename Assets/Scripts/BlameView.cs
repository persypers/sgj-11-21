using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlameView : MonoBehaviour
{
	public TMPro.TMP_Text label;
	public Transform predicateRoot;
	public TMPro.TMP_Text predicateDebugLabel;
	public EffectView effectView;

	GameObject predicateIcon = null;
	public void Apply(Encounter.Blame blame)
	{
		label.text = blame.text;
		effectView.Apply(blame.effect);
		if(predicateIcon != null) GameObject.Destroy(predicateIcon);
		GameObject icon = null;
		if(Global.Instance.config.predicateIcons.TryGetValue(blame.check, out icon))
		{
			predicateIcon = GameObject.Instantiate(icon, predicateRoot);
		} else 
		{
			blalbalba
		}

		
	}
}
