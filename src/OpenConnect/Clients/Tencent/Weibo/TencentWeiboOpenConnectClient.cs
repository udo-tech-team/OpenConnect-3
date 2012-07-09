﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenConnect.Utils;
using OpenConnect.Clients.Utils;

namespace OpenConnect.Clients.Tencent.Weibo
{
    public class TencentWeiboOpenConnectClient : IOpenConnectClient
    {
        private IHttpClient _httpClient;

        public AppInfo AppInfo { get; private set; }

        public TencentWeiboOpenConnectClient(AppInfo appInfo)
            : this(appInfo, HttpClient.Instance)
        {
        }

        public TencentWeiboOpenConnectClient(AppInfo appInfo, IHttpClient httpClient)
        {
            AppInfo = appInfo;
            _httpClient = httpClient;
        }

        public string BuildLoginUrl(ResponseType responseType, string redirectUri, string scope, string display)
        {
            return new LoginUrlBuilder("https://open.t.qq.com/cgi-bin/oauth2/authorize")
            .Build(AppInfo, responseType, redirectUri, scope, display);
        }

        public AccessTokenResponse GetAccessToken(string authCode, string redirectUri, string state)
        {
            var now = DateTime.Now;

            var request = new GetAccessTokenRequest("https://open.t.qq.com/cgi-bin/oauth2/access_token", _httpClient);
            var response = request.GetResponse(AppInfo, authCode, redirectUri, state);

            return TencentGetAccessTokenResponseParser.Parse(response, now);
        }

        public IUserInfo GetUserInfo(string accessToken, string userId)
        {
            return new TencentWeiboGetUserInfoRequest(_httpClient)
                .GetResponse(AppInfo, accessToken, userId);
        }
    }
}
