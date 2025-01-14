using UnityEngine.Events;

namespace GameModes.Helicopter
{
    public class LetterCompare
    {
        #region EVENTS

        public UnityEvent CorrectLetterSelected = new UnityEvent(); 

        #endregion
        
        public LetterCompare(char correctLetter)
        {
            _correctLetter = correctLetter;
            
            ListenToEvents();
        }

        ~LetterCompare()
        {
            StopListeningToEvents();
        }

        private readonly char _correctLetter;
        
        private void OnLetterSelected(LetterVariant variant)
        {
            Compare(variant);
        }

        private void Compare(LetterVariant variant)
        {
            if (variant.Letter != _correctLetter)
            {
                variant.Wrong();
                return;
            }
            
            variant.Correct();
            
            CorrectLetterSelected?.Invoke();
        }

        private void ListenToEvents()
        {
            LetterVariant.Selected.AddListener(OnLetterSelected);
        }

        private void StopListeningToEvents()
        {
            LetterVariant.Selected.RemoveListener(OnLetterSelected);
        }
    }
}