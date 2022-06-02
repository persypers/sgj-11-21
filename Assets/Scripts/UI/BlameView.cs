using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlameView : MonoBehaviour
{
	public TMPro.TMP_Text label;
	public Transform predicateRoot;
	public TMPro.TMP_Text predicateDebugLabel;
	public EffectView effectView;

	[HideInInspector]
	public GameObject predicateIcon = null;
	public void Apply(Encounter.Blame blame)
	{
		label.text = Localization.Localize(blame.text);
		effectView.Apply(blame.effect);
		RemovePredicateIcon();
		GameObject icon = blame.predicateIconPrefab;

		if(icon != null && Global.Instance != null)
		{
			//Global.Instance.config.predicateIcons.TryGetValue(blame.predicate, out icon);
		}

		if(icon != null)
		{
#if UNITY_EDITOR
			if(Application.IsPlaying(this))
			{
				//Debug.Log("Pop instance");
				predicateIcon = GameObject.Instantiate(icon, predicateRoot);
			}
			else
			{
				//Debug.Log("Pop ref");
				predicateIcon = UnityEditor.PrefabUtility.InstantiatePrefab(icon, predicateRoot) as GameObject;
				predicateIcon.hideFlags = HideFlags.DontSave;
			}
#else
			predicateIcon = GameObject.Instantiate(icon, predicateRoot);
#endif
		} else
		{
			predicateDebugLabel.text = System.Enum.GetName(typeof(Predicates.Enum), blame.predicate);
		}
		predicateDebugLabel.gameObject.SetActive(icon == null);
	}

	public void RemovePredicateIcon()
	{
		if(predicateIcon != null)
		{
#if UNITY_EDITOR
			if(Application.IsPlaying(this))
			{
				//Debug.Log("kill instance");
				GameObject.Destroy(predicateIcon);
			}
			else
			{
				//Debug.Log("kill ref");
				GameObject.DestroyImmediate(predicateIcon);
			}
#else
			GameObject.Destroy(predicateIcon);
#endif
			predicateIcon = null;
		}
	}
}
