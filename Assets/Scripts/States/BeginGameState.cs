using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class BeginGameState : GameState
{
	protected override void OnEnable()
	{
		base.OnEnable();
		Global.Instance.ResetGame();
		Global.Instance.gameUi.Show();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void Update()
	{
		base.Update();
		GameState.SwitchState<WalkState>();
	}
}
