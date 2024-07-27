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

using System.Linq;
using Nuke.Common;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

interface ITest : IBuild
{
    public Target Test => _ => _
        .DependsOn<ICompile>(target => target.Compile)
        .Produces(ArtifactsDirectory / "tst" / "*.*", ArtifactsDirectory / "log" / "*.*")
        .Executes(() =>
        {
            foreach (var project in Solution.AllProjects.Where(e => e.Name.EndsWith(".Tests")))
            {
                foreach (var targetFramework in project.GetTargetFrameworks())
                {
                    DotNetTest(settings => settings
                        .SetProjectFile(project)
                        .SetConfiguration(Configuration)
                        .SetNoRestore(true)
                        .SetNoBuild(true)
                        .SetVerbosity(Verbosity.ToDotNetVerbosity())
                        .SetFramework(targetFramework)
                        .SetProcessArgumentConfigurator(_ => _.Add("-- RunConfiguration.DisableAppDomain=true"))
                    );
                }
            }
        });
}
