using UnityEngine;

namespace RedPanda.ObjectPooling
{
    [CreateAssetMenu(fileName = "Pooled Object", menuName = "Red Panda/Object Pool/Pool Object", order = 0)]
    public class SO_PooledObject : ScriptableObject
    {
        #region Fields And Properties
        [SerializeField] private SO_ObjectPool _pool;
        [SerializeField] private string _pooledObjectTag;
        [SerializeField] private GameObject _prefab;

        public string PooledObjectTag => _pooledObjectTag;
        public GameObject Prefab => _prefab;
        #endregion Fields And Properties

        #region Unity Methods
        private void OnEnable()
        {
            if (!_pool.PoolList.Contains(this))
            {
                _pool.PoolList.Add(this);
            }
        }
        #endregion Unity Methods

        #region Public Methods
        public void OnStart()
        {
            //Checks Garbage collector game object, if there is or not.
            _pool.GarbageCollectorCheck();
        }
        public void RelaseObjectToPool(BasePooledObject obj)
        {
            //This method is for release this object to pool.
            _pool.RelaseObject(obj);
        }
        #endregion Public Methods
    }
}