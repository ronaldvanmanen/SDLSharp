// Generated from https://github.com/ronaldvanmanen/nuke/blob/master/source/Nuke.Common/Tools/ClangSharpPInvokeGenerator/ClangSharpPInvokeGenerator.json

using JetBrains.Annotations;
using Newtonsoft.Json;
using Nuke.Common;
using Nuke.Common.Tooling;
using Nuke.Common.Tools;
using Nuke.Common.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;

namespace Nuke.Common.Tools.ClangSharpPInvokeGenerator;

/// <summary><p>ClangSharp P/Invoke Binding Generator is a tool for generating strongly-typed safe bindings written in C# for .NET and Mono.</p><p>For more details, visit the <a href="https://github.com/dotnet/clangsharp/">official website</a>.</p></summary>
[PublicAPI]
[ExcludeFromCodeCoverage]
[NuGetTool(Id = PackageId, Executable = PackageExecutable)]
public partial class ClangSharpPInvokeGeneratorTasks : ToolTasks, IRequireNuGetPackage
{
    public static string ClangSharpPInvokeGeneratorPath { get => new ClangSharpPInvokeGeneratorTasks().GetToolPathInternal(); set => new ClangSharpPInvokeGeneratorTasks().SetToolPath(value); }
    public const string PackageId = "ClangSharpPInvokeGenerator";
    public const string PackageExecutable = "ClangSharpPInvokeGenerator.dll";
    /// <summary><p>ClangSharp P/Invoke Binding Generator is a tool for generating strongly-typed safe bindings written in C# for .NET and Mono.</p><p>For more details, visit the <a href="https://github.com/dotnet/clangsharp/">official website</a>.</p></summary>
    public static IReadOnlyCollection<Output> ClangSharpPInvokeGenerator(ArgumentStringHandler arguments, string workingDirectory = null, IReadOnlyDictionary<string, string> environmentVariables = null, int? timeout = null, bool? logOutput = null, bool? logInvocation = null, Action<OutputType, string> logger = null, Func<IProcess, object> exitHandler = null) => new ClangSharpPInvokeGeneratorTasks().Run(arguments, workingDirectory, environmentVariables, timeout, logOutput, logInvocation, logger, exitHandler);
    /// <summary><p>ClangSharp P/Invoke Binding Generator is a tool for generating strongly-typed safe bindings written in C# for .NET and Mono.</p><p>For more details, visit the <a href="https://github.com/dotnet/clangsharp/">official website</a>.</p></summary>
    /// <remarks><p>This is a <a href="https://www.nuke.build/docs/common/cli-tools/#fluent-api">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p><ul><li><c>--additional</c> via <see cref="ClangSharpPInvokeGeneratorSettings.Additional"/></li><li><c>--config</c> via <see cref="ClangSharpPInvokeGeneratorSettings.Config"/></li><li><c>--define-macro</c> via <see cref="ClangSharpPInvokeGeneratorSettings.DefineMacro"/></li><li><c>--exclude</c> via <see cref="ClangSharpPInvokeGeneratorSettings.Exclude"/></li><li><c>--file</c> via <see cref="ClangSharpPInvokeGeneratorSettings.File"/></li><li><c>--file-directory</c> via <see cref="ClangSharpPInvokeGeneratorSettings.FileDirectories"/></li><li><c>--headerFile</c> via <see cref="ClangSharpPInvokeGeneratorSettings.HeaderFiles"/></li><li><c>--include</c> via <see cref="ClangSharpPInvokeGeneratorSettings.Include"/></li><li><c>--include-directory</c> via <see cref="ClangSharpPInvokeGeneratorSettings.IncludeDirectory"/></li><li><c>--language</c> via <see cref="ClangSharpPInvokeGeneratorSettings.Language"/></li><li><c>--libraryPath</c> via <see cref="ClangSharpPInvokeGeneratorSettings.LibraryPath"/></li><li><c>--methodClassName</c> via <see cref="ClangSharpPInvokeGeneratorSettings.MethodClassName"/></li><li><c>--namespace</c> via <see cref="ClangSharpPInvokeGeneratorSettings.Namespace"/></li><li><c>--nativeTypeNamesToStrip</c> via <see cref="ClangSharpPInvokeGeneratorSettings.NativeTypeNamesToStrip"/></li><li><c>--output</c> via <see cref="ClangSharpPInvokeGeneratorSettings.Output"/></li><li><c>--output-mode</c> via <see cref="ClangSharpPInvokeGeneratorSettings.OutputMode"/></li><li><c>--prefixStrip</c> via <see cref="ClangSharpPInvokeGeneratorSettings.PrefixStrip"/></li><li><c>--remap</c> via <see cref="ClangSharpPInvokeGeneratorSettings.Remap"/></li><li><c>--std</c> via <see cref="ClangSharpPInvokeGeneratorSettings.Std"/></li><li><c>--test-output</c> via <see cref="ClangSharpPInvokeGeneratorSettings.TestOutput"/></li><li><c>--traverse</c> via <see cref="ClangSharpPInvokeGeneratorSettings.Traverse"/></li><li><c>--with-access-specifier</c> via <see cref="ClangSharpPInvokeGeneratorSettings.WithAccessSpecifier"/></li><li><c>--with-attribute</c> via <see cref="ClangSharpPInvokeGeneratorSettings.WithAttribute"/></li><li><c>--with-callconv</c> via <see cref="ClangSharpPInvokeGeneratorSettings.WithCallConv"/></li><li><c>--with-class</c> via <see cref="ClangSharpPInvokeGeneratorSettings.WithClass"/></li><li><c>--with-guid</c> via <see cref="ClangSharpPInvokeGeneratorSettings.WithGuid"/></li><li><c>--with-librarypath</c> via <see cref="ClangSharpPInvokeGeneratorSettings.WithLibraryPath"/></li><li><c>--with-manual-import</c> via <see cref="ClangSharpPInvokeGeneratorSettings.WithManualImport"/></li><li><c>--with-namespace</c> via <see cref="ClangSharpPInvokeGeneratorSettings.WithNamespace"/></li><li><c>--with-packing</c> via <see cref="ClangSharpPInvokeGeneratorSettings.WithPacking"/></li><li><c>--with-setlasterror</c> via <see cref="ClangSharpPInvokeGeneratorSettings.WithSetLastError"/></li><li><c>--with-suppressgctransition</c> via <see cref="ClangSharpPInvokeGeneratorSettings.WithSuppressGCTransition"/></li><li><c>--with-transparent-struct</c> via <see cref="ClangSharpPInvokeGeneratorSettings.WithTransparentStruct"/></li><li><c>--with-type</c> via <see cref="ClangSharpPInvokeGeneratorSettings.WithType"/></li><li><c>--with-using</c> via <see cref="ClangSharpPInvokeGeneratorSettings.WithUsing"/></li></ul></remarks>
    public static IReadOnlyCollection<Output> ClangSharpPInvokeGenerator(ClangSharpPInvokeGeneratorSettings options = null) => new ClangSharpPInvokeGeneratorTasks().Run<ClangSharpPInvokeGeneratorSettings>(options);
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorTasks.ClangSharpPInvokeGenerator(Nuke.Common.Tools.ClangSharpPInvokeGenerator.ClangSharpPInvokeGeneratorSettings)"/>
    public static IReadOnlyCollection<Output> ClangSharpPInvokeGenerator(Configure<ClangSharpPInvokeGeneratorSettings> configurator) => new ClangSharpPInvokeGeneratorTasks().Run<ClangSharpPInvokeGeneratorSettings>(configurator.Invoke(new ClangSharpPInvokeGeneratorSettings()));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorTasks.ClangSharpPInvokeGenerator(Nuke.Common.Tools.ClangSharpPInvokeGenerator.ClangSharpPInvokeGeneratorSettings)"/>
    public static IEnumerable<(ClangSharpPInvokeGeneratorSettings Settings, IReadOnlyCollection<Output> Output)> ClangSharpPInvokeGenerator(CombinatorialConfigure<ClangSharpPInvokeGeneratorSettings> configurator, int degreeOfParallelism = 1, bool completeOnFailure = false) => configurator.Invoke(ClangSharpPInvokeGenerator, degreeOfParallelism, completeOnFailure);
}
#region ClangSharpPInvokeGeneratorSettings
/// <inheritdoc cref="ClangSharpPInvokeGeneratorTasks.ClangSharpPInvokeGenerator(Nuke.Common.Tools.ClangSharpPInvokeGenerator.ClangSharpPInvokeGeneratorSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
[Command(Type = typeof(ClangSharpPInvokeGeneratorTasks), Command = nameof(ClangSharpPInvokeGeneratorTasks.ClangSharpPInvokeGenerator))]
public partial class ClangSharpPInvokeGeneratorSettings : ToolOptions
{
    /// <summary>An argument to pass to Clang when parsing the input files.</summary>
    [Argument(Format = "--additional {value}")] public IReadOnlyList<string> Additional => Get<List<string>>(() => Additional);
    /// <summary>A configuration option that controls how the bindings are generated.</summary>
    [Argument(Format = "--config {value}")] public IReadOnlyList<ClangSharpPInvokeGeneratorConfigOption> Config => Get<List<ClangSharpPInvokeGeneratorConfigOption>>(() => Config);
    /// <summary>Define <macro> to <value> (or 1 if <value> omitted).</summary>
    [Argument(Format = "--define-macro {value}")] public IReadOnlyList<string> DefineMacro => Get<List<string>>(() => DefineMacro);
    /// <summary>A declaration name to exclude from binding generation.</summary>
    [Argument(Format = "--exclude {value}")] public IReadOnlyList<string> Exclude => Get<List<string>>(() => Exclude);
    /// <summary>A file to parse and generate bindings for.</summary>
    [Argument(Format = "--file {value}")] public IReadOnlyList<string> File => Get<List<string>>(() => File);
    /// <summary>The base path for files to parse.</summary>
    [Argument(Format = "--file-directory {value}")] public IReadOnlyList<string> FileDirectories => Get<List<string>>(() => FileDirectories);
    /// <summary>A file which contains the header to prefix every generated file with.</summary>
    [Argument(Format = "--headerFile {value}")] public IReadOnlyList<string> HeaderFiles => Get<List<string>>(() => HeaderFiles);
    /// <summary>A declaration name to include in binding generation.</summary>
    [Argument(Format = "--include {value}")] public IReadOnlyList<string> Include => Get<List<string>>(() => Include);
    /// <summary>Add directory to include search path.</summary>
    [Argument(Format = "--include-directory {value}")] public IReadOnlyList<string> IncludeDirectory => Get<List<string>>(() => IncludeDirectory);
    /// <summary>Treat subsequent input files as having type <language>.</summary>
    [Argument(Format = "--language {value}")] public string Language => Get<string>(() => Language);
    /// <summary>The string to use in the <c>DllImport</c> attribute used when generating bindings.</summary>
    [Argument(Format = "--libraryPath {value}")] public string LibraryPath => Get<string>(() => LibraryPath);
    /// <summary>The name of the static class that will contain the generated method bindings.</summary>
    [Argument(Format = "--methodClassName {value}")] public string MethodClassName => Get<string>(() => MethodClassName);
    /// <summary>The namespace in which to place the generated bindings.</summary>
    [Argument(Format = "--namespace {value}")] public string Namespace => Get<string>(() => Namespace);
    /// <summary>The contents to strip from the generated NativeTypeName attributes.</summary>
    [Argument(Format = "--nativeTypeNamesToStrip {value}")] public string NativeTypeNamesToStrip => Get<string>(() => NativeTypeNamesToStrip);
    /// <summary>The mode describing how the information collected from the headers are presented in the resultant bindings.</summary>
    [Argument(Format = "--output-mode {value}")] public ClangSharpPInvokeGeneratorOutputMode OutputMode => Get<ClangSharpPInvokeGeneratorOutputMode>(() => OutputMode);
    /// <summary>The output location to write the generated bindings to.</summary>
    [Argument(Format = "--output {value}")] public string Output => Get<string>(() => Output);
    /// <summary>The prefix to strip from the generated method bindings.</summary>
    [Argument(Format = "--prefixStrip {value}")] public IReadOnlyList<string> PrefixStrip => Get<List<string>>(() => PrefixStrip);
    /// <summary>A declaration name to be remapped to another name during binding generation.</summary>
    [Argument(Format = "--remap {value}")] public IReadOnlyList<string> Remap => Get<List<string>>(() => Remap);
    /// <summary>Language standard to compile for.</summary>
    [Argument(Format = "--std {value}")] public string Std => Get<string>(() => Std);
    /// <summary>The output location to write the generated tests to.</summary>
    [Argument(Format = "--test-output {value}")] public string TestOutput => Get<string>(() => TestOutput);
    /// <summary>A file name included either directly or indirectly by -f that should be traversed during binding generation.</summary>
    [Argument(Format = "--traverse {value}")] public IReadOnlyList<string> Traverse => Get<List<string>>(() => Traverse);
    /// <summary>An access specifier to be used with the given qualified or remapped declaration name during binding generation. Supports wildcards.</summary>
    [Argument(Format = "--with-access-specifier {value}")] public IReadOnlyList<string> WithAccessSpecifier => Get<List<string>>(() => WithAccessSpecifier);
    /// <summary>An attribute to be added to the given remapped declaration name during binding generation. Supports wildcards.</summary>
    [Argument(Format = "--with-attribute {value}")] public IReadOnlyList<string> WithAttribute => Get<List<string>>(() => WithAttribute);
    /// <summary>A calling convention to be used for the given declaration during binding generation. Supports wildcards.</summary>
    [Argument(Format = "--with-callconv {value}")] public IReadOnlyList<string> WithCallConv => Get<List<string>>(() => WithCallConv);
    /// <summary>A class to be used for the given remapped constant or function declaration name during binding generation. Supports wildcards.</summary>
    [Argument(Format = "--with-class {value}")] public IReadOnlyList<string> WithClass => Get<List<string>>(() => WithClass);
    /// <summary>A GUID to be used for the given declaration during binding generation. Supports wildcards.</summary>
    [Argument(Format = "--with-guid {value}")] public IReadOnlyList<string> WithGuid => Get<List<string>>(() => WithGuid);
    /// <summary>A library path to be used for the given declaration during binding generation. Supports wildcards.</summary>
    [Argument(Format = "--with-librarypath {value}")] public IReadOnlyList<string> WithLibraryPath => Get<List<string>>(() => WithLibraryPath);
    /// <summary>A remapped function name to be treated as a manual import during binding generation. Supports wildcards.</summary>
    [Argument(Format = "--with-manual-import {value}")] public IReadOnlyList<string> WithManualImport => Get<List<string>>(() => WithManualImport);
    /// <summary>A namespace to be used for the given remapped declaration name during binding generation. Supports wildcards.</summary>
    [Argument(Format = "--with-namespace {value}")] public IReadOnlyList<string> WithNamespace => Get<List<string>>(() => WithNamespace);
    /// <summary>Overrides the <c>StructLayoutAttribute.Pack</c> property for the given type. Supports wildcards.</summary>
    [Argument(Format = "--with-packing {value}")] public IReadOnlyList<string> WithPacking => Get<List<string>>(() => WithPacking);
    /// <summary>Add the <c>SetLastError=true</c> modifier to a given <c>DllImport</c> or <c>UnmanagedFunctionPointer</c>. Supports wildcards.</summary>
    [Argument(Format = "--with-setlasterror {value}")] public IReadOnlyList<string> WithSetLastError => Get<List<string>>(() => WithSetLastError);
    /// <summary>Add the <c>SuppressGCTransition</c> calling convention to a given <c>DllImport</c> or <c>UnmanagedFunctionPointer</c>. Supports wildcards.</summary>
    [Argument(Format = "--with-suppressgctransition {value}")] public IReadOnlyList<string> WithSuppressGCTransition => Get<List<string>>(() => WithSuppressGCTransition);
    /// <summary>A remapped type name to be treated as a transparent wrapper during binding generation. Supports wildcards.</summary>
    [Argument(Format = "--with-transparent-struct {value}")] public IReadOnlyList<string> WithTransparentStruct => Get<List<string>>(() => WithTransparentStruct);
    /// <summary>A type to be used for the given enum declaration during binding generation. Supports wildcards.</summary>
    [Argument(Format = "--with-type {value}")] public IReadOnlyList<string> WithType => Get<List<string>>(() => WithType);
    /// <summary>A using directive to be included for the given remapped declaration name during binding generation. Supports wildcards.</summary>
    [Argument(Format = "--with-using {value}")] public IReadOnlyList<string> WithUsing => Get<List<string>>(() => WithUsing);
}
#endregion
#region ClangSharpPInvokeGeneratorSettingsExtensions
/// <inheritdoc cref="ClangSharpPInvokeGeneratorTasks.ClangSharpPInvokeGenerator(Nuke.Common.Tools.ClangSharpPInvokeGenerator.ClangSharpPInvokeGeneratorSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
public static partial class ClangSharpPInvokeGeneratorSettingsExtensions
{
    #region Additional
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Additional"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Additional))]
    public static T SetAdditional<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.Additional, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Additional"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Additional))]
    public static T SetAdditional<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.Additional, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Additional"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Additional))]
    public static T AddAdditional<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.Additional, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Additional"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Additional))]
    public static T AddAdditional<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.Additional, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Additional"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Additional))]
    public static T RemoveAdditional<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.Additional, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Additional"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Additional))]
    public static T RemoveAdditional<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.Additional, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Additional"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Additional))]
    public static T ClearAdditional<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.Additional));
    #endregion
    #region Config
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Config"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Config))]
    public static T SetConfig<T>(this T o, params ClangSharpPInvokeGeneratorConfigOption[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.Config, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Config"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Config))]
    public static T SetConfig<T>(this T o, IEnumerable<ClangSharpPInvokeGeneratorConfigOption> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.Config, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Config"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Config))]
    public static T AddConfig<T>(this T o, params ClangSharpPInvokeGeneratorConfigOption[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.Config, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Config"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Config))]
    public static T AddConfig<T>(this T o, IEnumerable<ClangSharpPInvokeGeneratorConfigOption> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.Config, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Config"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Config))]
    public static T RemoveConfig<T>(this T o, params ClangSharpPInvokeGeneratorConfigOption[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.Config, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Config"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Config))]
    public static T RemoveConfig<T>(this T o, IEnumerable<ClangSharpPInvokeGeneratorConfigOption> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.Config, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Config"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Config))]
    public static T ClearConfig<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.Config));
    #endregion
    #region DefineMacro
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.DefineMacro"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.DefineMacro))]
    public static T SetDefineMacro<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.DefineMacro, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.DefineMacro"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.DefineMacro))]
    public static T SetDefineMacro<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.DefineMacro, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.DefineMacro"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.DefineMacro))]
    public static T AddDefineMacro<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.DefineMacro, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.DefineMacro"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.DefineMacro))]
    public static T AddDefineMacro<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.DefineMacro, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.DefineMacro"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.DefineMacro))]
    public static T RemoveDefineMacro<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.DefineMacro, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.DefineMacro"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.DefineMacro))]
    public static T RemoveDefineMacro<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.DefineMacro, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.DefineMacro"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.DefineMacro))]
    public static T ClearDefineMacro<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.DefineMacro));
    #endregion
    #region Exclude
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Exclude"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Exclude))]
    public static T SetExclude<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.Exclude, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Exclude"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Exclude))]
    public static T SetExclude<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.Exclude, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Exclude"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Exclude))]
    public static T AddExclude<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.Exclude, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Exclude"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Exclude))]
    public static T AddExclude<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.Exclude, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Exclude"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Exclude))]
    public static T RemoveExclude<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.Exclude, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Exclude"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Exclude))]
    public static T RemoveExclude<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.Exclude, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Exclude"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Exclude))]
    public static T ClearExclude<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.Exclude));
    #endregion
    #region File
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.File"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.File))]
    public static T SetFile<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.File, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.File"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.File))]
    public static T SetFile<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.File, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.File"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.File))]
    public static T AddFile<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.File, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.File"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.File))]
    public static T AddFile<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.File, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.File"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.File))]
    public static T RemoveFile<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.File, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.File"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.File))]
    public static T RemoveFile<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.File, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.File"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.File))]
    public static T ClearFile<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.File));
    #endregion
    #region FileDirectories
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.FileDirectories"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.FileDirectories))]
    public static T SetFileDirectories<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.FileDirectories, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.FileDirectories"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.FileDirectories))]
    public static T SetFileDirectories<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.FileDirectories, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.FileDirectories"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.FileDirectories))]
    public static T AddFileDirectories<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.FileDirectories, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.FileDirectories"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.FileDirectories))]
    public static T AddFileDirectories<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.FileDirectories, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.FileDirectories"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.FileDirectories))]
    public static T RemoveFileDirectories<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.FileDirectories, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.FileDirectories"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.FileDirectories))]
    public static T RemoveFileDirectories<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.FileDirectories, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.FileDirectories"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.FileDirectories))]
    public static T ClearFileDirectories<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.FileDirectories));
    #endregion
    #region HeaderFiles
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.HeaderFiles"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.HeaderFiles))]
    public static T SetHeaderFiles<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.HeaderFiles, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.HeaderFiles"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.HeaderFiles))]
    public static T SetHeaderFiles<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.HeaderFiles, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.HeaderFiles"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.HeaderFiles))]
    public static T AddHeaderFiles<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.HeaderFiles, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.HeaderFiles"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.HeaderFiles))]
    public static T AddHeaderFiles<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.HeaderFiles, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.HeaderFiles"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.HeaderFiles))]
    public static T RemoveHeaderFiles<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.HeaderFiles, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.HeaderFiles"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.HeaderFiles))]
    public static T RemoveHeaderFiles<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.HeaderFiles, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.HeaderFiles"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.HeaderFiles))]
    public static T ClearHeaderFiles<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.HeaderFiles));
    #endregion
    #region Include
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Include"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Include))]
    public static T SetInclude<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.Include, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Include"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Include))]
    public static T SetInclude<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.Include, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Include"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Include))]
    public static T AddInclude<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.Include, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Include"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Include))]
    public static T AddInclude<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.Include, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Include"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Include))]
    public static T RemoveInclude<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.Include, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Include"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Include))]
    public static T RemoveInclude<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.Include, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Include"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Include))]
    public static T ClearInclude<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.Include));
    #endregion
    #region IncludeDirectory
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.IncludeDirectory"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.IncludeDirectory))]
    public static T SetIncludeDirectory<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.IncludeDirectory, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.IncludeDirectory"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.IncludeDirectory))]
    public static T SetIncludeDirectory<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.IncludeDirectory, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.IncludeDirectory"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.IncludeDirectory))]
    public static T AddIncludeDirectory<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.IncludeDirectory, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.IncludeDirectory"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.IncludeDirectory))]
    public static T AddIncludeDirectory<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.IncludeDirectory, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.IncludeDirectory"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.IncludeDirectory))]
    public static T RemoveIncludeDirectory<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.IncludeDirectory, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.IncludeDirectory"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.IncludeDirectory))]
    public static T RemoveIncludeDirectory<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.IncludeDirectory, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.IncludeDirectory"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.IncludeDirectory))]
    public static T ClearIncludeDirectory<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.IncludeDirectory));
    #endregion
    #region Language
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Language"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Language))]
    public static T SetLanguage<T>(this T o, string v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.Language, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Language"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Language))]
    public static T ResetLanguage<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Remove(() => o.Language));
    #endregion
    #region LibraryPath
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.LibraryPath"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.LibraryPath))]
    public static T SetLibraryPath<T>(this T o, string v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.LibraryPath, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.LibraryPath"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.LibraryPath))]
    public static T ResetLibraryPath<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Remove(() => o.LibraryPath));
    #endregion
    #region MethodClassName
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.MethodClassName"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.MethodClassName))]
    public static T SetMethodClassName<T>(this T o, string v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.MethodClassName, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.MethodClassName"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.MethodClassName))]
    public static T ResetMethodClassName<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Remove(() => o.MethodClassName));
    #endregion
    #region Namespace
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Namespace"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Namespace))]
    public static T SetNamespace<T>(this T o, string v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.Namespace, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Namespace"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Namespace))]
    public static T ResetNamespace<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Remove(() => o.Namespace));
    #endregion
    #region NativeTypeNamesToStrip
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.NativeTypeNamesToStrip"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.NativeTypeNamesToStrip))]
    public static T SetNativeTypeNamesToStrip<T>(this T o, string v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.NativeTypeNamesToStrip, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.NativeTypeNamesToStrip"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.NativeTypeNamesToStrip))]
    public static T ResetNativeTypeNamesToStrip<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Remove(() => o.NativeTypeNamesToStrip));
    #endregion
    #region OutputMode
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.OutputMode"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.OutputMode))]
    public static T SetOutputMode<T>(this T o, ClangSharpPInvokeGeneratorOutputMode v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.OutputMode, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.OutputMode"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.OutputMode))]
    public static T ResetOutputMode<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Remove(() => o.OutputMode));
    #endregion
    #region Output
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Output"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Output))]
    public static T SetOutput<T>(this T o, string v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.Output, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Output"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Output))]
    public static T ResetOutput<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Remove(() => o.Output));
    #endregion
    #region PrefixStrip
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.PrefixStrip"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.PrefixStrip))]
    public static T SetPrefixStrip<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.PrefixStrip, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.PrefixStrip"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.PrefixStrip))]
    public static T SetPrefixStrip<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.PrefixStrip, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.PrefixStrip"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.PrefixStrip))]
    public static T AddPrefixStrip<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.PrefixStrip, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.PrefixStrip"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.PrefixStrip))]
    public static T AddPrefixStrip<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.PrefixStrip, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.PrefixStrip"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.PrefixStrip))]
    public static T RemovePrefixStrip<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.PrefixStrip, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.PrefixStrip"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.PrefixStrip))]
    public static T RemovePrefixStrip<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.PrefixStrip, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.PrefixStrip"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.PrefixStrip))]
    public static T ClearPrefixStrip<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.PrefixStrip));
    #endregion
    #region Remap
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Remap"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Remap))]
    public static T SetRemap<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.Remap, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Remap"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Remap))]
    public static T SetRemap<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.Remap, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Remap"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Remap))]
    public static T AddRemap<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.Remap, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Remap"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Remap))]
    public static T AddRemap<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.Remap, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Remap"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Remap))]
    public static T RemoveRemap<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.Remap, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Remap"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Remap))]
    public static T RemoveRemap<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.Remap, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Remap"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Remap))]
    public static T ClearRemap<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.Remap));
    #endregion
    #region Std
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Std"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Std))]
    public static T SetStd<T>(this T o, string v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.Std, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Std"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Std))]
    public static T ResetStd<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Remove(() => o.Std));
    #endregion
    #region TestOutput
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.TestOutput"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.TestOutput))]
    public static T SetTestOutput<T>(this T o, string v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.TestOutput, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.TestOutput"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.TestOutput))]
    public static T ResetTestOutput<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Remove(() => o.TestOutput));
    #endregion
    #region Traverse
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Traverse"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Traverse))]
    public static T SetTraverse<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.Traverse, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Traverse"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Traverse))]
    public static T SetTraverse<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.Traverse, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Traverse"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Traverse))]
    public static T AddTraverse<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.Traverse, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Traverse"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Traverse))]
    public static T AddTraverse<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.Traverse, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Traverse"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Traverse))]
    public static T RemoveTraverse<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.Traverse, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Traverse"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Traverse))]
    public static T RemoveTraverse<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.Traverse, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.Traverse"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.Traverse))]
    public static T ClearTraverse<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.Traverse));
    #endregion
    #region WithAccessSpecifier
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithAccessSpecifier"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithAccessSpecifier))]
    public static T SetWithAccessSpecifier<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithAccessSpecifier, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithAccessSpecifier"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithAccessSpecifier))]
    public static T SetWithAccessSpecifier<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithAccessSpecifier, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithAccessSpecifier"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithAccessSpecifier))]
    public static T AddWithAccessSpecifier<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithAccessSpecifier, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithAccessSpecifier"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithAccessSpecifier))]
    public static T AddWithAccessSpecifier<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithAccessSpecifier, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithAccessSpecifier"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithAccessSpecifier))]
    public static T RemoveWithAccessSpecifier<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithAccessSpecifier, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithAccessSpecifier"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithAccessSpecifier))]
    public static T RemoveWithAccessSpecifier<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithAccessSpecifier, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithAccessSpecifier"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithAccessSpecifier))]
    public static T ClearWithAccessSpecifier<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.WithAccessSpecifier));
    #endregion
    #region WithAttribute
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithAttribute"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithAttribute))]
    public static T SetWithAttribute<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithAttribute, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithAttribute"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithAttribute))]
    public static T SetWithAttribute<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithAttribute, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithAttribute"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithAttribute))]
    public static T AddWithAttribute<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithAttribute, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithAttribute"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithAttribute))]
    public static T AddWithAttribute<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithAttribute, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithAttribute"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithAttribute))]
    public static T RemoveWithAttribute<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithAttribute, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithAttribute"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithAttribute))]
    public static T RemoveWithAttribute<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithAttribute, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithAttribute"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithAttribute))]
    public static T ClearWithAttribute<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.WithAttribute));
    #endregion
    #region WithCallConv
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithCallConv"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithCallConv))]
    public static T SetWithCallConv<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithCallConv, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithCallConv"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithCallConv))]
    public static T SetWithCallConv<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithCallConv, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithCallConv"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithCallConv))]
    public static T AddWithCallConv<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithCallConv, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithCallConv"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithCallConv))]
    public static T AddWithCallConv<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithCallConv, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithCallConv"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithCallConv))]
    public static T RemoveWithCallConv<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithCallConv, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithCallConv"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithCallConv))]
    public static T RemoveWithCallConv<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithCallConv, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithCallConv"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithCallConv))]
    public static T ClearWithCallConv<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.WithCallConv));
    #endregion
    #region WithClass
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithClass"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithClass))]
    public static T SetWithClass<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithClass, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithClass"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithClass))]
    public static T SetWithClass<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithClass, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithClass"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithClass))]
    public static T AddWithClass<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithClass, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithClass"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithClass))]
    public static T AddWithClass<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithClass, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithClass"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithClass))]
    public static T RemoveWithClass<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithClass, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithClass"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithClass))]
    public static T RemoveWithClass<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithClass, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithClass"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithClass))]
    public static T ClearWithClass<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.WithClass));
    #endregion
    #region WithGuid
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithGuid"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithGuid))]
    public static T SetWithGuid<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithGuid, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithGuid"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithGuid))]
    public static T SetWithGuid<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithGuid, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithGuid"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithGuid))]
    public static T AddWithGuid<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithGuid, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithGuid"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithGuid))]
    public static T AddWithGuid<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithGuid, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithGuid"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithGuid))]
    public static T RemoveWithGuid<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithGuid, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithGuid"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithGuid))]
    public static T RemoveWithGuid<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithGuid, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithGuid"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithGuid))]
    public static T ClearWithGuid<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.WithGuid));
    #endregion
    #region WithLibraryPath
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithLibraryPath"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithLibraryPath))]
    public static T SetWithLibraryPath<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithLibraryPath, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithLibraryPath"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithLibraryPath))]
    public static T SetWithLibraryPath<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithLibraryPath, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithLibraryPath"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithLibraryPath))]
    public static T AddWithLibraryPath<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithLibraryPath, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithLibraryPath"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithLibraryPath))]
    public static T AddWithLibraryPath<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithLibraryPath, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithLibraryPath"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithLibraryPath))]
    public static T RemoveWithLibraryPath<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithLibraryPath, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithLibraryPath"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithLibraryPath))]
    public static T RemoveWithLibraryPath<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithLibraryPath, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithLibraryPath"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithLibraryPath))]
    public static T ClearWithLibraryPath<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.WithLibraryPath));
    #endregion
    #region WithManualImport
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithManualImport"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithManualImport))]
    public static T SetWithManualImport<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithManualImport, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithManualImport"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithManualImport))]
    public static T SetWithManualImport<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithManualImport, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithManualImport"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithManualImport))]
    public static T AddWithManualImport<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithManualImport, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithManualImport"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithManualImport))]
    public static T AddWithManualImport<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithManualImport, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithManualImport"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithManualImport))]
    public static T RemoveWithManualImport<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithManualImport, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithManualImport"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithManualImport))]
    public static T RemoveWithManualImport<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithManualImport, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithManualImport"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithManualImport))]
    public static T ClearWithManualImport<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.WithManualImport));
    #endregion
    #region WithNamespace
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithNamespace"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithNamespace))]
    public static T SetWithNamespace<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithNamespace, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithNamespace"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithNamespace))]
    public static T SetWithNamespace<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithNamespace, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithNamespace"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithNamespace))]
    public static T AddWithNamespace<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithNamespace, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithNamespace"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithNamespace))]
    public static T AddWithNamespace<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithNamespace, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithNamespace"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithNamespace))]
    public static T RemoveWithNamespace<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithNamespace, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithNamespace"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithNamespace))]
    public static T RemoveWithNamespace<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithNamespace, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithNamespace"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithNamespace))]
    public static T ClearWithNamespace<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.WithNamespace));
    #endregion
    #region WithPacking
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithPacking"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithPacking))]
    public static T SetWithPacking<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithPacking, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithPacking"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithPacking))]
    public static T SetWithPacking<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithPacking, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithPacking"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithPacking))]
    public static T AddWithPacking<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithPacking, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithPacking"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithPacking))]
    public static T AddWithPacking<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithPacking, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithPacking"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithPacking))]
    public static T RemoveWithPacking<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithPacking, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithPacking"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithPacking))]
    public static T RemoveWithPacking<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithPacking, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithPacking"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithPacking))]
    public static T ClearWithPacking<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.WithPacking));
    #endregion
    #region WithSetLastError
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithSetLastError"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithSetLastError))]
    public static T SetWithSetLastError<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithSetLastError, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithSetLastError"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithSetLastError))]
    public static T SetWithSetLastError<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithSetLastError, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithSetLastError"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithSetLastError))]
    public static T AddWithSetLastError<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithSetLastError, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithSetLastError"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithSetLastError))]
    public static T AddWithSetLastError<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithSetLastError, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithSetLastError"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithSetLastError))]
    public static T RemoveWithSetLastError<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithSetLastError, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithSetLastError"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithSetLastError))]
    public static T RemoveWithSetLastError<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithSetLastError, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithSetLastError"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithSetLastError))]
    public static T ClearWithSetLastError<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.WithSetLastError));
    #endregion
    #region WithSuppressGCTransition
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithSuppressGCTransition"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithSuppressGCTransition))]
    public static T SetWithSuppressGCTransition<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithSuppressGCTransition, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithSuppressGCTransition"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithSuppressGCTransition))]
    public static T SetWithSuppressGCTransition<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithSuppressGCTransition, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithSuppressGCTransition"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithSuppressGCTransition))]
    public static T AddWithSuppressGCTransition<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithSuppressGCTransition, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithSuppressGCTransition"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithSuppressGCTransition))]
    public static T AddWithSuppressGCTransition<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithSuppressGCTransition, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithSuppressGCTransition"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithSuppressGCTransition))]
    public static T RemoveWithSuppressGCTransition<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithSuppressGCTransition, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithSuppressGCTransition"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithSuppressGCTransition))]
    public static T RemoveWithSuppressGCTransition<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithSuppressGCTransition, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithSuppressGCTransition"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithSuppressGCTransition))]
    public static T ClearWithSuppressGCTransition<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.WithSuppressGCTransition));
    #endregion
    #region WithTransparentStruct
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithTransparentStruct"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithTransparentStruct))]
    public static T SetWithTransparentStruct<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithTransparentStruct, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithTransparentStruct"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithTransparentStruct))]
    public static T SetWithTransparentStruct<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithTransparentStruct, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithTransparentStruct"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithTransparentStruct))]
    public static T AddWithTransparentStruct<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithTransparentStruct, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithTransparentStruct"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithTransparentStruct))]
    public static T AddWithTransparentStruct<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithTransparentStruct, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithTransparentStruct"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithTransparentStruct))]
    public static T RemoveWithTransparentStruct<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithTransparentStruct, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithTransparentStruct"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithTransparentStruct))]
    public static T RemoveWithTransparentStruct<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithTransparentStruct, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithTransparentStruct"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithTransparentStruct))]
    public static T ClearWithTransparentStruct<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.WithTransparentStruct));
    #endregion
    #region WithType
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithType"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithType))]
    public static T SetWithType<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithType, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithType"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithType))]
    public static T SetWithType<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithType, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithType"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithType))]
    public static T AddWithType<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithType, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithType"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithType))]
    public static T AddWithType<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithType, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithType"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithType))]
    public static T RemoveWithType<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithType, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithType"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithType))]
    public static T RemoveWithType<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithType, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithType"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithType))]
    public static T ClearWithType<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.WithType));
    #endregion
    #region WithUsing
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithUsing"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithUsing))]
    public static T SetWithUsing<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithUsing, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithUsing"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithUsing))]
    public static T SetWithUsing<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.Set(() => o.WithUsing, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithUsing"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithUsing))]
    public static T AddWithUsing<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithUsing, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithUsing"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithUsing))]
    public static T AddWithUsing<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.AddCollection(() => o.WithUsing, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithUsing"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithUsing))]
    public static T RemoveWithUsing<T>(this T o, params string[] v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithUsing, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithUsing"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithUsing))]
    public static T RemoveWithUsing<T>(this T o, IEnumerable<string> v) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.RemoveCollection(() => o.WithUsing, v));
    /// <inheritdoc cref="ClangSharpPInvokeGeneratorSettings.WithUsing"/>
    [Pure] [Builder(Type = typeof(ClangSharpPInvokeGeneratorSettings), Property = nameof(ClangSharpPInvokeGeneratorSettings.WithUsing))]
    public static T ClearWithUsing<T>(this T o) where T : ClangSharpPInvokeGeneratorSettings => o.Modify(b => b.ClearCollection(() => o.WithUsing));
    #endregion
}
#endregion
#region ClangSharpPInvokeGeneratorConfigOption
/// <summary>Used within <see cref="ClangSharpPInvokeGeneratorTasks"/>.</summary>
[PublicAPI]
[Serializable]
[ExcludeFromCodeCoverage]
[TypeConverter(typeof(TypeConverter<ClangSharpPInvokeGeneratorConfigOption>))]
public partial class ClangSharpPInvokeGeneratorConfigOption : Enumeration
{
    public static ClangSharpPInvokeGeneratorConfigOption compatible_codegen = (ClangSharpPInvokeGeneratorConfigOption) "compatible-codegen";
    public static ClangSharpPInvokeGeneratorConfigOption default_codegen = (ClangSharpPInvokeGeneratorConfigOption) "default-codegen";
    public static ClangSharpPInvokeGeneratorConfigOption latest_codegen = (ClangSharpPInvokeGeneratorConfigOption) "latest-codegen";
    public static ClangSharpPInvokeGeneratorConfigOption preview_codegen = (ClangSharpPInvokeGeneratorConfigOption) "preview-codegen";
    public static ClangSharpPInvokeGeneratorConfigOption single_file = (ClangSharpPInvokeGeneratorConfigOption) "single-file";
    public static ClangSharpPInvokeGeneratorConfigOption multi_file = (ClangSharpPInvokeGeneratorConfigOption) "multi-file";
    public static ClangSharpPInvokeGeneratorConfigOption unix_types = (ClangSharpPInvokeGeneratorConfigOption) "unix-types";
    public static ClangSharpPInvokeGeneratorConfigOption windows_types = (ClangSharpPInvokeGeneratorConfigOption) "windows-types";
    public static ClangSharpPInvokeGeneratorConfigOption exclude_anonymous_field_helpers = (ClangSharpPInvokeGeneratorConfigOption) "exclude-anonymous-field-helpers";
    public static ClangSharpPInvokeGeneratorConfigOption exclude_com_proxies = (ClangSharpPInvokeGeneratorConfigOption) "exclude-com-proxies";
    public static ClangSharpPInvokeGeneratorConfigOption exclude_default_remappings = (ClangSharpPInvokeGeneratorConfigOption) "exclude-default-remappings";
    public static ClangSharpPInvokeGeneratorConfigOption exclude_empty_records = (ClangSharpPInvokeGeneratorConfigOption) "exclude-empty-records";
    public static ClangSharpPInvokeGeneratorConfigOption exclude_enum_operators = (ClangSharpPInvokeGeneratorConfigOption) "exclude-enum-operators";
    public static ClangSharpPInvokeGeneratorConfigOption exclude_fnptr_codegen = (ClangSharpPInvokeGeneratorConfigOption) "exclude-fnptr-codegen";
    public static ClangSharpPInvokeGeneratorConfigOption exclude_funcs_with_body = (ClangSharpPInvokeGeneratorConfigOption) "exclude-funcs-with-body";
    public static ClangSharpPInvokeGeneratorConfigOption exclude_using_statics_for_enums = (ClangSharpPInvokeGeneratorConfigOption) "exclude-using-statics-for-enums";
    public static ClangSharpPInvokeGeneratorConfigOption explicit_vtbls = (ClangSharpPInvokeGeneratorConfigOption) "explicit-vtbls";
    public static ClangSharpPInvokeGeneratorConfigOption implicit_vtbls = (ClangSharpPInvokeGeneratorConfigOption) "implicit-vtbls";
    public static ClangSharpPInvokeGeneratorConfigOption trimmable_vtbls = (ClangSharpPInvokeGeneratorConfigOption) "trimmable-vtbls";
    public static ClangSharpPInvokeGeneratorConfigOption generate_tests_nunit = (ClangSharpPInvokeGeneratorConfigOption) "generate-tests-nunit";
    public static ClangSharpPInvokeGeneratorConfigOption generate_tests_xunit = (ClangSharpPInvokeGeneratorConfigOption) "generate-tests-xunit";
    public static ClangSharpPInvokeGeneratorConfigOption generate_aggressive_inlining = (ClangSharpPInvokeGeneratorConfigOption) "generate-aggressive-inlining";
    public static ClangSharpPInvokeGeneratorConfigOption generate_callconv_member_function = (ClangSharpPInvokeGeneratorConfigOption) "generate-callconv-member-function";
    public static ClangSharpPInvokeGeneratorConfigOption generate_cpp_attributes = (ClangSharpPInvokeGeneratorConfigOption) "generate-cpp-attributes";
    public static ClangSharpPInvokeGeneratorConfigOption generate_disable_runtime_marshalling = (ClangSharpPInvokeGeneratorConfigOption) "generate-disable-runtime-marshalling";
    public static ClangSharpPInvokeGeneratorConfigOption generate_doc_includes = (ClangSharpPInvokeGeneratorConfigOption) "generate-doc-includes";
    public static ClangSharpPInvokeGeneratorConfigOption generate_file_scoped_namespaces = (ClangSharpPInvokeGeneratorConfigOption) "generate-file-scoped-namespaces";
    public static ClangSharpPInvokeGeneratorConfigOption generate_guid_member = (ClangSharpPInvokeGeneratorConfigOption) "generate-guid-member";
    public static ClangSharpPInvokeGeneratorConfigOption generate_helper_types = (ClangSharpPInvokeGeneratorConfigOption) "generate-helper-types";
    public static ClangSharpPInvokeGeneratorConfigOption generate_macro_bindings = (ClangSharpPInvokeGeneratorConfigOption) "generate-macro-bindings";
    public static ClangSharpPInvokeGeneratorConfigOption generate_marker_interfaces = (ClangSharpPInvokeGeneratorConfigOption) "generate-marker-interfaces";
    public static ClangSharpPInvokeGeneratorConfigOption generate_native_bitfield_attribute = (ClangSharpPInvokeGeneratorConfigOption) "generate-native-bitfield-attribute";
    public static ClangSharpPInvokeGeneratorConfigOption generate_native_inheritance_attribute = (ClangSharpPInvokeGeneratorConfigOption) "generate-native-inheritance-attribute";
    public static ClangSharpPInvokeGeneratorConfigOption generate_setslastsystemerror_attribute = (ClangSharpPInvokeGeneratorConfigOption) "generate-setslastsystemerror-attribute";
    public static ClangSharpPInvokeGeneratorConfigOption generate_template_bindings = (ClangSharpPInvokeGeneratorConfigOption) "generate-template-bindings";
    public static ClangSharpPInvokeGeneratorConfigOption generate_unmanaged_constants = (ClangSharpPInvokeGeneratorConfigOption) "generate-unmanaged-constants";
    public static ClangSharpPInvokeGeneratorConfigOption generate_vtbl_index_attribute = (ClangSharpPInvokeGeneratorConfigOption) "generate-vtbl-index-attribute";
    public static ClangSharpPInvokeGeneratorConfigOption log_exclusions = (ClangSharpPInvokeGeneratorConfigOption) "log-exclusions";
    public static ClangSharpPInvokeGeneratorConfigOption log_potential_typedef_remappings = (ClangSharpPInvokeGeneratorConfigOption) "log-potential-typedef-remappings";
    public static ClangSharpPInvokeGeneratorConfigOption log_visited_files = (ClangSharpPInvokeGeneratorConfigOption) "log-visited-files";
    public static implicit operator ClangSharpPInvokeGeneratorConfigOption(string value)
    {
        return new ClangSharpPInvokeGeneratorConfigOption { Value = value };
    }
}
#endregion
#region ClangSharpPInvokeGeneratorOutputMode
/// <summary>Used within <see cref="ClangSharpPInvokeGeneratorTasks"/>.</summary>
[PublicAPI]
[Serializable]
[ExcludeFromCodeCoverage]
[TypeConverter(typeof(TypeConverter<ClangSharpPInvokeGeneratorOutputMode>))]
public partial class ClangSharpPInvokeGeneratorOutputMode : Enumeration
{
    public static ClangSharpPInvokeGeneratorOutputMode CSharp = (ClangSharpPInvokeGeneratorOutputMode) "CSharp";
    public static ClangSharpPInvokeGeneratorOutputMode Xml = (ClangSharpPInvokeGeneratorOutputMode) "Xml";
    public static implicit operator ClangSharpPInvokeGeneratorOutputMode(string value)
    {
        return new ClangSharpPInvokeGeneratorOutputMode { Value = value };
    }
}
#endregion
