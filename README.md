# LINE Login SDK for C#

This repository contains LINE Login SDK for C# and MVC Authentication Middleware (Full .NET)
See [Integrating LINE Login with your web app](https://developers.line.me/en/docs/line-login/web/integrate-line-login/) for LINE Login detail.

# LINE Login SDK for C#
[line-login-csharp](/line-login-csharp) contains source code for LINE Login. You can get it via NuGet. [LineLogin.CSharp](https://www.nuget.org/packages/LineLogin.CSharp/)

## Usage

Instantiate LINE Login Client

```csharp
var client = new LineLoginClient(channelId, channelSecret, redirectUri, "State", Scope.Profile | Scope.OpenId);
```

Get Authorization URL

```csharp
var authUri = client.GetAuthUri();
```

Get Token (AccessToken and JWT)

```csharp
var code = Request.QueryString["code"];
var token = await client.GetToken(code);
```

# LINE Login OWIN Middleware
[LineLoginMiddleware](/LineLoginMiddleware) contains source code for LINE Login OWIN Middleware. You can get it via NuGet. [Owin.Security.Middleware.Line](https://www.nuget.org/packages/Owin.Security.Middleware.Line/)

## Usage

Add following in ConfigureAuth method in Startup.Auth.cs. Also add necessary parameters in Web.config.

```csharp
app.UseLineAuthentication(new LineAuthenticationOptions(
    channelId : ConfigurationManager.AppSettings["ChannelId"],
    channelSecret : ConfigurationManager.AppSettings["ChannelSecret"],
    redirectUri : ConfigurationManager.AppSettings["RedirectUri"],
    scope : Scope.OpenId | Scope.Profile
));
```

# Samples
[line-login-csharp-web](/line-login-csharp-web) contains an ASP.NET web application which uses LINE Login SDK.

[line-login-csharp-mvc-owin](/line-login-csharp-mvc-owin) contains a MVC application which uses LINE Login OWIN Middleware.

# LICENSE
[MIT](/LICENSE)

# Reference
I refer to following references to get basic coding idea.

[TerribleDev/OwinOAuthProviders](https://github.com/TerribleDev/OwinOAuthProviders)<br>
[Understanding OWIN Forms authentication in MVC 5](https://blogs.msdn.microsoft.com/webdev/2013/07/03/understanding-owin-forms-authentication-in-mvc-5/)<br>
[Integrating LINE Login with your web app](https://developers.line.me/en/docs/line-login/web/integrate-line-login/) 
