using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--FileDownLogMDL填写类描述
	/// </summary>
	[Serializable]
	public class FileDownLogMDL
	{
		public FileDownLogMDL()
		{			
		}
			
		//主键
		protected long? _LogID;
		
		//其它属性
		protected string _FileID;
		protected string _DownMAN;
        protected string _DownManID;
		protected DateTime? _DownTime;
		protected string _DownFileName;
		protected int? _FileTypeCode;
		protected string _DownDesc;
		
		public long? LogID
		{
			get {return _LogID;}
			set {_LogID = value;}
		}

		public string FileID
		{
			get {return _FileID;}
			set {_FileID = value;}
		}

		public string DownMAN
		{
			get {return _DownMAN;}
			set {_DownMAN = value;}
		}

        public string DownManID
		{
			get {return _DownManID;}
			set {_DownManID = value;}
		}

		public DateTime? DownTime
		{
			get {return _DownTime;}
			set {_DownTime = value;}
		}

		public string DownFileName
		{
			get {return _DownFileName;}
			set {_DownFileName = value;}
		}

        public int? FileTypeCode
		{
			get {return _FileTypeCode;}
			set {_FileTypeCode = value;}
		}

		public string DownDesc
		{
			get {return _DownDesc;}
			set {_DownDesc = value;}
		}
	}
}
