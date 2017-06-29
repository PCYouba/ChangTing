//------------------------------------------------------------------------------
// <copyright company="yuhou.cn">
//     Copyright © YuHou Technology, All Rights Reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Configuration;

namespace ChangTing.Core.Common
{
    /// <summary>
    /// 图片处理类，提供相关GDI+图片处理及图片优化存储函数。
    /// </summary>  
    public static class ImageOperation
    {
        #region DefaultImageWidthLimit
        /// <summary>
        /// 默认图片的最大像素宽度限制
        /// </summary>
        public static readonly int DefaultImageWidthLimit = int.Parse(ConfigurationManager.AppSettings["DefaultImageWidthLimit"] ?? "700");
        #endregion

        #region IntelligentSaveImageFile
        /// <summary>
        /// 打水印时使用的字体文件完整的磁盘路径
        /// </summary>
        public static string WaterMarkFontFilePath
        {
            set; get;
        }
        /// <summary>
        /// 智能存储文件函数(优化存储)。不符合本函数处理的文件会忽略，调用处需做好后续保存检查。
        /// </summary>
        /// <remarks>
        /// 调用示例：
        /// if (!IntelligentSaveImageFile(InputStream, FileName, TargetSize, TargetSizeOnlyForWidthLimit, WaterMark, new Size(0, 0)))
        ///     UploadFile.SaveAs(FileName); //需要检查保存IntelligentSaveImageFile忽略的文件。
        /// </remarks>
        /// <param name="InputStream">上传文件数据流UploadFile.InputStream</param>
        /// <param name="FileName">存储目标磁盘路径</param>
        /// <param name="TargetSize">目标图片像素大小限制(0表示自动原图大小)</param>
        /// <param name="TargetSizeOnlyForWidthLimit">目标图片大小是否仅用作宽度限制，否则宽高均不得超限</param>
        /// <param name="WaterMark">图片的水印字符串(如果为空“string.Empty”则不打印水印)</param>
        /// <param name="WaterMarkOnImageSizeLimit">打水印时图片尺寸限制，宽高均达到此要求的才打水印</param>
        /// <returns>false没存储(说明不支持处理此类型文件 需自行存储)，true已经存储(成功)</returns>
        public static bool IntelligentSaveImageFile(Stream InputStream, string FileName, int TargetSize, bool TargetSizeOnlyForWidthLimit, string WaterMark, Size WaterMarkOnImageSizeLimit)
        {
            byte[] ImageFileData = new byte[InputStream.Length];
            InputStream.Read(ImageFileData, 0, ImageFileData.Length);
            return IntelligentSaveImageFile(ImageFileData, FileName, TargetSize, TargetSizeOnlyForWidthLimit, WaterMark, WaterMarkOnImageSizeLimit);
        }
        /// <summary>
        /// 智能存储文件函数(优化存储)。不符合本函数处理的文件会忽略，调用处需做好后续保存检查。
        /// </summary>
        /// <remarks>
        /// 调用示例：
        /// if (!IntelligentSaveImageFile(ImageFileData, FileName, TargetSize, TargetSizeOnlyForWidthLimit, WaterMark, new Size(0, 0)))
        ///     UploadFile.SaveAs(FileName); //需要检查保存IntelligentSaveImageFile忽略的文件。
        /// </remarks>
        /// <param name="ImageFileData">文件字节数据数组</param>
        /// <param name="FileName">存储目标磁盘路径</param>
        /// <param name="TargetSize">目标图片像素大小限制(0表示自动原图大小)</param>
        /// <param name="TargetSizeOnlyForWidthLimit">目标图片大小是否仅用作宽度限制，否则宽高均不得超限</param>
        /// <param name="WaterMark">图片的水印字符串(如果为空“string.Empty”则不打印水印)</param>
        /// <param name="WaterMarkOnImageSizeLimit">打水印时图片尺寸限制，宽高均达到此要求的才打水印</param>
        /// <returns>false没存储(说明不支持处理此类型文件 需自行存储)，true已经存储(成功)</returns>
        public static bool IntelligentSaveImageFile(byte[] ImageFileData, string FileName, int TargetSize, bool TargetSizeOnlyForWidthLimit, string WaterMark, Size WaterMarkOnImageSizeLimit)
        {
            ImageFormat SaveImgFormat = ImageFormat.Jpeg;
            switch (Path.GetExtension(FileName).ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    break;
                case ".png":
                    SaveImgFormat = ImageFormat.Png;
                    break;
                case ".bmp": //bmp自动转为jpg
                    FileName = string.Format("{0}.jpg", FileName.Substring(0, FileName.Length - 4));
                    break;
                default:
                    return false;
            }
            using (System.Drawing.Image oldImage = System.Drawing.Image.FromStream(new MemoryStream(ImageFileData)))
            {
                if (oldImage.Width < WaterMarkOnImageSizeLimit.Width || oldImage.Height < WaterMarkOnImageSizeLimit.Height)
                    WaterMark = string.Empty;
                int ImageKB = ImageFileData.Length / 1024;
                if (ImageKB <= 100 && TargetSize <= 0 && string.IsNullOrEmpty(WaterMark))
                    return false;
                Size newSize = oldImage.Size;
                if (TargetSize <= 0 && oldImage.Width > DefaultImageWidthLimit)
                {
                    TargetSize = DefaultImageWidthLimit;
                    TargetSizeOnlyForWidthLimit = true;
                }
                if (TargetSize > 0)
                    newSize = CalculateDimensions(oldImage.Size, TargetSize, TargetSizeOnlyForWidthLimit);
                using (Bitmap newImage = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format24bppRgb))
                {
                    using (Graphics canvas = Graphics.FromImage(newImage))
                    {
                        canvas.SmoothingMode = SmoothingMode.AntiAlias;
                        canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        canvas.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        canvas.DrawImage(oldImage, new Rectangle(new Point(-1, -1), new Size(newSize.Width + 2, newSize.Height + 2)));
                        if (!string.IsNullOrEmpty(WaterMark))
                        {
                            canvas.TextRenderingHint = TextRenderingHint.AntiAliasGridFit; //文字消除锯齿。
                            Font font;
                            if (string.IsNullOrEmpty(WaterMarkFontFilePath))
                            {
                                font = new Font("华文彩云", 25, FontStyle.Regular, GraphicsUnit.Pixel); //服务器上要安装此字体。
                            }
                            else
                            {
                                PrivateFontCollection YPSFonts = new PrivateFontCollection();
                                YPSFonts.AddFontFile(WaterMarkFontFilePath);
                                font = new Font(YPSFonts.Families[0], 25, FontStyle.Regular, GraphicsUnit.Pixel);
                            }
                            SolidBrush s = new SolidBrush(Color.FromArgb(150, 0, 0, 0));
                            SolidBrush s1 = new SolidBrush(Color.FromArgb(150, 255, 255, 255)); //修改水印字体、颜色、透明度。
                            int top = newSize.Height - 45;
                            int left = 25;
                            if (top >= 0 && left >= 0)
                            {
                                canvas.DrawString(WaterMark, font, s, left, top);
                                canvas.DrawString(WaterMark, font, s1, left + 1, top + 1);
                            }
                        }
                        //以下为设置图片质量重要代码。
                        ImageCodecInfo theImageCodecInfo;
                        //不同文件类型记得更改ImageFormat值。
                        theImageCodecInfo = GetEncoder(SaveImgFormat);
                        Encoder theEncoder = Encoder.Quality;
                        using (EncoderParameters theEncoderParameters = new EncoderParameters(1))
                        {
                            //设定图片质量阀值(95L)，未经允许不得更动。
                            using (EncoderParameter theEncoderParameter = new EncoderParameter(theEncoder, 95L))
                            {
                                theEncoderParameters.Param[0] = theEncoderParameter;
                                newImage.Save(FileName, theImageCodecInfo, theEncoderParameters);
                            }
                        }
                    }
                }
            }
            return true;
        }
        #endregion

        #region (Private)GetEncoder
        /// <summary>
        /// 获得图像编码器/解码器相关信息。
        /// </summary>
        /// <param name="format">图像类型</param>
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        #endregion
        
        #region (Private)CalculateDimensions
        /// <summary>
        /// 按比例缩放图片大小
        /// </summary>
        /// <param name="OldSize">图片原始大小</param>
        /// <param name="TargetSize">图片新大小</param>
        /// <param name="TargetSizeOnlyForWidthLimit">目标图片大小是否仅用作宽度限制，否则宽高均不得超限</param>
        /// <returns>Size图片大小</returns>
        private static Size CalculateDimensions(Size OldSize, int TargetSize, bool TargetSizeOnlyForWidthLimit)
        {
            if (OldSize.Width <= TargetSize && OldSize.Height <= TargetSize)
                return OldSize; //原图尺寸。
            if (TargetSizeOnlyForWidthLimit && OldSize.Width <= TargetSize)
                return OldSize; //原图尺寸。
            Size newSize = new Size();
            if (TargetSizeOnlyForWidthLimit || OldSize.Width >= OldSize.Height)
            {
                newSize.Width = TargetSize;
                newSize.Height = (int)(OldSize.Height * ((float)TargetSize / (float)OldSize.Width));
            }
            else
            {
                newSize.Width = (int)(OldSize.Width * ((float)TargetSize / (float)OldSize.Height));
                newSize.Height = TargetSize;
            }
            return newSize;
        }
        #endregion
        
    }
}

