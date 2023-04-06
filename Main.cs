using BepInEx;
using HarmonyLib;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MuteableJustinRun
{
    [BepInPlugin(ModId, "Mutable Justin Run", "1.0.0")]
    [BepInProcess("Justin run 2.exe")]
    public class Main : BaseUnityPlugin
    {
        private const string ModId = "scyye.mutablejustinrun";

        private Player player;
        private GameObject musicPlayer;

        private void Awake()
        {
            SceneManager.activeSceneChanged += ChangedActiveScene;
            ToggleSound();
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= ChangedActiveScene;
        }

        private void ChangedActiveScene(Scene current, Scene next)
        {
            preInit();
            System.Threading.Thread.Sleep(2000);
            ToggleSound();
        }

        private void preInit()
        {
            player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
            musicPlayer = GameObject.Find("music player");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                ToggleSound();
            }
        }

        private void ToggleSound()
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
            }
            if (musicPlayer == null)
            {
                musicPlayer = GameObject.Find("music player");
            }
            if (player != null && musicPlayer != null)
            {
                musicPlayer.SetActive(!musicPlayer.activeSelf);
                player.playerSoundSource.mute = !player.playerSoundSource.mute;
            }
        }
    }
}
