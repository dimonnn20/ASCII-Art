﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;


namespace ASCII_Art
{
    internal class Program
    {
        private const double WIDTH_OFFSET = 2.0; //1,5
        private const int MAX_WIDTH = 200;

        [STAThread]
        static void Main(string[] args)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Images | *.bmp; *.png; *.jpg; *.JPEG"
            };
            Console.WriteLine("Please enter to start ... \n");
            while (true)
            {
                Console.ReadLine();

                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    continue;

                Console.Clear();
                Bitmap bitmap = new Bitmap(openFileDialog.FileName);
                bitmap = ResizeBitmap(bitmap);
                bitmap.ToGrayScale();
                var converter = new BitmapToAsciiConverter(bitmap);
                var rows = converter.Convert();
                foreach (var row in rows)
                {
                    Console.WriteLine(row);
                    File.WriteAllLines("image.txt",rows.Select(r => new string(r)));
                }
                Console.SetCursorPosition(0,0);

            }
        }

        private static Bitmap ResizeBitmap(Bitmap bitmap)
        {
            var newHeight = bitmap.Height/ WIDTH_OFFSET * MAX_WIDTH/bitmap.Width;
            if (bitmap.Width > MAX_WIDTH || bitmap.Height > newHeight)
                bitmap = new Bitmap(bitmap, new Size(MAX_WIDTH, (int)newHeight));
            return bitmap;
        }
    }
}
