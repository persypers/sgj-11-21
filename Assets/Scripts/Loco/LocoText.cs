using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TMPro.TMP_Text))]
public class LocoText : MonoBehaviour
{
	public string term;
	public void OnEnable()
	{
		GetComponent<TMPro.TMP_Text>().text = Localization.Localize(term);
	}
}
