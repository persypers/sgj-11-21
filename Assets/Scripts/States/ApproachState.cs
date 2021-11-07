using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class ApproachState : GameState
{
	Encounter enc;
	GameObject character;
	protected override void OnEnable()
	{
		//Global.Instance.PopNextEncounter();
		enc = Global.Instance.nextEncounter;
		character = enc.character == null ? Global.Instance.config.captainPlaceholder : enc.character;
		character = GameObject.Instantiate(character, Global.Instance.characterAppearRoot);
		Vector3 pos = character.transform.localPosition;
		var box = character.GetComponent<BoxCollider2D>();
		pos.x = pos.x + box.offset.x + box.size.x * 0.5f;
		character.transform.localPosition = pos;
		Global.Instance.groundLayer.GetComponent<ParaLayer>().Pop();
		character.transform.SetParent(Global.Instance.groundLayer, true);
		Global.Instance.character = character;

		base.OnEnable();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void Update()
	{
		base.Update();

		float delta = Global.Instance.characterStopRoot.position.x - character.transform.position.x;
		if(Mathf.Abs(delta) < 0.01f)
		{
			GameState.SwitchState<EncounterState>();
		}
	}
}
