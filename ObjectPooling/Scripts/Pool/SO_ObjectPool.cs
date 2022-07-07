using System.Collections.Generic;
using UnityEngine;

namespace RedPanda.ObjectPooling
{
    [CreateAssetMenu(fileName = "Object Pool", menuName = "Red Panda/Object Pool/Object Pool", order = 1)]
    public class SO_ObjectPool : ScriptableObject
    {
        #region Fields And Properties
        [SerializeField] private List<SO_PooledObject> _poolList = new List<SO_PooledObject>();

        private readonly Dictionary<string, Queue<GameObject>> _inPool = new Dictionary<string, Queue<GameObject>>();
        private readonly Dictionary<string, Queue<GameObject>> _inUse = new Dictionary<string, Queue<GameObject>>();
        private GameObject _garbageCollector;

        public List<SO_PooledObject> PoolList => _poolList;
        #endregion Fields And Properties

        #region Unity Methods
        private void OnDisable()
        {
            _poolList = new List<SO_PooledObject>();
        }
        #endregion

        #region Public Methods
        public GameObject GetObject(SO_PooledObject pooledObject, Vector3 position, Vector3 rotation, Transform parent)
        {
            GameObject prefab;

            string tag = pooledObject.PooledObjectTag;

            if (!_inPool.ContainsKey(tag))
            {
                //If pools does not contain the key, adds the key and queues to the dictionary.
                _inPool.Add(tag, new Queue<GameObject>());
                _inUse.Add(tag, new Queue<GameObject>());
            }

            if (_inPool[tag].Count > 0)
            {
                //if there is an object with current tag in the pool, gets the object from pool.
                prefab = _inPool.FromDictionary(tag);
            }
            else
            {
                //if there is no object with current tag in pool, creates a new object.
                prefab = Instantiate(pooledObject.Prefab);
            }

            //Activates object.
            prefab.SetActive(true);

            //Adds the object to the in use dictionary.
            _inUse.ToDictionary(tag, prefab);

            //Sets position and rotation of the object.
            prefab.transform.SetPositionAndRotation(position, Quaternion.Euler(rotation));
            prefab.transform.SetParent(parent);

            return prefab;
        }

        public void RelaseObject(SO_PooledObject pooledObject)
        {
            string tag = pooledObject.PooledObjectTag;

            if (!_inUse.ContainsKey(tag))
            {
                return;
            }

            //Gets the object from in use dictionary.
            GameObject pooledObj = _inUse.FromDictionary(tag);

            if (pooledObj == null)
            {
                return;
            }

            //Deactivates the object.
            pooledObj.SetActive(false);

            //Sets the object to the in pool dictionary.
            _inPool.ToDictionary(tag, pooledObj);

            //if there is no garbage collector, it spawns one.
            if (_garbageCollector == null)
            {
                GameObject obj = new GameObject("Garbage Collector");
                obj.AddComponent<GarbageCollector>();
                _garbageCollector = obj;
            }

            //Collects relased objects under garbage collector. 
            pooledObj.transform.SetParent(_garbageCollector.transform);
        }
        public void ReleaseAllObjects()
        {
            //Gets all element of list.
            foreach (SO_PooledObject pooledObject in PoolList)
            {
                string tag = pooledObject.PooledObjectTag;

                if (!_inUse.ContainsKey(tag))
                {
                    continue;
                }

                //Finds all in use objects.
                int loopCount = _inUse[tag].Count;

                if (loopCount <= 0)
                {
                    continue;
                }

                //Relases all of them.
                for (int i = 0; i < loopCount; i++)
                {
                    RelaseObject(pooledObject);
                }
            }
        }
        #endregion Public Methods
    }
}