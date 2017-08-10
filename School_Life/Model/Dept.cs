/**  版本信息模板在安装目录下，可自行修改。
* dept.cs
*
* 功 能： N/A
* 类 名： dept
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017-01-31 20:45:09   N/A    初版
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
	/// dept:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Dept
	{
		public Dept()
		{}
		#region Model
		private int _id;
		private string _deptno;
		private string _deptname;
		private string _deptdesc;
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
		public string deptNo
		{
			set{ _deptno=value;}
			get{return _deptno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string deptName
		{
			set{ _deptname=value;}
			get{return _deptname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string deptDesc
		{
			set{ _deptdesc=value;}
			get{return _deptdesc;}
		}
		#endregion Model

	}
}

