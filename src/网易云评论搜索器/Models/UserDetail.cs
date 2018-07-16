using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 网易云评论搜索器.Models
{
    public class UserDetail
    {
        public Profile profile { get; set; }
        public int level { get; set; }
        public int listenSongs { get; set; }
        public bool peopleCanSeeMyPlayRecord { get; set; }
        public List<Binding> bindings { get; set; }
        public bool adValid { get; set; }
        public int code { get; set; }
        public long createTime { get; set; }
        public int createDays { get; set; }
    }

    public  class Profile
    {
        public int province { get; set; }
        public string nickname { get; set; }
        public string remarkName { get; set; }
        public long userId { get; set; }
        public int accountStatus { get; set; }
        public int city { get; set; }
        public int vipType { get; set; }
        public int gender { get; set; }
        public bool mutual { get; set; }
        public long backgroundImgId { get; set; }
        public int userType { get; set; }
        public long avatarImgId { get; set; }
        public bool defaultAvatar { get; set; }
        public string avatarUrl { get; set; }
        public long birthday { get; set; }
        public object expertTags { get; set; }
        public int authStatus { get; set; }
        public string detailDescription { get; set; }
        public int djStatus { get; set; }
        public bool followed { get; set; }
        public string backgroundImgIdStr { get; set; }
        public object experts { get; set; }
        public string avatarImgIdStr { get; set; }
        public string description { get; set; }
        public string backgroundUrl { get; set; }
        public string signature { get; set; }
        public int authority { get; set; }
        public string avatarImgId_str { get; set; }
        public object artistIdentity { get; set; }
        public int followeds { get; set; }
        public int follows { get; set; }
        public bool blacklist { get; set; }
        public int eventCount { get; set; }
        public int playlistCount { get; set; }
        public int playlistBeSubscribedCount { get; set; }
    }

    public class Binding
    {
        public long expiresIn { get; set; }
        public long userId { get; set; }
        public object tokenJsonStr { get; set; }
        public string url { get; set; }
        public bool expired { get; set; }
        public int refreshTime { get; set; }
        public long id { get; set; }
        public int type { get; set; }
    }
}
