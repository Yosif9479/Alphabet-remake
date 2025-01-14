using System.Collections;
using Additionals;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace GameModes.Helicopter
{
    public class VictoryHandler
    {
        public VictoryHandler(Letter letter)
        {
            _letter = letter;
            _currentSceneName = SceneManager.GetActiveScene().name;
            
            ListenToEvents();
        }

        ~VictoryHandler()
        {
            StopListeningToEvent();
        }

        private readonly Letter _letter;
        private readonly string _currentSceneName;

        private void Victory()
        {
            SceneManager.LoadScene(_currentSceneName);
        }
        
        private void OnLetterReachedVariant()
        {
            _letter.StartCoroutine(CallMethodDelayed(0.5f, Victory));
        }

        private IEnumerator CallMethodDelayed(float delaySeconds, UnityAction method)
        {
            yield return new WaitForSeconds(delaySeconds);
            method.Invoke();
        }

        private void ListenToEvents()
        {
            _letter.ReachedVariant.AddListener(OnLetterReachedVariant);
        }

        private void StopListeningToEvent()
        {
            _letter.ReachedVariant.RemoveListener(OnLetterReachedVariant);
        }
    }
}