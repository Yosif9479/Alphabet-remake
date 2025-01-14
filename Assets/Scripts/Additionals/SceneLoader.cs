using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Additionals
{
    public class SceneLoader : MonoBehaviour
    {
        #region CONSTANTS

        private const string LOADING_SCENE_NAME = "Loading";

        #endregion

        [Tooltip("Minimal time to wait before switching loading scene")]
        [SerializeField] private float _minCooldown = 1f;

        public void LoadSceneAsync(string name)
        {
            StartCoroutine(LoadSceneAsyncCoroutine(name));
            UnityAdsManager.Instance.AddScore();
        }

        private IEnumerator LoadSceneAsyncCoroutine(string name)
        {
            SceneManager.LoadScene(LOADING_SCENE_NAME, LoadSceneMode.Additive);

            yield return new WaitForSeconds(_minCooldown);

            SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        }
    }
}