using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Globalization;
using System.Security.Cryptography;

namespace MainCommon
{
    public static class MyUtils
    {
        #region Crypto

        private static DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        private static byte[] _rgbKey = { 217, 38, 55, 47, 255, 1, 238, 65 };//new Guid("{FB916843-F7F8-46d3-BB54-74AF05D6E012}").ToByteArray();
        private static byte[] _rgbIV = { 47, 21, 6, 77, 34, 176, 15, 182 };//new Guid("{C35B2CEA-FC7F-47a5-8967-756DA4F7628F}").ToByteArray();

        // Encode the specified byte array by using CryptoAPITranform.
        public static byte[] EncodeBytes(byte[] sourceBytes)
        {
            //int currentPosition = 0;
            byte[] targetBytes = new byte[1024];
            int sourceByteLength = sourceBytes.Length;
            System.IO.MemoryStream mem = new System.IO.MemoryStream();
            lock (des)
            {

                // Create a DES encryptor from this instance to perform encryption.
                CryptoAPITransform cryptoTransform =
                    (CryptoAPITransform)des.CreateEncryptor(_rgbKey, _rgbIV);
                CryptoStream w = new CryptoStream(mem, cryptoTransform, CryptoStreamMode.Write);
                w.Write(sourceBytes, 0, sourceBytes.Length);
                w.Close();
                mem.Close();
            }
            return mem.ToArray();
        }

        // Decode the specified byte array using CryptoAPITranform.
        public static byte[] DecodeBytes(byte[] sourceBytes)
        {
            byte[] rval;
            lock (des)
            {
                CryptoAPITransform cryptoTransform =
                    (CryptoAPITransform)des.CreateDecryptor(_rgbKey, _rgbIV);

                System.IO.MemoryStream mem = new System.IO.MemoryStream(sourceBytes);//15%23NFqQEahjWS6N9isdxpSkDfVx5uxc70TRFhhlWvJ8GsudG4jVBej66o1WOXnVkUpI
                CryptoStream r = new CryptoStream(mem, cryptoTransform, CryptoStreamMode.Read);
                System.IO.BinaryReader br = new System.IO.BinaryReader(r);
                rval = br.ReadBytes(4096);
                br.Close();
                r.Close();
                mem.Close();
            }
            return rval;
        }

        #endregion


        public static string GetImageUrl(string root_image_url, string image_path)
        {
            try
            {
                Uri url = null;
                if (Uri.TryCreate(image_path, UriKind.RelativeOrAbsolute, out url))
                {
                    if (!url.IsAbsoluteUri)
                        return System.IO.Path.Combine(root_image_url, url.OriginalString).Replace(System.IO.Path.DirectorySeparatorChar,System.IO.Path.AltDirectorySeparatorChar);
                    else if (url.IsFile || url.IsUnc)
                        return System.IO.Path.Combine(root_image_url, url.Segments[url.Segments.Length - 1]).Replace(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar);
                    else return url.ToString();
                }
                else return image_path;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string ReplaceFileName(string full_file_name, string new_file_name)
        {
            string rval;

            int startIndex = full_file_name.LastIndexOf('\\');
            int endIndex = full_file_name.LastIndexOf('.');

            rval = full_file_name.Remove(startIndex + 1, endIndex - startIndex - 1).Insert(startIndex + 1, new_file_name);

            return rval;
        }

        public static string GetFileName(string full_file_name)
        {
            string rval;

            int startIndex = full_file_name.LastIndexOf('\\');

            rval = full_file_name.Substring(startIndex + 1);

            return rval;
        }

        public static string GetKeyValuePairString(List<KeyValuePair<string, string>> qs)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < qs.Count; i++)
                sb.Append(qs[i].Key + "=" + qs[i].Value + "&");

            return sb.ToString();
        }

        public static List<KeyValuePair<string, string>> ExcludeQueryParam(NameValueCollection query_string, string[] p)
        {
            var collection = new List<KeyValuePair<string, string>>();
            foreach(var key in query_string.AllKeys)
                foreach(var val in query_string.GetValues(key))
                    collection.Add(new KeyValuePair<string,string>(key,val));

            var qs = collection.Where(q => !p.Contains(q.Key)).ToList();

            return qs;
        }

        public static string GetPathAndQueryParams(string path, NameValueCollection query_string, string query_param_name, object query_param_value)
        {
            var q = ExcludeQueryParam(query_string, new string[] { query_param_name });
            return path + "?" + GetKeyValuePairString(q) + query_param_name + "=" + query_param_value;
        }

        public static long[] Remove(this long[] arr_1, long[] arr_2)
        {
            List<long> list1 = arr_1.ToList();
            List<long> list2 = arr_2.ToList();

            foreach (long item in list2)
            {
                if (list1.Contains(item))
                {
                    int index = list1.IndexOf(item);
                    list1.RemoveAt(index);
                }
            }

            return list1.ToArray();
        }

        #region Format

        public static string FormatPrice(decimal price, bool is_availabe,string prefix, string not_available_text)
        {
            price = CeilingPrice(price);
            return is_availabe ? prefix + price.ToString("0.00", CultureInfo.InvariantCulture) : not_available_text;
        }

        public static decimal CeilingPrice(decimal price)
        {
            return Math.Ceiling(price * 100) / 100;
        }

        #endregion

        public static T GetOrAdd<T, Tk>(this Dictionary<Tk, T> dict, Tk key) where T : new()
        {
            T rval;
            if (!dict.TryGetValue(key, out rval))
            {
                rval = new T();
                dict.Add(key, rval);
            }
            return rval;
        }

        public static IEnumerable<T> Each<T>(this IEnumerable<T> coll, Action<T> action)
        {
            foreach (T item in coll)
                action(item);
            return coll;
        }

    }
}
