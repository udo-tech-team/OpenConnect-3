﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenConnect.Utils;

namespace OpenConnect.Clients.Tencent.Weibo
{
    class TencentWeiboGetUserInfoRequest
    {
        public IHttpClient HttpClient { get; private set; }

        public TencentWeiboGetUserInfoRequest(IHttpClient httpClient)
        {
            Require.NotNull(httpClient, "httpClient");

            HttpClient = httpClient;
        }

        public IUserInfo GetResponse(AppInfo appInfo, string accessToken, string userId)
        {
            Require.NotNull(appInfo, "appInfo");
            Require.NotNullOrEmpty(accessToken, "accessToken");

            var url = UrlBuilder.Create("https://open.t.qq.com/api/user/info")
                                .WithParam("oauth_consumer_key", appInfo.AppId)
                                .WithParam("access_token", accessToken)
                                .WithParam("scope", appInfo.DefaultScope)
                                .WithParam("oauth_version", "2.a")
                                .WithParam("openid", userId)
                                .Build();

            var json = HttpClient.Get(url, null, Encoding.UTF8);

            var result = (GetUserInfoResult)JsonSerializer.Deserialize(json, typeof(GetUserInfoResult));

            if (result.ret != 0)
                throw new ApiException(result.msg, result.errcode.ToString());

            return result.data;
        }
    }
}
