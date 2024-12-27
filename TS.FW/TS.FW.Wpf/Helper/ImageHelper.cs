using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TS.FW.Wpf.Helper
{
    public static class ImageHelper
    {
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteObject([In] IntPtr hobject);

        public static ImageSource ToConvert(this Bitmap bmp)
        {
            var handler = bmp.GetHbitmap();

            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handler, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(handler);
            }
        }

        public static Bitmap ToCovert(this BitmapImage img)
        {
            using (var stream = new MemoryStream())
            {
                var enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(img));
                enc.Save(stream);

                using (var bmp = new System.Drawing.Bitmap(stream))
                    return new System.Drawing.Bitmap(bmp);
            }
        }

        public static BitmapImage ToBitmapImage(this string path)
        {
            var bitmap = new BitmapImage();

            using (var stream = File.OpenRead(path))
            {
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = stream;
                bitmap.EndInit();

                return bitmap;
            }
            
        }

        public static void Save(this BitmapImage img, string path, Rect rect)
        {
            try
            {
                using (var bmp = img.ToCovert())
                {
                    using (var save = bmp.Clone(rect.ToRectangleF(), bmp.PixelFormat))
                    {
                        save.Save(path);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(ImageHelper), ex);
            }
        }

        private static RectangleF ToRectangleF(this Rect rect) => new RectangleF((float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
    }
}
