/**  版本信息模板在安装目录下，可自行修改。
* goodsCat.cs
*
* 功 能： N/A
* 类 名： goodsCat
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
	/// goodsCat:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GoodsCat
	{
        public GoodsCat()
		{}
		#region Model
		private int _goods_catid;
		private string _goods_catnam;
		private string _catdesc;
		private int _isenable=1;
		/// <summary>
		/// 
		/// </summary>
		public int goods_catId
		{
			set{ _goods_catid=value;}
			get{return _goods_catid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string goods_catNam
		{
			set{ _goods_catnam=value;}
			get{return _goods_catnam;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string catdesc
		{
			set{ _catdesc=value;}
			get{return _catdesc;}
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

