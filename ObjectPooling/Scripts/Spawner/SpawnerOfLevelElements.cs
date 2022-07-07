using System.Collections.Generic;
using UnityEngine;

namespace RedPanda.ObjectPooling
{
    public class SpawnerOfLevelElements : BaseSpawner
    {
        [SerializeField] private List<ObjectAndLocation> _objAndLocList = new List<ObjectAndLocation>();

        public void SpawnLevel()
        {
            //Spawns all objects.
            foreach (var item in _objAndLocList)
            {
                p_objectPool.GetObject(item.pooledObject, item.position, item.rotation, p_isParentThis ? transform : null);
            }
        }
        public void ReleaseLevel()
        {
            //Relases all objects.
            p_objectPool.ReleaseAllObjects();
        }
    }
}