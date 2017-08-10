using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;

namespace BLL
{
    public class BLL_opinion
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(Opinions model)
        {
            return DAL_opinion.Add(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Delete(int opnid)
        {
            return DAL_opinion.Delete(opnid);
        }
    }
}
