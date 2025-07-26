// Copyright (c) Guillem Serra. All Rights Reserved.

using GameJamBase.UI.Presenters;
using GameJamBase.UI.View;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameJamBase.UI
{
    public class UIInstallerBase: MonoBehaviour
    {
        [SerializeField] private InputActionAsset[] _inputActionAssets;
        [SerializeField] private ButtonView _muteButtonView;

        private MutePresenter _mutePresenter;
        
        protected void OnEnable()
        {
            foreach (InputActionAsset inputActionAsset in _inputActionAssets)
            {
                inputActionAsset.Enable();
            }
        }

        protected void OnDisable()
        {
            foreach (InputActionAsset inputActionAsset in _inputActionAssets)
            {
                inputActionAsset.Disable();
            }
        }

        private void Start()
        {
            _mutePresenter = new MutePresenter(_muteButtonView);
        }
    }
}