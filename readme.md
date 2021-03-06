# VueJsAspnetCore

Template to integrate [VueJs](https://github.com/vuejs/vue) on [Visual Studio](https://visualstudio.microsoft.com/) aspnetcore project.

Publish `.csproj` using `vuejs` as SPA 


# How to use

## Requirements
Needs yarn, but you can adapt easily to use npm instead, just need to edit `.csproj`

## Development
For development I recommend using 
`yarn serve`
or 
`npm run serve`

## Publish
`dotnet publish`
or 
using Visual Studio


# From the scratch

Create an empty aspnetcore project +2.0

## Update *.csproj*

Add 

```xml

  <!-- Build and copy files on publish -->
  <Target Name="PublishRunWebpack" AfterTargets="ComputeFilesToPublish">
    <!-- As part of publishing, ensure the JS resources are freshly built in production mode -->
    <Exec Command="yarn" />

    <Exec Command="yarn build --mode production --dest dist --target app" />
    <!-- Include the newly-built files in the publish output -->
    <ItemGroup>
      <DistFiles Include="dist\**" />
      <ResolvedFileToPublish Include="@(DistFiles->'%(FullPath)')" Exclude="@(ResolvedFileToPublish)">
        <RelativePath>%(DistFiles.Identity)</RelativePath>
        <CopyToPublishDirectory>PreserveNewest</CopyToPublishDirectory>
      </ResolvedFileToPublish>
    </ItemGroup>
  </Target>
```

### if using npm 
use 
```xml

  <!-- Build and copy files on publish -->
  <Target Name="PublishRunWebpack" AfterTargets="ComputeFilesToPublish">
    <!-- As part of publishing, ensure the JS resources are freshly built in production mode -->
    <Exec Command="npm install" />

    <Exec Command="npm run build --mode production --dest dist --target app" />
    <!-- Include the newly-built files in the publish output -->
    <ItemGroup>
      <DistFiles Include="dist\**" />
      <ResolvedFileToPublish Include="@(DistFiles->'%(FullPath)')" Exclude="@(ResolvedFileToPublish)">
        <RelativePath>%(DistFiles.Identity)</RelativePath>
        <CopyToPublishDirectory>PreserveNewest</CopyToPublishDirectory>
      </ResolvedFileToPublish>
    </ItemGroup>
  </Target>
```


## update `Startup.cs`


### AddSpaStaticFiles
Add in ConfigureServices
```csharp

services.AddSpaStaticFiles(x =>
{
    x.RootPath = "dist"; // set static files to dist
});
```

like this
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddSpaStaticFiles(x =>
    {
        x.RootPath = "dist"; // set static files to dist
    });
}
```


### Configure
Add in Configure
```csharp
app.UseSpaStaticFiles(new StaticFileOptions()
{
#if !DEBUG
    OnPrepareResponse = ctx =>
    {
        // https://developers.google.com/web/fundamentals/performance/webpack/use-long-term-caching
        ctx.Context.Response.Headers[HeaderNames.CacheControl] = "max-age=" + 31536000;
    }  
#endif
});
app.UseSpa(x =>
{
    x.Options.DefaultPage = "/index.html";
});
```




