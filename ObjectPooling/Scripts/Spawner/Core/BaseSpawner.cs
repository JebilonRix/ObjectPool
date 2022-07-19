using UnityEngine;

namespace RedPanda.ObjectPooling
{
    public class BaseSpawner : MonoBehaviour
    {
        #region Fields
        [Header("Pool Base")]
        [SerializeField] protected SO_ObjectPool objectPool;
        [SerializeField] protected bool isParentThis = false;
        #endregion Fields

        #region Public Methods
        public virtual void ReleaseAllObjects()
        {
            BasePooledObject[] all = FindObjectsOfType<BasePooledObject>();

            for (int i = 0; i < all.Length; i++)
            {
                objectPool.RelaseObject(all[i]);
                all[i].gameObject.SetActive(false);
            }
        }
        #endregion Public Methods
    }
}