

using UnityEngine.SocialPlatforms;

namespace WolfWithPotionEffect.Patch
{
    internal class PlayerControllerPatch
    {
        public static void Patch()
        {
            On.PlayerController.UpdateWolf += PlayerController_UpdateWolf;
        }

        private static void PlayerController_UpdateWolf(On.PlayerController.orig_UpdateWolf orig, PlayerController self)
        {
            if ((bool)self.IsWolf)
            {
                if (self.Item != null)
                {
                    self.Item.Cancel();
                }
            }
            PlayerController povPlayer = PlayerController.Local.LocalCameraHandler.PovPlayer;
            if (povPlayer == self)
            {
                GameManager.LightingManager.DisplayWolfLight(self.IsWolf);
                GameManager.LightingManager.DisplayNightLight(self.IsWolf);
            }
            if (!povPlayer.PlayerEffectManager.Paranoia)
            {
                self.UpdateModel(self.IsWolf);
            }
            self.UpdateCollider();
            self.UpdateCameraAnchorOffset();
        }
    }
}
