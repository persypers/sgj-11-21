using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public abstract class GameState : Fancy.MonoManager<GameState> 
{
	public static void SwitchState<T>() where T : GameState
	{
		T newState = Fancy.Helpers.FindSceneComponent<T>(true);
		Debug.Assert(newState != null);
		newState.Activate();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		Debug.Log("State entered: " + this.GetType().ToString());
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		Debug.Log("State exited: " + this.GetType().ToString());
	}

	protected virtual void Update()
	{
		//if(Input.GetKeyDown(KeyCode.Alpha1)) SwitchState<SystemOverviewState>();
		//if(Input.GetKeyDown(KeyCode.Alpha2)) SwitchState<PlanetEditorState>();
	}
}
