using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class ApproachState : GameState
{
	Encounter enc;
	GameObject character;
	public float safetyTime = 20f;
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

		t = 0;

		base.OnEnable();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	float t = 0;
	protected override void Update()
	{
		base.Update();

		t += Time.deltaTime;

		float delta = character.transform.position.x - Global.Instance.characterStopRoot.position.x ;
		if(delta < 0.01f || t > safetyTime)
		{
			GameState.SwitchState<EncounterState>();
		}
	}
}
