using DoubanGroup.Core.Api.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Core.Api
{
    public class ApiClient
    {
        #region 常量

        /// <summary>
        /// APP ID
        /// </summary>
        private const string APP_ID = "00a0951fbec80b0501e1bf5f3c58210f";

        /// <summary>
        /// APP SECRET
        /// </summary>
        private const string APP_SECRET = "77faec137e9bda16";

        /// <summary>
        /// REDIRECT URL
        /// </summary>
        private const string REDIRECT_URI = "http://group.douban.com/!service/android";

        /// <summary>
        /// 获取Token的请求路径
        /// </summary>
        private const string TOKEN_URL = "https://www.douban.com/service/auth2/token";

        /// <summary>
        /// 每次请求需要带上的APP NAME
        /// </summary>
        public const string APP_NAME = "radio_android";

        /// <summary>
        /// 每次请求需要带上的VERSION
        /// </summary>
        public const string VERSION = "635";

        /// <summary>
        /// 常规的Api Host
        /// </summary>
        private static readonly Uri NORMAL_API_HOST = new Uri("http://api.douban.com/v2/", UriKind.Absolute);

        /// <summary>
        /// 需要SSL加密的Api Host
        /// </summary>
        private static readonly Uri SSL_API_HOST = new Uri("https://api.douban.com/v2/", UriKind.Absolute);

        #endregion

        #region 辅助方法

        /// <summary>
        /// 构造一个默认的Parameters实例
        /// </summary>
        /// <param name="addClientAndVersion">是否将客户端信息添加到Parameters里</param>
        /// <returns></returns>
        private Parameters CreateQueryParameters(bool addClientAndVersion = true)
        {
            var para = new Parameters();
            para.Add("apiKey", APP_ID);

            return para;
        }

        /// <summary>
        /// 构造一个默认的HttpClient实例
        /// </summary>
        /// <returns></returns>
        private HttpClient CreateClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "api-client/2.0 com.douban.group");

            if (this.AccessTokenProvider?.AccessToken != null)
            {
                client.BaseAddress = SSL_API_HOST;
                client.DefaultRequestHeaders.Add("Authorization", string.Format($"Bearer {this.AccessTokenProvider.AccessToken}"));
            }
            else
            {
                client.BaseAddress = NORMAL_API_HOST;
            }

            return client;
        }

        /// <summary>
        /// 构造一个包含Get参数的URL
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private string BuildUrl(string url, Parameters parameters)
        {
            var para = this.CreateQueryParameters();

            if (parameters != null && parameters.Count > 0)
            {
                foreach (string name in parameters)
                {
                    para.Add(name, parameters[name]);
                }
            }

            if (url.IndexOf("?") > -1)
            {
                url = url + "&" + para.ToString();
            }
            else
            {
                url = url + "?" + para.ToString();
            }

            return url;
        }

        /// <summary>
        /// 将HTTP输出转换为实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        private async Task<T> ProcessResponse<T>(HttpResponseMessage response)
        {
            string json = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ApiError>(json);
                throw new ApiException(error);
            }
        }

        /// <summary>
        /// 通用的GET方法，返回实体类
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">请求路径</param>
        /// <param name="parameters">GET参数</param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        private async Task<T> Get<T>(string url, Parameters parameters)
        {
            url = this.BuildUrl(url, parameters);

            using (var client = this.CreateClient())
            {
                var response = await client.GetAsync(url);

                return await this.ProcessResponse<T>(response);
            }
        }

        /// <summary>
        /// 通用的POST方法，返回实体类
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="url">请求路径</param>
        /// <param name="parameters">POST参数</param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        private async Task<T> Post<T>(string url, Parameters parameters)
        {
            url = this.BuildUrl(url, null);

            using (var client = this.CreateClient())
            {
                HttpResponseMessage response;
                if (parameters != null)
                {
                    var postContent = new FormUrlEncodedContent(parameters.ToKeyValuePairs());
                    response = await client.PostAsync(url, postContent);
                }
                else
                {
                    response = await client.PostAsync(url, null);
                }

                return await this.ProcessResponse<T>(response);
            }
        }

        #endregion

        private IAccessTokenProvider AccessTokenProvider { get; set; }

        public ApiClient(IAccessTokenProvider accessTokenProvider)
        {
            this.AccessTokenProvider = accessTokenProvider;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public async Task<Session> Login(string userName, string password)
        {
            var para = this.CreateQueryParameters(false);
            para.Add("client_id", APP_ID);
            para.Add("client_secret", APP_SECRET);
            para.Add("redirect_uri", REDIRECT_URI);
            para.Add("grant_type", "password");
            para.Add("username", userName);
            para.Add("password", password);

            return await this.Post<Session>(TOKEN_URL, para);
        }

        /// <summary>
        /// 得到频道列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Channel>> GetChannelList()
        {
            string url = "group/channels";

            return await this.Get<List<Channel>>(url, null);
        }

        /// <summary>
        /// 根据频道得到小组列表
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        public async Task<GroupList> GetGroupByChannel(string channel)
        {
            string url = $"group/channels/{channel}/groups";

            return await this.Get<GroupList>(url, null);
        }

        /// <summary>
        /// 根据频道得到主题列表
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<ChannelTopicList> GetTopicByChannel(string channel, int start, int count)
        {
            string url = $"group/channels/{channel}/topics";

            var para = new Parameters();
            para.Add("start", start.ToString());
            para.Add("count", count.ToString());

            return await this.Get<ChannelTopicList>(url, para);
        }

        /// <summary>
        /// 得到小组的主题列表
        /// </summary>
        /// <param name="groupID"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<TopicList> GetTopicByGroup(long groupID, int start, int count)
        {
            string url = $"group/{groupID}/topics";

            var para = new Parameters();
            para.Add("start", start.ToString());
            para.Add("count", count.ToString());

            return await this.Get<TopicList>(url, para);
        }

        /// <summary>
        /// 得到小组信息
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public async Task<Group> GetGroup(long groupID)
        {
            string url = $"group/{groupID}/";

            return await this.Get<Group>(url, null);
        }

        /// <summary>
        /// 得到小组成员列表
        /// </summary>
        /// <param name="groupID"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<GroupMemberList> GetGroupMembers(long groupID, int start, int count)
        {
            string url = $"group/{groupID}/members";

            var para = new Parameters();
            para.Add("start", start.ToString());
            para.Add("count", count.ToString());

            return await this.Get<GroupMemberList>(url, para);
        }

        /// <summary>
        /// 得到文章下的评论
        /// </summary>
        /// <param name="topicID"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<CommentList> GetCommentList(long topicID, int start, int count)
        {
            string url = $"group/topic/{topicID}/comments";

            var para = new Parameters();
            para.Add("start", start.ToString());
            para.Add("count", count.ToString());

            return await this.Get<CommentList>(url, para);
        }

        /// <summary>
        /// 得到作者发表的评论
        /// </summary>
        /// <param name="topicID"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<CommentList> GetOpCommentList(long topicID, int start, int count)
        {
            string url = $"group/topic/{topicID}/op_comments";

            var para = new Parameters();
            para.Add("start", start.ToString());
            para.Add("count", count.ToString());

            return await this.Get<CommentList>(url, para);
        }

        /// <summary>
        /// 得到主题信息
        /// </summary>
        /// <param name="topicID"></param>
        /// <returns></returns>
        public async Task<Topic> GetTopic(long topicID)
        {
            string url = $"group/topic/{topicID}/";

            return await this.Get<Topic>(url, null);
        }

        /// <summary>
        /// 得到用户详细信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<UserDetail> GetUserDetail(long userID, int count)
        {
            string url = $"group/user/{userID}/";

            var para = new Parameters();

            List<string> fields = new List<string>();
            fields.Add("rec_topic_count");
            fields.Add("like_topic_count");
            fields.Add("join_group_count");
            fields.Add("join_groups");
            fields.Add("album_count");
            fields.Add("user");

            para.Add("fields", string.Join(",", fields.ToArray()));
            para.Add("count", count.ToString());

            return await this.Get<UserDetail>(url, para);
        }

        /// <summary>
        /// 得到用户加入的小组列表
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<GroupList> GetUserJoinedGroups(long userID, int start, int count)
        {
            string url = $"group/people/{userID}/joined_groups";

            var para = new Parameters();
            para.Add("start", start.ToString());
            para.Add("count", count.ToString());

            return await this.Get<GroupList>(url, para);
        }

        /// <summary>
        /// 得到用户管理的小组列表
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<GroupList> GetUserManagedGroups(long userID, int start, int count)
        {
            string url = $"group/people/{userID}/managed_groups";

            var para = new Parameters();
            para.Add("start", start.ToString());
            para.Add("count", count.ToString());

            return await this.Get<GroupList>(url, para);
        }

        /// <summary>
        /// 加入小组
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public async Task JoinGroup(long groupID)
        {
            string url = $"group/{groupID}/join";
            var para = new Parameters();
            para.Add("type", "join");

            await this.Post<object>(url, para);
        }

        /// <summary>
        /// 退出小组
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public async Task QuitGroup(long groupID)
        {
            string url = $"group/{groupID}/join";
            var para = new Parameters();
            para.Add("type", "quit");

            await this.Post<object>(url, para);
        }

        /// <summary>
        /// 得到用户首页主题
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<UserTopicList> GetUserTopics(int start, int count)
        {
            string url = "group/user_topics";

            var para = new Parameters();
            para.Add("start", start.ToString());
            para.Add("count", count.ToString());

            return await this.Get<UserTopicList>(url, para);
        }

        /// <summary>
        /// 得到用户喜欢的主题
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<TopicList> GetLikedTopis(int start, int count)
        {
            string url = "group/liked_topics";

            var para = new Parameters();
            para.Add("start", start.ToString());
            para.Add("count", count.ToString());

            return await this.Get<TopicList>(url, para);
        }

        /// <summary>
        /// 关注用户
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<User> FollowUser(long userID)
        {
            string url = $"user/{userID}/follow";

            return await this.Post<User>(url, null);
        }

        /// <summary>
        /// 喜欢主题
        /// </summary>
        /// <param name="topicID"></param>
        /// <returns></returns>
        public async Task<object> LikeTopic(long topicID)
        {
            string url = $"group/topic/{topicID}/like";

            return await this.Post<object>(url, null);
        }

        /// <summary>
        /// 取消喜欢主题
        /// </summary>
        /// <param name="topicID"></param>
        /// <returns></returns>
        public async Task<object> DislikeTopic(long topicID)
        {
            string url = $"group/topic/{topicID}/remove_like";

            return await this.Post<object>(url, null);
        }

        /// <summary>
        /// 给评论点赞
        /// </summary>
        /// <param name="commentID"></param>
        /// <returns></returns>
        public async Task<VoteCommentResult> VoteComment(long topicID, long commentID)
        {
            string url = $"group/topic/{topicID}/vote_comment";

            var para = new Parameters();
            para.Add("comment_id", commentID.ToString());

            return await this.Post<VoteCommentResult>(url, para);
        }
    }
}
