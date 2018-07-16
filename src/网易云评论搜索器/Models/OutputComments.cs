using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 网易云评论搜索器.Models
{
    public class OutputComments
    {
        public long 歌曲ID { get; set; }
        public string 歌曲名 { get; set; }
        public string 评论内容 { get; set; }
        public string 评论人 { get; set; }
        public DateTime 评论时间 { get; set; }
        public int 评论位置 { get; set; }
    }
}
