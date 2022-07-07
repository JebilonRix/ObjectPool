using UnityEngine;

namespace RedPanda.ObjectPooling
{
    public class PrefabPooled : MonoBehaviour
    {
        //This class is an example.

        //All of pooled objects must contains this.
        [SerializeField] private SO_PooledObject _pooledObject;

        private void OnEnable()
        {
            //Debug.Log("enable");
        }
        private void OnDisable()
        {
            //Debug.Log("disable");
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}