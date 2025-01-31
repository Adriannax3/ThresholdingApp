# ThresholdingApp
<p>The program is designed to perform image thresholding. Two versions of the thresholding library were written - one in C# and the other in ASM. The graphical interface was made using C# and Windows Forms.</p>

![JA1](https://github.com/user-attachments/assets/6b7b8f4e-6e93-4817-b078-f24b8c71dd3d)

## Algorithm
<p>Image thresholding is a binary way of saving an image. We compare the pixel values ​​with a given threshold and save darker pixels as black (0) and lighter pixels as white (255).
In my program, I first convert the image to grayscale to calculate the pixel's luminance and only then compare the pixel to the threshold.</p>
<p>I also made it possible to calculate the threshold automatically using the Otsu method. Then, while processing the image to grayscale, I also create a histogram. Then the histogram is normalized and the search for the largest inter-class variance takes place. After calculating the threshold, each pixel in the image is compared. </p>

![image](https://github.com/user-attachments/assets/8ff39245-e62d-4178-b868-6c3d19b3553e)

## Program
<p>
  The program consists of two panels - the actual program and the settings. In the settings we can choose:
  <ul>
  <li><strong>Library: </strong>choose between C# or ASM implementation</li>
  <li><strong>Threshold: </strong>set manually or calculate automatically (Otsu's method)</li>
  <li><strong>Threads: </strong>select the number of threads (1-64)</li>
  </ul>
  The program panel contains buttons for uploading an image, performing thresholding, and downloading the result.
  There is also information text on the left side. Here you can read the image path, its height and width, and after thresholding you can also find: execution time [ms], ticks, number of threads and threshold.
</p>

![image](https://github.com/user-attachments/assets/91a3a19f-e196-4f8c-8276-f97555709505)

<p>
  The program uses ASM x64, multithreading and vector instructions.
</p>
