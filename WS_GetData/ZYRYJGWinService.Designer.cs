namespace WS_GetData
{
    partial class ZYRYJGWinService
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void  Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }        

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.timer1 = new System.Timers.Timer();
            //this.interFaceService1 = new JCSJKService.InterFaceServiceSoapClient();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 60000D;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            // 
            // interFaceService1
            // 
            //this.interFaceService1.InnerChannel.c = null;
            //this.interFaceService1.Url = "http://210.75.213.142/SQSWeb_Silverlight/Exchage/InterFaceService.asmx";
            //this.interFaceService1.UseDefaultCredentials = false;
            // 
            // ExamWinService
            // 
            this.ServiceName = "ZYRYJGWinService";
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();

        }
        private System.Timers.Timer timer1;

        #endregion
        //private JCSJKService.InterFaceServiceSoapClient interFaceService1;
    }
}
