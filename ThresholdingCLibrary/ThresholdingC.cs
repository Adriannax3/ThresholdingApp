using System;

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
            byte* source = (byte*)sourcePointer;
            byte* result = (byte*)resultPointer;
            int* histogram = (int*)histogramPointer;

            // definition of auxiliary variables
            int bytesPerPixel = 4;
            float redWeight = 0.299f;
            float greenWeight = 0.587f;
            float blueWeight = 0.114f;

            for (int y = startRow; y < endRow; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // calculate the offset for each pixel
                    int pixelIndex = (y * stride) + (x * bytesPerPixel);

                    // getting the value of each color
                    byte blue = source[pixelIndex];
                    byte green = source[pixelIndex + 1];
                    byte red = source[pixelIndex + 2];

                    // calculated gray value from the formula
                    float grayValue = (red * redWeight + green * greenWeight + blue * blueWeight);
                    // conversion
                    int gray = (int) grayValue;

                    // increment histogram
                    Interlocked.Increment(ref histogram[gray]);

                    // saving the resulting color
                    result[pixelIndex] = (byte)gray;     
                    result[pixelIndex + 1] = (byte)gray;
                    result[pixelIndex + 2] = (byte)gray;
                    result[pixelIndex + 3] = 255;
                }
            }
        }

        public unsafe void Thresholding(IntPtr sourcePointer, IntPtr resultPointer, int width, int thresholdValue, int stride, int startRow, int endRow)
        {
            byte* source = (byte*)sourcePointer;
            byte* result = (byte*)resultPointer;

            // definition of auxiliary variables
            byte color = 0;
            const int bytesPerPixel = 4;

            const float redWeight = 0.299f;
            const float greenWeight = 0.587f;
            const float blueWeight = 0.114f;

            // outer loop
            for (int y = startRow; y < endRow; y++)
            {
                // inner loop
                for (int x = 0; x < width; x++)
                {
                    // calculate the offset for each pixel
                    int pixelIndex = (y * stride) + (x * bytesPerPixel);

                    // getting the value of each color
                    byte blue = source[pixelIndex];
                    byte green = source[pixelIndex + 1];
                    byte red = source[pixelIndex + 2];

                    // calculated gray value from the formula
                    float grayValue = (red * redWeight + green * greenWeight + blue * blueWeight);

                    // conversion 
                    int gray = (int)grayValue;

                    // comparison with the threshold
                    color = gray <= thresholdValue ? (byte)0 : (byte)255;

                    // saving the resulting color
                    result[pixelIndex] = color;      
                    result[pixelIndex + 1] = color;  
                    result[pixelIndex + 2] = color;
                    result[pixelIndex + 3] = 255;
                }
            }
        }

        public unsafe void ThresholdingOnly(IntPtr sourcePointer, IntPtr resultPointer, int width, int thresholdValue, int stride, int startRow, int endRow)
        {
            // definition of variables
            int bytesPerPixel = 4;
            byte* source = (byte*)sourcePointer;
            byte* result = (byte*)resultPointer;
            byte color = 0;

            // outer loop
            for (int y = startRow; y < endRow; y++)
            {
                // inner loop
                for (int x = 0; x < width; x++)
                {
                    // calculate the offset for each pixel
                    int pixelIndex = (y * stride) + (x * bytesPerPixel);

                    // getting the value of each color
                    byte gray = source[pixelIndex];

                    // comparison with the threshold
                    color = gray <= thresholdValue ? (byte)0 : (byte)255;

                    // saving the resulting color
                    result[pixelIndex] = color;
                    result[pixelIndex + 1] = color;
                    result[pixelIndex + 2] = color;
                    result[pixelIndex + 3] = 255;
                }
            }
        }

    }
}