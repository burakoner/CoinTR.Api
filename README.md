# CoinTR.Api

A .Net wrapper for the CoinTR API as described on [CoinTR API Documents](https://www.cointr.com/api-doc/common/intro), including all features the API provides using clear and readable objects.

**If you think something is broken, something is missing or have any questions, please open an [Issue](https://github.com/burakoner/CoinTR.Api/issues)**

## Donations

Donations are greatly appreciated and a motivation to keep improving.

|Coin|Address|
|:--|:--|
|**BTC**|33WbRKqt7wXARVdAJSu1G1x3QnbyPtZ2bH|
|**ETH**|0x65b02db9b67b73f5f1e983ae10796f91ded57b64|
|**USDT (TRC-20)**|TXwqoD7doMESgitfWa8B2gHL7HuweMmNBJ|

## Changes & Release Notes

Please see the 👉 [CHANGELOG](https://github.com/burakoner/CoinTR.Api/blob/main/CHANGELOG.md) for all changes.

## Supported Frameworks
The library is targeting both `.NET Standard 2.0` and `.NET Standard 2.1` for optimal compatibility, as well as dotnet 6.0, 7.0, 8.0 and 9.0 to use the latest framework features.

|.NET Implementation|Version Support|
|--|--|
|.NET Core|`2.0` and higher|
|.NET Framework|`4.6.1` and higher|
|.NET Standard|`2.0` and higher|
|Mono|`5.4` and higher|
|Unity|`2018.1` and higher|
|UWP|`10.0.16299` and higher|
|Xamarin.Android|`8.0` and higher|
|Xamarin.iOS|`10.14` and higher|

## Installation

[![NuGet Version](https://img.shields.io/nuget/v/CoinTR.Api.svg?style=for-the-badge)](https://www.nuget.org/packages/CoinTR.Api)  [![Nuget Downloads](https://img.shields.io/nuget/dt/CoinTR.Api.svg?style=for-the-badge)](https://www.nuget.org/packages/CoinTR.Api)


```console
PM> Install-Package CoinTR.Api
```

To get started with CoinTR.Api first you will need to get the library itself. The easiest way to do this is to install the package into your project using  [NuGet](https://www.nuget.org/packages/CoinTR.Api). Using Visual Studio this can be done in two ways.

### Using the package manager

In Visual Studio right click on your solution and select 'Manage NuGet Packages for solution...'. A screen will appear which initially shows the currently installed packages. In the top bit select 'Browse'. This will let you download net package from the NuGet server. In the search box type 'CoinTR.Api' and hit enter. The CoinTR.Api package should come up in the results. After selecting the package you can then on the right hand side select in which projects in your solution the package should install. After you've selected all project you wish to install and use CoinTR.Api in hit 'Install' and the package will be downloaded and added to you projects.

### Using the package manager console

In Visual Studio in the top menu select 'Tools' -> 'NuGet Package Manager' -> 'Package Manager Console'. This should open up a command line interface. On top of the interface there is a dropdown menu where you can select the Default Project. This is the project that CoinTR.Api will be installed in. After selecting the correct project type  `Install-Package CoinTR.Api`  in the command line interface. This should install the latest version of the package in your project.

After doing either of above steps you should now be ready to actually start using CoinTR.Api.

## Getting started

After installing it's time to actually use it. To get started we have to add the CoinTR.Api namespace:  `using CoinTR.Api;`.

CoinTR.Api provides two clients to interact with the CoinTR.Api. The  `CoinTRRestApiClient`  provides all rest API calls. The  `CoinTRSocketApiClient` provides functions to interact with the websocket provided by the CoinTR.Api. Both clients are disposable and as such can be used in a  `using`statement.

## Usage

You can find the basic usage of the methods in this library as below.  
👉 [Rest Api Methods](#rest-api-methods)  
👉 [WebSocket Api Methods](#websocket-api-methods)  

## Rest Api Methods

```csharp

```

## WebSocket Api Methods

```csharp

```
