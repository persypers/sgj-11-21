using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class SignalState : StateMachineBehaviour {
	public string message = "Msg";
	public bool onExit = false;
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if(!onExit) GameState.Instance.SendMessage(message, animator.gameObject, SendMessageOptions.DontRequireReceiver);
	}
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if(onExit) GameState.Instance.SendMessage(message, animator.gameObject, SendMessageOptions.DontRequireReceiver);
	}
}
