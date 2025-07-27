// Copyright (c) Guillem Serra. All Rights Reserved.

using System;
using GameJamBase.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameBase.Input
{
    public class InputManager : Singleton<InputManager>
    {
        private InputAction _leftClickAction;
        private InputAction _rightClickAction;

        public Action<GameObject> OnGameObjectClick;
        public Action OnAnyClick;

        private void Awake()
        {
            _leftClickAction = InputSystem.actions.FindAction("Click");
            _rightClickAction = InputSystem.actions.FindAction("RightClick");
        }

        private void OnEnable()
        {
            _leftClickAction.performed += HandleClick;
            _rightClickAction.performed += HandleRightClick;
        }

        private void OnDisable()
        {
            _leftClickAction.performed -= HandleClick;
            _rightClickAction.performed -= HandleRightClick;
        }

        private void HandleClick(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            Vector2 mousePosition = UnityEngine.Input.mousePosition;
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            GameObject clickedObject = null;
            if (Physics.Raycast(ray, out hit))
            {
                clickedObject = hit.collider.gameObject;
            }

            OnGameObjectClick?.Invoke(clickedObject);
            OnAnyClick?.Invoke();
        }

        private void HandleRightClick(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            // Similar raycast logic for right click if needed
        }
    }
}