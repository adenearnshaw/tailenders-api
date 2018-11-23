using System;
using System.ComponentModel;

namespace TailendersApi.Contracts
{
    public enum SearchCategory
    {
        [DisplayName("Men")]
        Men = 0,
        [DisplayName("Women")]
        Women = 1,
        [DisplayName("Both")]
        Both = 2
    }
}
