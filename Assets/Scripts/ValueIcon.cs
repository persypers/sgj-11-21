using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ValueIcon : MonoBehaviour
{
	public Card.Value value;
	public Card.Color color = Card.Color.Default;

	public void OnValidate()
	{
		var image = GetComponent<Image>();
		image.sprite = Global.Instance.config.GetValueIcon(value, color);
		image.SetNativeSize();
	}
}
