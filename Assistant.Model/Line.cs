using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assistant.Model
{
    /// <summary>
    /// 线路/通道
    /// </summary>
    public class Line
    {
        public Line()
        {
            this.State = 0;
        }

        /// <summary>
        /// 句柄，唯一标示
        /// </summary>
        public int Handle { get; set; }
        /// <summary>
        /// 最后一次来电号码记录
        /// </summary>
        public string LastPhone { get; set; }
        /// <summary>
        /// 当前来电记录表的guid
        /// </summary>
        public string CallRecordId { get; set; }
        /// <summary>
        /// 当前来电记录表的创建时间
        /// </summary>
        public DateTime CallTime { get; set; }
        /// <summary>
        /// 当前线路名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 当前线路状态
        /// 0：空闲；1：来电；2：摘机；3：挂机；4：振铃（来电1秒前）
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 当前线路的状态栏对象
        /// </summary>
        public object Tool { get; set; }
    }
}
