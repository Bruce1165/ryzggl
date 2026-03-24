using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--jcsjk_RY_JZS_ZSSDMDL填写类描述
	/// </summary>
	[Serializable]
	public class jcsjk_RY_JZS_ZSSDMDL
	{
		public jcsjk_RY_JZS_ZSSDMDL()
		{			
		}
			
		//主键
		protected Guid _ID = Guid.Empty;
		
		//其它属性
		protected string _XM;
		protected string _ZJHM;
		protected string _ZCH;
		protected string _HTBH;
		protected string _XMMC;
		protected string _ZBQY;
		protected int? _SDZT;
		protected DateTime? _SDSJ;
		protected string _BZ;
		protected int? _VALID;
		protected DateTime? _CJSJ;
		protected DateTime? _XGSJ;
		protected string _CJR;
		protected string _XGR;
		protected string _CJDEPTID;
		protected string _XGDEPTID;
		protected string _CheckCode;
		
		public Guid ID
		{
			get {return _ID;}
			set {_ID = value;}
		}

		public string XM
		{
			get {return _XM;}
			set {_XM = value;}
		}

		public string ZJHM
		{
			get {return _ZJHM;}
			set {_ZJHM = value;}
		}

		public string ZCH
		{
			get {return _ZCH;}
			set {_ZCH = value;}
		}

		public string HTBH
		{
			get {return _HTBH;}
			set {_HTBH = value;}
		}

		public string XMMC
		{
			get {return _XMMC;}
			set {_XMMC = value;}
		}

		public string ZBQY
		{
			get {return _ZBQY;}
			set {_ZBQY = value;}
		}

		public int? SDZT
		{
			get {return _SDZT;}
			set {_SDZT = value;}
		}

		public DateTime? SDSJ
		{
			get {return _SDSJ;}
			set {_SDSJ = value;}
		}

		public string BZ
		{
			get {return _BZ;}
			set {_BZ = value;}
		}

		public int? VALID
		{
			get {return _VALID;}
			set {_VALID = value;}
		}

		public DateTime? CJSJ
		{
			get {return _CJSJ;}
			set {_CJSJ = value;}
		}

		public DateTime? XGSJ
		{
			get {return _XGSJ;}
			set {_XGSJ = value;}
		}

		public string CJR
		{
			get {return _CJR;}
			set {_CJR = value;}
		}

		public string XGR
		{
			get {return _XGR;}
			set {_XGR = value;}
		}

		public string CJDEPTID
		{
			get {return _CJDEPTID;}
			set {_CJDEPTID = value;}
		}

		public string XGDEPTID
		{
			get {return _XGDEPTID;}
			set {_XGDEPTID = value;}
		}

		public string CheckCode
		{
			get {return _CheckCode;}
			set {_CheckCode = value;}
		}
	}
}
