using System.ComponentModel;

namespace TailendersApi.Contracts
{
    public enum CricketPosition
    {
        [Description("Cover")]
        Cover = 1,
        [Description("Cow corner")]
        CowCorner = 2,
        [Description("Deep cover")]
        DeepCover = 3,
        [Description("Deep midwicket")]
        DeepMidwicket = 4,
        [Description("Deep point")]
        DeepPoint = 5,
        [Description("Fine leg")]
        FineLeg = 6,
        [Description("Gully")]
        Gully = 7, 
        [Description("Keeper")]
        Keeper = 8, 
        [Description("Long leg")]
        LongLeg = 9,
        [Description("Long off")]
        LongOff = 10,
        [Description("Long on")]
        LongOn = 11,
        [Description("Long stop")]
        LongStop = 12,
        [Description("Mid off")]
        MidOff = 13,
        [Description("Mid on")]
        MidOn  = 14,
        [Description("Midwicket")]
        Midwicket = 15,
        [Description("Point")]
        Point = 16,
        [Description("Short leg")]
        ShortLeg = 17,
        [Description("Silly mid off")]
        SillyMidOff = 18,
        [Description("Silly mid on")]
        SillyMidOn = 19,
        [Description("Silly point")]
        SillyPoint = 20,
        [Description("Slips")]
        Slips = 21,
        [Description("Square leg")]
        SquareLeg = 22,
        [Description("Straight hit")]
        StraightHit = 23,
        [Description("Third man")]
        ThirdMan = 24
    }
}