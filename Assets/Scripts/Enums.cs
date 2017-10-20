namespace Enums {

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
        MaxSpeed,
        FuelTank,
        FuelConsume
    }

    public enum CarModificationActivation
    {
        Active,
        Passive
    }

    public enum CarModifationRarity
    {
        Common = 0,
        Rare = 1,
        Epic = 2
    }

    public enum ChestRarity
    {
        Common = 0,
        Rare = 1,
        Epic = 2,
        free = 3
    }

    public enum ChestState
    {
        Closed,
        Opening,
        Opened
        
    }
}
