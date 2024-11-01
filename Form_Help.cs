using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PFPExtractetor
{
    public partial class Form_Help : Form
    {
        public Form_Help()
        {
            InitializeComponent();
        }

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_Help_Load(object sender, EventArgs e)
        {
            btn_OK.Focus();
        }
    }
}
