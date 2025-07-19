using System.Collections.Generic;
using UnityEngine;

namespace SimplePieMenu
{
    [ExecuteInEditMode]
    public class PieMenuAudioSettings : MonoBehaviour
    {
        [Range(0f, 1f)] [SerializeField] float volume = 1f;

        public float Volume
        {
            get { return volume; }
        }

        public int HoverClipsDropdownList { get; private set; }
        public int ClickClipsDropdownList { get; private set; }

        public List<string> MouseHoverClipNames { get; private set; } = new();
        public List<string> MouseClickClipNames { get; private set; } = new();

        public PieMenu PieMenu { get; private set; }
        public PieMenuAudioSettingsHandler AudioHandler { get; private set; }


        private void OnEnable()
        {
            volume = 1f;
            PieMenu = GetComponent<PieMenu>();
            PieMenu.OnComponentsInitialized += InitializeAudioSettings;
        }

        private void OnDisable()
        {
            PieMenu.OnComponentsInitialized -= InitializeAudioSettings;
        }

        public void CreateHoverDropdownList(int list)
        {
            HoverClipsDropdownList = list;
        }

        public void CreateClickDropdownList(int list)
        {
            ClickClipsDropdownList = list;
        }

        private void InitializeAudioSettings()
        {
            AudioHandler = PieMenuShared.References.AudioSettingsHandler;

            InitializeSoundList(AudioHandler.MouseHoverClips, MouseHoverClipNames);
            InitializeSoundList(AudioHandler.MouseClickClips, MouseClickClipNames);

            SelectRightSoundsInTheLists();
        }

        private void InitializeSoundList(List<AudioClip> sourceList, List<string> targetList)
        {
            foreach (AudioClip audioClip in sourceList)
            {
                targetList.Add(audioClip.name);
            }
        }

        private void SelectRightSoundsInTheLists()
        {
            AudioClip hoverClip = PieMenu.GetTemplate().GetComponent<AudioSource>().clip;
            PieMenu.PieMenuInfo.SetMouseHoverClip(hoverClip);
            HoverClipsDropdownList = AudioHandler.MouseHoverClips.IndexOf(hoverClip);

            AudioClip clickClip = PieMenu.PieMenuElements.MouseClickAudioSource.clip;
            PieMenu.PieMenuInfo.SetMouseClickClip(clickClip);
            ClickClipsDropdownList = AudioHandler.MouseClickClips.IndexOf(clickClip);
        }
    }
}
