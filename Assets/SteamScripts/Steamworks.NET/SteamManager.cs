// The SteamManager is designed to work with Steamworks.NET
// This file is released into the public domain.
// Where that dedication is not recognized you are granted a perpetual,
// irrevocable license to copy and modify this file as you see fit.
//
// Version: 1.0.13

/*#if !(UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX || STEAMWORKS_WIN || STEAMWORKS_LIN_OSX)
#define DISABLESTEAMWORKS
#endif
*/

using UnityEngine;
using System.Globalization;

using System.Threading;

#if !DISABLESTEAMWORKS
using System.Collections;
using Steamworks;
#endif

//
// The SteamManager provides a base implementation of Steamworks.NET on which you can build upon.
// It handles the basics of starting up and shutting down the SteamAPI for use.
//
[DisallowMultipleComponent]
public class SteamManager : MonoBehaviour {
#if !DISABLESTEAMWORKS
	protected static bool s_EverInitialized = false;

	protected static SteamManager s_instance;
	protected static SteamManager Instance {
		get {
			if (s_instance == null) {
				return new GameObject("SteamManager").AddComponent<SteamManager>();
			}
			else {
				return s_instance;
			}
		}
	}

	protected bool m_bInitialized = false;
	public static bool Initialized {
		get {
			return Instance.m_bInitialized;
		}
	}

	protected SteamAPIWarningMessageHook_t m_SteamAPIWarningMessageHook;

	[AOT.MonoPInvokeCallback(typeof(SteamAPIWarningMessageHook_t))]
	protected static void SteamAPIDebugTextHook(int nSeverity, System.Text.StringBuilder pchDebugText) {
		Debug.LogWarning(pchDebugText);
	}

#if UNITY_2019_3_OR_NEWER
	// In case of disabled Domain Reload, reset static members before entering Play Mode.
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void InitOnPlayMode()
	{
		s_EverInitialized = false;
		s_instance = null;
	}
#endif

    void OnDisable()
    {
        Debug.LogWarning("SteamManager DISABLED: " + gameObject.name);
    }

    public static void EnforceInvariantCulture()
    {
        // Force the main application thread to remain uniform globally
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;


        // Ensure the current active running thread matches
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;
    }

    protected virtual void Awake()
    {

        if (s_instance != null && s_instance != this)
        {
            Debug.LogWarning("SteamManager duplicate found, destroying this instance.");
            Destroy(gameObject);
            return;
        }

        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);

        SteamManager.EnforceInvariantCulture();

        s_instance = this;
        DontDestroyOnLoad(gameObject);

        if (s_EverInitialized)
        {
            throw new System.Exception("Tried to Initialize the SteamAPI twice in one session!");
        }

        if (!Packsize.Test())
        {
            Debug.LogError("[Steamworks.NET] Packsize Test failed.");
        }

        if (!DllCheck.Test())
        {
            Debug.LogError("[Steamworks.NET] DllCheck Test failed.");
        }

        try
        {
            if (SteamAPI.RestartAppIfNecessary(new AppId_t(3813150)))
            {
                Debug.Log("[Steamworks.NET] Restarting via Steam.");
                Application.Quit();
                return;
            }
        }
        catch (System.DllNotFoundException e)
        {
            Debug.LogError("[Steamworks.NET] Missing Steam DLL: " + e);
            Application.Quit();
            return;
        }

        m_bInitialized = SteamAPI.Init();
        Debug.Log("SteamAPI Init success? " + m_bInitialized);

        if (!m_bInitialized)
        {
            Debug.LogError("[Steamworks.NET] SteamAPI_Init() failed.");
            return;
        }

        SteamUserStats.RequestCurrentStats();

        s_EverInitialized = true;
    }

    // This should only ever get called on first load and after an Assembly reload, You should never Disable the Steamworks Manager yourself.
    protected virtual void OnEnable() {
		if (s_instance == null) {
			s_instance = this;
		}

		if (!m_bInitialized) {
			return;
		}

		if (m_SteamAPIWarningMessageHook == null) {
			// Set up our callback to receive warning messages from Steam.
			// You must launch with "-debug_steamapi" in the launch args to receive warnings.
			m_SteamAPIWarningMessageHook = new SteamAPIWarningMessageHook_t(SteamAPIDebugTextHook);
			SteamClient.SetWarningMessageHook(m_SteamAPIWarningMessageHook);
		}
	}

	// OnApplicationQuit gets called too early to shutdown the SteamAPI.
	// Because the SteamManager should be persistent and never disabled or destroyed we can shutdown the SteamAPI here.
	// Thus it is not recommended to perform any Steamworks work in other OnDestroy functions as the order of execution can not be garenteed upon Shutdown. Prefer OnDisable().
	protected virtual void OnDestroy() {
        Debug.LogWarning("SteamManager Destroy: " + gameObject.name);

        if (s_instance != this) {
			return;
		}

		s_instance = null;

		if (!m_bInitialized) {
			return;
		}

		SteamAPI.Shutdown();
	}

	protected virtual void Update() {
		if (!m_bInitialized) {
			return;
		}

		// Run Steam client callbacks
		SteamAPI.RunCallbacks();
	}

    public static void UnlockAchievement(string achievementId)
    {
        if (!SteamManager.Initialized)
        {
            Debug.LogWarning("Steam is not initialized.");
            return;
        }

        try
        {
            bool success = SteamUserStats.SetAchievement(achievementId);
            if (!success)
            {
                Debug.LogWarning($"Failed to set achievement {achievementId}");
                return;
            }

            SteamUserStats.StoreStats();
            Debug.Log($"Achievement '{achievementId}' unlocked!");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Exception while unlocking achievement {achievementId}: {e.Message}");
        }
    }

#else
	public static bool Initialized {
		get {
			return false;
		}
	}
#endif // !DISABLESTEAMWORKS
}
