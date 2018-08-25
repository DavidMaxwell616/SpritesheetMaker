using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpritesheetMaker
{

    public static class BitmapFunctions
    {
        public static void CopyRegionIntoImage(Bitmap srcBitmap, Rectangle srcRegion, ref Bitmap destBitmap, Rectangle destRegion)
        {
            using (Graphics grD = Graphics.FromImage(destBitmap))
            {
                grD.DrawImage(srcBitmap, destRegion, srcRegion, GraphicsUnit.Pixel);
            }
        }

        public static Bitmap ExpandTargetBitmap(Bitmap _target, int cropWidth, int cropHeight)
        {

            if ( _target.Height < cropHeight)
            {
                Bitmap newImage = new Bitmap(cropWidth, cropHeight, _target.PixelFormat);
                using (Graphics g = Graphics.FromImage(newImage))
                {
                    g.FillRectangle(Brushes.Black, 0, 0, cropWidth, cropHeight);
                    g.DrawImage(_target, 0, 0);
                }
                return newImage;
            }
            return _target;
        }


    }
}
