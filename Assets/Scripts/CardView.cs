using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardView : MonoBehaviour, IPointerDownHandler
{
	public TMPro.TMP_Text label;
	public Transform anchor;
	public bool dieOnEnd;
	public static float Slinky = 0.16f;
	public static float endThreshold = 4f;
	public void Apply(Card c)
	{
		label.text = c.ToString();
	}

	public void Snap(Transform target = null)
	{
		if(target == null) target = anchor;
		transform.position = target.position;
		isMoving = false;
		if(dieOnEnd)
		{
			gameObject.SetActive(false);
			dieOnEnd = false;
		}
	}

	bool isMoving = false;
	public void Update()
	{
		Vector3 d = anchor.position - transform.position;
		if(d.sqrMagnitude > endThreshold)
		{
			isMoving = true;
			transform.position += d * Slinky;
		}
		else if(isMoving) Snap();
	}

	public System.Action<CardView, int> OnClick = null;
	public System.Action<CardView, int> OnRightClick = null;
	public int cardIndex;
	public void OnPointerDown(PointerEventData eventData)
	{
		if(eventData.button == PointerEventData.InputButton.Left)
			if(OnClick != null) OnClick(this, cardIndex);
		if(eventData.button == PointerEventData.InputButton.Right)
			if(OnRightClick != null) OnRightClick(this, cardIndex);
	}
}
