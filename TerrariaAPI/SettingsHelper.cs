using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TerrariaAPI
{
    public static class SettingsHelper
    {
        public static bool Save<T>(T obj, string fileName)
        {
            string path = Path.Combine(Program.PluginSettingsPath, fileName);

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
                Debug.WriteLine(e.ToString());
            }

            return false;
        }

        public static T Load<T>(string fileName, bool onErrorShowWarning = true) where T : new()
        {
            string path = Path.Combine(Program.PluginSettingsPath, fileName);

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
                Debug.WriteLine(e.ToString());

                if (onErrorShowWarning)
                {
                    string text = string.Format("Settings path:\r\n{0}\r\n\r\nError:\r\n{1}", path, e.ToString());
                    MessageBox.Show(text, "Error when loading settings file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return new T();
        }
    }
}