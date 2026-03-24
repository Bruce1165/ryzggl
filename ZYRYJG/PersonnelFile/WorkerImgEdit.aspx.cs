using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace ZYRYJG.PersonnelFile
{
    public partial class WorkerImgEdit : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "WorkerInfoEdit.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string CertificateCode = Utility.Cryptography.Decrypt(Request["o"]);
                string imgType = Request["t"];//1：一寸照片；2：签名照片；3：手持半身照；
                if (imgType !="1") myImg.Visible = false;
                if (imgType != "2") DivImgSign.Visible = false;
                if (imgType != "3") DivImgIDCard.Visible = false;

                ViewState["CertificateCode"] = CertificateCode;
                ViewState["imgType"] = imgType;

                System.Random rm = new Random();

                int gbs = 0;//宽度与高度公倍数

                //myImg.Style.Add("backgroud", string.Format("url({0})",string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(CertificateCode)))));

                if (imgType == "1")
                {
                    ImgCode.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(CertificateCode))); //绑定照片;
                    System.Drawing.Image initImage = System.Drawing.Image.FromFile(Server.MapPath(GetFacePhotoPath(CertificateCode)));
                    gbs = GCD(initImage.Width, initImage.Height);
                    Hiddenwidthheight.Value = string.Format("{0};{1}", initImage.Width / gbs, initImage.Height / gbs);
                    ImgCode.Height = initImage.Height * 110 / initImage.Width;
                    initImage.Dispose();
                }
                if (imgType == "2")
                {
                    ImgSign.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), Utility.Cryptography.Encrypt(GetSignImgPath(CertificateCode))); //绑定签名照片;
                    System.Drawing.Image initImage = System.Drawing.Image.FromFile(Server.MapPath(GetSignImgPath(CertificateCode)));
                    gbs = GCD(initImage.Width, initImage.Height);
                    Hiddenwidthheight.Value = string.Format("{0};{1}", initImage.Width / gbs, initImage.Height / gbs);
                    ImgSign.Height = initImage.Height * 99 / initImage.Width;
                    initImage.Dispose();
                }

                if (imgType == "3")
                {
                    ImgIDCard.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), Utility.Cryptography.Encrypt(GetIDCardImgPath(CertificateCode))); //绑定手持身份证半身照片;
                    System.Drawing.Image initImage = System.Drawing.Image.FromFile(Server.MapPath(GetIDCardImgPath(CertificateCode)));
                    gbs = GCD(initImage.Width, initImage.Height);
                    Hiddenwidthheight.Value = string.Format("{0};{1}", initImage.Width / gbs, initImage.Height / gbs);
                    ImgIDCard.Height = initImage.Height * 400 / initImage.Width;
                    initImage.Dispose();
                }

            }
        }

        /// <summary>
        /// 最小公倍数
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int LCM(int a, int b)
        {
            int lcm = a * b;
            int max = a > b ? a : b;
            for (int i = max, len = a * b; i <= len; i++)
            {
                if (i % a == 0 && i % b == 0)
                {
                    lcm = i;
                    break;
                }
            }
            return lcm;
        }

        /// <summary>
        /// 最大公约数
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int GCD(int a, int b)
        {
            int gcd = 1;
            int min = a > b ? b : a;
            for (int i = min; i >= 1; i--)
            {
                if (a % i == 0 && b % i == 0)
                {
                    gcd = i;
                    break;
                }
            }
            return gcd;
        }

        /// <summary>
        /// 获取个人照片地址
        /// </summary>
        /// <param name="CertificateCode">证件号码</param>
        /// <returns></returns>
        protected string GetFacePhotoPath(string CertificateCode)
        {
            if (CertificateCode == "") return "~/Images/photo_ry.jpg";

            string path = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", CertificateCode.Substring(CertificateCode.Length - 3, 3), CertificateCode);
            if (File.Exists(Server.MapPath(path)) == true)
                return path;
            else
                return "~/Images/photo_ry.jpg";
        }

        /// <summary>
        /// 获取手持身份证半身照地址
        /// </summary>
        /// <param name="CertificateCode">证件号码</param>
        /// <returns></returns>
        protected string GetIDCardImgPath(string CertificateCode)
        {
            if (CertificateCode == "") return "~/Images/IDCard.jpg";

            string path = string.Format("~/UpLoad/HandIDCard/{0}/{1}.jpg", CertificateCode.Substring(CertificateCode.Length - 3, 3), CertificateCode);
            if (File.Exists(Server.MapPath(path)) == true)
                return path;
            else
                return "~/Images/IDCard.jpg";
        }

        /// <summary>
        /// 获取手写签名照地址
        /// </summary>
        /// <param name="CertificateCode">证件号码</param>
        /// <returns></returns>
        protected string GetSignImgPath(string CertificateCode)
        {
            if (CertificateCode == "") return "~/Images/SignImg.jpg";

            string path = string.Format("~/UpLoad/SignImg/{0}/{1}.jpg", CertificateCode.Substring(CertificateCode.Length - 3, 3), CertificateCode);
            if (File.Exists(Server.MapPath(path)) == true)
                return path;
            else
                return "~/Images/SignImg.jpg";
        }

        //修改
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string[] imgparms = HiddenField1.Value.Split(';');
                System.Collections.Specialized.StringDictionary imgSet = new System.Collections.Specialized.StringDictionary();//编辑后图片属性
                foreach (string s in imgparms)
                {
                    string[] t = s.Split(':');
                    imgSet.Add(t[0], t[1].Replace("px", ""));
                }
                string CertificateCode = ViewState["CertificateCode"].ToString();

                int maxWidth = 110;//模板宽度
                int maxHeight = 140;//模板高度
                int quality = 24;//图像质量 0~100
                double changeValue = 1;//缩放比例
                double changeValue2 = 1;//缩放比例
                System.Drawing.Image initImage = null;//待编辑图片
                string fileSaveUrl = "";//图片保存地址

                switch (ViewState["imgType"].ToString())
                {
                    case "1"://一寸照片；
                        if (!Directory.Exists(Page.Server.MapPath(string.Format("~/UpLoad/WorkerPhoto/{0}", CertificateCode.Substring(CertificateCode.Length - 3, 3)))))
                        {
                            Directory.CreateDirectory(Page.Server.MapPath(string.Format("~/UpLoad/WorkerPhoto/{0}", CertificateCode.Substring(CertificateCode.Length - 3, 3))));
                        }
                        fileSaveUrl = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", CertificateCode.Substring(CertificateCode.Length - 3, 3), CertificateCode);
                        //从文件获取原始图片，并使用流中嵌入的颜色管理信息
                        initImage = System.Drawing.Image.FromFile(Server.MapPath(GetFacePhotoPath(CertificateCode)));
                        break;
                    case "2"://签名照片
                        if (!Directory.Exists(Page.Server.MapPath(string.Format("~/UpLoad/SignImg/{0}", CertificateCode.Substring(CertificateCode.Length - 3, 3)))))
                        {
                            Directory.CreateDirectory(Page.Server.MapPath(string.Format("~/UpLoad/SignImg/{0}", CertificateCode.Substring(CertificateCode.Length - 3, 3))));
                        }
                        fileSaveUrl = string.Format("~/UpLoad/SignImg/{0}/{1}.jpg", CertificateCode.Substring(CertificateCode.Length - 3, 3), CertificateCode);
                        //从文件获取原始图片，并使用流中嵌入的颜色管理信息
                        initImage = System.Drawing.Image.FromFile(Server.MapPath(GetSignImgPath(CertificateCode)));
                        maxWidth = 99;//模板宽度
                        maxHeight = 42;//模板高度
                        break;
                    case "3"://手持半身照；
                        if (!Directory.Exists(Page.Server.MapPath(string.Format("~/UpLoad/HandIDCard/{0}", CertificateCode.Substring(CertificateCode.Length - 3, 3)))))
                        {
                            Directory.CreateDirectory(Page.Server.MapPath(string.Format("~/UpLoad/HandIDCard/{0}", CertificateCode.Substring(CertificateCode.Length - 3, 3))));
                        }
                        fileSaveUrl = string.Format("~/UpLoad/HandIDCard/{0}/{1}.jpg", CertificateCode.Substring(CertificateCode.Length - 3, 3), CertificateCode);
                        //从文件获取原始图片，并使用流中嵌入的颜色管理信息
                        initImage = System.Drawing.Image.FromFile(Server.MapPath(GetIDCardImgPath(CertificateCode)));
                        maxWidth = 400;//模板宽度
                        maxHeight = 300;//模板高度
                        break;
                }

                changeValue = maxWidth / Convert.ToDouble(imgSet["width"]);
                changeValue2 = Convert.ToDouble(initImage.Width) / maxWidth;
                imgSet["left"] = ((int)(System.Math.Floor(Convert.ToDouble(imgSet["left"]) * changeValue * changeValue2 * -1))).ToString();
                imgSet["top"] = ((int)(System.Math.Floor(Convert.ToDouble(imgSet["top"]) * changeValue * changeValue2 * -1))).ToString();
                imgSet["width"] = ((int)(System.Math.Floor(maxWidth * changeValue * changeValue2))).ToString();
                imgSet["height"] = ((int)(System.Math.Floor(maxHeight * changeValue * changeValue2))).ToString();

                //按模版大小生成最终图片
                System.Drawing.Image templateImage = new System.Drawing.Bitmap(maxWidth, maxHeight);
                System.Drawing.Graphics templateG = System.Drawing.Graphics.FromImage(templateImage);
                //templateG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //templateG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                templateG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                templateG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                templateG.Clear(Color.White);
                templateG.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, maxWidth, maxHeight)
                    , new System.Drawing.Rectangle(Convert.ToInt32(imgSet["left"]), Convert.ToInt32(imgSet["top"]), Convert.ToInt32(imgSet["width"]), Convert.ToInt32(imgSet["height"]))
                    , System.Drawing.GraphicsUnit.Pixel);

                //关键质量控制
                //获取系统编码类型数组,包含了jpeg,bmp,png,gif,tiff
                ImageCodecInfo[] icis = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo ici = null;
                foreach (ImageCodecInfo i in icis)
                {
                    if (i.MimeType == "image/jpeg" || i.MimeType == "image/bmp" || i.MimeType == "image/png" || i.MimeType == "image/gif")
                    {
                        ici = i;
                    }
                }
                EncoderParameters ep = new EncoderParameters(1);
                //ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)quality);
                ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)quality);

                initImage.Dispose();

                //保存缩略图
                //templateImage.Save(Server.MapPath(fileSaveUrl), ici, ep);
                templateImage.Save(Server.MapPath(fileSaveUrl), System.Drawing.Imaging.ImageFormat.Jpeg);

                //释放资源
                templateG.Dispose();
                templateImage.Dispose();
                
                UIHelp.layerAlert(Page, "个人信息修改成功！", 6, 3000);
                Response.Redirect("WorkerInfoEdit.aspx");

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "个人信息修改失败！", ex);
            }
         }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("WorkerInfoEdit.aspx");
        }
    }
}
