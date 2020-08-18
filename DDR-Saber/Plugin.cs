using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using UnityEngine.SceneManagement;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;
using DDR_Saber.HarmonyPatches;
using BS_Utils.Utilities;
using BS_Utils.Gameplay;

namespace DDR_Saber
{

    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin instance { get; private set; }
        internal static string Name => "DDR-Saber";

        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        public void Init(IPALogger logger)
        {
            instance = this;
            Logger.log = logger;
            Logger.log.Debug("Logger initialized.");
        }

        [OnStart]
        public void OnApplicationStart()
        {
            Logger.log.Debug("OnApplicationStart");
            DDRSaberPatches.ApplyHarmonyPatches();
            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        }

        private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
        {
            if(arg1.name == "GameCore")
            {
                CustomNotes.Data.CustomNote activeNote = CustomNotes.Utilities.NoteAssetLoader.CustomNoteObjects[CustomNotes.Utilities.NoteAssetLoader.SelectedNote];
                if (activeNote.Descriptor.NoteName.ToLower().Contains("ddr notes"))
                {
                    ScoreSubmission.DisableSubmission("DDR Saber");
                }
            }
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            Logger.log.Debug("OnApplicationQuit");

        }
    }
}
