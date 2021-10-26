# UCN 3rd semester project - StreetPatch
## Abstract
This repository contains the code for our 3rd semester project for our Computer Science degree at UCN. The premise of the project is to design a distributed system, a back-end for it in C# and 2 front-end clients.

StreetPatch is an application which lets its users report incidents on the streets, such as pollution, roadwork, crashes, etc.

## Project set-up
In order to set up the project locally, the following steps must be taken:
1. Clone the repository locally: `git clone https://github.com/TheRandomTroll/ucn-3rd-semester-project`
2. Navigate via Terminal/CMD/Powershell to the directory where you cloned the project.
3. Run the following command: `docker-compose up -d`
4. That should be everything set up. The images run on the following ports:
    - SQL Server (MSSQL): 1433
    - API (via HTTPS): 8080
    - Front-end client - Web: **TBD**

## License
The content of this site is licensed under the [MIT](https://choosealicense.com/licenses/mit/) license.
