using System.Collections;
using System.Collections.Generic;
using Gamekit3D;
using UnityEngine;
using UnityEngine.Playables;
#if UNITY_EDITOR
using AK.Wwise;
using UnityEditor;
#endif

namespace Gamekit3D
{
    public class StartUI : MonoBehaviour
    {
        public bool alwaysDisplayMouse;
        public GameObject pauseCanvas;
        public GameObject optionsCanvas;
        public GameObject controlsCanvas;
        public GameObject audioCanvas;
        
        [Header("Wwise Events")]
        public AK.Wwise.Event Play_UI_PauseOpen;
        public AK.Wwise.Event Play_UI_PauseClose;
        public AK.Wwise.Event Play_UI_Hover;
        public AK.Wwise.Event Play_UI_Click;
        public GameObject AudioSource;

        protected bool m_InPause;
        protected PlayableDirector[] m_Directors;

        void Start()
        {
            if (!alwaysDisplayMouse)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            m_Directors = FindObjectsOfType<PlayableDirector> ();
            if (AudioSource == null)
                AudioSource = gameObject;
        }

        public void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
		    Application.Quit();
#endif
        }

        public void ExitPause()
        {
            m_InPause = true;
            SwitchPauseState();
        }

        public void RestartLevel()
        {
            m_InPause = true;
            SwitchPauseState();
            SceneController.RestartZone();
        }

        void Update()
        {
            if (PlayerInput.Instance != null && PlayerInput.Instance.Pause)
            {
                SwitchPauseState();
            }
        }

        protected void SwitchPauseState()
        {
            if (m_InPause && Time.timeScale > 0 || !m_InPause && ScreenFader.IsFading)
                return;

            if (!alwaysDisplayMouse)
            {
                Cursor.lockState = m_InPause ? CursorLockMode.Locked : CursorLockMode.None;
                Cursor.visible = !m_InPause;
            }

            for (int i = 0; i < m_Directors.Length; i++)
            {
                if (m_Directors[i].state == PlayState.Playing && !m_InPause)
                {
                    m_Directors[i].Pause ();
                }
                else if(m_Directors[i].state == PlayState.Paused && m_InPause)
                {
                    m_Directors[i].Resume ();
                }
            }
            
            if(!m_InPause)
                CameraShake.Stop ();

            if (m_InPause)
                PlayerInput.Instance.GainControl();
            else
                PlayerInput.Instance.ReleaseControl();
            // Joue le son AVANT de changer le timeScale
            if (!m_InPause) // On va mettre en pause
            {
                PlayPauseOpen();
            }
            else // On va sortir de la pause
            {
                PlayPauseClose();
            }

            Time.timeScale = m_InPause ? 1 : 0;

            if (pauseCanvas)
                pauseCanvas.SetActive(!m_InPause);

            if (optionsCanvas)
                optionsCanvas.SetActive(false);

            if (controlsCanvas)
                controlsCanvas.SetActive(false);

            if (audioCanvas)
                audioCanvas.SetActive(false);

            m_InPause = !m_InPause;
        }
        private void PlayPauseOpen()
        {
            if (Play_UI_PauseOpen != null && AudioSource != null)
            {
                Play_UI_PauseOpen.Post(AudioSource);
                Debug.Log("[StartUI] Pause Open");
            }
        }
        
        private void PlayPauseClose()
        {
            if (Play_UI_PauseClose != null && AudioSource != null)
            {
                Play_UI_PauseClose.Post(AudioSource);
                Debug.Log("[StartUI] Pause Close");
            }
        }
        
        public void PlayUIHover()
        {
            if (Play_UI_Hover != null && AudioSource != null)
            {
                Play_UI_Hover.Post(AudioSource);
                Debug.Log("[StartUI] UI Hover");
            }
        }

        public void PlayUIClick()
        {
            if (Play_UI_Click != null && AudioSource != null)
            {
                Play_UI_Click.Post(AudioSource);
                Debug.Log("[StartUI] UI Click");
            }
        }
    }
}
