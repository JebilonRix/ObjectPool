using UnityEngine;

namespace RedPanda.ObjectPooling
{
    public class TesterPooling : MonoBehaviour
    {
        #region Fields
        [SerializeField] private KeyCode Start = KeyCode.Alpha1;
        [SerializeField] private KeyCode Stop = KeyCode.Alpha2;
        [SerializeField] private KeyCode LoadNewScene = KeyCode.Alpha3;
        [SerializeField] private bool _isTestinSpawner1 = true;
        [SerializeField] private SpawnerWithTimer _spawner1;
        [SerializeField] private SpawnerOfLevelElements _spawner2;
        #endregion Fields

        #region Unity Methods
        private void Update()
        {
            if (Input.GetKeyDown(Start))
            {
                if (_isTestinSpawner1)
                {
                    _spawner1.StartStop(true);
                }
                else
                {
                    _spawner2.SpawnLevel();
                }
            }
            if (Input.GetKeyDown(Stop))
            {
                if (_isTestinSpawner1)
                {
                    _spawner1.StartStop(false);
                }
                else
                {
                    _spawner2.ReleaseLevel();
                }
            }

            if (Input.GetKeyDown(LoadNewScene))
            {
                if (_isTestinSpawner1)
                {
                    _spawner1.StartStop(false);
                }
                else
                {
                    _spawner2.ReleaseLevel();
                }

                UnityEngine.SceneManagement.SceneManager.LoadScene("Pool Test 2");
            }
        }
        #endregion Unity Methods
    }
}