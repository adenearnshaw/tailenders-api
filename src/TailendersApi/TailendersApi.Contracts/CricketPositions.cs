using System.ComponentModel;

namespace TailendersApi.Contracts
{
    public enum CricketPosition
    {
        [DisplayName("Cover")]
        Cover = 1,
        [DisplayName("Cow corner")]
        CowCorner = 2,
        [DisplayName("Deep cover")]
        DeepCover = 3,
        [DisplayName("Deep midwicket")]
        DeepMidwicket = 4,
        [DisplayName("Deep point")]
        DeepPoint = 5,
        [DisplayName("Fine leg")]
        FineLeg = 6,
        [DisplayName("Gully")]
        Gully = 7, 
        [DisplayName("Keeper")]
        Keeper = 8, 
        [DisplayName("Long leg")]
        LongLeg = 9,
        [DisplayName("Long off")]
        LongOff = 10,
        [DisplayName("Long on")]
        LongOn = 11,
        [DisplayName("Long stop")]
        LongStop = 12,
        [DisplayName("Mid off")]
        MidOff = 13,
        [DisplayName("Mid on")]
        MidOn  = 14,
        [DisplayName("Midwicket")]
        Midwicket = 15,
        [DisplayName("Point")]
        Point = 16,
        [DisplayName("Short leg")]
        ShortLeg = 17,
        [DisplayName("Silly mid off")]
        SillyMidOff = 18,
        [DisplayName("Silly mid on")]
        SillyMidOn = 19,
        [DisplayName("Silly point")]
        SillyPoint = 20,
        [DisplayName("Slips")]
        Slips = 21,
        [DisplayName("Square leg")]
        SquareLeg = 22,
        [DisplayName("Straight hit")]
        StraightHit = 23,
        [DisplayName("Third man")]
        ThirdMan = 24
    }
}