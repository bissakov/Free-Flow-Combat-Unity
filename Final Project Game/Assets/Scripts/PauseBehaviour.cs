using UnityEngine;
using UnityEngine.InputSystem;

public class PauseBehaviour : MonoBehaviour
{
    [SerializeField] GameObject _pauseMenu;

    private PlayerInput _input;
    private bool _isMenuVisible = false;
    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }

    private void Start()
    {
    }

    public void OnPause()
    {
        Time.timeScale = (Time.timeScale == 0) ? 1 : 0;

        _isMenuVisible = !_isMenuVisible;

        _pauseMenu.SetActive(_isMenuVisible);

        
    }
}
