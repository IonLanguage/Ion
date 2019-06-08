all: Ion
	dotnet build -c Release

clean:
	rm -r Ion/bin Ion/obj

install:
	# TODO: Make this path not constant.
	mkdir /opt/Ion
	cp -r Ion/bin/Release /opt/Ion

uninstall:
	rm -r /opt/Ion
