using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace DoubanGroup.Core.Api
{
    public class File
    {
        public string FieldName { get; set; }

        public string FileName { get; set; }

        public byte[] Stream { get; set; }
    }
}
