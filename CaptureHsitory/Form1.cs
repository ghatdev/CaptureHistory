using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaptureHsitory
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Directory.CreateDirectory("images");
                Directory.CreateDirectory(@"images\"+DateTime.Now.ToString("yyyy년MM월dd일"));
            }
            catch (Exception es)
            {
                MessageBox.Show(es.ToString());
            }
            ClipboardNotification.ClipboardUpdate += ClipboardNotification_ClipboardUpdate;
        }

        void ClipboardNotification_ClipboardUpdate(object sender, EventArgs e)
        {
            Image img = Clipboard.GetImage();
            imageList1.Images.Add(img);
            String ntime = System.DateTime.Now.ToString("yyyy-MM-dd HH시mm분ss초ms");


            img.Save(@"images\" + DateTime.Now.ToString("yyyy년MM월dd일") + @"\"+ ntime + ".png", System.Drawing.Imaging.ImageFormat.Png);
            ListViewItem lvi = new ListViewItem(ntime);
            
            lvi.ImageIndex = 0;
            listView1.Items.Add(lvi);
           
        }

        /*
         * ------------------------------------------------
         * ClipboardNotification Class from StackOverFlow
         * ------------------------------------------------
         * */
        public sealed class ClipboardNotification
        {
            /// <summary>
            /// Occurs when the contents of the clipboard is updated.
            /// </summary>
            public static event EventHandler ClipboardUpdate;

            private static NotificationForm _form = new NotificationForm();

            /// <summary>
            /// Raises the <see cref="ClipboardUpdate"/> event.
            /// </summary>
            /// <param name="e">Event arguments for the event.</param>
            private static void OnClipboardUpdate(EventArgs e)
            {
                var handler = ClipboardUpdate;
                if (handler != null)
                {
                    handler(null, e);
                }
            }

            /// <summary>
            /// Hidden form to recieve the WM_CLIPBOARDUPDATE message.
            /// </summary>
            private class NotificationForm : Form
            {
                public NotificationForm()
                {
                    NativeMethods.SetParent(Handle, NativeMethods.HWND_MESSAGE);
                    NativeMethods.AddClipboardFormatListener(Handle);
                }

                protected override void WndProc(ref Message m)
                {
                    if (m.Msg == NativeMethods.WM_CLIPBOARDUPDATE)
                    {
                        OnClipboardUpdate(null);
                    }
                    base.WndProc(ref m);
                }
            }
        }

        internal static class NativeMethods
        {
            // See http://msdn.microsoft.com/en-us/library/ms649021%28v=vs.85%29.aspx
            public const int WM_CLIPBOARDUPDATE = 0x031D;
            public static IntPtr HWND_MESSAGE = new IntPtr(-3);

            // See http://msdn.microsoft.com/en-us/library/ms632599%28VS.85%29.aspx#message_only
            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool AddClipboardFormatListener(IntPtr hwnd);

            // See http://msdn.microsoft.com/en-us/library/ms633541%28v=vs.85%29.aspx
            // See http://msdn.microsoft.com/en-us/library/ms649033%28VS.85%29.aspx
            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2(this);
            frm2.Text = listView1.FocusedItem.Text;
            Image img = Image.FromFile(@"images\" + DateTime.Now.ToString("yyyy년MM월dd일") +@"\"+ listView1.FocusedItem.Text + ".png");
            frm2.pictureBox1.Image = img;
            frm2.Show();
        }
    }
}
