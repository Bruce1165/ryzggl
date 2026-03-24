using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--Unit_AQSCXKZMDL填写类描述
	/// </summary>
	[Serializable]
	public class Unit_AQSCXKZMDL
	{
		public Unit_AQSCXKZMDL()
		{			
		}
			
		//主键
		protected Guid _ID = Guid.Empty;
		
		//其它属性
		protected string _AQSCXKZBH;
		protected string _ZZJGDM;
		protected string _QYMC;
		protected string _JJLX;
		protected string _DWDZ;
		protected string _ZYFZR;
		protected string _ZYFZRZJLX;
		protected string _ZYFZRZJHM;
		protected string _AQSCXKZSQLX;
		protected string _AQSCXKFW;
		protected DateTime? _AQSCXKZKSRQ;
		protected DateTime? _AQSCXKZJSRQ;
		protected DateTime? _AQSCXKZKSRQ_SYC;
		protected DateTime? _AQSCXKZJSRQ_SYC;
		protected string _AQSCXKZFZJG;
		protected DateTime? _AQSCXKZFZRQ;
		protected string _AQSCXKZZSZT;
		protected string _BZ;
		protected int? _VALID;
		protected string _CJR;
		protected string _CJDEPTID;
		protected DateTime? _CJSJ;
		protected string _XGR;
		protected string _XGDEPTID;
		protected DateTime? _XGSJ;
		protected string _SFDYZS;
		protected string _CheckCode;
		
		public Guid ID
		{
			get {return _ID;}
			set {_ID = value;}
		}

		public string AQSCXKZBH
		{
			get {return _AQSCXKZBH;}
			set {_AQSCXKZBH = value;}
		}

		public string ZZJGDM
		{
			get {return _ZZJGDM;}
			set {_ZZJGDM = value;}
		}

		public string QYMC
		{
			get {return _QYMC;}
			set {_QYMC = value;}
		}

		public string JJLX
		{
			get {return _JJLX;}
			set {_JJLX = value;}
		}

		public string DWDZ
		{
			get {return _DWDZ;}
			set {_DWDZ = value;}
		}

		public string ZYFZR
		{
			get {return _ZYFZR;}
			set {_ZYFZR = value;}
		}

		public string ZYFZRZJLX
		{
			get {return _ZYFZRZJLX;}
			set {_ZYFZRZJLX = value;}
		}

		public string ZYFZRZJHM
		{
			get {return _ZYFZRZJHM;}
			set {_ZYFZRZJHM = value;}
		}

		public string AQSCXKZSQLX
		{
			get {return _AQSCXKZSQLX;}
			set {_AQSCXKZSQLX = value;}
		}

		public string AQSCXKFW
		{
			get {return _AQSCXKFW;}
			set {_AQSCXKFW = value;}
		}

		public DateTime? AQSCXKZKSRQ
		{
			get {return _AQSCXKZKSRQ;}
			set {_AQSCXKZKSRQ = value;}
		}

		public DateTime? AQSCXKZJSRQ
		{
			get {return _AQSCXKZJSRQ;}
			set {_AQSCXKZJSRQ = value;}
		}

		public DateTime? AQSCXKZKSRQ_SYC
		{
			get {return _AQSCXKZKSRQ_SYC;}
			set {_AQSCXKZKSRQ_SYC = value;}
		}

		public DateTime? AQSCXKZJSRQ_SYC
		{
			get {return _AQSCXKZJSRQ_SYC;}
			set {_AQSCXKZJSRQ_SYC = value;}
		}

		public string AQSCXKZFZJG
		{
			get {return _AQSCXKZFZJG;}
			set {_AQSCXKZFZJG = value;}
		}

		public DateTime? AQSCXKZFZRQ
		{
			get {return _AQSCXKZFZRQ;}
			set {_AQSCXKZFZRQ = value;}
		}

		public string AQSCXKZZSZT
		{
			get {return _AQSCXKZZSZT;}
			set {_AQSCXKZZSZT = value;}
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

		public string CJR
		{
			get {return _CJR;}
			set {_CJR = value;}
		}

		public string CJDEPTID
		{
			get {return _CJDEPTID;}
			set {_CJDEPTID = value;}
		}

		public DateTime? CJSJ
		{
			get {return _CJSJ;}
			set {_CJSJ = value;}
		}

		public string XGR
		{
			get {return _XGR;}
			set {_XGR = value;}
		}

		public string XGDEPTID
		{
			get {return _XGDEPTID;}
			set {_XGDEPTID = value;}
		}

		public DateTime? XGSJ
		{
			get {return _XGSJ;}
			set {_XGSJ = value;}
		}

		public string SFDYZS
		{
			get {return _SFDYZS;}
			set {_SFDYZS = value;}
		}

		public string CheckCode
		{
			get {return _CheckCode;}
			set {_CheckCode = value;}
		}
	}
}
