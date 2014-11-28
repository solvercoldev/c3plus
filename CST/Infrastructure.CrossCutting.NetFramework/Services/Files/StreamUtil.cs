using System.IO;

namespace Infrastructure.CrossCutting.NetFramework.Services.Files
{
    public class StreamUtil
    {
        private const int BufferSize = 1024;

        private StreamUtil()
        {
        }

        /// <summary>
        /// Copy the contents from one stream into another one.
        /// </summary>
        /// <param name="fromStream"></param>
        /// <param name="toStream"></param>
        public static void Copy(Stream fromStream, Stream toStream)
        {
            fromStream.Position = 0;
            toStream.Position = 0;

            var buffer = new byte[BufferSize];
            int len;
            while ((len = fromStream.Read(buffer, 0, BufferSize)) > 0)
            {
                toStream.Write(buffer, 0, len);
            }
        }
    }
}
