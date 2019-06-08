all: Ion
	dotnet build -c Release

clean:
	rm -r Ion/bin Ion/obj

install:
	# TODO: Path is hard-coded.
	mkdir /opt/ion
	cp -r Ion/bin/Release /opt/ion

uninstall:
	rm -r /opt/ion
