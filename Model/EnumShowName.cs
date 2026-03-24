using System;

namespace Model
{
    /// <summary>
    /// 用于给各枚举类型添加说明属性
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.All, AllowMultiple = false)]
    public class EnumShowName : Attribute
    {
        protected string _StringName;

        //返回属性的内容
        public string StringName
        {
            get { return _StringName; }
            set { _StringName = value; }
        }

        public EnumShowName(string Name)
        {
            _StringName = Name;
        }

        public EnumShowName()
        {
            _StringName = "Not Set";
        }
    }
}