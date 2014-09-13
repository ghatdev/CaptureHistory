using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaptureHsitory
{
    public partial class Form2 : Form
    {
        Form1 frm1;
        public Form2(Form1 _form)
        {
            InitializeComponent();
            frm1 = _form;
        }
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
