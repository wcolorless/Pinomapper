using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Newtonsoft.Json;

namespace Pinomapper.core
{
    public class FullCopy
    {
        public static object Go(object cloneable)
        {
            var json = JsonConvert.SerializeObject(cloneable);
            var type = cloneable.GetType();
            return JsonConvert.DeserializeObject(json, type);
        }
    }
}
