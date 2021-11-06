using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class MenuState : GameState
{
	public GameObject menu;
	protected override void OnEnable()
	{
		base.OnEnable();
		menu.Show();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		menu.Hide();
	}

	public void StartGame()
	{
		Global.Instance.ResetGame();
		Global.Instance.handView.gameObject.Show();
		GameState.SwitchState<WalkState>();
	}
}
