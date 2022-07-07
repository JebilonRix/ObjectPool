using UnityEngine;

namespace RedPanda.ObjectPooling
{
    public class BaseSpawner : MonoBehaviour
    {
        #region Fields
        [Header("Pool Base")]
        [SerializeField] protected SO_ObjectPool p_objectPool;
        [SerializeField] protected bool p_isParentThis = false;
        #endregion Fields
    }
}