db action:
	dotnet ef -p SmallRetail.WebApi database {{action}}

migrate +args:
	dotnet ef -p SmallRetail.WebApi migrations {{args}}

run:
	dotnet run --project SmallRetail.WebApi

format:
    dotnet format

build-docker:
    docker build -t mfaizudd/smallretail-api -f SmallRetail.WebApi/Dockerfile .

run-docker: build-docker
    docker run -it \
        --rm \
        -p 8000:80 \
        mfaizudd/smallretail-api

web +args:
	. smallretail-solid/.envrc
	cd smallretail-solid && pnpm {{args}}

build-docker-web:
	cd smallretail-solid && ./scripts/docker-build.sh

run-docker-web:
	cd smallretail-solid && ./scripts/docker-run.sh