namespace ThresholdingCLibrary
{
    public class ThresholdingC
    {
        public int BasicOperation(int a, int b)
        {
            return a + b;
        }

        public unsafe void ConvertToGrayScale(IntPtr sourcePointer, IntPtr resultPointer, int width, IntPtr histogramPointer, int stride, int startRow, int endRow)
        {
            int bytesPerPixel = 4; // R G B A (empty)
            byte* source = (byte*)sourcePointer;
            byte* result = (byte*)resultPointer;
            int* histogram = (int*)histogramPointer;

            float redWeight = 0.299f;
            float greenWeight = 0.587f;
            float blueWeight = 0.114f;

            for (int y = startRow; y < endRow; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Obliczenie pozycji piksela w buforze
                    int pixelIndex = (y * stride) + (x * bytesPerPixel);

                    // Składowe koloru: B, G, R (A ignorujemy, ponieważ to skala szarości)
                    byte blue = source[pixelIndex];
                    byte green = source[pixelIndex + 1];
                    byte red = source[pixelIndex + 2];

                    // Konwersja do skali szarości (luminancja)
                    float grayValue = (red * redWeight + green * greenWeight + blue * blueWeight);
                    int gray = (int) grayValue;

                    //Zablokowanie i inkrementacja histogramu
                    Interlocked.Increment(ref histogram[gray]); 

                    // Zapis wyniku do bufora wynikowego
                    result[pixelIndex] = (byte)gray;      // Blue
                    result[pixelIndex + 1] = (byte)gray;  // Green
                    result[pixelIndex + 2] = (byte)gray;  // Red
                    result[pixelIndex + 3] = 255;   // Alpha
                }
            }
        }

        public unsafe void Thresholding(IntPtr sourcePointer, IntPtr resultPointer, int width, int thresholdValue, int stride, int startRow, int endRow)
        {
            int bytesPerPixel = 4; // R G B A (empty)
            byte* source = (byte*)sourcePointer;
            byte* result = (byte*)resultPointer;
            byte color = 0;

            float redWeight = 0.299f;
            float greenWeight = 0.587f;
            float blueWeight = 0.114f;

            for (int y = startRow; y < endRow; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Obliczenie pozycji piksela w buforze
                    int pixelIndex = (y * stride) + (x * bytesPerPixel);

                    // Składowe koloru: B, G, R (A ignorujemy, ponieważ to skala szarości)
                    byte blue = source[pixelIndex];
                    byte green = source[pixelIndex + 1];
                    byte red = source[pixelIndex + 2];

                    float grayValue = (red * redWeight + green * greenWeight + blue * blueWeight);
                    int gray = (int)grayValue;

                    color = gray <= thresholdValue ? (byte)0 : (byte)255;

                    // Zapis wyniku do bufora wynikowego
                    result[pixelIndex] = color;      // Blue
                    result[pixelIndex + 1] = color;  // Green
                    result[pixelIndex + 2] = color;  // Red
                    result[pixelIndex + 3] = 255;   // Alpha
                }
            }
        }

        public unsafe void ThresholdingOnly(IntPtr sourcePointer, IntPtr resultPointer, int width, int thresholdValue, int stride, int startRow, int endRow)
        {
            int bytesPerPixel = 4; // R G B A (empty)
            byte* source = (byte*)sourcePointer;
            byte* result = (byte*)resultPointer;
            byte color = 0;

            for (int y = startRow; y < endRow; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Obliczenie pozycji piksela w buforze
                    int pixelIndex = (y * stride) + (x * bytesPerPixel);
                    
                    byte gray = source[pixelIndex];

                    color = gray <= thresholdValue ? (byte)0 : (byte)255;

                    // Zapis wyniku do bufora wynikowego
                    result[pixelIndex] = color;      // Blue
                    result[pixelIndex + 1] = color;  // Green
                    result[pixelIndex + 2] = color;  // Red
                    result[pixelIndex + 3] = 255;   // Alpha
                }
            }
        }

    }
}