# Project Variables

PROJECT_NAME ?= SmallRetail

.PHONY: migrations db run clean

mname =
mcommand = add

migrations:
	cd ./SmallRetail.Data && dotnet ef --startup-project ../SmallRetail.Web migrations $(mcommand) $(mname) && cd ..

db:
	cd ./SmallRetail.Data && dotnet ef --startup-project ../SmallRetail.Web database update $(mname) && cd ..

run:
	cd ./SmallRetail.Web && dotnet build && dotnet run && cd ..

