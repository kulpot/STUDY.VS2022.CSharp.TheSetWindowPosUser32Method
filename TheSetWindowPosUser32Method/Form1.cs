using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// ref link:https://www.youtube.com/watch?v=XiwzFJBNeKA&list=PLAIBPfq19p2EJ6JY0f5DyQfybwBGVglcR&index=74&t=325s
// READ Link: MSDN SetWindowPos function //Copy(Signature/ User-Defined Types): Link: pinvoke.net: SetWindowPos

namespace TheSetWindowPosUser32Method
{
    public partial class form1 : Form
    {
        #region Native
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        // Returns zero if function fails, otherwise return a non-zero value

        // effected window      z-order indication    xPos   yPos   width    height   flags         
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        // Flags
        const int SWP_ASYNCWINDOWPOS = 0x4000;
        const int SWP_DEFERERASE = 0x2000;
        const int SWP_DRAWFRAME = 0x0020;
        const int SWP_FRAMECHANGED = 0x0020;
        const int SWP_HIDEWINDOW = 0x0080;
        const int SWP_NOACTIVATE = 0x0010;
        const int SWP_NOCOPYBITS = 0x0100;
        const int SWP_NOMOVE = 0x0002;
        const int SWP_NOOWNERZORDER = 0x0200;
        const int SWP_NOREDRAW = 0x0008;
        const int SWP_NOREPOSITION = 0x0200;
        const int SWP_NOSENDCHANGING = 0x0400;
        const int SWP_NOSIZE = 0x0001;
        const int SWP_NOZORDER = 0x0004;
        const int SWP_SHOWWINDOW = 0x0040;

        const int HWND_TOP = 0; // Bring to front
        const int HWND_BOTTOM = 1; // Send to back
        const int HWND_TOPMOST = -1; // Bring to front and stay on top of other windows that do not have this flag set
        const int HWND_NOTOPMOST = -2; // Disable topmost for the window
        #endregion

        public form1()
        {
            InitializeComponent();
        }

        private void btnExpand_Click(object sender, EventArgs e)
        {
            bool hasSmallerWidth = true;
            bool hasSmallerHeight = true;

            while(hasSmallerWidth || hasSmallerHeight)
            {
                hasSmallerWidth = this.Size.Width < Screen.PrimaryScreen.WorkingArea.Width;
                hasSmallerHeight = this.Size.Height < Screen.PrimaryScreen.WorkingArea.Height;

                if (hasSmallerWidth)
                {
                    //this.Width++;
                    SetWindowPos(this.Handle, IntPtr.Zero, 0, 0, this.Width + 1, this.Height, SWP_ASYNCWINDOWPOS);
                }

                if (hasSmallerHeight)
                {
                    //this.Height++;
                    SetWindowPos(this.Handle, IntPtr.Zero, 0, 0, this.Width, this.Height + 1, SWP_ASYNCWINDOWPOS);
                }
            }
        }

        private void btnPosition_Click(object sender, EventArgs e)
        {
            SetWindowPos(this.Handle, (IntPtr)HWND_TOP, 100, 100, 45454545, 4544545, SWP_NOSIZE);
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            SetWindowPos(this.Handle, (IntPtr)HWND_TOPMOST, 45545, 45545, 454545, 45545, SWP_NOSIZE | SWP_NOMOVE);
        }
    }
}
