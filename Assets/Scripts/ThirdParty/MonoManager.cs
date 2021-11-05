using UnityEngine;

namespace Fancy
{
	public abstract class MonoManager : MonoBehaviour
	{
		public enum ActivationMode
		{
			DisableGameObject,
			DisableComponent
		}
		public virtual ActivationMode GetActivationMode() { return ActivationMode.DisableGameObject; }
	}
	public abstract class MonoManager<T> : MonoManager where T : MonoManager<T> {
		static T instance;
		public static T Instance {
			get {
				if(!instance) {
					instance = Helpers.FindSceneComponent<T>(false);
				}
				return instance;
			}
		}

		public void Activate()
		{
			if(GetActivationMode() == ActivationMode.DisableComponent)
			{
				enabled = true;
			} else
			{
				gameObject.SetActive(true);
			}
		}

		protected virtual void OnEnable()
		{
			if(instance != null && instance != this)
			{
				if(GetActivationMode() == ActivationMode.DisableComponent)
				{
					instance.enabled = false;
				} else
				{
					instance.gameObject.SetActive(false);
				}
			}
			instance = this as T;
		}
		protected virtual void OnDisable() {
			if(instance == this) instance = null;
		}
	}
}
