using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Dynamic
    {
        /// <summary>
        /// dynamicid
        /// </summary>		
        private int _dynamicid;
        public int dynamicid
        {
            get { return _dynamicid; }
            set { _dynamicid = value; }
        }
        /// <summary>
        /// userid
        /// </summary>		
        private Student _userid;
        public Student userid
        {
            get { return _userid; }
            set { _userid = value; }
        }
        /// <summary>
        /// createtime
        /// </summary>		
        private string _createtime;
        public string createtime
        {
            get { return _createtime; }
            set { _createtime = value; }
        }
        /// <summary>
        /// context
        /// </summary>		
        private string _context;
        public string context
        {
            get { return _context; }
            set { _context = value; }
        }
    }
}
