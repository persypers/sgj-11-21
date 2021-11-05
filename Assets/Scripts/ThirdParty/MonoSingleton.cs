using UnityEngine;

namespace Fancy
{
	public abstract class MonoSingleton : MonoBehaviour{ }
	public abstract class MonoSingleton<T> : MonoSingleton where T : MonoSingleton<T> {

		static T instance;
		protected bool isInited = false;
		public static T Instance {
			get {
				if(!instance) {
					instance = Helpers.FindSceneComponent<T>(true);
				}
				if(instance && !instance.isInited)
				{
					instance.Init();
				}
				return instance;
			}
		}

		protected virtual void Awake() {
			if (instance != null && instance != this) {
				Debug.LogWarningFormat("MonoSingleton({0}) already created!", typeof(T));
			}
			if(!isInited) {
				Init();
				isInited = true;
			}
			instance = this as T;
		}
		protected virtual void Init(){}
		protected virtual void OnDestroy() {
			if(instance == this) instance = null;
		}
	}
}
