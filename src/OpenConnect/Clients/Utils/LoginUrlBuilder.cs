﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenConnect.Clients.Utils
{
    public class LoginUrlBuilder
    {
        public string ApiPath { get; set; }

        public string ResponseTypeParamName { get; set; }

        public string ClientIdParamName { get; set; }

        public string RedirectUriParamName { get; set; }

        public string ScopeParamName { get; set; }

        public string DisplayParamName { get; set; }

        public LoginUrlBuilder(string apiPath)
        {
            Require.NotNullOrEmpty(apiPath, "apiPath");

            ApiPath = apiPath;

            ResponseTypeParamName = "response_type";
            ClientIdParamName = "client_id";
            RedirectUriParamName = "redirect_uri";
            ScopeParamName = "scope";
            DisplayParamName = "display";
        }

        public string Build(AppInfo appInfo, string display, ResponseType responseType)
        {
            Require.NotNull(appInfo, "appInfo");

            var builder = UrlBuilder.Create(ApiPath)
                                    .WithParam(ResponseTypeParamName, responseType == ResponseType.Code ? "code" : "token")
                                    .WithParam(ClientIdParamName, appInfo.AppId)
                                    .WithParam(RedirectUriParamName, appInfo.RedirectUri)
                                    .WithParam(ScopeParamName, appInfo.Scope)
                                    .WithParam(DisplayParamName, display);

            return builder.Build();
        }
    }
}