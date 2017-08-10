/**  版本信息模板在安装目录下，可自行修改。
* gender.cs
*
* 功 能： N/A
* 类 名： gender
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
	/// gender:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	public partial class Gender
	{
        public Gender()
		{}
		#region Model
		private int _id;
		private string _genderno;
		private string _genderval;
		/// <summary>
		/// 自增编号
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string genderNo
		{
			set{ _genderno=value;}
			get{return _genderno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string genderVal
		{
			set{ _genderval=value;}
			get{return _genderval;}
		}
		#endregion Model

	}
}

