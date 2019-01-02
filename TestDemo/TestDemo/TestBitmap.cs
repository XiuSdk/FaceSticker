using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace TestDemo
{
    unsafe class TestBitmap
    {
        [DllImport("TestDemo_C.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.None, ExactSpelling = true)]
        private static extern int FD_Initialize();
        [DllImport("TestDemo_C.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.None, ExactSpelling = true)]
        private static extern int FD_Process(byte* srcData, int width, int height, int stride, int []faceInfos);

        [DllImport("TestDemo_C.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.None, ExactSpelling = true)]
        private static extern void FD_Unitialize();
        public int FDInitialize()
        {
            return FD_Initialize();
        }
        public void FDUnitialize()
        {
            FD_Unitialize();
        }
        public void FDProcess(Bitmap src, ref int[] faceInfos)
        {
            Bitmap a = new Bitmap(src);
            int w = a.Width;
            int h = a.Height;
            BitmapData srcData = a.LockBits(new Rectangle(0, 0, a.Width, a.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            byte* p = (byte*)srcData.Scan0;
            FD_Process(p, w, h, srcData.Stride, faceInfos);
            a.UnlockBits(srcData);
        }


        [DllImport("TestDemo_C.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.None, ExactSpelling = true)]
        private static extern int Trent_Sticker(byte* srcData, int width, int height, int stride, byte* mask, int maskWidth, int maskHeight, int maskStride, int []srcFacePoints, int []maskFacePoints, int ratio);
        public Bitmap FaceSticker(Bitmap src, Bitmap mask, int[]srcFacePoints, int[]maskFacePoints, int ratio)
        {
            Bitmap a = new Bitmap(src);
            int w = a.Width;
            int h = a.Height;
            BitmapData srcData = a.LockBits(new Rectangle(0, 0, a.Width, a.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            BitmapData maskData = mask.LockBits(new Rectangle(0, 0, mask.Width, mask.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            byte* p = (byte*)srcData.Scan0;
            byte* pMask = (byte*)maskData.Scan0;
            Trent_Sticker(p, w, h, srcData.Stride, pMask, mask.Width, mask.Height, maskData.Stride, srcFacePoints, maskFacePoints, ratio);
            a.UnlockBits(srcData);
            mask.UnlockBits(maskData);
            return a;
        }
    }
}
