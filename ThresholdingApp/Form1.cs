using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ThresholdingCLibrary;

namespace ThresholdingApp
{
    public partial class Form1 : Form
    {
        [DllImport(@"C:\Users\Adrianna\Documents\GitHub\ThresholdingApp\ThresholdingApp\x64\Debug\ThresholdingAsmLib.dll", EntryPoint = "ConvertToGrayScale")]
        private static extern long ConvertToGrayScale(IntPtr sourcePointer, IntPtr resultPointer, int width, int height, int stride, int startRow, int endRow);

        private string filePath = "";
        private Bitmap image;

        public Form1()
        {
            InitializeComponent();
        }

        private void testLibraries(object sender, EventArgs e)
        {
            ThresholdingC cLib = new ThresholdingC();
            MessageBox.Show(cLib.BasicOperation(2, 3).ToString());

            //MessageBox.Show(MyProc1(3, 5).ToString());
        }

        // update informations about image
        private void updateInfo()
        {
            richTextBox_Info.Clear();
            richTextBox_Info.AppendText($"File: {filePath}\n");
            richTextBox_Info.AppendText($"Width: {image.Width}\n");
            richTextBox_Info.AppendText($"Height: {image.Height}\n");
        }

        private void doThresholding()
        {
            if (image == null) { 
                MessageBox.Show("Wyst¹pi³ b³¹d!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int numberOfThreads = 1;

            // Copy of image
            try
            {
                BitmapData sourceData = null;
                BitmapData resultData = null;


                int width = image.Width;
                int height = image.Height;

                try
                {
                    Bitmap resultBitmap = new Bitmap(width, height, PixelFormat.Format32bppRgb);
                    // Bitmap memory area locking - read only for source
                    sourceData = image.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
                    // Bitmap memory area locking - write only for result
                    resultData = resultBitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);



                    int stride = sourceData.Stride; // size of one row in bytes
                    int bytes = Math.Abs(stride) * height; // number of total bytes 

                    // Create buffer to send to library
                    byte[] sourceBuffer = new byte[bytes];
                    byte[] resultBuffer = new byte[bytes];

                    Marshal.Copy(sourceData.Scan0, sourceBuffer, 0, bytes);

                    int alignedBytes = (bytes + 15) & ~15; // byte alignment to nearest multiple of 16 (optimization)
                                                           // memory allocation for source and result pointers
                    IntPtr sourcePointer = Marshal.AllocHGlobal(alignedBytes);
                    IntPtr resultPointer = Marshal.AllocHGlobal(alignedBytes);

                    try
                    {
                        Marshal.Copy(sourceBuffer, 0, sourcePointer, bytes);
                        Marshal.Copy(sourceBuffer, 0, resultPointer, bytes);

                        int rowsPerThread = height / numberOfThreads;
                        Task[] tasks = new Task[numberOfThreads];

                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();

                        for (int i = 0; i < numberOfThreads; i++)
                        {
                            int startRow = i * rowsPerThread;
                            int endRow = (i == numberOfThreads - 1) ? height : startRow + rowsPerThread;

                            if (radioASMLib.Checked)
                            {
                                tasks[i] = Task.Run(() =>
                                {
                                    MessageBox.Show($"{startRow} - {endRow} - {stride}");
                                    MessageBox.Show(ConvertToGrayScale(sourcePointer, resultPointer, width, height, stride, startRow, endRow).ToString());
                                });
                            }
                            if (radioCLib.Checked)
                            {
                                ThresholdingC cLib = new ThresholdingC();
                                tasks[i] = Task.Run(() =>
                                {
                                    cLib.ConvertToGrayscale(sourcePointer, resultPointer, width, height, stride, startRow, endRow);
                                });
                            }


                        }

                        Task.WaitAll(tasks);

                        Marshal.Copy(resultPointer, resultBuffer, 0, bytes);
                        Marshal.Copy(resultBuffer, 0, resultData.Scan0, bytes);

                        stopwatch.Stop();
                        MessageBox.Show($"Time: {stopwatch.ElapsedMilliseconds} ms\nTicks: {stopwatch.ElapsedTicks}", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    finally
                    {
                        Marshal.FreeHGlobal(sourcePointer);
                        Marshal.FreeHGlobal(resultPointer);
                        if (sourceData != null) { image.UnlockBits(sourceData); }
                        if (resultData != null) { resultBitmap.UnlockBits(resultData); }

                    }
                    pictureBox_after.Image = resultBitmap;
                }
                finally
                {


                }


            } finally
            {

            }
        }

        private void selectFile(object sender, EventArgs e)
        {
            using (openFileDialog1)
            {
                openFileDialog1.Filter = "Image Files (*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog1.FileName;

                    try
                    {
                        image = new Bitmap(filePath);

                        if (image == null)
                        {
                            throw new Exception("Failed to load image.");
                        }
                        else
                        {
                            pictureBox_before.Image = Image.FromFile(filePath);
                            updateInfo();
                            doThresholding();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btn_thresholding_Click(object sender, EventArgs e)
        {
            panel_settings.Visible = false;
            panel_thresholding.Visible = true;
        }

        private void btn_settings_Click(object sender, EventArgs e)
        {
            panel_settings.Visible = true;
            panel_thresholding.Visible = false;
        }

        private void radioCLib_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}