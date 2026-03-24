using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--FileInfoMDL填写类描述
	/// </summary>
	[Serializable]
	public class FileInfoMDL
	{
		public FileInfoMDL()
		{			
		}
			
		//主键
		protected string _FileID;
		
		//其它属性
		protected string _FileName;
		protected long? _FileSize;
		protected string _FileUrl;
		protected string _DataType;
		protected string _FileType;
		protected DateTime? _AddTime;
		protected string _UploadMan;
		
		public string FileID
		{
			get {return _FileID;}
			set {_FileID = value;}
		}

		public string FileName
		{
			get {return _FileName;}
			set {_FileName = value;}
		}

		public long? FileSize
		{
			get {return _FileSize;}
			set {_FileSize = value;}
		}

		public string FileUrl
		{
			get {return _FileUrl;}
			set {_FileUrl = value;}
		}

		public string DataType
		{
			get {return _DataType;}
			set {_DataType = value;}
		}

		public string FileType
		{
			get {return _FileType;}
			set {_FileType = value;}
		}

		public DateTime? AddTime
		{
			get {return _AddTime;}
			set {_AddTime = value;}
		}

		public string UploadMan
		{
			get {return _UploadMan;}
			set {_UploadMan = value;}
		}
	}
}
