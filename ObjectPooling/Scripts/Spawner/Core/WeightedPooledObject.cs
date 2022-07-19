using UnityEngine;

namespace RedPanda.ObjectPooling
{
    [System.Serializable]
    public class WeightedPooledObject
    {
        [SerializeField] private SO_PooledObject _pooledObject;
        [SerializeField] private int _weight;

        public SO_PooledObject PooledObject { get => _pooledObject; }
        public int Weight { get => _weight; }
    }
}