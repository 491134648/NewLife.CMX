:: ���ű����ڴ�Դ�����������ļ�DLL

@echo off
cls
setlocal enabledelayedexpansion
title ���������ļ�DLL

:: ������Դ��ַ
:: Ϊ������ٶȣ����Բ��ñ��ص�ַ
set svn=https://svn.newlifex.com/svn/X/trunk
set name=DLL
if exist C:\X (
	:: �ȸ���һ��Դ
	svn info %svn%/%name%
	svn update C:\X\%name%

	set svn=C:\X
)
set url=%svn%/trunk

:: �����ļ�DLL
set url=%svn%/%name%
for /r %%f in (*.*) do svn export --force %url%/%%~nxf %%~nxf

pause