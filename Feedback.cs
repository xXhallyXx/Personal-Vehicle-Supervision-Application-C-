using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class Feedback : Form
    {
        public Feedback()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            ExpertInfo back = new ExpertInfo();
            back.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
