using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Vovinam.Common
{
    public class ImageUtils
    {
        public static bool RewriteImageFix(Bitmap bmpUpload, int width, int height, string filename)
        {
            try
            {

                // nếu kích cỡ hình nhỏ hơn kích cỡ mong muốn thumbnail thì không cần thumbnail
                //if (bmpUpload.Width <= width && bmpUpload.Height <= height)
                //{
                //    return RewriteImage(bmpUpload, filename);
                //}

                #region Caculate thumb width, height
                float widthTh, heightTh;
                float widthOrig, heightOrig;
                float fx, fy, f;

                // retain aspect ratio
                widthOrig = bmpUpload.Width;
                heightOrig = bmpUpload.Height;

                // subsample factors
                fx = widthOrig / width;
                fy = heightOrig / height;

                // must fit in thumbnail size
                f = Math.Max(fx, fy);
                if (f < 1) f = 1;
                // Recalculate thumbnail size
                widthTh = (widthOrig / f);
                heightTh = (heightOrig / f);

                float beginX = (width - widthTh) / 2;
                float beginY = (height - heightTh) / 2;
                #endregion

                Bitmap bmpSave = new Bitmap(width, height);

                Graphics graph = Graphics.FromImage(bmpSave);
                graph.SmoothingMode = SmoothingMode.AntiAlias;
                graph.CompositingQuality = CompositingQuality.HighQuality;
                graph.InterpolationMode = InterpolationMode.HighQualityBicubic;

                graph.Clear(Color.White);
                graph.DrawImage(bmpUpload, beginX, beginY, widthTh, heightTh);

                string contentType = "image/jpeg";
                ImageCodecInfo codec = GetEncoderInfo(contentType);
                if (codec == null)
                    codec = FindEncoder(ImageFormat.Jpeg);
                EncoderParameters eps = new EncoderParameters();
                eps.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
                bmpSave.Save(filename, codec, eps);
                bmpSave.Dispose();

                //Finish
                graph.Dispose();
                eps.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static bool RewriteImage(Bitmap sourceBitmap, string saveFilePath)
        {
            if (sourceBitmap != null)
            {
                EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
                ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = qualityParam;
                sourceBitmap.Save(saveFilePath, jpegCodec, encoderParams);

                qualityParam.Dispose();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the encoder information for the specified mimetype.  Used in imagescaling
        /// </summary>
        /// <param name="mimeType">The mimetype of the picture.</param>
        /// <returns>System.Drawing.Imaging.ImageCodecInfo</returns>
        internal static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] myEncoders = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo myEncoder in myEncoders)
                if (myEncoder.MimeType == mimeType)
                    return myEncoder;

            return null;
        }

        internal static ImageCodecInfo FindEncoder(ImageFormat format)
        {
            ImageCodecInfo[] infoArray1 = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo[] infoArray2 = infoArray1;
            for (int num1 = 0; num1 < infoArray2.Length; num1++)
            {
                ImageCodecInfo info1 = infoArray2[num1];
                if (info1.FormatID.Equals(format.Guid))
                    return info1;
            }
            return null;
        }
    }
}
