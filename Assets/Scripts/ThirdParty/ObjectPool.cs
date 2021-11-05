using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fancy
{
	public class ObjectPool : MonoBehaviour {
		public class PooledObject : MonoBehaviour
		{
			public ObjectPool pool;
			public void ReturnToPool()
			{
				if(pool)
				{
					gameObject.SetActive(false);
					transform.SetParent(pool.transform);
				} else {
					GameObject.Destroy(gameObject);
				}
			}
		}

		public GameObject sample;
		public List<GameObject> pool;

		void OnEnable()
		{
			if(pool == null) pool = new List<GameObject>();
			for(int i = 0; i < pool.Count; i++)
			{
				pool[i].GetOrAddComponent<PooledObject>().pool = this;
			}
		}

		public GameObject Get()
		{
			GameObject go;
			for(int i = 0; i < pool.Count; i++)
			{
				go = pool[i];
				if(! go.activeSelf) return go;
			}
			return InstantiateSample();
		}

		public GameObject InstantiateSample(int count = 1)
		{
			GameObject go = null;
			for (int i = 0; i < count; i++)
			{
				go = GameObject.Instantiate(sample);
				pool.Add(go);
				go.GetOrAddComponent<PooledObject>().pool = this;
				go.transform.SetParent(transform);
				go.SetActive(false);
			}
			return go;
		}

		public void Resize(int newSize)
		{
			if(newSize < 0) newSize = 0;
			if(newSize > pool.Count)
			{
				InstantiateSample(newSize - pool.Count);
			} else
			{
				for(int i = newSize; i < pool.Count; i++)
				{
					GameObject.Destroy(pool[i]);
				}
				pool.RemoveRange(newSize, pool.Count - newSize);
			}
		}
	}
}