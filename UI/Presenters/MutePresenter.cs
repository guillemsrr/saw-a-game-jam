// Copyright (c) Guillem Serra. All Rights Reserved.

using GameJamBase.Audio;
using GameJamBase.Input;
using GameJamBase.UI.View;
using UnityEngine.InputSystem;

namespace GameJamBase.UI.Presenters
{
    public class MutePresenter
    {
        private readonly ButtonView _buttonView;

        public MutePresenter(ButtonView spatialButton)
        {
            _buttonView = spatialButton;
            _buttonView.Button.onClick.AddListener(OnClick);

            InputManager.Instance.MuteAction.performed += OnMute;
        }

        private void OnMute(InputAction.CallbackContext obj)
        {
            OnClick();
        }

        private void OnClick()
        {
            AudioManager.Instance.ToggleMute();

            if (AudioManager.Instance.IsMuted)
            {
                _buttonView.Text.SetText("Music Off (M)");
            }
            else
            {
                _buttonView.Text.SetText("Music On (M)");
            }
        }
    }
}