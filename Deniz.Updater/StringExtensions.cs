namespace Deniz.Updater
{
    public static class StringExtensions
    {
        private const long MB_VALUE = 1048576;
        private const long GB_VALUE = 1073741824;

        public static string BytesToReadable(this long byteCount)
        {
            if (byteCount < 1024)
            {
                return $"{byteCount} Bytes";
            }

            if (byteCount < MB_VALUE)
            {
                var kb = Convert.ToDouble(byteCount) / 1024d;
                return $"{kb:0.00} KB";
            }

            if (byteCount < GB_VALUE)
            {
                var mb = Convert.ToDouble(byteCount) / Convert.ToDouble(MB_VALUE);
                return $"{mb:0.00} MB";
            }

            var gb = Convert.ToDouble(byteCount) / Convert.ToDouble(GB_VALUE);
            return $"{gb:#,##0.000} GB";
        }
        
    }
}
