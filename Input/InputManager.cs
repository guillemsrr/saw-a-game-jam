// Copyright (c) Guillem Serra. All Rights Reserved.

using System;
using GameJamBase.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameJamBase.Input
{
    public class InputManager : Singleton<InputManager>
    {
        [SerializeField] private InputActionAsset _inputActionAsset;

        public InputAction MuteAction { get; private set; }

        public override void Awake()
        {
            base.Awake();

            foreach (InputActionMap inputActionMap in _inputActionAsset.actionMaps)
            {
                inputActionMap.Enable();
            }

            MuteAction = InputSystem.actions.FindAction("Mute");
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _inputActionAsset.FindActionMap("Player").Disable();
        }
    }
}