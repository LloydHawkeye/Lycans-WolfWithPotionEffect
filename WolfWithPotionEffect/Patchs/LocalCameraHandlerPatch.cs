

using UnityEngine;

namespace WolfWithPotionEffect.Patchs
{
    internal class LocalCameraHandlerPatch
    {
        public static void Patch()
        {
            On.LocalCameraHandler.UpdateAnchorOffset += LocalCameraHandler_UpdateAnchorOffset;
        }

        private static void LocalCameraHandler_UpdateAnchorOffset(On.LocalCameraHandler.orig_UpdateAnchorOffset orig, LocalCameraHandler self)
        {
            if (GameManager.LocalGameState == GameState.EGameState.Off || !(self.PovPlayer != null) || (bool)self.PovPlayer.IsDead)
            {
                return;
            }
            int movementAction = self.PovPlayer.MovementAction;
            bool flag = self.PovPlayer.IsWolf;
            bool flag2 = self.PovPlayer.PlayerEffectManager.Giant;
            if (movementAction != self._movementStatus || flag != self._isWolf || flag2 != self._giant)
            {
                float num = ((movementAction == 1) ? (self._baseCameraOffset - 0.5f) : self._baseCameraOffset);
                if (flag && !flag2)
                {
                    num += 0.25f;
                }
                else if (flag2)
                {
                    num = ((movementAction == 1) ? 7.2f : 10.2f);
                }
                else if (flag && flag2)
                {
                    num = ((movementAction == 1) ? 7.2f : 10.2f);
                }
                Vector3 localPosition = self.localCameraAnchorPoint.localPosition;
                localPosition = new Vector3(localPosition.x, num, localPosition.z);
                self.localCameraAnchorPoint.localPosition = localPosition;
                self._movementStatus = movementAction;
                self._isWolf = flag;
                self._giant = flag2;
            }
        }
    }
}
