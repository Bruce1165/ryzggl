using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{ 
    /// <summary>
    /// 从业人员实体类
    /// </summary>
	[Serializable()]
	public class WorkerOB
	{
        private long? workerID; 
		private string workerName = String.Empty;
		private string certificateType = String.Empty;
		private string certificateCode = String.Empty;
		private string sex = String.Empty;
        private DateTime? birthday;
		private string nation = String.Empty;
		private string culturalLevel = String.Empty;
		private string politicalBackground = String.Empty;
		private string phone = String.Empty;
		private string mobile = String.Empty;
		private string email = String.Empty;
		private string address = String.Empty;
		private string zipCode = String.Empty;
        private string facePhoto = String.Empty;

        private DateTime? _FacePhotoUpdateTime;
        private DateTime? _SignPhotoTime;
        private DateTime? _IDCardPhotoUpdateTime;

        protected string _UUID;

        /// <summary>
        /// 统一登录平台用户ID
        /// </summary>
        public string UUID
        {
            get { return this._UUID; }
            set { this._UUID = value; }
        }	

        /// <summary>
        /// 一寸照更新时间
        /// </summary>
        public DateTime? FacePhotoUpdateTime
        {
            get { return this._FacePhotoUpdateTime; }
            set { this._FacePhotoUpdateTime = value; }
        }
        /// <summary>
        /// 签名照更新时间
        /// </summary>
        public DateTime? SignPhotoTime
        {
            get { return this._SignPhotoTime; }
            set { this._SignPhotoTime = value; }
        }
        /// <summary>
        /// 手持身份证更新时间
        /// </summary>
        public DateTime? IDCardPhotoUpdateTime
        {
            get { return this._IDCardPhotoUpdateTime; }
            set { this._IDCardPhotoUpdateTime = value; }
        }	 

        public long? WorkerID
		{
			get { return this.workerID; }
			set { this.workerID = value; }
		}  
		/// <summary>
		/// 姓名
		/// </summary>
		public string WorkerName
		{
			get { return this.workerName; }
			set { this.workerName = value; }
		}	 
		/// <summary>
		/// 证件类型
		/// </summary>
		public string CertificateType
		{
			get { return this.certificateType; }
			set { this.certificateType = value; }
		}	 
        /// <summary>
        /// 证件号码
        /// </summary>
		public string CertificateCode
		{
			get { return this.certificateCode; }
			set { this.certificateCode = value; }
		}	 
		
		public string Sex
		{
			get { return this.sex; }
			set { this.sex = value; }
		}

        public DateTime? Birthday
		{
			get { return this.birthday; }
			set { this.birthday = value; }
		}	 
		/// <summary>
		/// 民族
		/// </summary>
		public string Nation
		{
			get { return this.nation; }
			set { this.nation = value; }
		}	 
		/// <summary>
		/// 学历
		/// </summary>
		public string CulturalLevel
		{
			get { return this.culturalLevel; }
			set { this.culturalLevel = value; }
		}	 
		/// <summary>
		/// 政治面貌
		/// </summary>
		public string PoliticalBackground
		{
			get { return this.politicalBackground; }
			set { this.politicalBackground = value; }
		}	 
		/// <summary>
		/// 联系电话
		/// </summary>
		public string Phone
		{
			get { return this.phone; }
			set { this.phone = value; }
		}	 
		/// <summary>
		/// 手机
		/// </summary>
		public string Mobile
		{
			get { return this.mobile; }
			set { this.mobile = value; }
		}	 
		
		public string Email
		{
			get { return this.email; }
			set { this.email = value; }
		}	 
		
		public string Address
		{
			get { return this.address; }
			set { this.address = value; }
		}	 
		
		public string ZipCode
		{
			get { return this.zipCode; }
			set { this.zipCode = value; }
		}	 
		/// <summary>
		/// 一寸照片
		/// </summary>
		public string FacePhoto
		{
			get { return this.facePhoto; }
			set { this.facePhoto = value; }
		}	 
	}
}
