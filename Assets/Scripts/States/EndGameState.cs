using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class EndGameState : GameState
{
	protected override void OnEnable()
	{
		base.OnEnable();
		Global.Instance.gameUi.Hide();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void Update()
	{
		base.Update();
		GameState.SwitchState<MenuState>();
	}
}
