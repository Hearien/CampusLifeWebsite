/**  版本信息模板在安装目录下，可自行修改。
* goods.cs
*
* 功 能： N/A
* 类 名： goods
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017-02-08 21:12:40   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Model
{
	/// <summary>
	/// goods:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Goods
	{
		public Goods()
		{}
		#region Model
		private int _id;
		private GoodsCat _gooodscat;
		private string _thumb;
		private string _title;
		private string _note;
        private Brand _brandid;
		private decimal _price;
		private string _detail;
		private DateTime _uptime;
		private Student student;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
        /// <summary>
		/// 
		/// </summary>
        public Brand brandid
		{
			set{ _brandid=value;}
			get{return _brandid;}
		}
		/// <summary>
		/// 
		/// </summary>
        public GoodsCat gooodsCat
		{
			set{ _gooodscat=value;}
			get{return _gooodscat;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string thumb
		{
			set{ _thumb=value;}
			get{return _thumb;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string note
		{
			set{ _note=value;}
			get{return _note;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string detail
		{
			set{ _detail=value;}
			get{return _detail;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime upTime
		{
			set{ _uptime=value;}
			get{return _uptime;}
		}
		/// <summary>
		/// 
		/// </summary>
        public Student Student
		{
            set { student = value; }
            get { return student; }
		}
		#endregion Model

	}
}

