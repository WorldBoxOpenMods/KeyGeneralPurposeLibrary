using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace KeyGeneralPurposeLibrary.Utils {
  public static class HarmonyUtils {
            public static void Patch(this MethodInfo original, Harmony harmony, HarmonyPatchType type, Delegate patch) {
            try {
                switch (type) {
                    case HarmonyPatchType.All:
                        harmony.Patch(
                            original,
                            postfix: new HarmonyMethod(patch.Method)
                        );
                        break;
                    case HarmonyPatchType.Prefix:
                        harmony.Patch(
                            original,
                            prefix: new HarmonyMethod(patch.Method)
                        );
                        break;
                    case HarmonyPatchType.Postfix:
                        harmony.Patch(
                            original,
                            postfix: new HarmonyMethod(patch.Method)
                        );
                        break;
                    case HarmonyPatchType.Transpiler:
                        harmony.Patch(
                            original,
                            transpiler: new HarmonyMethod(patch.Method)
                        );
                        break;
                    case HarmonyPatchType.Finalizer:
                        harmony.Patch(
                            original,
                            finalizer: new HarmonyMethod(patch.Method)
                        );
                        break;
                    case HarmonyPatchType.ILManipulator:
                        harmony.Patch(
                            original,
                            ilmanipulator: new HarmonyMethod(patch.Method)
                        );
                        break;
                    case HarmonyPatchType.ReversePatch:
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, "Unsupported Harmony patch type");
                }
            }
            catch (HarmonyException e) {
                Debug.LogError("Failed to apply Harmony " + type + " patch:\n" + e);
            }
            Debug.Log("Applied Harmony " + type + " patch: " + patch.Method.Name);
        }
  }
}