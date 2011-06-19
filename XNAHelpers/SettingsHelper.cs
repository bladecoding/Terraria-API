using System;
using System.IO;
using System.Xml.Serialization;

namespace XNAHelpers
{
    public static class SettingsHelper
    {
        public static bool Save<T>(T obj, string path)
        {
            try
            {
                lock (obj)
                {
                    if (!string.IsNullOrEmpty(path))
                    {
                        string directoryName = Path.GetDirectoryName(path);

                        if (!string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName))
                        {
                            Directory.CreateDirectory(directoryName);
                        }

                        using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
                        {
                            new XmlSerializer(typeof(T)).Serialize(fs, obj);
                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return false;
        }

        public static T Load<T>(string path) where T : new()
        {
            try
            {
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        return (T)new XmlSerializer(typeof(T)).Deserialize(fs);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return new T();
        }
    }
}