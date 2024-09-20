FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build

ADD localtime /etc/localtime
RUN echo "Asia/Shanghai" > /etc/timezone

WORKDIR /app

COPY Publish/. .


RUN apt-get update 
RUN apt-get install libgdiplus -y && ln -s libgdiplus.so gdiplus.dll
RUN apt-get install -y locales
RUN localedef -c -f UTF-8 -i zh_CN zh_CN.utf8
ENV LANG zh_CN.utf8 
RUN apt-get install fontconfig -y
RUN cp ./Fonts/* /usr/share/fonts

EXPOSE 80
EXPOSE 443
EXPOSE 31999

ENV TZ Asia/Shanghai
ENTRYPOINT ["dotnet", "/app/YiJian.BodyParts.WebAPI.dll"]





