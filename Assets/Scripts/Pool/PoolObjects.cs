using Properties;
using UnityEngine;
using System.Collections.Generic;

namespace Pool
{
    public class PoolObjects<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly Transform _container;
        private readonly bool _autoExpand;

        private List<T> _poolList;

        public PoolObjects(T prefab, Transform container, bool autoExpand = true)
        {
            _prefab = prefab;
            _container = container;
            _autoExpand = autoExpand;
        }

        public void CreatePool(int count)
        {
            _poolList = new List<T>();

            for (int i = 0; i < count; i++)
            {
                CreateObject();
            }
        }

        public void DestroyPool()
        {
            foreach (T objectByDestroy in _poolList)
            {
                IDestroyed destroyed = objectByDestroy.GetComponent<IDestroyed>();
                destroyed?.Destroy();
            }

            _poolList.Clear();
        }


        private T CreateObject(bool setActiveByDefault = false)
        {
            T newObject = Object.Instantiate(_prefab, _container);
            newObject.gameObject.SetActive(setActiveByDefault);
            _poolList.Add(newObject);
            return newObject;
        }

        public bool GetFreeElement(out T freeObjectInPool)
        {
            foreach (T objectInPool in _poolList)
            {
                if (objectInPool.gameObject.activeSelf) 
                    continue;
                freeObjectInPool = objectInPool;
                freeObjectInPool.gameObject.SetActive(true);
                return true;
            }

            AutoExpand(out T createdObject);

            freeObjectInPool = createdObject;
            return false;
        }

        private void AutoExpand(out T createdObject)
        {
            if (_autoExpand)
            {
                createdObject = CreateObject();
                createdObject.gameObject.SetActive(true);
                return;
            }

            createdObject = null;

            throw new System.Exception($"The pool ran out of objects with the type {typeof(T)}");
        }

        public void AddToPool(IEnumerable<T> addList)
        {
            foreach (T var in addList)
            {
                var.gameObject.transform.SetParent(_container);
                _poolList.Add(var);
            }
        }
    }
}