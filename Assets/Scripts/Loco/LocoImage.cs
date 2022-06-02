using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Image))]
public class LocoImage : MonoBehaviour
{
	[System.Serializable]
	public class LocalizedSprites : SerializableDictionary<string, Sprite> {};
	public LocalizedSprites sprites;
	public void OnEnable()
	{
		Sprite sprite = null;
		sprites.TryGetValue(Localization.LocaleKey, out sprite);
		GetComponent<UnityEngine.UI.Image>().sprite = sprite;
	}
}
