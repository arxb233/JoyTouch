@echo off
setlocal
set "arg_count=0"
for %%i in (%*) do set /a arg_count+=1

echo �ű�����: %0
echo ��������: %arg_count%
echo ����ʱ���ӵĲ���:
if "%~1"=="" (
    echo �޲�������
    goto :eof
)

set "index=1"
:loop
if not "%~1"=="" (
    echo ����%index%: %1
    set /a index+=1
    shift
    goto :loop
)

endlocal
exit /b 0