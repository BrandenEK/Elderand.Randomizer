using Elderand.Level;
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
                PlayerReference.Inventory.AddItem(Main.Data.GetItemObject("Sword03"), 1);
                PlayerReference.Inventory.AddItem(Main.Data.GetItemObject("Relic01"), 1);
                PlayerReference.Inventory.AddItem(Main.Data.GetItemObject("Relic02"), 1);
                PlayerReference.Inventory.AddItem(Main.Data.GetItemObject("Relic03"), 1);
                PlayerReference.Inventory.AddItem(Main.Data.GetItemObject("Relic05"), 1);
                PlayerReference.Inventory.AddItem(Main.Data.GetItemObject("Relic07"), 1);
                PlayerReference.Inventory.AddItem(Main.Data.GetItemObject("Relic09"), 1);
                PlayerReference.Inventory.AddItem(Main.Data.GetItemObject("Ring20"), 1);

                for (int i = 0; i < 30; i++)
                {
                    PlayerReference.Attributes.AddAttribute(Data.BaseAttribute.Vitality);
                    PlayerReference.Attributes.AddAttribute(Data.BaseAttribute.Strength);
                }
            }
            else if (keyboard.numpad6Key.wasPressedThisFrame)
            {
                LevelManager.LoadRoomAsyncAtSpecialPoint(new SlotIndex(-21, -3), ReferenceType.SavePoint);
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
