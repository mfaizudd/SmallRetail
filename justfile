db action:
	dotnet ef -p SmallRetail.WebApi database {{action}}

migrate +args:
	dotnet ef -p SmallRetail.WebApi migrations {{args}}

web +args:
	cd smallretail-solid && pnpm {{args}}

build-docker-web:
	cd smallretail-solid && ./scripts/docker-build.sh

run-docker-web:
	cd smallretail-solid && ./scripts/docker-run.sh