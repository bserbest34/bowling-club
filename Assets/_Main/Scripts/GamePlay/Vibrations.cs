using MoreMountains.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrations
{
    public static void Failure()
    {
        HapticManager.Haptic((HapticType)HapticTypes.Failure);
    }

    public static void Heavy()
    {
        HapticManager.Haptic((HapticType)HapticTypes.HeavyImpact);
    }

    public static void Light()
    {
        HapticManager.Haptic((HapticType)HapticTypes.LightImpact);
    }

    public static void Warning()
    {
        HapticManager.Haptic((HapticType)HapticTypes.Warning);
    }

    public static void Medium()
    {
        HapticManager.Haptic((HapticType)HapticTypes.MediumImpact);
    }

    public static void Soft()
    {
        HapticManager.Haptic((HapticType)HapticTypes.SoftImpact);
    }

    public static void Rigid()
    {
        HapticManager.Haptic((HapticType)HapticTypes.RigidImpact);
    }

    public static void Succes()
    {
        HapticManager.Haptic((HapticType)HapticTypes.Success);
    }

    public static void Selection()
    {
        HapticManager.Haptic((HapticType)HapticTypes.Selection);
    }
}