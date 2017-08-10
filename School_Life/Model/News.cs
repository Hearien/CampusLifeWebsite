using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class News
    {
        /// <summary>
        /// newsid
        /// </summary>		
        private int _newsid;
        public int newsid
        {
            get { return _newsid; }
            set { _newsid = value; }
        }
        /// <summary>
        /// newscatno
        /// </summary>		
        private NewsCat _newscatno;
        public NewsCat newscatno
        {
            get { return _newscatno; }
            set { _newscatno = value; }
        }
        /// <summary>
        /// newstitle
        /// </summary>		
        private string _newstitle;
        public string newstitle
        {
            get { return _newstitle; }
            set { _newstitle = value; }
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
        /// source
        /// </summary>		
        private string _source;
        public string source
        {
            get { return _source; }
            set { _source = value; }
        }
        /// <summary>
        /// newsdetsc
        /// </summary>		
        private string _newsdetsc;
        public string newsdetsc
        {
            get { return _newsdetsc; }
            set { _newsdetsc = value; }
        }
        /// <summary>
        /// authorid
        /// </summary>		
        private Student _authorid;
        public Student authorid
        {
            get { return _authorid; }
            set { _authorid = value; }
        }
        /// <summary>
        /// clickcount
        /// </summary>		
        private decimal _clickcount;
        public decimal clickcount
        {
            get { return _clickcount; }
            set { _clickcount = value; }
        }
        /// <summary>
        /// ishead
        /// </summary>		
        private decimal _ishead;
        public decimal ishead
        {
            get { return _ishead; }
            set { _ishead = value; }
        }
        /// <summary>
        /// ishot
        /// </summary>		
        private decimal _ishot;
        public decimal ishot
        {
            get { return _ishot; }
            set { _ishot = value; }
        }
        /// <summary>
        /// lastcomid
        /// </summary>		
        private Student _lastcomid;
        public Student lastcomid
        {
            get { return _lastcomid; }
            set { _lastcomid = value; }
        }
        /// <summary>
        /// lastcomtime
        /// </summary>		
        private string _lastcomtime;
        public string lastcomtime
        {
            get { return _lastcomtime; }
            set { _lastcomtime = value; }
        }
        /// <summary>
        /// comcount
        /// </summary>		
        private decimal _comcount;
        public decimal comcount
        {
            get { return _comcount; }
            set { _comcount = value; }
        }
    }
}
