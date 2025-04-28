@echo off
setlocal
set "arg_count=0"
for %%i in (%*) do set /a arg_count+=1

echo 脚本名称: %0
echo 参数总数: %arg_count%
echo 调用时附加的参数:
if "%~1"=="" (
    echo 无参数传递
    goto :eof
)

set "index=1"
:loop
if not "%~1"=="" (
    echo 参数%index%: %1
    set /a index+=1
    shift
    goto :loop
)

endlocal
exit /b 0