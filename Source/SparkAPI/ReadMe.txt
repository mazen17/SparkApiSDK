BACKGROUND:

While the SparkAPI component references the 'Spark.Net.dll' .NET library to access the Spark
API, 'Spark.Net.dll' is actually an interop wrapper to a C library called 'spark.dll'. As the
C library is not a COM object, it cannot be referenced directly. 

When a call is made to 'Spark.Net.dll', it looks for the 'spark.dll' library in the executing
application folder. If it cannot find it, the application will not work. As the 'spark.dll'
file cannot be referenced like a .NET or COM component, it has to be added manually to the 
project. It is important to change the file's 'Copy To Output Directory' to be 'Copy If Newer'
to force the build to copy the file to the bin folder.

There are two versions of the 'Spark.Net.dll' and associated 'spark.dll' files: a 32-bit and a
64-bit version. The sample project uses the 64-bit version by default. If you are running on a
32-bit OS, you will need to follow the instructions below to reference the correct files. The
same instructions should be followed when upgrading to a later version of the API.

INSTRUCTIONS:

In the Spark API project:

1. Select 'References', then delete the 'Spark.Net' reference.
2. Right-click on 'References' and select 'Add Reference'.
3. Navigate to the Spark API binaries folder, selecting the correct OS version (32-bit or
   64-bit), and then select 'Spark.Net.dll'. 
   (The default location in the sample is \Assemblies\Spark)
4. Delete the 'spark.dll' file in the SparkAPI project.
5. Right-click on the 'SparkAPI' project and select 'Add->Add Existing Item...'
6. Navigate to the Spark API binaries folder, selecting the correct OS version (32-bit or 
   64-bit), and then select 'spark.dll'.
   (The default location in the sample is \Assemblies\Spark)   
7. Right-click on the file in the SparkAPI project, and select 'Properties'.
8. Set the property 'Copy To Output Directory' equal to 'Copy If Newer'.


