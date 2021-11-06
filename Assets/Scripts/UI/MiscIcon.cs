using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MiscIcon : MonoBehaviour
{
	public enum Type
	{
		And,
		Black,
		Red,
		Less,
		LessEq,
		Minus,
		More,
		MoreEq,
		Plus,
		X1,
		X2,
		X3,
		X4,
		Sanity,
		Innocence,
		Card,
		CardFlip
	}

	public Type type;

	public void OnValidate()
	{
		var image = GetComponent<Image>();
		image.sprite = Global.Instance.config.miscIcons[(int)type];
		image.SetNativeSize();
	}
}