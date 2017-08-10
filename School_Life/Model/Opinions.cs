using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Opinions
    {
        /// <summary>
        /// opnid
        /// </summary>		
        private int _opnid;
        public int opnid
        {
            get { return _opnid; }
            set { _opnid = value; }
        }
        /// <summary>
        /// opntime
        /// </summary>		
        private string _opntime;
        public string opntime
        {
            get { return _opntime; }
            set { _opntime = value; }
        }
        /// <summary>
        /// opncontent
        /// </summary>		
        private string _opncontent;
        public string opncontent
        {
            get { return _opncontent; }
            set { _opncontent = value; }
        }
        /// <summary>
        /// userid
        /// </summary>		
        private string _userid;
        public string userid
        {
            get { return _userid; }
            set { _userid = value; }
        }
    }
}
