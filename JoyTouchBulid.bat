@echo off
setlocal EnableDelayedExpansion

:: ����·��
set "AHK_COMPILER=F:\Program Files\AutoHotKey\Compiler\Ahk2Exe.exe"
set "AHK_BASE=F:\Program Files\AutoHotKey\v2\AutoHotkey64.exe"
set "SCRIPT_IN=F:\Github\github\JoyTouch\JoyTouch.ahk"
set "SCRIPT_OUT=F:\Github\github\JoyTouch\build\JoyTouch.exe"
set "ICON_FILE=F:\Github\github\JoyTouch\Tool\JoyTouch.ico"

:: ���������Ƿ����
if not exist "%AHK_COMPILER%" (
    echo �����Ҳ��� Ahk2Exe ��������%AHK_COMPILER%
    pause
    exit /b 1
)

:: ��� AutoHotkey �����ļ��Ƿ����
if not exist "%AHK_BASE%" (
    echo �����Ҳ��� AutoHotkey ��������%AHK_BASE%
    pause
    exit /b 1
)

:: �������ű��Ƿ����
if not exist "%SCRIPT_IN%" (
    echo �����Ҳ�������ű���%SCRIPT_IN%
    pause
    exit /b 1
)

:: ���ͼ���ļ��Ƿ����
if not exist "%ICON_FILE%" (
    echo �����Ҳ���ͼ���ļ���%ICON_FILE%
    pause
    exit /b 1
)

:: ������Ŀ¼�Ƿ���ڣ�����������򴴽�
set "OUT_DIR=%SCRIPT_OUT%\.."
if not exist "%OUT_DIR%" (
    mkdir "%OUT_DIR%"
    if errorlevel 1 (
        echo �����޷��������Ŀ¼��%OUT_DIR%
        pause
        exit /b 1
    )
)

:: ִ�б���
echo ���ڱ��� %SCRIPT_IN% ...
"%AHK_COMPILER%" ^
    /in "%SCRIPT_IN%" ^
    /out "%SCRIPT_OUT%" ^
    /base "%AHK_BASE%" ^
    /icon "%ICON_FILE%"

:: �������Ƿ�ɹ�
if errorlevel 1 (
    echo ���󣺱���ʧ�ܣ�
    pause
    exit /b 1
) else (
    echo ����ɹ�������ļ���%SCRIPT_OUT%
    pause
    exit /b 0
)
