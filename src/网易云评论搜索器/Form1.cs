using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using 网易云评论搜索器.Models;

namespace 网易云评论搜索器
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private NetEaseMusic netEaseMusic;
        private BindingList<OutputComments> PrintDataList = new BindingList<OutputComments>();
        private readonly int Interval = 1000;

        private void BtnOK_Click(object sender, EventArgs e)
        {
            BtnOK.Enabled = false;
            BtnOK.Text = "正在搜索...";
            long.TryParse(txtUserId.Text, out long userId);
            long.TryParse(txtMusicId.Text, out long musicId);
            string proxy = string.IsNullOrWhiteSpace(txtProxy.Text) ? null : txtProxy.Text;
            gvResult.DataSource = PrintDataList;
            netEaseMusic = new NetEaseMusic(musicId, userId, proxy);

            BegianSearch();

            BtnOK.Enabled = true;
            BtnOK.Text = "确认";
        }

        private async void BegianSearch()
        {
            Param p = new Param() { offset = 0 };
            string nickName = (await netEaseMusic.GetUserNameById<UserDetail>())?.profile?.nickname;
            string musicName = (await netEaseMusic.GetMusicNameById<MusicDetail>())?.songs?.First()?.name;
            CommentsEntity commentsEntity;
            do
            {
                BtnOK.Text = $"正在搜索第{p.offset / 20 + 1 }页";
                commentsEntity = await netEaseMusic.GetComments<CommentsEntity>(p);
                foreach (var comment in commentsEntity.comments)
                {
                    if (comment.user.userId == netEaseMusic.UserID)
                    {
                        PrintDataList.Add(new OutputComments()
                        {
                            歌曲ID = netEaseMusic.MusicID,
                            歌曲名 = musicName,
                            评论人 = nickName,
                            评论位置 = p.offset / 20 + 1,
                            评论内容 = comment.content,
                            评论时间 = comment.time.ToDatetime()
                        });
                    }
                }
                p.offset += 20;
                Thread.Sleep(Interval);
            }
            while (p.offset < commentsEntity.total + 20);
        }
    }
}
