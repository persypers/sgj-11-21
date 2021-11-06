using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MastIcon : MonoBehaviour
{
	public Card.Mast mast;
	public void OnValidate()
	{
		var image = GetComponent<Image>();
		image.sprite = Global.Instance.config.mastIcons[(int)mast];
		image.SetNativeSize();
	}
}
