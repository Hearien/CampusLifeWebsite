using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class NewsCom
    {
        /// <summary>
        /// comid
        /// </summary>		
        private int _comid;
        public int comid
        {
            get { return _comid; }
            set { _comid = value; }
        }
        /// <summary>
        /// newsid
        /// </summary>		
        private News _newsid;
        public News newsid
        {
            get { return _newsid; }
            set { _newsid = value; }
        }
        /// <summary>
        /// comdesc
        /// </summary>		
        private string _comdesc;
        public string comdesc
        {
            get { return _comdesc; }
            set { _comdesc = value; }
        }
        /// <summary>
        /// comtime
        /// </summary>		
        private string _comtime;
        public string comtime
        {
            get { return _comtime; }
            set { _comtime = value; }
        }
        /// <summary>
        /// comip
        /// </summary>		
        private string _comip;
        public string comip
        {
            get { return _comip; }
            set { _comip = value; }
        }
        /// <summary>
        /// uerid
        /// </summary>		
        private Student _uerid;
        public Student uerid
        {
            get { return _uerid; }
            set { _uerid = value; }
        }
    }
}
