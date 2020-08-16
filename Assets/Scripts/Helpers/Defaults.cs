using UnityEngine;

namespace NGTT.Helpers
{
    public static class Defaults
    {
        public static Vector3 ProjectileSpawnPoint = new Vector3(0, 10, 0);
        public static Vector3 LocationPlaneScale = new Vector3(3, 1, 3);
        
        public static float ProjectileSpawnCooldown = 1;
        public static float ProjectileExplosionScale = 3;
        public static float ProjectileDestroyDelay = 0.2f;
        public static float ProjectileDamage = 20;
        public static float ProjectileMass = 3;
        public static float ProjectileCountdown = 3;

        public static string ProjectileActivatedTag = "activated";
        
        public static float UnitHealth = 100;
        public static int UnitPoolSize = 30;
        
        public static int MinBarrierNumber = 10;
        public static int MaxBarrierNumber = 30;
        
        public static string UIManagerNotFound = "UI Manager not found!";
        public static string HaltButtonIsNotSet = "Halt button is not set!";
        public static string LaunchButtonIsNotSet = "Launch button is not set!";
    }
}