@echo off

for /f "usebackq tokens=*" %%i in (`vswhere.cmd -latest -products * -requires Microsoft.VisualStudio.Component.VC.Tools.x86.x64 -property installationPath`) do (
  set InstallDir=%%i
)

if exist "%InstallDir%\Common7\Tools\vsdevcmd.bat" (
  "%InstallDir%\Common7\Tools\vsdevcmd.bat" %*
)
