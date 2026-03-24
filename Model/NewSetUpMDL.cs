using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class NewSetUpMDL
    {
        public NewSetUpMDL()
		{			

		}
        private string _ApplyId;
        private DateTime? _XslDateTime;
        private string _Psn_Name;
        private string _ENT_OrganizationsCode;
        private string _ENT_Name;
        private DateTime? _XslEndTime;
        private string _ENT_City;
        private string _ApplyType;

 
        
      
        

        public string ApplyId
        {
            get { return _ApplyId; }
            set { _ApplyId = value; }
        }
        public DateTime? XslDateTime
        {
            get { return _XslDateTime; }
            set { _XslDateTime = value; }
        }
        public string Psn_Name
        {
            get { return _Psn_Name; }
            set { _Psn_Name = value; }
        }
        public string ENT_OrganizationsCode
        {
            get { return _ENT_OrganizationsCode; }
            set { _ENT_OrganizationsCode = value; }
        }
        public string ENT_Name
        {
            get { return _ENT_Name; }
            set { _ENT_Name = value; }
        }
        public DateTime? XslEndTime
        {
            get { return _XslEndTime; }
            set { _XslEndTime = value; }
        }
        public string ENT_City
        {
            get { return _ENT_City; }
            set { _ENT_City = value; }
        }
        public string ApplyType
        {
            get { return _ApplyType; }
            set { _ApplyType = value; }
        }
      
    }
}
