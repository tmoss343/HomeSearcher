FROM microsoft/aspnetcore:1.1
ARG source
WORKDIR /app
EXPOSE 80
COPY ../HouseService/obj/Debug/netcoreapp1.1/ .

ENV ASPNETCORE_URLS http://*:5000

ENTRYPOINT ["dotnet"] ["HouseService.dll"]