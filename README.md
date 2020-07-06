# Automation testing Framework
For more information about running mstest using CLI --> [Click here](https://docs.microsoft.com/en-us/dotnet/core/testing/selective-unit-tests?pivots=mstest)

*** Please note that this framework executes in parallel, the number of workers will be decided during runtime based on  how many cores are available. ***

##  Running the test framework
Executing the **WHOLE** framework
1. Navigate to the root directory of the framework using CLI
2. Type `dotnet test`

<br>

##  Running just the **Web** scripts
1. Navigate to the root directory of the framework using CLI
2. Type `dotnet test --filter testCategory=Web`

<br>

##  Running just the **Api** scripts
1. Navigate to the root directory of the framework using CLI
2. Type `dotnet test --filter testCategory=Api`

<br>

## ApiTask file:
This is the automation script for 'Task 1'.

<br>

## WebTask file:
This contains two automation scripts for 'Task 2'. The reason for having two automation scripts is to demonstrate using Test data that has been hard coded and test data that has been read in from a csv file.

<br> The test method called 'Web_Test_DynamiclyFetchingTestData' will execute as many times as there are lines in the csv file.

<br>

## Technology stack used for **Integration testing**
* Mstest
* RestSharp
* C#
* Dotnet Core 3.1
* Selenium 3.141.0
* Extent reports

## Authors
> Matthew Aliriza