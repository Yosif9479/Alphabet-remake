using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Restart : MonoBehaviour
{
    private Button _button;
    private int _currentSceneId;

    private void Awake()
    {
        _button = GetComponent<Button>();

        _currentSceneId = SceneManager.GetActiveScene().buildIndex;
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
        SceneManager.LoadScene(_currentSceneId);
    }
}
