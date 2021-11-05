using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class HelpState : GameState
{
	public GameObject fakeUI;
	protected override void OnEnable()
	{
		base.OnEnable();
		fakeUI.Show();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		fakeUI.Hide();
	}

	protected override void Update()
	{
		if(Input.anyKeyDown)
		{
			GameState.SwitchState<MenuState>();
		}
		base.Update();
	}
}
