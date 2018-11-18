using System;

namespace TailendersApi.Client
{
    public class RetrieverBase
    {
        public T Get<T>(string url)
        {
            return default(T);
        }

        public TO Post<TI, TO>(string url, TI body)
        {
            return default(TO);

        }
    }
}
