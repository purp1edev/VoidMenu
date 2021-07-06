using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using BeatSaberMarkupLanguage.Settings;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;
using BS_Utils.Utilities;

namespace VoidMenu
{

    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        public void Init(IPALogger logger, IPA.Config.Config conf)
        {
            Instance = this;
            Log = logger;

            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            BSMLSettings.instance.AddSettingsMenu("VoidMenu", "VoidMenu.Configuration.settings.bsml", Configuration.PluginConfig.Instance);

            Log.Info("VoidMenu initialized.");
            BSEvents.lateMenuSceneLoadedFresh += LateMenuSceneLoaded;
        }

        [OnStart]
        public void OnApplicationStart()
        {
            Log.Debug("OnApplicationStart");

        }
        void LateMenuSceneLoaded(ScenesTransitionSetupDataSO setupDataSO)
        {
            if (Configuration.PluginConfig.Instance.enabled)
            {
                GameObject.Find("MenuEnvironmentManager").SetActive(false);
            }
        }
        [OnExit]
        public void OnApplicationQuit()
        {
            Log.Debug("OnApplicationQuit");

        }
    }
}
