﻿namespace Enums {

    public enum TypeOfDrive
    {
        ForwardDrive,
        RearDrive,
        FourXFour,
        FourWithLocks,
        FourWD
    }

    public enum CarModificatorType
    {
        EngineTorque,
        BreakTorcue,
        CenterMass,
        Gravity,
        Rotation,
        SuspentionLift,
        SuspentionHardness,
        TearsGrip,
        TorquePercentForWd,
        GearBox,
        MaxRPM,
        MaxSpeed
    }

    public enum CarModificationActivation
    {
        Active,
        Passive
    }

    public enum CarModifationRarity
    {
        Common,
        Rare,
        Epic
    }
}
