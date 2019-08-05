copy *.dll                      %windir%\system32 /y
copy *.ocx                      %windir%\system32 /y
copy ubox.ini                   %windir%\system32 /y
%windir%\system32\regsvr32      phonic_usb.ocx