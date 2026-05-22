using System;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Vivox;
using UnityEngine.UI;
using TMPro;

public class VivoxSetup : MonoBehaviour
{
    [Header("Vivox Settings")]
    public string voiceChannelName = "StoryTellerRoom";
    public string playerNamePrefix = "Player";

    [Header("Mute Button UI")]
    public Button muteButton;
    public TMP_Text muteButtonText;
    public Image muteButtonImage;
    public Sprite micOnIcon;
    public Sprite micMutedIcon;

    private bool isMicMuted = false;
    private bool isVivoxReady = false;
    private bool isJoiningVivox = false;

    private string testPlayerName;

    // Static protection so Vivox does not join the same channel twice
    private static bool globalVivoxJoined = false;
    private static bool globalVivoxJoining = false;
    private static VivoxSetup instance;

    private async void Awake()
    {
        // Prevent duplicate Vivox managers across scene loads
        if (instance != null && instance != this)
        {
            Debug.LogWarning("Duplicate VivoxSetup found. Destroying this copy.");
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private async void Start()
    {
        SetupMuteButton();
        UpdateMuteButtonText();

        await InitializeVivox();
    }

    private void SetupMuteButton()
    {
        if (muteButton != null)
        {
            muteButton.onClick.RemoveListener(ToggleMute);
            muteButton.onClick.AddListener(ToggleMute);
        }
        else
        {
            Debug.LogWarning("Mute Button is not assigned in the Inspector.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMute();
        }
    }

    private async Task InitializeVivox()
    {
        if (isJoiningVivox || isVivoxReady || globalVivoxJoining || globalVivoxJoined)
        {
            Debug.LogWarning("Vivox is already joining or already connected. Skipping duplicate join.");

            isVivoxReady = true;
            return;
        }

        isJoiningVivox = true;
        globalVivoxJoining = true;

        try
        {
            testPlayerName = playerNamePrefix + "_" + UnityEngine.Random.Range(1000, 9999);
            Debug.Log("Starting Vivox test as: " + testPlayerName);

            if (UnityServices.State != ServicesInitializationState.Initialized)
            {
                await UnityServices.InitializeAsync();
                Debug.Log("Unity Services initialized successfully.");
            }

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                Debug.Log("Authentication signed in successfully.");
            }

            Debug.Log("Vivox setup reached authentication stage.");

            await VivoxService.Instance.InitializeAsync();
            Debug.Log("Vivox initialized successfully.");

            await VivoxService.Instance.JoinGroupChannelAsync(
                voiceChannelName,
                ChatCapability.AudioOnly
            );

            isVivoxReady = true;
            globalVivoxJoined = true;

            Debug.Log(testPlayerName + " joined Vivox voice channel: " + voiceChannelName);
        }
        catch (InvalidOperationException e)
        {
            Debug.LogWarning("Vivox was already in this channel. Treating as connected: " + e.Message);

            isVivoxReady = true;
            globalVivoxJoined = true;
        }
        catch (Exception e)
        {
            Debug.LogError("Vivox setup failed: " + e.Message);

            isVivoxReady = false;
            globalVivoxJoined = false;
        }
        finally
        {
            isJoiningVivox = false;
            globalVivoxJoining = false;
        }
    }

    public void ToggleMute()
    {
        if (!isVivoxReady)
        {
            Debug.LogWarning("Vivox is not ready yet. Wait until the voice channel is joined.");
            return;
        }

        try
        {
            if (!isMicMuted)
            {
                VivoxService.Instance.MuteInputDevice();
                isMicMuted = true;
                Debug.Log(testPlayerName + " microphone muted.");
            }
            else
            {
                VivoxService.Instance.UnmuteInputDevice();
                isMicMuted = false;
                Debug.Log(testPlayerName + " microphone unmuted.");
            }

            UpdateMuteButtonText();
        }
        catch (Exception e)
        {
            Debug.LogWarning("Mute toggle failed: " + e.Message);
        }
    }

    private void UpdateMuteButtonText()
    {
        if (muteButtonText != null)
        {
            muteButtonText.text = isMicMuted ? "Unmute" : "Mute";
        }

        if (muteButtonImage != null)
        {
            Sprite targetIcon = isMicMuted ? micMutedIcon : micOnIcon;

            if (targetIcon != null)
            {
                muteButtonImage.sprite = targetIcon;
            }
        }

        if (muteButtonText == null && muteButtonImage == null)
        {
            Debug.LogWarning("Mute Button Text/Image is not assigned in the Inspector.");
        }
    }

    public async Task LeaveVoiceChannel()
    {
        if (!globalVivoxJoined)
        {
            return;
        }

        try
        {
            await VivoxService.Instance.LeaveAllChannelsAsync();

            isVivoxReady = false;
            isMicMuted = false;
            globalVivoxJoined = false;

            Debug.Log(testPlayerName + " left Vivox voice channel successfully.");
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed to leave voice channel: " + e.Message);
        }
    }

    private async void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }

        // Only leave when this real manager is destroyed
        if (globalVivoxJoined)
        {
            await LeaveVoiceChannel();
        }
    }
}