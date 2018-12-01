using System.ComponentModel;

namespace TailendersApi.Contracts
{
    public enum Gender
    {
        [Description("Men")]
        Male = 0,
        [Description("Women")]
        Female = 1
    }

    public enum SearchCategory
    {
        [Description("Men")]
        Men = 0,
        [Description("Women")]
        Women = 1,
        [Description("Men and women")]
        Both = 2
    }
}
