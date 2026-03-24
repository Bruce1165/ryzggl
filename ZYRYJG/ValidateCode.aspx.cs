using System;
using System.Drawing;
using System.Web;

namespace ZYRYJG
{
    public partial class ValidateCode : System.Web.UI.Page
    {
        private void Page_Load(object sender, System.EventArgs e)
        {
            this.CreateCheckCodeImage(GenerateCheckCode());
        }
        
        public string GenerateCheckCode()
        {
            int number;
            char code;
            string checkCode = String.Empty;

            System.Random random = new Random();

            for (int i = 0; i < 4; i++)
            {
                number = random.Next();

                //if (number % 2 == 0)
                code = (char)('0' + (char)(number % 10));
                //else
                //code = (char)('A' + (char)(number % 26));

                checkCode += code.ToString();
            }
            Response.Cookies.Add(new HttpCookie("ExamCheckCode", checkCode));

            Session["timeout"] = DateTime.Now;
            return checkCode;
        }

        private void CreateCheckCodeImage(string checkCode)
        {
            if (checkCode == null || checkCode.Trim() == String.Empty)
                return;

            System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling((checkCode.Length * 16.1)), 36);
            Graphics g = Graphics.FromImage(image);

            try
            {
                //生成随机生成器
                Random random = new Random();

                //清空图片背景色
                g.Clear(ColorTranslator.FromHtml("#EBF3FF"));

                //画图片的背景噪音线
                /*for (int i = 0; i < 20; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }*/

                Font font = new System.Drawing.Font("Arial", 16, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.DarkRed, Color.Cyan, 1.2f, true);
                g.DrawString(checkCode, font, brush, 2, 6);

                //画图片的
                for (int i = 0; i < 60; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);

                    image.SetPixel(x, y, Color.FromArgb(random.Next()));//前景噪音点
                    //g.DrawLine(new Pen(Color.FromArgb(random.Next())), x, y, x, y);//背景噪音点
                }

                //画图片的边框线
                //g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                Response.ClearContent();
                Response.ContentType = "image/Gif";
                Response.BinaryWrite(ms.ToArray());
                
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
    }
}