using Elderand.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Elderand.Randomizer.Debugging
{
    public class Debugger : Manager
    {
        public override void Update()
        {
            Keyboard keyboard = Keyboard.current;
            if (keyboard == null || Player == null) return;

            if (keyboard.numpad1Key.wasPressedThisFrame)
            {
                Main.LogWarning("Filling health");
                Player.Status.AddMaxHP();
            }
            else if (keyboard.numpad2Key.wasPressedThisFrame)
            {
                Main.LogWarning("Filling mana");
                Player.Status.AddMaxMP();
            }
            else if (keyboard.numpad3Key.wasPressedThisFrame)
            {
                Main.LogWarning("Filling stamina");
                Player.Status.AddMaxSP();
            }
        }

        private PlayerController _player;
        private PlayerController Player
        {
            get
            {
                if (_player == null)
                    _player = Object.FindObjectOfType<PlayerController>();
                return _player;
            }
        }
    }
}
