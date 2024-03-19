using GameNetcodeStuff;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace LCSpawnOnPlayerFix.Patches
{
    [HarmonyPatch(typeof(RoundManager))]
    internal static class RoundManagerPatch
    {
        [HarmonyPatch(nameof(RoundManager.SpawnRandomOutsideEnemy))]
        [HarmonyPrefix]
        private static bool SpawnRandomOutsideEnemyPrefix(ref GameObject[] spawnPoints)
        {
            int spawnPointsCount = spawnPoints.Length;
            List<GameObject> filteredSpawnPoints = new List<GameObject>(spawnPoints);
            int playersCount = StartOfRound.Instance.allPlayerScripts.Length;
            for (int i = 0; i < playersCount; ++i)
            {
                PlayerControllerB player = StartOfRound.Instance.allPlayerScripts[i];
                if (player != null && player.IsTrulyAlive() && player.IsUnsafeOutside())
                {
                    filteredSpawnPoints.RemoveAll(x => player.IsOutsidePositionNearPlayer(x.transform.position));
                }
            }

            Plugin.Logger?.LogDebug($"\"{nameof(RoundManagerPatch)}\" Removed {spawnPointsCount - filteredSpawnPoints.Count} spawn points too close to players");
            if (filteredSpawnPoints.Count < 1)
            {
                // Skip original method.
                return false;
            }

            spawnPoints = filteredSpawnPoints.ToArray();
            return true;
        }
    }
}