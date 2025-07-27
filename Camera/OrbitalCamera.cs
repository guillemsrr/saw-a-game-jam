// Copyright (c) Guillem Serra. All Rights Reserved.

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace GameBase.Camera
{
    public class OrbitalCamera : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 1f;
        [SerializeField] private float _zoomSpeed = 2.5f;
        [SerializeField] private float _minDistance = 2f;
        [SerializeField] private float _maxDistance = 30f;
        [SerializeField] private Vector3 _offset = new Vector3(0f, 2f, 0f);
        [SerializeField] Transform _defaultTarget;

        [Header("Input Actions")] [SerializeField]
        InputAction _cameraRotationStartAction;

        [SerializeField] InputAction _cameraRotationAction;
        [SerializeField] InputAction _cameraZoom;

        private Vector2 _currentRotation;
        private float _currentDistance;

        public Transform Target { get; set; }

        private void OnEnable()
        {
            _cameraRotationStartAction.Enable();
            _cameraRotationAction.Enable();
            _cameraZoom.Enable();
        }

        private void OnDisable()
        {
            _cameraRotationStartAction.Disable();
            _cameraRotationAction.Disable();
            _cameraZoom.Disable();
        }

        private void Start()
        {
            Target = _defaultTarget;

            _currentDistance = (_maxDistance + _minDistance) / 2f;
            _currentRotation = transform.eulerAngles;
        }

        private void Update()
        {
            if (Target == null) return;

            // Handle rotation

            if (_cameraRotationStartAction.inProgress)
            {
                _currentRotation += _cameraRotationAction.ReadValue<Vector2>() * _rotationSpeed;
            }

            // Handle zooming
            float scrollInput = _cameraZoom.ReadValue<float>();
            _currentDistance = Mathf.Clamp(_currentDistance - scrollInput * _zoomSpeed, _minDistance, _maxDistance);

            // Calculate camera position
            Vector3 direction = new Vector3(0f, 0f, -_currentDistance);
            Quaternion rotation = Quaternion.Euler(-_currentRotation.y, _currentRotation.x, 0);
            Vector3 targetPosition = Target.position + _offset;

            transform.position = targetPosition + rotation * direction;
            transform.LookAt(targetPosition);
        }
    }
}