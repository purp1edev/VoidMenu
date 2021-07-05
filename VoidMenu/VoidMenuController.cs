using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BS_Utils.Utilities;

namespace VoidMenu
{
    /// <summary>
    /// Monobehaviours (scripts) are added to GameObjects.
    /// For a full list of Messages a Monobehaviour can receive from the game, see https://docs.unity3d.com/ScriptReference/MonoBehaviour.html.
    /// </summary>
    public class VoidMenuController : MonoBehaviour
    {
        public static VoidMenuController Instance { get; private set; }

        #region Monobehaviour Messages
        // Only ever called once, mainly used to initialize variables.
        private void Awake()
        {
            // For this particular MonoBehaviour, we only want one instance to exist at any time, so store a reference to it in a static property
            //   and destroy any that are created while one already exists.
            if (Instance != null)
            {
                Plugin.Log?.Warn($"Instance of {GetType().Name} already exists, destroying.");
                GameObject.DestroyImmediate(this);
                return;
            }
            GameObject.DontDestroyOnLoad(this); // Don't destroy this object on scene changes
            Instance = this;
            Plugin.Log?.Debug($"{name}: Awake()");

            BSEvents.lateMenuSceneLoadedFresh += LateMenuSceneLoaded;
        }
        // Called when the script is being destroyed.
        private void OnDestroy()
        {
            Plugin.Log?.Debug($"{name}: OnDestroy()");
            if (Instance == this)
                Instance = null; // This MonoBehaviour is being destroyed, so set the static instance property to null.

        }
        #endregion

        void LateMenuSceneLoaded(ScenesTransitionSetupDataSO setupDataSO)
        {
            if (Configuration.PluginConfig.Instance.enabled)
            {
                GameObject.Find("MenuEnvironmentManager").SetActive(false);
            }
        }
    }
}
