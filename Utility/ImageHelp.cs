using System;
using System.Drawing;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using System.IO;
using O2S.Components.PDFRender4NET;

namespace Utility
{
    /// <summary>
    /// 图像处理帮助类 
    /// </summary>
    public class ImageHelp
    {
        /// <summary>
        /// 生成二维码：最大1850字符，字符多越多，像素越密集，识别率下降。
        /// 使用方法举例 bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        /// </summary>
        /// <param name="contents">数据内容（）</param>
        /// <param name="width">宽度</param>
        /// <param name="height">长度</param>
        /// <returns></returns>
        public static Bitmap CreateQRCode(string contents, int width, int height)
        {
            EncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = width,
                Margin=0,
                Height = height
            };

            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;

            Bitmap bitmap = writer.Write(contents);
            return bitmap;
        }

        /// <summary>
        /// 生成条码
        /// 使用方法举例 bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        /// </summary>
        /// <param name="contents">数据内容</param>
        /// <param name="width">宽度</param>
        /// <param name="height">长度</param>
        /// <returns></returns>
        public static Bitmap CreateITFCode(string contents, int width, int height)
        {
            EncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = width,
                Height = height
            };

            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.CODE_128;
            writer.Options = options;
            writer.Options.PureBarcode = true;

            Bitmap bitmap = writer.Write(string.Format("{0}", contents));
            return bitmap;
        }

        /// <summary>
        /// 将图片返回Base64格式编码
        /// </summary>
        /// <param name="ImageFilePath">图片地址</param>
        /// <returns>图片Base64编码</returns>
        public static string ImgToBase64String(string ImageFilePath)
        {
            Image img = Image.FromFile(ImageFilePath);
            byte[] arr = null;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
            }
            img.Dispose();
            return Convert.ToBase64String(arr);
        }

        /// <summary>
        /// 将图片返回二进制数组
        /// </summary>
        /// <param name="ImageFilePath">图片地址</param>
        /// <returns>图片二进制数组</returns>
        public static byte[] ImgToByte(string ImageFilePath)
        {
            Image img = Image.FromFile(ImageFilePath);
            byte[] arr = null;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
            }
            img.Dispose();
            return arr;
        }

        /// <summary>
        /// 将文件转化为二进制数组
        /// </summary>
        /// <param name="fileName">文件地址</param>
        /// <returns>二进制数组</returns>
        public static byte[] FileToByte(string fileName)
        {
            byte[] bBuffer;

            using (FileStream fs = new FileStream(fileName, FileMode.Open,FileAccess.Read))
            {
                BinaryReader binReader = new BinaryReader(fs);

                bBuffer = new byte[fs.Length];
                binReader.Read(bBuffer, 0, (int)fs.Length);
                binReader.Close();
                fs.Close();
                return bBuffer;
            }
        }


        /// <summary>
        /// 将Base64String生成文件
        /// </summary>
        /// <param name="filePath">文件保存路径</param>
        /// <param name="fileContent">文件内容Base64String</param>
        public static void CreateFileByBase64String(string filePath, string fileContent)
        {
            byte[] s = Convert.FromBase64String(fileContent);

            File.WriteAllBytes(filePath, s);

        }

        /// <summary>
        /// 获取Url地址中排除？号及后参数的实际地址
        /// </summary>
        /// <param name="url">url地址</param>
        /// <returns>排除？号及后参数的Url地址</returns>
        public static string GetUrlNoParam(string url)
        {
            if (url.Contains("?") == false)
                return url;
            else
                return url.Substring(0, url.IndexOf("?"));
        }

   //     /// <summary>
   //     /// 图片添加任意角度文字(文字旋转是中心旋转，角度顺时针为正)
   //     /// </summary>
   //     /// <param name="imgpath">图片路径</param>
   //     /// <param name="locationlefttop">文字左上角定位(x1,y1)</param>
   //     /// <param name="fontsize">字体大小，单位为像素</param>
   //     /// <param name="text">文字内容</param>
   //     /// <param name="angle">文字旋转角度</param>
   //     /// <param name="fontname">字体名称</param>
   //     /// <returns>添加文字后的bitmap对象</returns>
   //     public bitmap addtext(string imgpath, string locationlefttop, int fontsize, string text, int angle = 0, string fontname = "华文行楷")
   //     {
   //         image img = image.fromfile(imgpath);
   //         int width = img.width;
   //         int height = img.height;
   //         bitmap bmp = new bitmap(width, height);
   //         graphics graphics = graphics.fromimage(bmp);
   //         // 画底图
   //         graphics.drawimage(img, 0, 0, width, height);
   //         font font = new font(fontname, fontsize, graphicsunit.pixel);
   //         sizef sf = graphics.measurestring(text, font); // 计算出来文字所占矩形区域
   //         // 左上角定位
   //         string[] location = locationlefttop.split(',');
   //         float x1 = float.parse(location[0]);
   //         float y1 = float.parse(location[1]);
   //         // 进行文字旋转的角度定位
   //         if (angle != 0)
   //         {
   //             #region 法一：translatetransform平移 + rotatetransform旋转
   //             /*
   //* 注意：
   //* graphics.rotatetransform的旋转是以graphics对象的左上角为原点，旋转整个画板的。
   //* 同时x，y坐标轴也会跟着旋转。即旋转后的x，y轴依然与矩形的边平行
   //* 而graphics.translatetransform方法，是沿着x，y轴平移的
   //* 因此分三步可以实现中心旋转
   //* 1.把画板(graphics对象)平移到旋转中心
   //* 2.旋转画板
   //* 3.把画板平移退回相同的距离(此时的x，y轴仍然是与旋转后的矩形平行的)
   //*/
   //             //// 把画板的原点(默认是左上角)定位移到文字中心
   //             //graphics.translatetransform(x1 + sf.width / 2, y1 + sf.height / 2);
   //             //// 旋转画板
   //             //graphics.rotatetransform(angle);
   //             //// 回退画板x,y轴移动过的距离
   //             //graphics.translatetransform(-(x1 + sf.width / 2), -(y1 + sf.height / 2));
   //             #endregion
   //             #region 法二：矩阵旋转
   //             matrix matrix = graphics.transform;
   //             matrix.rotateat(angle, new pointf(x1 + sf.width / 2, y1 + sf.height / 2));
   //             graphics.transform = matrix;
   //             #endregion
   //         }
   //         // 写上自定义角度的文字
   //         graphics.drawstring(text, font, new solidbrush(color.black), x1, y1);
   //         graphics.dispose();
   //         img.dispose();
   //         return bmp;
   //     }

        /// <summary>
        /// 图片添加任意角度文字(文字旋转是中心旋转，角度顺时针为正)
        /// </summary>
        /// <param name="imgpath">图片路径</param>
        /// <param name="locationlefttop">文字左上角定位(x1,y1)</param>
        /// <param name="fontsize">字体大小，单位为像素</param>
        /// <param name="text">文字内容</param>
        /// <param name="angle">文字旋转角度</param>
        /// <param name="fontname">字体名称</param>
        /// <returns>添加文字后的bitmap对象</returns>
        public static void AddWaterMark(string imgpath, string locationlefttop, int fontsize, string text, int angle = 0, string fontname = "宋体")
        {
            //保存预览图片地址
            //string localPath = string.Format("~/UpLoad/PrintCertificate/{0}.jpg", ob.CertificateCAID);

            //////证书图片路径
            ////if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/PrintCert/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/PrintCert/"));


            //string personPhoto = Server.MapPath(UIHelp.GetFaceImagePath(ob.FacePhoto, ob.WorkerCertificateCode));//一寸照片
            ////**********可以修改成首次打印用考试报名照片，其它业务用人员照片
            if (File.Exists(imgpath) == false) return;
        
            System.Drawing.Image imgSrc = null;//证书照片背景图
            Bitmap bmpDest = null;


            imgSrc = System.Drawing.Image.FromFile(imgpath);
            using (Graphics g = Graphics.FromImage(imgSrc))
            {

                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //StringFormat format = new StringFormat();
                //format.Alignment = StringAlignment.Center;


                //输出证书信息
                using (Font f = new Font(fontname, fontsize,FontStyle.Bold, GraphicsUnit.Point))
                {
                    using (Brush b = new SolidBrush(Color.Red))
                    {

                        SizeF sf = g.MeasureString(text, f); // 计算出来文字所占矩形区域
                        // 左上角定位
                        string[] location = locationlefttop.Split(',');
                        float x1 = float.Parse(location[0]);
                        float y1 = float.Parse(location[1]);
                        // 进行文字旋转的角度定位
                        if (angle != 0)
                        {
                            //矩阵旋转
                            System.Drawing.Drawing2D.Matrix matrix = g.Transform;
                            matrix.RotateAt(angle, new PointF(x1 + sf.Width / 2, y1 + sf.Height / 2));
                            g.Transform = matrix;
                           }
                        // 写上自定义角度的文字
                        g.DrawString(text, f, new SolidBrush(Color.FromArgb(98, Color.Gray)), x1, y1);

                    }
                }
            }


            using (bmpDest = new Bitmap(Convert.ToInt32(imgSrc.Width * 1), Convert.ToInt32(imgSrc.Height * 1)))
            {
                using (Graphics gg = Graphics.FromImage(bmpDest))
                {
                    gg.DrawImage(imgSrc, 0, 0, (float)(imgSrc.Width * 1), (float)(imgSrc.Height * 1));
                }
                imgSrc.Dispose();
                bmpDest.Save(imgpath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }


        /// <summary>
        /// pdf转图片
        /// </summary>
        /// <param name="filePath">pdf文件路径</param>
        /// <param name="picPath">图片保存路径</param>
        /// <param name="sizeTimes">图片尺寸倍数，默认1</param>
        public static void PdfToPic(string filePath, string picPath, float sizeTimes = 1)
        {
            var pdf = PdfiumViewer.PdfDocument.Load(filePath);
            var pdfpage = pdf.PageCount;
            var pagesizes = pdf.PageSizes;
            for (int i = 1; i <= pdfpage; i++)
            {
                Size size = new Size();
                size.Height = (int)(pagesizes[(i - 1)].Height * sizeTimes);
                size.Width = (int)(pagesizes[(i - 1)].Width * sizeTimes);
                //可以把".jpg"写成其他形式
                RenderPage(filePath, i, size, picPath,96);
            }
        }
        private static void RenderPage(string pdfPath, int pageNumber, System.Drawing.Size size, string outputPath, int dpi = 9)
        {
            using (var document = PdfiumViewer.PdfDocument.Load(pdfPath))
            using (var stream = new FileStream(outputPath, FileMode.Create))
            using (var image = GetPageImage(pageNumber, size, document, dpi))
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
        private static System.Drawing.Image GetPageImage(int pageNumber, Size size, PdfiumViewer.PdfDocument document, int dpi)
        {
            return document.Render(pageNumber - 1, size.Width, size.Height, dpi, dpi, PdfiumViewer.PdfRenderFlags.Annotations);
        }


        /// <summary>
        /// 作废：将PDF文档转换为图片（O2S.Components.PDFRender4NET.dll版本过低，转化后丢汉字）
        /// </summary>
        /// <param name="pdfInputPath">PDF文件路径</param>
        /// <param name="imageOutputPath">图片输出路径(含文件名)</param>
        /// <param name="startPageNum">从PDF文档的第几页开始转换，从１开始</param>
        /// <param name="endPageNum">从PDF文档的第几页开始停止转换</param>
        /// <param name="imageFormat">设置所需图片格式</param>
        /// <param name="definition">设置图片的清晰度，数字越大越清晰,图片长宽大小也变大（100,200,300....）</param>
        public static void ConvertPDF2Image(string pdfInputPath, string imageOutputPath, int startPageNum, int endPageNum, System.Drawing.Imaging.ImageFormat imageFormat, int definition)
        {
            using (PDFFile pdfFile = PDFFile.Open(pdfInputPath))
            {
                if (!Directory.Exists(Path.GetDirectoryName(imageOutputPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(imageOutputPath));
                }
                // validate pageNum
                if (startPageNum <= 0)
                {
                    startPageNum = 1;
                }
                if (endPageNum > pdfFile.PageCount)
                {
                    endPageNum = pdfFile.PageCount;
                }
                if (startPageNum > endPageNum)
                {
                    int tempPageNum = startPageNum;
                    startPageNum = endPageNum;
                    endPageNum = startPageNum;
                }
                // start to convert each page
                for (int i = startPageNum; i <= endPageNum; i++)
                {
                    using (Bitmap pageImage = pdfFile.GetPageImage(i - 1, definition))
                    {
                        pageImage.Save(imageOutputPath, imageFormat);
                        //pageImage.Dispose();
                    }
                }
                //pdfFile.Dispose();
            }
        }

        /// <summary>
        /// 将PDF文档第一页转换为jpg图片
        /// </summary>
        /// <param name="pdfInputPath">文件路径</param>
        /// <param name="imageOutputPath">图片输出路径(含文件名)</param>
        /// <param name="definition">设置图片的清晰度，数字越大越清晰,图片长宽大小也变大（100,200,300....）</param>
        public static void ConvertPDFtoJPG(string pdfInputPath, string imageOutputPath, int definition)
        {
             ConvertPDF2Image( pdfInputPath,  imageOutputPath,1, 1, System.Drawing.Imaging.ImageFormat.Png, definition);
        }
    }
}
