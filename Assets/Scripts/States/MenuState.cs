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
		GameState.SwitchState<BeginGameState>();
	}
}
