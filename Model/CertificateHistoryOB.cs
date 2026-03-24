using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{

    /// <summary>
    /// 
    /// </summary>
	[Serializable()]
	public class CertificateHistoryOB
	{ 
		private long certificateHistoryID; 
		private string operateType = String.Empty;
		private long certificateID;
		private long examPlanID;
		private long workerID;
		private string certificateType = String.Empty;
		private int postTypeID;
		private int postID;
		private string certificateCode = String.Empty;
		private string workerName = String.Empty;
		private string sex = String.Empty;
		private DateTime birthday;
		private string unitName = String.Empty;
		private DateTime conferDate;
		private DateTime validStartDate;
		private DateTime validEndDate;
		private string conferUnit = String.Empty;
		private string status = String.Empty;
		private string checkMan = String.Empty;
		private string checkAdvise = String.Empty;
		private DateTime checkDate;
		private string printMan = String.Empty;
		private DateTime printDate;
		private string status_1 = String.Empty;
		private long createPersonID;
		private DateTime createTime;
		private long modifyPersonID;
		private DateTime modifyTime;
        protected string _AddItemName;

        /// <summary>
        /// 增项
        /// </summary>
        public string AddItemName
        {
            get { return _AddItemName; }
            set { _AddItemName = value; }
        }

		public long CertificateHistoryID
		{
			get { return this.certificateHistoryID; }
			set { this.certificateHistoryID = value; }
		}  
		
		public string OperateType
		{
			get { return this.operateType; }
			set { this.operateType = value; }
		}	 
		
		public long CertificateID
		{
			get { return this.certificateID; }
			set { this.certificateID = value; }
		}	 
		
		public long ExamPlanID
		{
			get { return this.examPlanID; }
			set { this.examPlanID = value; }
		}	 
		
		public long WorkerID
		{
			get { return this.workerID; }
			set { this.workerID = value; }
		}	 
		
		public string CertificateType
		{
			get { return this.certificateType; }
			set { this.certificateType = value; }
		}	 
		
		public int PostTypeID
		{
			get { return this.postTypeID; }
			set { this.postTypeID = value; }
		}	 
		
		public int PostID
		{
			get { return this.postID; }
			set { this.postID = value; }
		}	 
		
		public string CertificateCode
		{
			get { return this.certificateCode; }
			set { this.certificateCode = value; }
		}	 
		
		public string WorkerName
		{
			get { return this.workerName; }
			set { this.workerName = value; }
		}	 
		
		public string Sex
		{
			get { return this.sex; }
			set { this.sex = value; }
		}	 
		
		public DateTime Birthday
		{
			get { return this.birthday; }
			set { this.birthday = value; }
		}	 
		
		public string UnitName
		{
			get { return this.unitName; }
			set { this.unitName = value; }
		}	 
		
		public DateTime ConferDate
		{
			get { return this.conferDate; }
			set { this.conferDate = value; }
		}	 
		
		public DateTime ValidStartDate
		{
			get { return this.validStartDate; }
			set { this.validStartDate = value; }
		}	 
		
		public DateTime ValidEndDate
		{
			get { return this.validEndDate; }
			set { this.validEndDate = value; }
		}	 
		
		public string ConferUnit
		{
			get { return this.conferUnit; }
			set { this.conferUnit = value; }
		}	 
		
		public string Status
		{
			get { return this.status; }
			set { this.status = value; }
		}	 
		
		public string CheckMan
		{
			get { return this.checkMan; }
			set { this.checkMan = value; }
		}	 
		
		public string CheckAdvise
		{
			get { return this.checkAdvise; }
			set { this.checkAdvise = value; }
		}	 
		
		public DateTime CheckDate
		{
			get { return this.checkDate; }
			set { this.checkDate = value; }
		}	 
		
		public string PrintMan
		{
			get { return this.printMan; }
			set { this.printMan = value; }
		}	 
		
		public DateTime PrintDate
		{
			get { return this.printDate; }
			set { this.printDate = value; }
		}	 
		
		public string Status_1
		{
			get { return this.status_1; }
			set { this.status_1 = value; }
		}	 
		
		public long CreatePersonID
		{
			get { return this.createPersonID; }
			set { this.createPersonID = value; }
		}	 
		
		public DateTime CreateTime
		{
			get { return this.createTime; }
			set { this.createTime = value; }
		}	 
		
		public long ModifyPersonID
		{
			get { return this.modifyPersonID; }
			set { this.modifyPersonID = value; }
		}	 
		
		public DateTime ModifyTime
		{
			get { return this.modifyTime; }
			set { this.modifyTime = value; }
		}	 
	}
}
