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
        public SO_ObjectPool Pool { get => _pool; private set => _pool = value; }
        #endregion Fields And Properties

        #region Unity Methods
        private void Awake()
        {
            if (Pool == null)
            {
                Pool = Resources.Load<SO_ObjectPool>("Pool");

                if (Pool == null)
                {
                    Debug.Log("Pool is not exist.");
                }
            }
        }
        private void OnEnable()
        {
            if (!Pool.PoolList.Contains(this))
            {
                Pool.PoolList.Add(this);
            }
        }
        #endregion Unity Methods

        #region Public Methods
        /// <summary>
        /// This method is for release this object to pool.
        /// </summary>
        public void RelaseObjectToPool()
        {
            Pool.RelaseObject(this);
        }
        #endregion Public Methods
    }
}