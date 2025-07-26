// Copyright (c) Guillem Serra. All Rights Reserved.

using GameJamBase.UI.Presenters;
using GameJamBase.UI.View;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameJamBase.UI
{
    public class UIInstallerBase : MonoBehaviour
    {
        [SerializeField] private ButtonView _muteButtonView;

        private MutePresenter _mutePresenter;

        private void Start()
        {
            _mutePresenter = new MutePresenter(_muteButtonView);
        }
    }
}