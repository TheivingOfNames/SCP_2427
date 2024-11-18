using BepInEx.Logging;
using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UIElements;
using UnityEngine.VFX;
using static SCP_2427.Plugin;

namespace SCP_2427.SCP
{
    internal class _2427_5 : NetworkBehaviour
    {
        private static ManualLogSource logger = LoggerInstance;
        private PlayerControllerB localPlayer { get { return StartOfRound.Instance.localPlayerController; } }
        Vector3 pos;
        public static Dictionary<PlayerControllerB, double> Playercount = new Dictionary<PlayerControllerB, double>();
        public GameObject leadBall;
        private AudioSource cloudSound;
        public AudioClip pipe;
        public AudioClip normal;
        public void Start()
        {
            Vector3 pos = transform.position;
            cloudSound = GetComponent<AudioSource>();

        }
        public void Update()
        {

            foreach (PlayerControllerB player in RoundManager.Instance.playersManager.allPlayerScripts)
            {
                if (player.HasLineOfSightToPosition(pos))
                {
                    checkingForEyes(player, 0);
                }

            }

        }

        private IEnumerator checkingForEyes(PlayerControllerB player, int count)
        {
            int check = count;
            yield return new WaitForSeconds(0.5f);
            count++;
            if (count == 6)
            {
                Vector3 position = player.transform.position;
                Quaternion quaternion = player.transform.rotation;
                
                Instantiate(leadBall, position, quaternion);
                cloudSound.PlayOneShot(pipe, 100f);
                player.DamagePlayer(100, false);
            }
            if (player.HasLineOfSightToPosition(pos))
            {
                checkingForEyes(player, check);
            }
        
        }
        
    }
}
