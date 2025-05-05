@echo off
setlocal EnableDelayedExpansion

:: 定义路径
set "AHK_COMPILER=F:\Program Files\AutoHotKey\Compiler\Ahk2Exe.exe"
set "AHK_BASE=F:\Program Files\AutoHotKey\v2\AutoHotkey64.exe"
set "SCRIPT_IN=F:\Github\github\JoyTouch\JoyTouch.ahk"
set "SCRIPT_OUT=F:\Github\github\JoyTouch\build\JoyTouch.exe"
set "ICON_FILE=F:\Github\github\JoyTouch\Tool\JoyTouch.ico"

:: 检查编译器是否存在
if not exist "%AHK_COMPILER%" (
    echo 错误：找不到 Ahk2Exe 编译器：%AHK_COMPILER%
    pause
    exit /b 1
)

:: 检查 AutoHotkey 基础文件是否存在
if not exist "%AHK_BASE%" (
    echo 错误：找不到 AutoHotkey 解释器：%AHK_BASE%
    pause
    exit /b 1
)

:: 检查输入脚本是否存在
if not exist "%SCRIPT_IN%" (
    echo 错误：找不到输入脚本：%SCRIPT_IN%
    pause
    exit /b 1
)

:: 检查图标文件是否存在
if not exist "%ICON_FILE%" (
    echo 错误：找不到图标文件：%ICON_FILE%
    pause
    exit /b 1
)

:: 检查输出目录是否存在，如果不存在则创建
set "OUT_DIR=%SCRIPT_OUT%\.."
if not exist "%OUT_DIR%" (
    mkdir "%OUT_DIR%"
    if errorlevel 1 (
        echo 错误：无法创建输出目录：%OUT_DIR%
        pause
        exit /b 1
    )
)

:: 执行编译
echo 正在编译 %SCRIPT_IN% ...
"%AHK_COMPILER%" ^
    /in "%SCRIPT_IN%" ^
    /out "%SCRIPT_OUT%" ^
    /base "%AHK_BASE%" ^
    /icon "%ICON_FILE%"

:: 检查编译是否成功
if errorlevel 1 (
    echo 错误：编译失败！
    pause
    exit /b 1
) else (
    echo 编译成功！输出文件：%SCRIPT_OUT%
    pause
    exit /b 0
)
