using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace ChangTing.Core.Models
{
    public static class CheckCode
    {
        private static Color[] clor = { Color.Red, Color.Orange, Color.Brown, Color.Green, Color.Blue, Color.Black, Color.Purple ,Color.DarkBlue};
        
        /// <summary>
        ///  生成验证图片核心代码
        /// </summary>
        /// <param name="hc"></param>
        public static byte[] ProcessRequest()
        {
            int intLength = 5;               //长度
            string strIdentify = "rndcode"; //随机字串存储键值，以便存储到Session中

            Random RND = new Random();
            using (Bitmap bm = new Bitmap(200, 60))//画板设计及其大小
            {
                using (Graphics gp = Graphics.FromImage(bm))//画布
                {
                    using (SolidBrush sBrush = new SolidBrush(Color.FromArgb(222, 222, 222)))//画板
                    {
                        gp.FillRectangle(sBrush, 0, 0, bm.Width, bm.Height);//画布所在的画板，粘贴的位置
                        Font font = new Font(FontFamily.GenericSerif, 48, FontStyle.Bold, GraphicsUnit.Pixel);//字体
                        StringBuilder sBuilder = new StringBuilder();

                        string strLetters = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ123456789";//合法随机显示字符列表
                                                                                                             //将随机生成的字符串绘制到图片上
                        for (int i = 0; i < intLength; i++)
                        {
                            string CurrChar = strLetters.Substring(RND.Next(0, strLetters.Length - 1), 1);//随机提取一个数字
                            gp.DrawString(CurrChar, font, new SolidBrush(clor[RND.Next(0, clor.Length)]), i * 38, RND.Next(0, 15));//字的颜色  字的X  Y
                            sBuilder.Append(CurrChar); //保存输出的字符。
                        }
                        
                        for (int i = 0; i < 10; i++)
                        {
                            Color cl = Color.FromArgb(RND.Next(0, 256), RND.Next(0, 256), RND.Next(0, 256));
                            using (Pen pen = new Pen(new SolidBrush(cl), 2))//生成干扰线条 线条的颜色 宽度
                            {
                                gp.DrawLine(pen, new Point(RND.Next(0, bm.Width), RND.Next(0, bm.Height)), new Point(RND.Next(0, bm.Width), RND.Next(0, bm.Height)));
                            }
                        }

                        using (MemoryStream ms = new MemoryStream())
                        {
                            bm.Save(ms, ImageFormat.Gif);//输出格式

                            HttpContext.Current.Session[strIdentify] = sBuilder.ToString().ToLower(); //先保存在Session中(小写)，验证与用户输入是否一致。\
                            return ms.ToArray();
                        }
                    }
                }
            }

        }

    }
}