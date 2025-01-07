using System.Runtime.InteropServices;
using ThresholdingCLibrary;

namespace ThresholdingApp
{
    public partial class Form1 : Form
    {
        [DllImport(@"C:\Users\Adrianna\Documents\GitHub\ThresholdingApp\ThresholdingApp\x64\Debug\ThresholdingAsmLib.dll")]
        private static extern int MyProc1(int x, int y);
        public Form1()
        {
            InitializeComponent();
        }

        private void testLibraries(object sender, EventArgs e)
        {
            ThresholdingC cLib = new ThresholdingC();
            MessageBox.Show(cLib.BasicOperation(2, 3).ToString());
            
            MessageBox.Show(MyProc1(3, 5).ToString());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}