using Elderand.Player;
using Elderand.Player.References;
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
            else if (keyboard.numpad9Key.wasPressedThisFrame)
            {
                foreach (var relic in PlayerReference.Inventory.GetAllRelics())
                    PlayerReference.Inventory.AddItem(relic, 1);

                PlayerReference.Inventory.AddItem(Main.ItemRandomizer.GetItemByName("Gurom'karah"), 1);
                PlayerReference.Inventory.AddItem(Main.ItemRandomizer.GetItemByName("Nyeth's Feather"), 1);
                PlayerReference.Inventory.AddItem(Main.ItemRandomizer.GetItemByName("Jewel of Renewed Will"), 1);
                PlayerReference.Inventory.AddItem(Main.ItemRandomizer.GetItemByName("Magical Catalyst"), 1);
                PlayerReference.Inventory.AddItem(Main.ItemRandomizer.GetItemByName("Last Hook"), 1);
                PlayerReference.Inventory.AddItem(Main.ItemRandomizer.GetItemByName("The Legacy of Virtue"), 1);
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
