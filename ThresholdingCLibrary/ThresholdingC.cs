namespace ThresholdingCLibrary
{
    public class ThresholdingC
    {
        public int BasicOperation(int a, int b)
        {
            return a + b;
        }

        public unsafe void ConvertToGrayscale(IntPtr sourcePointer, IntPtr resultPointer, int width, int height, int stride, int startRow, int endRow)
        {
            int bytesPerPixel = 4; // R G B A (empty)
            byte* source = (byte*)sourcePointer;
            byte* result = (byte*)resultPointer;

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
                    byte gray = (byte)(0.3 * red + 0.59 * green + 0.11 * blue);

                    // Zapis wyniku do bufora wynikowego
                    result[pixelIndex] = gray;      // Blue
                    result[pixelIndex + 1] = gray;  // Green
                    result[pixelIndex + 2] = gray;  // Red
                    result[pixelIndex + 3] = 255;   // Alpha (pełna przezroczystość)
                }
            }
        }

    }
}