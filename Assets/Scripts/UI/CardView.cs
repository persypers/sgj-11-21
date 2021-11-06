using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardView : MonoBehaviour, IPointerDownHandler
{
	public ValueIcon value;
	public MastIcon mast;
	public Transform anchor;
	public bool dieOnEnd;
	public static float Slinky = 0.16f;
	public static float endThreshold = 4f;
	public void Apply(Card c)
	{
		value.value = c.value;
		value.color = (int)c.mast > 1 ? Card.Color.Red : Card.Color.Black;
		mast.mast = c.mast;
		value.OnValidate();
		mast.OnValidate();
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

	public int cardIndex;
	public void OnPointerDown(PointerEventData eventData)
	{
		if(eventData.button == PointerEventData.InputButton.Left)
			GameState.Instance.SendMessage("CardTap", gameObject);
		if(eventData.button == PointerEventData.InputButton.Right)
			GameState.Instance.SendMessage("CardTapRight", gameObject);
	}
}
