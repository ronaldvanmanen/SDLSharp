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

using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.NuGet;
using static Nuke.Common.Tools.NuGet.NuGetTasks;

interface IRelease : IBuild
{
    public Target Release => _ => _
        .DependsOn<IPack>(target => target.Pack)
        .Produces(ArtifactsDirectory / "log" / "*.*")
        .Executes(() =>
        {
            var packagesDirectory = ArtifactsDirectory / "pkg" / "Release";
            var packages = packagesDirectory.GlobFiles("*.nupkg");
            foreach (var package in packages)
            {
                PublishOnAzure(package);
                PublishOnGitHub(package);
            }
        });

    void PublishOnAzure(AbsolutePath packagePath)
    {
        NuGetPush(settings => settings
            .SetTargetPath(packagePath)
            .SetSource("https://pkgs.dev.azure.com/ronaldvanmanen/_packaging/ronaldvanmanen/nuget/v3/index.json")
            .SetApiKey("AzureDevOps")
            .SetNonInteractive(IsServerBuild)
            .SetProcessAdditionalArguments("-SkipDuplicate")
        );
    }

    void PublishOnGitHub(AbsolutePath packagePath)
    {
        NuGetPush(settings => settings
            .SetTargetPath(packagePath)
            .SetSource("https://nuget.pkg.github.com/ronaldvanmanen/index.json")
            .SetApiKey(GitHubActions.Token)
            .SetNonInteractive(IsServerBuild)
            .SetProcessAdditionalArguments("-SkipDuplicate")
        );
    }
}
