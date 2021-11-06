using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Effect = Encounter.Effect;
public class EffectView : MonoBehaviour
{
	[System.Serializable]
	public class EffectIcon : SerializableDictionary<Effect.Type, GameObject> {}

	[SerializeField]
	public EffectIcon effectIcon;

	public void Apply(Encounter.Effect effect)
	{
		foreach(var kv in effectIcon)
		{
			kv.Value.SetActive(false);
		}

		GameObject icon = effectIcon[effect.type];
		icon.SetActive(true);

		if(effect.type == Effect.Type.Card)
		{
			icon.GetComponentInChildren<TMPro.TMP_Text>().text = effect.magnitude < 0 ? "-" : "+";
			var icons = GetComponentsInChildren<UnityEngine.UI.Image>(true);
			int iconCount = Mathf.Abs(effect.magnitude);
			for(int i = 0; i < icons.Length; i++)
			{
				icons[i].gameObject.SetActive(i < iconCount);
			}
		} else {
			string sign = effect.magnitude < 0 ? "-" : "+";
			icon.GetComponentInChildren<TMPro.TMP_Text>().text = sign + Mathf.Abs(effect.magnitude);
		}
	}
}
