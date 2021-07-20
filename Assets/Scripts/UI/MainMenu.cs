using System;
using Asteroids.Input;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Asteroids.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _continueBtn;
        [SerializeField] private Button _newGameBtn;
        [SerializeField] private Button _controlBtn;
        [SerializeField] private Button _exitBtn;
        [SerializeField] private Text _controlBtnText;
        [SerializeField] private Player _player;
        private bool _isOpened = true;
        private bool _isGameStarted;

        private void Awake()
        {
            if (!PlayerPrefs.HasKey(NamesManager.CONTROL_TYPE_KEY))
                _controlBtnText.text = NamesManager.CONTROL_KEYBOARD;
            else
            {
                _controlBtnText.text = PlayerPrefs.GetInt(NamesManager.CONTROL_TYPE_KEY) == (int) InputType.MouseAndKeyBoard ? 
                    NamesManager.CONTROL_KEYBOARD:
                    NamesManager.CONTROL_KEYBOARD_AND_MOUSE;
            }
            _newGameBtn.onClick.AddListener(NewGameButtonHandler);
            _continueBtn.onClick.AddListener(ContinueBtnHandler);
            _controlBtn.onClick.AddListener(ControlBtnHandler);
            _exitBtn.onClick.AddListener(ExitBtnHandler);
        }

        public void ToggleMenu(bool isKeyPressed)
        {
            if (isKeyPressed)
            {
                _isOpened = _isOpened ? false : isKeyPressed;
                gameObject.SetActive(_isOpened);
                if (_isOpened)
                    Time.timeScale = 0;
                else 
                    Time.timeScale = 1;
            }    
        }

        private void NewGameButtonHandler()
        {
            if (!_isGameStarted)
            {
                UnPauseGame();
                _isGameStarted = true;
                _continueBtn.gameObject.SetActive(true);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        private void ContinueBtnHandler()
        {
            UnPauseGame();
        }

        private void ControlBtnHandler()
        {
            _controlBtnText.text = _controlBtnText.text.Equals(NamesManager.CONTROL_KEYBOARD)
                ? NamesManager.CONTROL_KEYBOARD_AND_MOUSE
                : NamesManager.CONTROL_KEYBOARD;

            var inputType = _controlBtnText.text.Equals(NamesManager.CONTROL_KEYBOARD)
                ? InputType.MouseAndKeyBoard
                : InputType.KeyBoard;
            
            PlayerPrefs.SetInt(NamesManager.CONTROL_TYPE_KEY, (int)inputType);
            _player.ChangeControlSchemeOnyTheFly();
        }

        private void ExitBtnHandler()
        {
            Application.Quit();
        }

        private void UnPauseGame()
        {
            Time.timeScale = 1;
            _isOpened = false;
            gameObject.SetActive(_isOpened);
        }
    }
}