// SDL2Sharp
//
// Copyright (C) 2021-2024 Ronald van Manen <rvanmanen@gmail.com>
//
// This software is provided 'as-is', without any express or implied
// warranty.  In no event will the authors be held liable for any damages
// arising from the use of this software.
// 
// Permission is granted to anyone to use this software for any purpose,
// including commercial applications, and to alter it and redistribute it
// freely, subject to the following restrictions:
//
// 1. The origin of this software must not be misrepresented; you must not
//    claim that you wrote the original software. If you use this software
//    in a product, an acknowledgment in the product documentation would be
//    appreciated but is not required.
// 2. Altered source versions must be plainly marked as such, and must not be
//    misrepresented as being the original software.
// 3. This notice may not be removed or altered from any source distribution.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
using Nuke.Common;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.PowerShell;
using Nuke.Common.Tools.ClangSharpPInvokeGenerator;
using static System.Runtime.InteropServices.RuntimeInformation;
using static Nuke.Common.IO.HttpTasks;
using static Nuke.Common.Tooling.ProcessTasks;
using static Nuke.Common.Tools.PowerShell.PowerShellTasks;

partial interface ISetup : IBuild
{
    private Tool Bash => ToolResolver.GetPathTool("bash");

    private Tool Chmod => ToolResolver.GetPathTool("chmod");

    public Target Setup => _ => _
        .After<IClean>(target => target.Clean)
        .Produces(ArtifactsDirectory / "log" / "*.*")
        .Executes(() =>
        {
            InstallDotNet();
            InstallAzureArtifactsCredentialProvider();
        });

    private void InstallDotNet()
    {
        var versions = GetTargetFrameworkVersions();
        foreach (var version in versions)
        {
            InstallDotNet(version);
        }
    }

    private void InstallDotNet(Version version)
    {
        if (IsOSPlatform(OSPlatform.Windows))
        {
            var script = ArtifactsDirectory / "dotnet-install.ps1";
            HttpDownloadFile("https://dot.net/v1/dotnet-install.ps1", script, clientConfigurator: ConfigureHttpClient);
            var installDirectory = ArtifactsDirectory / "dotnet" / Architecture;

            Serilog.Log.Information($"Install latest release of .NET {version}.");
            PowerShell(settings => settings
                .SetFile(script)
                .SetFileArguments("-Architecture", Architecture, "-Channel", version.ToString(2), "-InstallDir", installDirectory, "-NoPath", "-Version", "latest")
                .SetProcessEnvironmentVariable("DOTNET_CLI_TELEMETRY_OPTOUT", "1")
                .SetProcessEnvironmentVariable("DOTNET_MULTILEVEL_LOOKUP", "1")
                .SetProcessEnvironmentVariable("DOTNET_SKIP_FIRST_TIME_EXPERIENCE", "1")
            );

            Environment.SetEnvironmentVariable("PATH", $"{installDirectory};{Environment.GetEnvironmentVariable("PATH")}");
            Environment.SetEnvironmentVariable("DOTNET_ROOT", installDirectory);
            Environment.SetEnvironmentVariable("DOTNET_EXE", installDirectory / "dotnet.exe");
        }
        else
        {
            var script = ArtifactsDirectory / "dotnet-install.sh";
            HttpDownloadFile("https://dot.net/v1/dotnet-install.sh", script, clientConfigurator: ConfigureHttpClient);
            Chmod($"+x {script}");
            var installDirectory = ArtifactsDirectory / "dotnet" / Architecture;

            var installCommand = $"{script} --architecture {Architecture} --channel {version} --install-dir {installDirectory} --no-path --version latest";
            var enviromentVariables = new Dictionary<string, string>
                {
                    { "DOTNET_CLI_TELEMETRY_OPTOUT", "1" },
                    { "DOTNET_MULTILEVEL_LOOKUP", "1" },
                    { "DOTNET_SKIP_FIRST_TIME_EXPERIENCE", "1" }
                };
            Serilog.Log.Information($"Install latest version of .NET \"{version}\".");
            Bash(installCommand, environmentVariables: enviromentVariables);

            Environment.SetEnvironmentVariable("PATH", $"{installDirectory}:{Environment.GetEnvironmentVariable("PATH")}");
            Environment.SetEnvironmentVariable("DOTNET_ROOT", installDirectory);
            Environment.SetEnvironmentVariable("DOTNET_EXE", installDirectory / "dotnet");
        }
    }

    private HttpClient ConfigureHttpClient(HttpClient client)
    {
        client.Timeout = TimeSpan.FromMinutes(1);
        return client;
    }

    private void InstallAzureArtifactsCredentialProvider()
    {
        if (IsOSPlatform(OSPlatform.Windows))
        {
            Serilog.Log.Information($"Install Azure Artifacts Credential Provider");
            PowerShell("iex \"& { $(irm https://aka.ms/install-artifacts-credprovider.ps1) } -AddNetfx\"");
        }
        else
        {
            Serilog.Log.Information($"Install Azure Artifacts Credential Provider");
            StartShell("sh -c \"$(curl -fsSL https://aka.ms/install-artifacts-credprovider.sh)\"");
        }
    }
}
