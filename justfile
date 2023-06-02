set shell := ["pwsh.exe", "-c"]

db action:
	dotnet ef -p SmallRetail.WebApi database {{action}}

migrate +args:
	dotnet ef -p SmallRetail.WebApi migrations {{args}}
