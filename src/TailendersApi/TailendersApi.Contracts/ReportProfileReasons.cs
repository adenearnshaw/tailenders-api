using System.ComponentModel;

namespace TailendersApi.Contracts
{
    public enum ReportProfileReason
    {
        [Description("Inappropriate profile")]
        InappropriateProfile = 1,
        [Description("Inappropriate photo")]
        InappropriatePhotos = 2,
        [Description("Feels like spam")]
        Spam = 3,
        [Description("Other")]
        Other = 99
    }
}