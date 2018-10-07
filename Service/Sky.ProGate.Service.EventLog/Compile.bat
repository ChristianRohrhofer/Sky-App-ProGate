"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.1A\Bin\x64\MC.exe" EventLog.mc
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.1A\Bin\x64\RC.exe" /r EventLog.rc
"C:\Program Files (x86)\Microsoft Visual Studio 14.0\VC\bin\LINK.exe" -machine:x86 -dll -noentry -out:EventLog.dll EventLog.res
del *.h
del *.rc
del *.res
del *.bin

