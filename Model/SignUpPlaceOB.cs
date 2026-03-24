using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--SignUpPlaceOB填写类描述
	/// </summary>
	[Serializable]
	public class SignUpPlaceOB
	{
		public SignUpPlaceOB()
		{			
			//默认值
		}
			
		//主键
		protected long? _SignUpPlaceID;
		
		//其它属性
		protected string _PlaceName;
		protected string _Address;
		protected string _Phone;
		protected int? _CheckPersonLimit;
		
		public long? SignUpPlaceID
		{
			get {return _SignUpPlaceID;}
			set {_SignUpPlaceID = value;}
		}

		public string PlaceName
		{
			get {return _PlaceName;}
			set {_PlaceName = value;}
		}

		public string Address
		{
			get {return _Address;}
			set {_Address = value;}
		}

		public string Phone
		{
			get {return _Phone;}
			set {_Phone = value;}
		}

		public int? CheckPersonLimit
		{
			get {return _CheckPersonLimit;}
			set {_CheckPersonLimit = value;}
		}
	}
}
