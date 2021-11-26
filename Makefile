.PHONY: clean

run:
	@dotnet run

clean:
	@dotnet clean

build:
	@dotnet publish -c Release -p:PublishSingleFile=true -r linux-x64 --self-contained true

# Install the VS code extension
install-ext:
	@rm -rf ~/.vscode/extensions/lune-vscode
	@cp -r tool/lune-vscode ~/.vscode/extensions/lune-vscode

	@echo Installed the extension, please restart VS code!