namespace ShopCommon.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class FileHelper
    {
           // aqui tomo un string y lo convieto a array de archivos:
        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
