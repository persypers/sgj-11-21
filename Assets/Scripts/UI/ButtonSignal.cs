using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSignal : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
{
	public enum Mode
	{
		Down,
		Click
	}

	public Mode mode;
	public string message;
	public Object arg;

	public void OnPointerDown(PointerEventData eventData)
	{
		if(mode == Mode.Down)
		{
			GameState.Instance.SendMessage(message, arg == null ? gameObject : arg);
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if(mode == Mode.Click)
		{
			GameState.Instance.SendMessage(message, arg == null ? gameObject : arg);
		}
	}
}
