using System.Collections.Generic;
using UnityEngine;

namespace SimplePieMenu
{
    [ExecuteInEditMode]
    public class PieMenuAudioSettingsHandler : MonoBehaviour
    {
        [SerializeField] List<AudioClip> mouseHoverClips;

        public List<AudioClip> MouseHoverClips
        {
            get { return mouseHoverClips; }
        }


        [SerializeField] List<AudioClip> mouseClickClips;

        public List<AudioClip> MouseClickClips
        {
            get { return mouseClickClips; }
        }

        public void PlayAudio(AudioSource audioSource)
        {
            audioSource.Play();
        }

        public void SetClip(AudioSource audioSource, AudioClip audioClip)
        {
            audioSource.clip = audioClip;
        }

        public void SetHoverClip(PieMenu pieMenu, int hoverClipIndex, float volume)
        {
            AudioSource audioSource;
            AudioClip hoverClip = GetClip(mouseHoverClips, hoverClipIndex);

            pieMenu.PieMenuInfo.SetMouseHoverClip(hoverClip);

            foreach (var menuItem in pieMenu.GetMenuItems())
            {
                audioSource = menuItem.Value.HoverAudioSource;
                SetClip(audioSource, hoverClip);
                ChangeVolume(audioSource, volume);
            }
        }

        public void SetClickClip(PieMenu pieMenu, int clickClipIndex, float volume)
        {
            AudioSource audioSource = pieMenu.PieMenuElements.MouseClickAudioSource;
            AudioClip clickClip = GetClip(mouseClickClips, clickClipIndex);

            pieMenu.PieMenuInfo.SetMouseClickClip(clickClip);
            SetClip(audioSource, clickClip);
            ChangeVolume(audioSource, volume);
        }

        public AudioClip GetClip(List<AudioClip> audioClips, int clipIndex)
        {
            AudioClip clip;

            if (clipIndex >= 0 && clipIndex < audioClips.Count)
            {
                clip = audioClips[clipIndex];
            }
            else
                clip = audioClips[0];


            return clip;
        }

        public void ChangeVolume(AudioSource audioSource, float volume)
        {
            audioSource.volume = volume;
        }
    }
}
