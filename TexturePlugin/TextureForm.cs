using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;

namespace TexturePlugin
{
    partial class TextureForm : Form
    {
        static readonly string ImgPath = "textures";
        GraphicsDevice Device;

        public TextureForm(GraphicsDevice device)
        {
            Device = device;
            InitializeComponent();
        }

        Thread loadthread = null;

        private void loadfunc(object obj)
        {
            var prov = (IFileProvider)obj;
            SetText("Loading images");
            var texts = ContentLoader.GetTextures(Device, prov);
            SetText("Replacing textures");
            ContentLoader.Load(texts);
            SetText("Done");
            loadthread = null;
        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            if (loadthread != null)
                return;

            var name = (string)TextureList.SelectedItem;
            IFileProvider prov;
            if (Directory.Exists(Path.Combine(ImgPath, name)))
            {
                prov = new DirProvider(new DirectoryInfo(Path.Combine(ImgPath, name)));
            }
            else if (File.Exists(Path.Combine(ImgPath, name + ".zip")))
            {
                prov = new ZipProvider(new FileStream(Path.Combine(ImgPath, name + ".zip"), FileMode.Open, FileAccess.Read));
            }
            else
            {
                SetText("Not found");
                return;
            }

            loadthread = new Thread(loadfunc);
            loadthread.Start(prov);
        }

        private void SetText(string text)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action<string>(SetText), text);
            else
                this.Text = text;
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            TextureList.Items.Clear();

            if (!Directory.Exists(ImgPath))
            {
                Directory.CreateDirectory(ImgPath);
                return;
            }

            var di = new DirectoryInfo(ImgPath);
            var texts = new List<string>();
            foreach (var d in di.GetDirectories())
            {
                if (!texts.Contains(d.Name))
                    texts.Add(d.Name);
            }
            foreach (var f in di.GetFiles("*.zip"))
            {
                var name = Path.GetFileNameWithoutExtension(f.Name);
                if (!texts.Contains(name))
                    texts.Add(name);
            }

            foreach (var s in texts)
                TextureList.Items.Add(s);
        }

        private void TextureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }
    }
}