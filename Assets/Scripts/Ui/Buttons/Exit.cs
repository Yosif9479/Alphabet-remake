using UnityEngine;
using UnityEngine.UI;

namespace Ui.Buttons
{
    [RequireComponent(typeof(Button))]
    public class Exit : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();      
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            Application.Quit();
        }
    }
}

