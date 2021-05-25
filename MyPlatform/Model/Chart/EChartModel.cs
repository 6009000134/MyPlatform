using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.Model.Chart
{
    /// <summary>
    /// Echart图表实体
    /// </summary>
    public class EChartModel
    {
        public EChartModel()
        {
            title = new List<object>();
            legend = new List<object>();
            xAxis = new List<object>();
            yAxis = new List<object>();
            grid = new List<object>();
            series = new List<object>();
            dataset = new List<Dictionary<string, object>>();
        }
        /// <summary>
        /// 前端DOM ID
        /// </summary>
        public string DomID { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public List<object> title { get; set; }
        /// <summary>
        /// 提示
        /// </summary>
        public object tooltip { get; set; }
        /// <summary>
        /// 图例
        /// </summary>
        public List<object> legend { get; set; }
        /// <summary>
        /// x轴
        /// </summary>
        public List<object> xAxis { get; set; }
        /// <summary>
        /// y轴
        /// </summary>
        public List<object> yAxis { get; set; }
        /// <summary>
        /// grid
        /// </summary>
        public List<object> grid { get; set; }
        /// <summary>
        /// 系列
        /// </summary>
        public List<object> series { get; set; }
        /// <summary>
        /// 数据集
        /// </summary>
        public List<Dictionary<string, object>> dataset { get; set; }
    }
}
