using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 网易云评论搜索器.Models
{
    public class CommentsEntity
    {
        public int code { get; set; }
        public List<Comment> comments { get; set; }
        public List<Comment> hotComments { get; set; }
        public bool isMusician { get; set; }
        public bool more { get; set; }
        public bool moreHot { get; set; }
        public dynamic topComments { get; set; }
        public int total { get; set; }
        public long userId { get; set; }
    }

    public class Comment
    {
        public dynamic beReplied { get; set; }
        public long commentId { get; set; }
        public string content { get; set; }
        public dynamic expressionUrl { get; set; }
        public bool liked { get; set; }
        public int likeCount { get; set; }
        public dynamic pendantData { get; set; }
        public long time { get; set; }
        public User user { get; set; }
    }

    public class User
    {
        public int authStatus { get; set; }
        public string avatarUrl { get; set; }
        public dynamic expertTags { get; set; }
        public dynamic experts { get; set; }
        public dynamic locationInfo { get; set; }
        public string nickname { get; set; }
        public string remarkName { get; set; }
        public long userId { get; set; }
        public int userType { get; set; }
        public int vipType { get; set; }
    }
}
