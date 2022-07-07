using UnityEngine;

namespace RedPanda.ObjectPooling
{
    public class GarbageCollector : MonoBehaviour
    {
        private static GarbageCollector instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                gameObject.isStatic = true;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}