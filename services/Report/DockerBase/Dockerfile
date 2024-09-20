#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

ENV TZ Asia/Shanghai
#RUN apt-get update 
RUN apt-get update && apt-get install -y libgdiplus libc6-dev && ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll
#RUN apt-get install libgdiplus -y && ln -s libgdiplus.so gdiplus.dll
RUN apt-get install -y locales
RUN localedef -c -f UTF-8 -i zh_CN zh_CN.utf8
ENV LANG zh_CN.utf8 
RUN apt-get install fontconfig -y
COPY ./Fonts/* /usr/share/fonts/
