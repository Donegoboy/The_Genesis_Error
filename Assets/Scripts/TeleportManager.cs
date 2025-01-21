using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{   
        [SerializeField] private float tileSize = 1f;

        public void HandleTeleport(PlayerMovement player, Teleporter teleporter)
        {
            switch (teleporter.gameObject.tag)
            {
                case "Teleport":
                    StartCoroutine(TeleportMethods.PerformBasicTeleport(player, teleporter, tileSize));
                    break;
                case "TeleportType2":
                    StartCoroutine(TeleportMethods.PerformType2Teleport(player, teleporter, tileSize));
                    break;
                case "TeleportType3":
                    StartCoroutine(TeleportMethods.PerformType3Teleport(player, teleporter, tileSize));
                    break;
                case "TeleportType4":
                    StartCoroutine(TeleportMethods.PerformType4Teleport(player, teleporter, tileSize));
                    break;
                case "TeleportType5":
                    StartCoroutine(TeleportMethods.PerformType5Teleport(player, teleporter, tileSize));
                    break;
                case "TeleportType6":
                    StartCoroutine(TeleportMethods.PerformType6Teleport(player, teleporter, tileSize));
                    break;
                case "TeleportType7":
                    StartCoroutine(TeleportMethods.PerformType7Teleport(player, teleporter, tileSize));
                    break;
                case "TeleportType8":
                    StartCoroutine(TeleportMethods.PerformType8Teleport(player, teleporter, tileSize));
                    break;
                case "TeleportType9":
                    StartCoroutine(TeleportMethods.PerformType9Teleport(player, teleporter, tileSize));
                    break;
            }
        }
}
