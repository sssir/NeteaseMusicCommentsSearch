using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 网易云评论搜索器.Models
{
    public class Param
    {
        public string rid { get; set; }
        public int offset { get; set; }
        public string total { get; set; } = "False";
        public int limit { get; set; } = 20;
        public string csrf_token { get; set; } = string.Empty;
    }
}
