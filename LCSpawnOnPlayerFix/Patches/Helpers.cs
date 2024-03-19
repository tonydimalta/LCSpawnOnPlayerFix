using GameNetcodeStuff;
using Vector3 = UnityEngine.Vector3;

namespace LCSpawnOnPlayerFix
{
    public static class Helpers
    {
        public const float SafeSpawnDistanceFromPlayer = 16f;
        public const float SafeSpawnSquaredDistanceFromPlayer = SafeSpawnDistanceFromPlayer * SafeSpawnDistanceFromPlayer;
        /// <value>Allow more upward safety to account for the <c>Jetpack</c> as a player can't look directly
        /// below their feet and probably wouldn't do it when landing anyway.</value>
        public static readonly Vector3 SafeOutsideScaleDistance = new Vector3(1f, .7f, 1f);

        /// <returns>
        /// The squared distance between <paramref name="v1"/> and <paramref name="v2"/>, multiplied by <paramref name="scale"/> to weight all axis separately.
        /// </returns>
        public static float SquaredDistance(Vector3 v1, Vector3 v2, Vector3 scale)
        {
            float xDiff = v1.x - v2.x;
            float yDiff = v1.y - v2.y;
            float zDiff = v1.z - v2.z;
            return (xDiff * xDiff * scale.x * scale.x) +
                (yDiff * yDiff * scale.y * scale.y) +
                (zDiff * zDiff * scale.z * scale.z);
        }

        /// <returns>
        /// <c>True</c> if <paramref name="player"/> is spawned, controlled by a player and not dead, <c>false</c> otherwise.
        /// </returns>
        public static bool IsTrulyAlive(this PlayerControllerB player)
        {
            return player.IsSpawned && player.isPlayerControlled && !player.isPlayerDead;
        }

        /// <returns>
        /// <c>True</c> if <paramref name="player"/> is outside the factory and not in the ship, <c>false</c> otherwise.
        /// </returns>
        public static bool IsUnsafeOutside(this PlayerControllerB player)
        {
            return !player.isInsideFactory && !player.isInHangarShipRoom;
            // NB: Not checking isInElevator (railing or on top of the ship) as it only updates
            // when the player lands on the ground, which isn't great for the Jetpack.
        }

        /// <summary>
        /// <para>Check the squared distance between <paramref name="player"/> and <paramref name="position"/>.</para>
        /// <para>See <see cref="SafeSpawnSquaredDistanceFromPlayer"/> for the distance considered as near a player.</para>
        /// <para>Also see <seealso cref="SafeOutsideScaleDistance"/> for the extra <c>Jetpack</c> safety.</para>
        /// </summary>
        /// <param name="position">a position <c>outside</c> the factory</param>
        /// <returns>
        /// <c>True</c> if <paramref name="player"/> is near <paramref name="position"/>, <c>false</c> otherwise.
        /// </returns>
        public static bool IsOutsidePositionNearPlayer(this PlayerControllerB player, Vector3 position)
        {
            return SquaredDistance(position, player.transform.position, SafeOutsideScaleDistance) < SafeSpawnSquaredDistanceFromPlayer;
        }
    }
}