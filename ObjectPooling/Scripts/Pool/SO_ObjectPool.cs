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

        public List<SO_PooledObject> PoolList => _poolList;
        public Transform GarbageCollector { get; private set; }
        #endregion Fields And Properties

        #region Unity Methods
        private void OnDisable()
        {
            GarbageCollector = null;
            _poolList = new List<SO_PooledObject>();
        }
        #endregion Unity Methods

        #region Public Methods
        public GameObject GetObject(SO_PooledObject pooledObject, Vector3 position, Vector3 rotation, Transform parent)
        {
            string tag = pooledObject.PooledObjectTag; //Tag

            //If pools does not contain the key, adds the key and queues to the dictionary.
            DictionaryCheck(tag);

            //If is there any an object with current tag in the pool, gets the object from pool or creates a new object.
            GameObject prefab = _inPool[tag].Count == 0 ? Instantiate(pooledObject.Prefab) : _inPool[tag].Dequeue();
            prefab.name = pooledObject.PooledObjectTag;
            Debug.Log(prefab.name);

            //Activates the object.
            prefab.SetActive(true);

            //Adds the object to the in use dictionary.
            _inUse[tag].Enqueue(prefab);

            //Sets position and rotation of the object.
            prefab.transform.SetPositionAndRotation(position, Quaternion.Euler(rotation));
            prefab.transform.SetParent(parent);

            return prefab;
        }
        public void RelaseObject(BasePooledObject pooled)
        {
            string tag = pooled.PooledObject.PooledObjectTag; //Tag

            //If pools does not contain the key, adds the key and queues to the dictionary.
            DictionaryCheck(tag);

            //Collects relased objects under garbage collector.
            pooled.transform.SetParent(GarbageCollector);

            //Sets the object to the in pool dictionary.
            _inPool[tag].Enqueue(pooled.gameObject);

            //Deactivates the object.
            pooled.gameObject.SetActive(false);
        }
        public void GarbageCollectorCheck()
        {
            if (GarbageCollector != null)
            {
                return;
            }

            GameObject obj = new GameObject("Garbage Collector");
            GarbageCollector = obj.AddComponent<GarbageCollector>().transform;
        }
        #endregion Public Methods

        #region Private Methods
        private void DictionaryCheck(string tag)
        {
            if (_inPool.ContainsKey(tag))
            {
                return;
            }

            _inPool.Add(tag, new Queue<GameObject>());
            _inUse.Add(tag, new Queue<GameObject>());

            GarbageCollectorCheck();
        }
        #endregion Private Methods
    }
}