/**  版本信息模板在安装目录下，可自行修改。
* newscat.cs
*
* 功 能： N/A
* 类 名： newscat
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017-02-25 22:19:53   N/A    初版
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
	/// newscat:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	public partial class NewsCat
	{
		#region Model
		private int _newscatid;
		private string _newscatnm;
		private decimal _cascadeid;
		private decimal _orderidx;
		/// <summary>
		/// 
		/// </summary>
		public int newscatid
		{
			set{ _newscatid=value;}
			get{return _newscatid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string newscatnm
		{
			set{ _newscatnm=value;}
			get{return _newscatnm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal cascadeid
		{
			set{ _cascadeid=value;}
			get{return _cascadeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal orderidx
		{
			set{ _orderidx=value;}
			get{return _orderidx;}
		}
		#endregion Model

	}
}

