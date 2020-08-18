using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomNotes.Data;
using CustomNotes.Utilities;
using HarmonyLib;
using UnityEngine;
using CustomNotes;

namespace DDR_Saber.HarmonyPatches.Patches
{
    [HarmonyPatch(typeof(NoteJump))]
    [HarmonyPatch("ManualUpdate", MethodType.Normal)]
    internal class NoteJumpPatch
    {
        private static void Prefix(ref Transform ____rotatedObject)
        {
            CustomNote activeNote = NoteAssetLoader.CustomNoteObjects[NoteAssetLoader.SelectedNote];
            if (activeNote.Descriptor.NoteName.ToLower().Contains("ddr notes"))
            {
                ____rotatedObject.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }

        private static void Postfix(ref Transform ____rotatedObject)
        {
            CustomNote activeNote = NoteAssetLoader.CustomNoteObjects[NoteAssetLoader.SelectedNote];
            if (activeNote.Descriptor.NoteName.ToLower().Contains("ddr notes"))
            {
                ____rotatedObject.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}