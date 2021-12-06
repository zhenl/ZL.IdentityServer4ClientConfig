# ZL.IdentityServer4ClientConfig
Help to create Identity Server 4 Client and Web Api easily.

## Guide For Client

First, Install the packege	ZL.IdentityServer4ClientConfig for nuget

Second, add below code in Program.cs

```
builder.Services.AddIS4OpenIdConnect(builder.Configuration); //Added

```

Last, add client configuration in appsettings.json:

```
 "IdentityServer4Client": {
    "Authority": "http://localhost:4010",
    "ClientId": "mvc client",
    "ClientSecret": "secret",
    "ResponseType": "code",
    "SaveTokens": "true",
    "RequireHttpsMetadata": "false",
    "Scopes": [ "openid", "profile", "myapi" ],
    "JsonKeys": [
      {
        "ClaimType": "age"
      },
      {
        "ClaimType": "nickname",
        "Key": "nickname"
      },
      {
        "ClaimType": "mydefine",
        "Key": "mydefine"
      }
    ]

  }
```

## Guide for Web Api

First, Install the packege	ZL.IdentityServer4ClientConfig for nuget

Second, add below code in Program.cs

```
builder.Services.AddIdentityServer4Api(builder.Configuration);//Added

```

Last, add  configuration in appsettings.json:
```
  "IdentityServer4Api": {
    "Authority": "http://localhost:4010",
    "CorsOrgins": [
      "https://localhost:5006"
    ],
    "Policies": [
      {
        "Name": "ApiScope",
        "RequireAuthenticatedUser": "true",
        "Claims": [
          {
            "ClaimType": "scope",
            "AllowValues": [ "myapi" ]
          }
        ]
      }
    ],
    "RequireHttpsMetadata": "false"
  }
```
