# Project Variables

PROJECT_NAME ?= SmallRetail

.PHONY: migrations db run clean

mname =
mcommand = add

migrations:
	cd ./SmallRetail.Data && dotnet ef --startup-project ../SmallRetail.Web migrations $(mcommand) $(mname) && cd ..

db:
	dotnet ef database update $(mname) -s SmallRetail.Web -p SmallRetail.Data

run:
	cd ./SmallRetail.Web && dotnet build && dotnet run && cd ..

