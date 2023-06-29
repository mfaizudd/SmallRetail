set shell := ["pwsh.exe", "-c"]

db action:
	dotnet ef -p SmallRetail.WebApi database {{action}}

migrate +args:
	dotnet ef -p SmallRetail.WebApi migrations {{args}}

web +args:
	cd smallretail-solid && pnpm {{args}}

build-docker-web:
	cd smallretail-solid && docker build -t mfaizudd/smallretail \
		--build-arg VITE_AUTHORIZE_URL=$VITE_AUTHORIZE \
		--build-arg VITE_CLIENT_ID=$VITE_CLIENT \
		--build-arg VITE_REDIRECT_URI=$VITE_REDIRECT \
		--build-arg VITE_TOKEN_URI=$VITE_TOKEN \
		--build-arg VITE_API_URL=$VITE_API \
		.