using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;

namespace 网易云评论搜索器
{
    public class NetEaseMusic
    {
        public NetEaseMusic(long musicId, long userId, string proxy = null)
        {
            MusicID = musicId;
            UserID = userId;
            _proxy = new WebProxy(proxy, true);
        }

        public long MusicID { get; private set; }
        public long UserID { get; private set; }

        private readonly WebProxy _proxy;

        #region 获取评论
        public async Task<string> GetComments(object data)
        {
            string url = $"https://music.163.com/weapi/v1/resource/comments/R_SO_4_{MusicID}?csrf_token=";
            return await Post(url, data);

        }

        public async Task<T> GetComments<T>(object data) where T : class
        {
            string result = await GetComments(data);
            return JsonConvert.DeserializeObject<T>(result);
        }

        #endregion

        public async Task<string> GetUserNameById()
        {
            string url = $"https://music.163.com/weapi/v1/user/detail/{UserID}";
            object data = new
            {
                csrf_token = ""
            };
            string result = await Post(url, data);
            return result;
        }

        public async Task<T> GetUserNameById<T>() where T : class
        {
            string result = await GetUserNameById();
            return JsonConvert.DeserializeObject<T>(result);
        }

        public async Task<string> GetMusicNameById()
        {
            string url = $"https://music.163.com/weapi/v3/song/detail";
            object data = new
            {
                c = JsonConvert.SerializeObject(new object[] {
                        new { id=MusicID }
                    }),
                ids = new long[] { MusicID },
                csrf_token = string.Empty
            };
            return await Post(url, data);
        }

        public async Task<T> GetMusicNameById<T>() where T : class
        {
            string result = await GetMusicNameById();
            return JsonConvert.DeserializeObject<T>(result);
        }

        private async Task<string> Post(string url, object data)
        {
            WebClient wb = new WebClient
            {
                Proxy = _proxy,
                Headers = new WebHeaderCollection()
                {
                    [HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36",
                    [HttpRequestHeader.Referer] = $"http://music.163.com/song?id={MusicID}",
                    [HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded",
                    [HttpRequestHeader.AcceptEncoding] = "gzip, deflate"
                }
            };
            var result = await wb.UploadValuesTaskAsync(url, "POST", Crypto.GetNameValue(data));
            var gs = new GZipStream(new MemoryStream(result), CompressionMode.Decompress);
            using (StreamReader sr = new StreamReader(gs))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
