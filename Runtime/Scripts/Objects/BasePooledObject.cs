using UnityEngine;

namespace RedPanda.ObjectPooling
{
    public class BasePooledObject : MonoBehaviour
    {
        #region Fields And Properties   
        [SerializeField] private SO_PooledObject _pooledObject;

        public SO_PooledObject PooledObject { get => _pooledObject; }
        #endregion Fields And Properties

        #region Unity Methods
        protected virtual void OnEnable()
        {
            PooledObject.OnStart();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Call this method when game object is setted disable.
        /// </summary>
        protected virtual void OnRelease()
        {
            PooledObject.RelaseObjectToPool(this);
        }
        #endregion
    }
}