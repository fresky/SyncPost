SyncPost
========

Sync post from octopress to blog provider support metaweblog API

## Usage

Fill the required infomation in the `SyncPost.exe.config`. Password is optional. If password is not specified in the configuration file. User need to input the password when run the program.

Example of the configuration is:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5" />
    </startup>
    <appSettings>
        <add key="FromBlog" value="http://fresky.github.io/"/>
		<add key="FromBlogName" value="fresky.github.io - Dawei XU"/>
        <add key="PostDir" value="D:\Software\post"/>
        <add key="ToBlog" value="http://fresky.cnblogs.com/services/metaweblog.aspx"/>
        <add key="UserName" value="fresky"/>
        <add key="Password" value=""/>
    </appSettings>
</configuration>
```

## Requirements

Require [.NET Framework 4.5](http://msdn.microsoft.com/library/vstudio/5a4x27ek).

## Credits
SyncPost used the [MetaWeblogSharp](http://metaweblogsharp.codeplex.com/) to communicate with MetaWeblog providers.


## License

SyncPost is released under the MIT License. See the bundled LICENSE file for details.

## Chang Log

1. 01/26/2014	update the blog name
2. 01/24/2014	initial version