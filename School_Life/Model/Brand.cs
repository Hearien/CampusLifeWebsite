/**  版本信息模板在安装目录下，可自行修改。
* brand.cs
*
* 功 能： N/A
* 类 名： brand
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017-02-25 22:19:52   N/A    初版
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
	/// brand:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Brand
	{
        public Brand()
		{}
		#region Model
		private int _brandid;
		private string _brandname;
        private GoodsCat _cat;
		private int _isenable;
		/// <summary>
		/// 
		/// </summary>
		public int brandid
		{
			set{ _brandid=value;}
			get{return _brandid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string brandname
		{
			set{ _brandname=value;}
			get{return _brandname;}
		}
        /// <summary>
        /// 
        /// </summary>
        public GoodsCat GoodsCat
        {
            set { _cat = value; }
            get { return _cat; }
        }
		/// <summary>
		/// 
		/// </summary>
		public int isenable
		{
			set{ _isenable=value;}
			get{return _isenable;}
		}
		#endregion Model

	}
}

