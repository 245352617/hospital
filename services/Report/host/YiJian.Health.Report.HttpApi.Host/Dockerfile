#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

ENV TZ Asia/Shanghai
#RUN echo "deb https://mirrors.tuna.tsinghua.edu.cn/debian/ sid main contrib non-free" > /etc/apt/sources.list
RUN echo "deb http://mirrors.aliyun.com/debian stable main bullseye non-free" > /etc/apt/sources.list
RUN echo "deb http://mirrors.aliyun.com/debian stable-proposed-updates main bullseye non-free" >> /etc/apt/sources.list
RUN echo "deb http://mirrors.aliyun.com/debian stable-updates main bullseye non-free" >> /etc/apt/sources.list
RUN echo "deb-src http://mirrors.aliyun.com/debian stable main bullseye non-free" >> /etc/apt/sources.list
RUN echo "deb-src http://mirrors.aliyun.com/debian stable-proposed-updates main bullseye non-free" >> /etc/apt/sources.list
RUN echo "deb-src http://mirrors.aliyun.com/debian stable-updates main bullseye non-free" >> /etc/apt/sources.list

#RUN apt-get update 
#RUN apt-get install libgdiplus -y && ln -s libgdiplus.so gdiplus.dll
ARG DEBIAN_FRONTEND=noninteractive
RUN apt-get update && apt-get install -y libgdiplus libc6-dev && ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll && apt-get install --assume-yes apt-utils && apt-get install -y locales && localedef -c -f UTF-8 -i zh_CN zh_CN.utf8
ENV LANG zh_CN.utf8 
RUN apt-get install fontconfig -y
COPY ./Fonts/* /usr/share/fonts/

FROM base AS report_base

WORKDIR /app
COPY . .

EXPOSE 80
EXPOSE 443

ENV TZ Asia/Shanghai
ENTRYPOINT ["dotnet", "YiJian.Health.Report.HttpApi.Host.dll"]
