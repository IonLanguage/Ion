all: Ion
	dotnet build -c Release
setup:
	git submodule init
	git submodule update
	cd Ion.Engine && make
	cd Ion.IR && make
	dotnet build
clean:
	rm -r Ion/bin Ion/obj
install:
	# TODO: Path is hard-coded.
	mkdir /opt/ion
	cp -r Ion/bin/Release /opt/ion
uninstall:
	rm -r /opt/ion
