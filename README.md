# task-killer-client-server
Implementation of software that provides functionality for managing processes on a remote computer
The software package Task Killer is a tool for remote PC administration. Its main purpose is to establish control over processes running on a remote PC with the ability to obtain complete information about them, closing or restarting certain processes.
The program includes three modules: two modules that directly interact with the user - the client and the service, as well as a class library that plays the role of an auxiliary module that interacts with the client and the service.
The service module is installed on a managed computer (one or more) and must be run as administrator. It has no interface and functions without disrupting the user. The functionality of the service module is represented by the following set of actions:
• collection of information about processes running on a PC and their systematization for subsequent processing;
• constant waiting for a request from the administrator during the working session;
• sending information about the processes running on the PC at the request of the administrator;
• a request for the consent of the PC user with the intention of the administrator to complete or restart a particular process;
• completion or restart of the process on the PC (if user consent was obtained for the corresponding action).

The client module is installed on the computer from which the control is performed and implements the following functionality:
• providing a graphical interface for the administrator;
• making requests for information about running processes on a managed PC;
• sorting processes depending on the amount of RAM used by them with the possibility of changing the amount of memory allowed for use;
• sending a request to close or restart the process selected by the administrator to the managed PC and receiving a message about the completion or non-execution of the requested action;
• the ability to add or remove information to the list of administrated PCs.
On both sides, both service and client, to ensure the functioning of the modules, there must be a dll-file of the class library, which implements the main classes and methods used for the operation of both modules. TaskKillerDLL library includes classes:
• to save and retrieve information stored in files;
• to process the structuring of process information;
• for interacting with processes on a PC, namely: obtaining information about them, their completion or restart;
• to describe the data models with which the software package works;
• for the formation of http-requests from the client to the service.
To ensure the functioning of the program, the administrator computer and the managed PC must be on the same local network and remote access to a specific port is configured on each of them.
