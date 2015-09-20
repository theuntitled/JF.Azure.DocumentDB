<?xml version="1.0" encoding="utf-8"?>
<package xmlns="http://schemas.microsoft.com/packaging/2012/06/nuspec.xsd">
	<metadata>
		<id>JF.Azure.DocumentDB</id>
		<version>1.0.0</version>
		<title>JF.Azure.DocumentDB</title>
		<authors>Josef Fazekas</authors>
		<owners>Josef Fazekas</owners>
		<licenseUrl>https://github.com/theuntitled/JF.Azure.DocumentDB/blob/master/LICENSE</licenseUrl>
		<projectUrl>https://github.com/theuntitled/JF.Azure.DocumentDB</projectUrl>
		<iconUrl>https://raw.githubusercontent.com/theuntitled/JF.Azure.DocumentDB/master/JF.Azure.DocumentDB/theuntitled.ico</iconUrl>
		<requireLicenseAcceptance>false</requireLicenseAcceptance>
		<description>Microsoft Azure DocumentDB helper</description>
		<copyright>Copyright © Josef Fazekas 2015</copyright>
		<language>en-GB</language>
		<tags>Azure, DocumentDB</tags>
		<dependencies>
			<group targetFramework=".NETFramework4.5.1">
				<dependency id="Microsoft.Azure.DocumentDB" version="1.4.1" />
				<dependency id="Newtonsoft.Json" version="7.0.1" />
				<dependency id="JF.Build.Tasks" version="1.0.0" />
			</group>
		</dependencies>
		<frameworkAssemblies>
			<frameworkAssembly assemblyName="System" targetFramework=".NETFramework4.5.2" />
			<frameworkAssembly assemblyName="System.Core" targetFramework=".NETFramework4.5.2" />
		</frameworkAssemblies>
	</metadata>
	<files>
		<file src="JF.Azure.DocumentDB\bin\Release\JF.Azure.DocumentDB.XML" target="lib\JF.Azure.DocumentDB.XML" />
		<file src="JF.Azure.DocumentDB\bin\Release\JF.Azure.DocumentDB.pdb" target="lib\JF.Azure.DocumentDB.pdb" />
		<file src="JF.Azure.DocumentDB\bin\Release\JF.Azure.DocumentDB.dll" target="lib\JF.Azure.DocumentDB.dll" />
	</files>
</package>