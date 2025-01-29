using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ThresholdingCLibrary;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ThresholdingApp
{
    public partial class Form1 : Form
    {
        //[DllImport(@"C:\Users\Adrianna\Documents\GitHub\ThresholdingApp\ThresholdingApp\x64\Debug\ThresholdingAsmLib.dll", EntryPoint = "ConvertToGrayScale")]
        //private static extern void ConvertToGrayScale(IntPtr sourcePointer, IntPtr resultPointer, int width, IntPtr height, int stride, int startRow, int endRow);

        //[DllImport(@"C:\Users\Adrianna\Documents\GitHub\ThresholdingApp\ThresholdingApp\x64\Debug\ThresholdingAsmLib.dll", EntryPoint = "Thresholding", CallingConvention = CallingConvention.Cdecl)]
        //private static extern void Thresholding(IntPtr sourcePointer, IntPtr resultPointer, int width, int thresholdValue, int stride, int startRow, int endRow);


        [DllImport(@"C:\Users\Adrianna\Documents\GitHub\ThresholdingApp\ThresholdingApp\x64\Debug\ThresholdingASMLibrary.dll")]
        private static extern void ThresholdingOnly(IntPtr sourcePointer, IntPtr resultPointer, int width, int thresholdValue, int stride, int startRow, int endRow);

        [DllImport(@"C:\Users\Adrianna\Documents\GitHub\ThresholdingApp\ThresholdingApp\x64\Debug\ThresholdingASMLibrary.dll")]
        private static extern void Thresholding(IntPtr sourcePointer, IntPtr resultPointer, int width, int thresholdValue, int stride, int startRow, int endRow);

        [DllImport(@"C:\Users\Adrianna\Documents\GitHub\ThresholdingApp\ThresholdingApp\x64\Debug\ThresholdingASMLibrary.dll")]
        private static extern void ConvertToGrayScale(IntPtr sourcePointer, IntPtr resultPointer, int width, IntPtr height, int stride, int startRow, int endRow);


        private string filePath = "";
        private Bitmap image;
        private Bitmap resultImage = null;

        private int thresholdValue = 128;

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
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

        private void updateInfoAfterThreshold(long elapsedMilliseconds, long elapsedTicks, int numberOfThreads, int thresholdValue)
        {
            richTextBox_Info.Clear();
            richTextBox_Info.AppendText($"File: {filePath}\n");
            richTextBox_Info.AppendText($"Width: {image.Width}\n");
            richTextBox_Info.AppendText($"Height: {image.Height}\n");
            richTextBox_Info.AppendText($"\n\nTime: {elapsedMilliseconds}\nTicks: {elapsedTicks}\nThreads: {numberOfThreads}\nThreshold: {thresholdValue}");
        }

        private void doThresholding_customThreshold()
        {
            thresholdValue = trackBar1.Value;
            int numberOfThreads = int.Parse(comboBox1.SelectedItem.ToString());

            if (image == null)
            {
                MessageBox.Show("No image!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int width = image.Width;
            int height = image.Height;

            BitmapData sourceData = null;
            BitmapData resultData = null;

            Bitmap resultBitmap = null;

            try
            {
                // BitmapData memory area locking - read only for source
                sourceData = image.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
                // Create new bitmap for result
                resultBitmap = new Bitmap(width, height, PixelFormat.Format32bppRgb);
                //BitmapData memory area locking - write only for result
                resultData = resultBitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);

                // size of one row in bytes
                int stride = sourceData.Stride;
                // all bytes in imagee
                int bytes = Math.Abs(stride) * height;

                // Create buffer
                byte[] sourceBuffer = new byte[bytes];
                byte[] resultBuffer = new byte[bytes];

                // Init source buffer
                Marshal.Copy(sourceData.Scan0, sourceBuffer, 0, bytes);

                // byte alignment to nearest multiple of 16 (optimization)
                int alignedBytes = (bytes + 15) & ~15;

                // Create IntPtr for source and result
                IntPtr sourcePointer = IntPtr.Zero;
                IntPtr resultPointer = IntPtr.Zero;

                try
                {
                    // Lock memory for pointers
                    sourcePointer = Marshal.AllocHGlobal(alignedBytes);
                    resultPointer = Marshal.AllocHGlobal(alignedBytes);

                    // Init pointers
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
                                //MessageBox.Show($"{stride} - {startRow} - {endRow} - {resultPointer} - {width}");
                                Thresholding(sourcePointer, resultPointer, width, thresholdValue, stride, startRow, endRow);
                            });
                        }
                        if (radioCLib.Checked)
                        {
                            ThresholdingC cLib = new ThresholdingC();
                            tasks[i] = Task.Run(() =>
                            {
                                cLib.Thresholding(sourcePointer, resultPointer, width, thresholdValue, stride, startRow, endRow);
                            });
                        }
                    }


                    Task.WaitAll(tasks);

                    Marshal.Copy(resultPointer, resultBuffer, 0, bytes);
                    Marshal.Copy(resultBuffer, 0, resultData.Scan0, bytes);


                    stopwatch.Stop();
                    updateInfoAfterThreshold(stopwatch.ElapsedMilliseconds, stopwatch.ElapsedTicks, numberOfThreads, thresholdValue);
                }
                catch
                {
                    MessageBox.Show("Pointers error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Free memory

                    sourceBuffer = null;
                    resultBuffer = null;
                    if (sourcePointer != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(sourcePointer);
                    }

                    if (resultPointer != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(resultPointer);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Memory error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                if (sourceData != null) { image.UnlockBits(sourceData); }
                if (resultData != null) { resultBitmap.UnlockBits(resultData); }
                if (resultImage != null)
                {
                    // Delete prev img
                    resultImage.Dispose();
                }
                resultImage = resultBitmap;
                pictureBox_after.Image = resultImage;
            }
        }

        private void doThresholding_autoThreshold()
        {
            thresholdValue = trackBar1.Value;
            int numberOfThreads = int.Parse(comboBox1.SelectedItem.ToString());

            if (image == null)
            {
                MessageBox.Show("No image!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int width = image.Width;
            int height = image.Height;

            BitmapData sourceData = null;
            BitmapData grayScaleData = null;
            BitmapData resultData = null;

            Bitmap grayScaleBitmap = null;
            Bitmap resultBitmap = null;

            try
            {
                // BitmapData memory area locking - read only for source
                sourceData = image.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);

                // Create new bitmap for grayScale
                grayScaleBitmap = new Bitmap(width, height, PixelFormat.Format32bppRgb);
                //BitmapData memory area locking - write only for grayScale
                grayScaleData = grayScaleBitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);

                // Create new bitmap for result
                resultBitmap = new Bitmap(width, height, PixelFormat.Format32bppRgb);
                //BitmapData memory area locking - write only for result
                resultData = resultBitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);

                // size of one row in bytes
                int stride = sourceData.Stride;
                // all bytes in imagee
                int bytes = Math.Abs(stride) * height;

                // Create buffer
                byte[] sourceBuffer = new byte[bytes];
                byte[] grayScaleBuffer = new byte[bytes];
                int[] histogramBuffer = new int[256];
                byte[] resultBuffer = new byte[bytes];


                // Init source buffer
                Marshal.Copy(sourceData.Scan0, sourceBuffer, 0, bytes);

                // byte alignment to nearest multiple of 16 (optimization)
                int alignedBytes = (bytes + 15) & ~15;

                // Create IntPtr for source and result
                IntPtr sourcePointer = IntPtr.Zero;
                IntPtr grayScalePointer = IntPtr.Zero;
                IntPtr histogramPointer = IntPtr.Zero;
                IntPtr resultPointer = IntPtr.Zero;

                try
                {
                    // Lock memory for pointers
                    sourcePointer = Marshal.AllocHGlobal(alignedBytes);
                    grayScalePointer = Marshal.AllocHGlobal(alignedBytes);
                    histogramPointer = Marshal.AllocHGlobal(256 * sizeof(int));
                    resultPointer = Marshal.AllocHGlobal(alignedBytes);

                    // Init pointers
                    Marshal.Copy(sourceBuffer, 0, sourcePointer, bytes);
                    Marshal.Copy(sourceBuffer, 0, grayScalePointer, bytes);
                    Marshal.Copy(histogramBuffer, 0, histogramPointer, 256);
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
                                ConvertToGrayScale(sourcePointer, grayScalePointer, width, histogramPointer, stride, startRow, endRow);
                            });
                        }
                        if (radioCLib.Checked)
                        {
                            ThresholdingC cLib = new ThresholdingC();
                            tasks[i] = Task.Run(() =>
                            {
                                cLib.ConvertToGrayScale(sourcePointer, grayScalePointer, width, histogramPointer, stride, startRow, endRow);
                            });
                        }
                    }
                    try
                    {
                        Task.WaitAll(tasks);
                    }
                    catch (AggregateException ex)
                    {
                        MessageBox.Show($"Thread error: {ex.InnerExceptions.FirstOrDefault()?.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    //histogram i obliczanie progu
                    Marshal.Copy(histogramPointer, histogramBuffer, 0, 256);
                    thresholdValue = OtsuMethod(histogramBuffer);

                    // change grayScalePointer from result to source
                    // step 1:  copy
                    Marshal.Copy(grayScalePointer, grayScaleBuffer, 0, bytes);
                    Marshal.Copy(grayScaleBuffer, 0, grayScaleData.Scan0, bytes);
                    // step 2: del pointer
                    if (grayScalePointer != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(grayScalePointer);
                        grayScalePointer = IntPtr.Zero;
                    }
                    // step 2: unlock bits
                    if (grayScaleData != null) { grayScaleBitmap.UnlockBits(sourceData); }
                    // step 3: lock for read only
                    grayScaleData = grayScaleBitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
                    // step 4: delete grayScalePointer
                    grayScalePointer = Marshal.AllocHGlobal(alignedBytes);
                    Marshal.Copy(grayScaleData.Scan0, grayScaleBuffer, 0, bytes);
                    Marshal.Copy(grayScaleBuffer, 0, grayScalePointer, bytes);

                    Task[] tasks2 = new Task[numberOfThreads];
                    for (int i = 0; i < numberOfThreads; i++)
                    {
                        int startRow = i * rowsPerThread;
                        int endRow = (i == numberOfThreads - 1) ? height : startRow + rowsPerThread;

                        if (radioASMLib.Checked)
                        {
                            tasks2[i] = Task.Run(() =>
                            {
                                ThresholdingOnly(grayScalePointer, resultPointer, width, thresholdValue, stride, startRow, endRow);
                            });
                        }
                        if (radioCLib.Checked)
                        {
                            ThresholdingC cLib = new ThresholdingC();
                            tasks2[i] = Task.Run(() =>
                            {
                                cLib.ThresholdingOnly(grayScalePointer, resultPointer, width, thresholdValue, stride, startRow, endRow);
                            });
                        }
                    }
                    try
                    {
                        Task.WaitAll(tasks2);
                    }
                    catch (AggregateException ex)
                    {
                        MessageBox.Show($"Thread error: {ex.InnerExceptions.FirstOrDefault()?.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    Marshal.Copy(resultPointer, resultBuffer, 0, bytes);
                    Marshal.Copy(resultBuffer, 0, resultData.Scan0, bytes);

                    stopwatch.Stop();
                    updateInfoAfterThreshold(stopwatch.ElapsedMilliseconds, stopwatch.ElapsedTicks, numberOfThreads, thresholdValue);
                }
                catch
                {
                    MessageBox.Show("Pointers error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Free memory
                    if (sourcePointer != IntPtr.Zero) { Marshal.FreeHGlobal(sourcePointer); }
                    if (grayScalePointer != IntPtr.Zero) { Marshal.FreeHGlobal(grayScalePointer); }
                    if (histogramPointer != IntPtr.Zero) { Marshal.FreeHGlobal(histogramPointer); }
                    if (resultPointer != IntPtr.Zero) { Marshal.FreeHGlobal(resultPointer); }
                    sourceBuffer = null;
                    grayScaleBuffer = null;
                    histogramBuffer = null;
                    resultBuffer = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Memory error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                if (grayScaleData != null) { grayScaleBitmap.UnlockBits(grayScaleData); }
                if (sourceData != null) { image.UnlockBits(sourceData); }
                if (resultData != null)
                {
                    resultBitmap.UnlockBits(resultData);

                    if (resultImage != null)
                    {
                        // Delete prev img
                        resultImage.Dispose();
                    }
                    resultImage = resultBitmap;
                    pictureBox_after.Image = resultImage;
                }
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
                        if (image != null) { image.Dispose(); }
                        image = new Bitmap(filePath);

                        if (image == null)
                        {
                            throw new Exception("Failed to load image.");
                        }
                        else
                        {
                            if (pictureBox_before.Image != null)
                            {
                                pictureBox_before.Image.Dispose();
                            }
                            pictureBox_before.Image = Image.FromFile(filePath);
                            updateInfo();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private int OtsuMethod(int[] histogram)
        {
            // Ca³kowita liczba pikseli
            int total = 0;
            for (int i = 0; i < histogram.Length; i++)
            {
                total += histogram[i];
            }

            // Normalizacja histogramu
            double[] normalizedHistogram = new double[histogram.Length];
            for (int i = 0; i < histogram.Length; i++)
            {
                normalizedHistogram[i] = (double)histogram[i] / total;
            }

            // Suma wartoœci i odcieni szaroœci (teraz z normalizowanymi wartoœciami)
            double sum = 0;
            for (int i = 0; i < normalizedHistogram.Length; i++)
            {
                sum += i * normalizedHistogram[i];
            }

            // Inicjacja zmiennych: suma t³a, liczba pikseli nale¿¹cych do t³a, liczba pikseli nale¿¹cych do pierwszego planu
            double sumB = 0;
            double weightBackground = 0;
            double weightForeground = 0;

            // Inicjacja zmiennych do maksymalnej wariancji miêdzy klasowej oraz obliczonego progu
            double maxVariance = 0;
            int thresholdValue = 0;

            // Pêtla po wszystkich mo¿liwych wartoœciach progu
            for (int i = 0; i < normalizedHistogram.Length; i++)
            {
                // Dodawanie liczby pikseli w tle (normalizowane wartoœci)
                weightBackground += normalizedHistogram[i];

                if (weightBackground == 0)
                    continue;

                // Obliczanie liczby pikseli w pierwszym planie 
                weightForeground = 1.0 - weightBackground;

                if (weightForeground == 0)
                    break;

                // Dodanie do sumy t³a wartoœci iloczynu aktualnego odcienia i liczby jego pikseli (normalizowane)
                sumB += i * normalizedHistogram[i];

                // Œrednia dla t³a (suma/iloœæ) - u¿ywamy normalizowanych wartoœci
                double meanBackground = sumB / weightBackground;
                // Œrednia dla pierwszego planu - równie¿ z normalizowanymi wartoœciami
                double meanForeground = (sum - sumB) / weightForeground;
                // Wariancja miêdzyklasowa
                double betweenVariance = weightBackground * weightForeground * Math.Pow(meanBackground - meanForeground, 2);

                // Zapis nowych wartoœci je¿eli aktualna wariancja > max wariancja
                if (betweenVariance > maxVariance)
                {
                    maxVariance = betweenVariance;
                    thresholdValue = i;
                }
            }
            // Zwróæ wynik
            return thresholdValue;
        }


        private void btn_thresholding_Click(object sender, EventArgs e)
        {
            panel_settings.Visible = false;
            panel_thresholding.Visible = true;
            // buttons settings
            btn_settings.BackColor = Color.Black;
            btn_settings.ForeColor = Color.White;

            btn_thresholding.BackColor = Color.White;
            btn_thresholding.ForeColor = Color.Black;
        }

        private void btn_settings_Click(object sender, EventArgs e)
        {
            panel_settings.Visible = true;
            panel_thresholding.Visible = false;
            // buttons settings
            btn_settings.BackColor = Color.White;
            btn_settings.ForeColor= Color.Black;

            btn_thresholding.BackColor= Color.Black;
            btn_thresholding.ForeColor= Color.White;
        }

        private void radioCLib_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                trackBar1.Enabled = false;
                thresholdValueTxt.Text = "Value: auto";
            }
            else
            {
                trackBar1.Enabled = true;
                thresholdValueTxt.Text = "Value: " + trackBar1.Value;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            thresholdValueTxt.Text = "Value: " + trackBar1.Value;
        }

        private void btnThresholding_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                doThresholding_autoThreshold();
            }
            else
            {
                doThresholding_customThreshold();
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (resultImage != null)
            {
                saveFileDialog1.Filter = "JPEG Image|*.jpg|PNG Image|*.png|Bitmap Image|*.bmp";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Zapisujemy obraz do wybranego pliku
                        resultImage.Save(saveFileDialog1.FileName);

                        // Informujemy u¿ytkownika o powodzeniu
                        MessageBox.Show("Obraz zosta³ pobrany!");
                    }
                    catch (Exception ex)
                    {
                        // W razie b³êdu wyœwietlamy komunikat
                        MessageBox.Show("Wyst¹pi³ b³¹d podczas zapisywania obrazu: " + ex.Message);
                    }
                }

            }
            else
            {
                MessageBox.Show("Brak obrazu do pobrania!", "Error", MessageBoxButtons.OK);
            }
        }
    }
}