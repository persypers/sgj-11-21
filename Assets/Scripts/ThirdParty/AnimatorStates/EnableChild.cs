using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fancy
{
public class ChildEnabler : StateMachineBehaviour
{
	public string path;
	public bool enable = true;
//	public bool hideSkeleton;
	public bool saveStateAfterExit;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Transform trans = animator.transform.Find(path);
		if(trans) {
			GameObject go = trans.gameObject;
			go.SetActive(enable);
//			if (hideSkeleton)
//				animator.GetComponentInChildren<Spine.Unity.SkeletonAnimation>().GetComponent<MeshRenderer>().enabled = false;
		} else {
			Debug.LogWarning("ChildEnabler error: " + animator.name + " is missing child named " + path);
		}
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Transform trans = animator.transform.Find(path);
		if (trans) {
			GameObject go = trans.gameObject;
			if (!saveStateAfterExit) go.SetActive(!enable);
//			if (hideSkeleton) animator.GetComponentInChildren<Spine.Unity.SkeletonAnimation>().GetComponent<MeshRenderer>().enabled = true;
		}
	}
}
}