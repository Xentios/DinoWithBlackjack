using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SimplePieMenu
{
    [CustomEditor(typeof(PieMenuAudioSettings))]
    public class PieMenuAudioSettingsEditor : Editor
    {
        PieMenuAudioSettings audioSettings;

        private void OnEnable()
        {
            audioSettings = (PieMenuAudioSettings)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            PieMenuAudioSettingsHandler settingsHandler = audioSettings.AudioHandler;
            DrawMouseHoverSection(settingsHandler);
            DrawMouseClickSection(settingsHandler);
        }

        private void DrawMouseHoverSection(PieMenuAudioSettingsHandler settingsHandler)
        {
            GUIContent mouseHover = new("Mouse Hover");

            audioSettings.CreateHoverDropdownList(
                EditorGUILayout.Popup(
                    mouseHover,
                    audioSettings.HoverClipsDropdownList,
                    audioSettings.MouseHoverClipNames.ToArray()));


            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Play Hover Sound"))
            {
                TestSound(
                    settingsHandler,
                    settingsHandler.MouseHoverClips,
                    audioSettings.HoverClipsDropdownList);
            }

            if (GUILayout.Button("Set Hover Sound"))
            {
                settingsHandler.SetHoverClip(
                    audioSettings.PieMenu,
                    audioSettings.HoverClipsDropdownList,
                    audioSettings.Volume);
            }

            GUILayout.EndHorizontal();
        }

        private void DrawMouseClickSection(PieMenuAudioSettingsHandler settingsHandler)
        {
            GUIContent mouseClick = new("Mouse Click");

            audioSettings.CreateClickDropdownList(
                EditorGUILayout.Popup(
                    mouseClick,
                    audioSettings.ClickClipsDropdownList,
                    audioSettings.MouseClickClipNames.ToArray()));

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Play Click Sound"))
            {
                TestSound(
                    settingsHandler,
                    settingsHandler.MouseClickClips,
                    audioSettings.ClickClipsDropdownList);
            }

            if (GUILayout.Button("Set Click Sound"))
            {
                settingsHandler.SetClickClip(
                    audioSettings.PieMenu,
                    audioSettings.ClickClipsDropdownList,
                    audioSettings.Volume);
            }

            GUILayout.EndHorizontal();
        }

        private void TestSound(PieMenuAudioSettingsHandler settingsHandler, List<AudioClip> audioClips, int clipIndex)
        {
            AudioSource audioSource = PieMenuShared.References.TestAudioSource;
            AudioClip audioClip = settingsHandler.GetClip(audioClips, clipIndex);

            settingsHandler.SetClip(audioSource, audioClip);
            settingsHandler.ChangeVolume(audioSource, audioSettings.Volume);
            settingsHandler.PlayAudio(audioSource);
        }
    }
}
