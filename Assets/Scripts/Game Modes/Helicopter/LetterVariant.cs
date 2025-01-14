using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace GameModes.Helicopter
{
    [RequireComponent(typeof(TextMeshPro), typeof(Animator))]
    public class LetterVariant : MonoBehaviour
    {
        #region EVENTS

        public static readonly UnityEvent<LetterVariant> Selected = new UnityEvent<LetterVariant>();

        #endregion
        
        public char Letter { get; private set; }

        private Animator _animator;
        private TextMeshPro _textMesh;

        private static bool _canSelect = true;

        #region AnimationNames

        private static readonly string WrongAnimation = "Wrong";
        private static readonly string CorrectAnimation = "Correct";

        #endregion
        
        #region MONO

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _textMesh = GetComponent<TextMeshPro>();
            _canSelect = true;
        }
        
        public void Init(char letter)
        {
            Letter = letter;
            _textMesh.text = Letter.ToString();
        }
        
        #endregion

        private void OnMouseDown()
        {
            if (!_canSelect)
            {
                return;
            }
            
            Selected?.Invoke(this);
            _canSelect = false;
            
            Debug.Log($"Clicked variant {Letter}");
        }
        
        //called from animation
        private void OnAnimationFinished()
        {
            _canSelect = true;
        }

        public void Wrong()
        {
            _animator.Play(WrongAnimation);
        }

        public void Correct()
        {
            _animator.Play(CorrectAnimation);
        }
    }
}