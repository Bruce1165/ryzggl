using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--jcsjk_QY_ZHXXMDL填写类描述
	/// </summary>
	[Serializable]
	public class jcsjk_QY_ZHXXMDL
	{
		public jcsjk_QY_ZHXXMDL()
		{			
		}
			
		//主键
		protected Guid _ID = Guid.Empty;
		
		//其它属性
		protected string _ZZZSBH;
		protected string _ZZJGDM;
		protected string _QYMC;
		protected string _ZCDZ;
		protected string _XXDZ;
		protected DateTime? _JLSJ;
		protected decimal? _ZCZBJ;
		protected string _YYZZZCH;
		protected string _JJLX;
		protected string _ZXZZDJ;
		protected string _ZXZZ;
		protected string _FDDBR;
		protected string _FDDBRZJLX;
		protected string _FDDBRZJHM;
		protected string _FDDBRZW;
		protected string _FDDBRZC;
		protected string _QYFZR;
		protected string _QYFZRZJLX;
		protected string _QYFZRZJHM;
		protected string _QYFZRZW;
		protected string _QYFZRZC;
		protected string _JSFZR;
		protected string _JSFZRZJLX;
		protected string _JSFZRZJHM;
		protected string _JSFZRZW;
		protected string _JSFZRZC;
		protected string _QYZZLX;
		protected string _FZJG;
		protected DateTime? _FZRQ;
		protected DateTime? _ZSYXQKS;
		protected DateTime? _ZSYXQJS;
		protected string _YWFW;
		protected string _BZ;
		protected int? _VALID;
		protected string _CJR;
		protected string _CJDEPTID;
		protected DateTime? _CJSJ;
		protected string _XGR;
		protected string _XGDEPTID;
		protected DateTime? _XGSJ;
		protected string _SFDYZS;
		protected string _SJLX;
		protected int? _YXSX;
		protected string _XZDQBM;
		protected string _HYLZGX;
		
		public Guid ID
		{
			get {return _ID;}
			set {_ID = value;}
		}

		public string ZZZSBH
		{
			get {return _ZZZSBH;}
			set {_ZZZSBH = value;}
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

		public string ZCDZ
		{
			get {return _ZCDZ;}
			set {_ZCDZ = value;}
		}

		public string XXDZ
		{
			get {return _XXDZ;}
			set {_XXDZ = value;}
		}

		public DateTime? JLSJ
		{
			get {return _JLSJ;}
			set {_JLSJ = value;}
		}

		public decimal? ZCZBJ
		{
			get {return _ZCZBJ;}
			set {_ZCZBJ = value;}
		}

		public string YYZZZCH
		{
			get {return _YYZZZCH;}
			set {_YYZZZCH = value;}
		}

		public string JJLX
		{
			get {return _JJLX;}
			set {_JJLX = value;}
		}

		public string ZXZZDJ
		{
			get {return _ZXZZDJ;}
			set {_ZXZZDJ = value;}
		}

		public string ZXZZ
		{
			get {return _ZXZZ;}
			set {_ZXZZ = value;}
		}

		public string FDDBR
		{
			get {return _FDDBR;}
			set {_FDDBR = value;}
		}

		public string FDDBRZJLX
		{
			get {return _FDDBRZJLX;}
			set {_FDDBRZJLX = value;}
		}

		public string FDDBRZJHM
		{
			get {return _FDDBRZJHM;}
			set {_FDDBRZJHM = value;}
		}

		public string FDDBRZW
		{
			get {return _FDDBRZW;}
			set {_FDDBRZW = value;}
		}

		public string FDDBRZC
		{
			get {return _FDDBRZC;}
			set {_FDDBRZC = value;}
		}

		public string QYFZR
		{
			get {return _QYFZR;}
			set {_QYFZR = value;}
		}

		public string QYFZRZJLX
		{
			get {return _QYFZRZJLX;}
			set {_QYFZRZJLX = value;}
		}

		public string QYFZRZJHM
		{
			get {return _QYFZRZJHM;}
			set {_QYFZRZJHM = value;}
		}

		public string QYFZRZW
		{
			get {return _QYFZRZW;}
			set {_QYFZRZW = value;}
		}

		public string QYFZRZC
		{
			get {return _QYFZRZC;}
			set {_QYFZRZC = value;}
		}

		public string JSFZR
		{
			get {return _JSFZR;}
			set {_JSFZR = value;}
		}

		public string JSFZRZJLX
		{
			get {return _JSFZRZJLX;}
			set {_JSFZRZJLX = value;}
		}

		public string JSFZRZJHM
		{
			get {return _JSFZRZJHM;}
			set {_JSFZRZJHM = value;}
		}

		public string JSFZRZW
		{
			get {return _JSFZRZW;}
			set {_JSFZRZW = value;}
		}

		public string JSFZRZC
		{
			get {return _JSFZRZC;}
			set {_JSFZRZC = value;}
		}

		public string QYZZLX
		{
			get {return _QYZZLX;}
			set {_QYZZLX = value;}
		}

		public string FZJG
		{
			get {return _FZJG;}
			set {_FZJG = value;}
		}

		public DateTime? FZRQ
		{
			get {return _FZRQ;}
			set {_FZRQ = value;}
		}

		public DateTime? ZSYXQKS
		{
			get {return _ZSYXQKS;}
			set {_ZSYXQKS = value;}
		}

		public DateTime? ZSYXQJS
		{
			get {return _ZSYXQJS;}
			set {_ZSYXQJS = value;}
		}

		public string YWFW
		{
			get {return _YWFW;}
			set {_YWFW = value;}
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

		public string SJLX
		{
			get {return _SJLX;}
			set {_SJLX = value;}
		}

		public int? YXSX
		{
			get {return _YXSX;}
			set {_YXSX = value;}
		}

		public string XZDQBM
		{
			get {return _XZDQBM;}
			set {_XZDQBM = value;}
		}

		public string HYLZGX
		{
			get {return _HYLZGX;}
			set {_HYLZGX = value;}
		}
	}
}
