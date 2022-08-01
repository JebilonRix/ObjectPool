using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RedPanda.Utils.ListUtils;

namespace RedPanda.ObjectPooling
{
    public class SpawnerWithTimer : BaseSpawner
    {
        #region Fields And Properties
        [Header("Spawner Settings")]
        [Header("Limit")]
        [SerializeField] private bool _hasLimit = false;
        [SerializeField] private int _limit = 10;
        private int _objectCounter = 0;

        [Header("Randomizing Settings")]
        [SerializeField] private bool _randomObject = false;
        [SerializeField] private bool _randomPosition = false;

        [Header("Delay Setting")]
        [SerializeField] private bool _hasDelay = false;
        [SerializeField] private float _delaySeconds = 0f;
        private bool _delayFinished = false;

        [Header("Spawn Attributes")]
        [SerializeField] private float _spawnRate = 1f;
        [SerializeField] private WeightedPooledObject[] _pooledObjects;
        [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
        private readonly List<SO_PooledObject> _listOfObjs = new List<SO_PooledObject>();
        private bool _startSpawner = false;
        private float _timeCounter = 0;
        #endregion Fields And Properties

        #region Unity Methods
        private void Start()
        {
            //Adds objects with weight.
            for (int i = 0; i < _pooledObjects.Length; i++)
            {
                for (int j = 0; j < _pooledObjects[i].Weight; j++)
                {
                    _listOfObjs.Add(_pooledObjects[i].PooledObject);
                }
            }

            //if there is no spawn point, adds itselves as transform.
            if (_spawnPoints.Count == 0)
            {
                _spawnPoints.Add(transform);
            }
        }
        private void Update()
        {
            SpawnLogic();
        }
        #endregion Unity Methods

        #region Public Methods
        public void StartStop(bool isStart)
        {
            _startSpawner = isStart;

            if (isStart)
            {
                StartCoroutine(WaitDelay());
            }
            else
            {
                ReleaseAllObjects();
                ResetCounter();
                ObjectCounter(0);
            }
        }
        public void ObjectCounter(int amount)
        {
            //Resets counter.
            if (amount == 0)
            {
                _objectCounter = 0;
                return;
            }

            //Increase counter.
            _objectCounter += amount;

            //Blocks bugs.
            if (_objectCounter < 0)
            {
                _objectCounter = 0;
            }
        }
        #endregion Public Methods

        #region Private Methods
        private void ResetCounter() => _timeCounter = 0;
        public override void ReleaseAllObjects()
        {
            ObjectCounter(0);
            base.ReleaseAllObjects();
        }
        private void SpawnLogic()
        {
            if (!_startSpawner)
            {
                return;
            }
            if (!_delayFinished)
            {
                return;
            }
            if (_hasLimit && _limit <= _objectCounter)
            {
                return;
            }

            _timeCounter += Time.deltaTime;

            if (_timeCounter >= _spawnRate)
            {
                //Decides which object is spawning.
                SO_PooledObject pooledObject = _randomObject ? _listOfObjs.GetRandomValue() : _listOfObjs[0];

                //Decides where to spawn.
                Transform spawnPoint = _randomPosition ? _spawnPoints.GetRandomValue() : _spawnPoints[0];

                //Gets object from pool.
                objectPool.GetObject(pooledObject, spawnPoint.position, spawnPoint.rotation.eulerAngles, isParentThis ? transform : null);

                ResetCounter();

                //increases counter
                ObjectCounter(1);
            }
        }
        private IEnumerator WaitDelay()
        {
            if (!_hasDelay)
            {
                _delayFinished = true;
                yield break;
            }

            yield return new WaitForSeconds(_delaySeconds);

            _delayFinished = true;
        }
        #endregion Private Methods
    }
}