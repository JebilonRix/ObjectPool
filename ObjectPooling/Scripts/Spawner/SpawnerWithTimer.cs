using RedPanda.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        [Header("Randomize Settings")]
        [SerializeField] private bool _randomObject = false;
        [SerializeField] private bool _randomPosition = false;

        [Header("Delay Setting")]
        [SerializeField] private bool _hasDelay = false;
        [SerializeField] private float _delaySeconds = 0f;
        private bool _delayFinished = false;

        [Header("Spawn Attributes")]
        [SerializeField] private float _spawnRate = 1f;
        [SerializeField] private SO_PooledObject[] _pooledObject;
        [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
        private bool _startSpawner = false;
        private float _timeCounter = 0;
        #endregion Fields And Properties

        #region Unity Methods
        private void Start()
        {
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
                p_objectPool.ReleaseAllObjects();
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
        private void SpawnLogic()
        {
            if (!_startSpawner)
            { return; }
            if (!_delayFinished)
            { return; }
            if (_hasLimit)
            { return; }
            if (_limit <= _objectCounter)
            { return; }

            _timeCounter += Time.deltaTime;

            if (_timeCounter >= _spawnRate)
            {
                //Decides which object is spawning.
                SO_PooledObject pooledObject = _randomObject ? _pooledObject.GetRandomValue() : _pooledObject[0];

                //Decides where to spawn.
                Transform spawnPoint = _randomPosition ? _spawnPoints.GetRandomValue() : _spawnPoints[0];

                //Gets object from pool.
                p_objectPool.GetObject(pooledObject, spawnPoint.position, spawnPoint.rotation.eulerAngles, p_isParentThis ? transform : null);

                ResetCounter();
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