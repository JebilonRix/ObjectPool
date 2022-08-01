using UnityEngine;

namespace RedPanda.ObjectPooling
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : BasePooledObject
    {
        #region Fields And Properties
        [SerializeField] private string[] _targetTags;
        [SerializeField] private float _lifeTime = 1f;

        private float _damage = 0f;
        private float _counter = 0f;
        private Rigidbody _rb;
        #endregion Fields And Properties

        #region Unity Methods
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }
        private void Update()
        {
            _counter += Time.deltaTime;

            if (_counter > _lifeTime)
            {
                OnRelease();
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            foreach (var item in _targetTags)
            {
                if (!other.CompareTag(item))
                {
                    continue;
                }

                //do damage
            }
        }
        protected override void OnEnable()
        {
            _counter = 0;
            base.OnEnable();
        }
        #endregion Unity Methods

        #region Public Methods
        protected override void OnRelease()
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            _damage = 0f;
            base.OnRelease();
        }
        public void Shoot(Vector3 force, float damage)
        {
            _damage = damage;
            _rb.AddForce(force, ForceMode.Impulse);
        }
        #endregion Public Methods
    }
}