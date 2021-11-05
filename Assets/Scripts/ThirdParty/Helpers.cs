using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fancy
{
public static class Helpers
{
	// Returns first encountered instance of T in currently loaded scenes
	// Note that result may have not been Awake'ned or OnEnable'ed yet
	public static T FindSceneComponent<T>(bool includeInactive = false) where T : MonoBehaviour
	{
		T result = null;
		result = GameObject.FindObjectOfType<T>(includeInactive);
		if(result != null) return result;

		var sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCount;
		for(int i = 0; i < sceneCount; i++)
		{
			var scene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
			var allGameObjects = scene.GetRootGameObjects();
			for (int j = 0; j < allGameObjects.Length; j++)
			{
				var go = allGameObjects[j];
				result = go.GetComponentInChildren<T>(includeInactive);
				if(result != null) {
					return result;
				}
			}
		}
		return null;
	}

	public static T GetOrAddComponent<T>(this GameObject source) where T : Component
	{
		T component = source.GetComponent<T>();
		if (component != null) return component;
		return source.AddComponent<T>();
	}

	public static void Show(this GameObject go)
	{
		Animator anim = go.GetComponent<Animator>();
		if(anim != null)
		{
			anim.SetTrigger("Show");
		}
		go.SetActive(true);
	}
	public static void Hide(this GameObject go)
	{
		Animator anim = go.GetComponent<Animator>();
		if(anim == null)
		{
			go.SetActive(false);
		} else
		{
			anim.SetTrigger("Hide");
		}
	}
}

}