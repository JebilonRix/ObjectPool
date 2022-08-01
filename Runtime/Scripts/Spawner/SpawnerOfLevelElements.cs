using System.Collections.Generic;
using UnityEngine;

namespace RedPanda.ObjectPooling
{
    public class SpawnerOfLevelElements : BaseSpawner
    {
        #region Fields And Properties
        [SerializeField] private List<ObjectAndLocation> _objectsAndLocationsList = new List<ObjectAndLocation>();
        #endregion Fields And Properties

        #region Public Methods
        /// <summary>
        /// Spawns all objects at once.
        /// </summary>
        public void SpawnLevel()
        {
            foreach (ObjectAndLocation item in _objectsAndLocationsList)
            {
                objectPool.GetObject(item.pooledObject, item.position, item.rotation, isParentThis ? transform : null);
                //GameObject obj = objectPool.GetObject(item.pooledObject, item.position, item.rotation, isParentThis ? transform : null);
                //obj.GetComponent<BasePooledObject>().SpawnedBySpawner = true;
            }
        }
        /// <summary>
        /// Relases all objects.
        /// </summary>
        public override void ReleaseAllObjects()
        {
            base.ReleaseAllObjects();
        }
        #endregion Public Methods
    }
}