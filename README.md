# _Band Tracker_

#### _A database which lists bands and venues, 7/22/2016_

#### By _**Alan Denison**_

## Description
This program will allow a user to store a list of bands and venues, and allow the user to see which bands have played at those venues, and vise versa.

## Setup/Installation Requirements
Instructions for Windows PC.
Get windows here: (http://www.mono-project.com/docs/getting-started/install/windows/)

1. From Powershell, clone this repository using the command 'git clone https://github.com/alandenison/BandTracker.git'
2. Navigate to project directory
3. Run the command 'dnu restore' to get dependencies
4. Use the command 'dnx kestrel' to run the kestrel server
5. Open a browser and go to: localhost:5004

DATABASE INSTRUCTIONS  
from sqlcmd, enter the following lines:
CREATE DATABASE band_tracker;  
GO  
USE DATABASE band_tracker;  
GO  
CREATE TABLE bands(id INT IDENTITY (1,1), name VARCHAR(255));  
GO  
CREATE TABLE venues(id INT IDENTITY (1,1), name VARCHAR(255));  
GO  
CREATE DATABASE band_tracker_test;  
GO  
USE DATABASE band_tracker_test;  
GO  
CREATE TABLE bands(id INT IDENTITY (1,1), name VARCHAR(255));  
GO  
CREATE TABLE venues(id INT IDENTITY (1,1), name VARCHAR(255));  
GO  

## Technologies Used

C#, Razor, Nancy, Xunit, SQLCMD
### License
MIT/open source license


Copyright (c) 2016 **_Alan Denison_**
