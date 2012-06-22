﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenConnect.Providers;

namespace OpenConnect
{
    public class OpenConnectClient
    {
        private IAuthorizationUrlBuilder _urlBuilder;
        private IGetAccessTokenRequest _accessTokenRetriever;
        private IGetUserInfoRequest _userInfoRetriever;

        public AppInfo AppInfo { get; private set; }

        public OpenConnectClient(AppInfo appInfo, IOpenConnectProvider provider)
        {
            Require.NotNull(appInfo, "appInfo");
            Require.NotNull(provider, "provider");

            AppInfo = appInfo;
            _urlBuilder = provider.GetAuthorizationUrlBuilder();
            _accessTokenRetriever = provider.GetAccessTokenRetriever();
            _userInfoRetriever = provider.GetUserInfoRetriever();
        }

        public string GetAuthUrl(string display, ResponseType responseType)
        {
            return _urlBuilder.Build(AppInfo, display, responseType);
        }

        public AccessToken GetAccessToken(string authCode, string state = null)
        {
            return _accessTokenRetriever.GetResponse(AppInfo, authCode, state);
        }

        public UserInfo GetUserInfo(string accessToken, string userOpenId = null)
        {
            return _userInfoRetriever.GetResponse(AppInfo, accessToken, userOpenId);
        }
    }
}
