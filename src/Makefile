# Project Variables

PROJECT_NAME ?= SmallRetail

.PHONY: migrations db run clean reset

mname =
mcommand = add

migrations:
	cd ./SmallRetail.Data && dotnet ef --startup-project ../SmallRetail.Web migrations $(mcommand) $(mname) && cd ..

db:
	cd ./SmallRetail.Data && dotnet ef --startup-project ../SmallRetail.Web database update $(mname) && cd ..

run:
	cd ./SmallRetail.Web && dotnet build && dotnet run && cd ..

reset:
	$(MAKE) db mname=0
	$(MAKE) migrations mcommand=remove
	$(MAKE) migrations mname=Initial
	$(MAKE) db
