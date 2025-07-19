﻿// Copyright (c) Guillem Serra. All Rights Reserved.

using System.Collections;
using GameJamBase.Core;
using UnityEngine;

namespace GameJamBase.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] private AudioClip _menuAudioClip;
        [SerializeField] private AudioClip _inGameAudioClip;

        private AudioSource _menuAudioSource;
        private AudioSource _inGameAudioSource;

        [SerializeField] private float _fadeDuration = 5f;

        Coroutine _fadeOutMusicCoroutine;
        Coroutine _fadeInMusicCoroutine;

        public bool IsMuted { get; private set; }

        public override void Awake()
        {
            _menuAudioSource = gameObject.AddComponent<AudioSource>();
            _inGameAudioSource = gameObject.AddComponent<AudioSource>();

            _menuAudioSource.loop = true;
            _menuAudioSource.volume = 0f;

            _inGameAudioSource.loop = true;
            _inGameAudioSource.volume = 0f;

            // Prewarm both clips
            _menuAudioSource.clip = _menuAudioClip;
            _menuAudioSource.Play();
            _menuAudioSource.Pause();

            _inGameAudioSource.clip = _inGameAudioClip;
            _inGameAudioSource.Play();
            _inGameAudioSource.Pause();
        }

        private void Start()
        {
            PlayMenuMusic();
        }

        public void PlayMenuMusic()
        {
            if (_fadeOutMusicCoroutine != null)
            {
                StopCoroutine(_fadeOutMusicCoroutine);
            }

            if (_fadeInMusicCoroutine != null)
            {
                StopCoroutine(_fadeInMusicCoroutine);
            }

            _fadeOutMusicCoroutine = StartCoroutine(FadeOut(_inGameAudioSource));
            _fadeInMusicCoroutine = StartCoroutine(FadeIn(_menuAudioSource, _menuAudioClip, 0.35f));
        }

        public void PlayInGameMusic()
        {
            if (_fadeOutMusicCoroutine != null)
            {
                StopCoroutine(_fadeOutMusicCoroutine);
            }

            if (_fadeInMusicCoroutine != null)
            {
                StopCoroutine(_fadeInMusicCoroutine);
            }

            _fadeOutMusicCoroutine = StartCoroutine(FadeOut(_menuAudioSource));
            _fadeInMusicCoroutine = StartCoroutine(FadeIn(_inGameAudioSource, _inGameAudioClip, 0.2f));
        }

        private IEnumerator FadeIn(AudioSource audioSource, AudioClip newClip, float volume)
        {
            audioSource.clip = newClip;
            audioSource.Play();

            yield return FadeRoutine(audioSource, volume);
        }

        private IEnumerator FadeOut(AudioSource audioSource)
        {
            yield return FadeRoutine(audioSource, 0f);
        }

        private IEnumerator FadeRoutine(AudioSource audioSource, float fadeTarget)
        {
            float startVolume = audioSource.volume;
            if (Mathf.Approximately(startVolume, fadeTarget))
            {
                yield break;
            }

            float elapsedTime = 0f;

            while (elapsedTime < _fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float percentageComplete = elapsedTime / _fadeDuration;
                audioSource.volume = Mathf.Lerp(startVolume, fadeTarget, percentageComplete);
                audioSource.mute = IsMuted;
                yield return null;
            }

            audioSource.volume = fadeTarget;
            if (fadeTarget == 0f)
            {
                audioSource.Stop();
            }
        }

        public void ToggleMute()
        {
            IsMuted = !IsMuted;
            _menuAudioSource.mute = IsMuted;
            _inGameAudioSource.mute = IsMuted;
        }
    }
}