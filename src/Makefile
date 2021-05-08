# Project Variables

PROJECT_NAME ?= SmallRetail

.PHONY: migrations db run

migrations:
	cd ./SmallRetail.Data && dotnet ef --startup-project ../SmallRetail.Web migrations add $(mname) && cd ..

db:
	cd ./SmallRetail.Data && dotnet ef --startup-project ../SmallRetail.Web database update && cd ..

run:
	cd ./SmallRetail.Web && dotnet build && dotnet run && cd ..
