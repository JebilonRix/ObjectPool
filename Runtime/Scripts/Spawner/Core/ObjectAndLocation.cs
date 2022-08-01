using UnityEngine;

namespace RedPanda.ObjectPooling
{
    [System.Serializable]
    internal struct ObjectAndLocation
    {
        public SO_PooledObject pooledObject;
        public Vector3 position;
        public Vector3 rotation;
    }
}