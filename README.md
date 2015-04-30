Azure Blob Storage for Umbraco
==============================
This code was inspired by Johannes Müller.

This is intended to replaces the Umbraco.Core.IO.PhysicalFileSystem for media files. 
All files will be stored in the configured Azure storage as blobs.

Some Hints for Testing
-----------------------
To run the tests in Visual Studio 2012 you need and extension like NuGet package "NUnit Test Adapter" in your Visual Studio toolbox.
And of course you have to make sure the Microsoft Azure SDK is installed and the MS Azure Storage Emulator is running. 
Sometimes I found it useful to delete the local database "WAStorageEmulatorDb34" for the emulator. 
After deleting the database via Sql Server Manager you can create a new one with three command lines. 
First move to the emulator install folder (on my machine "C:\Program Files (x86)\Microsoft SDKs\Azure\Storage Emulator").
My local sql server instance is called "SQLEXPRESS2008".
In your command line tool enter:

cd C:\Program Files (x86)\Microsoft SDKs\Azure\Storage Emulator

WAStorageEmulator stop

WAStorageEmulator init -sqlinstance SQLEXPRESS2008

WAStorageEmulator start

Update note: **With Micosoft Azure SDK for .Net (VS 2013) - 2.6** the emulator got a new name:

*WAStorageEmulator -> AzureStorageEmulator*

It also uses a new database. So be carefull when updating :)


Book recommendation
-------------------
The book "Programming Windows Azure [Kindle Edition]" form "Sriram Krishnan" gives you a good introduction into Azure Storage.
You find as Kindle book here:

http://www.amazon.de/Programming-Windows-Azure-Sriram-Krishnan-ebook/dp/B0043M58U8/ref=cm_sw_em_r_dpcod_pXMUub0J6GCXP_tt
