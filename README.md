<p align="center">
    <img src="https://raw.githubusercontent.com/DogOSdev/DogOSdev/main/img/Logo_700.png" alt="A shiba inu pixel art dog displaying in a old VGA monitor.">
</p>
<h1 align="center">DogOS</h1>
<p align="center">A operating system made with Cosmos and in C#.</p>

## Table of Contents
- [What is DogOS?](#what-is-dogos)
- [Features](#features)
- [Screenshots](#screenshots)
- [Building](#building)
- [Running Release](#running-release)
- [Additional Info](#additional-info)

***!!!WARNING!!!***

***DogOS should not be ran on physical hardware. If you try to, you risk the loss of your personal files or even your PC. YOU HAVE BEEN WARNED***

## What is DogOS?

DogOS will be a fully-featured operating system made in Cosmos(OS) and .NET 2.0 C#. It is in heavy development right now, and there is barely anything added so far. Star the repository to get updates on development, or join the [Discord](https://discord.gg/3N2HPf4bZe) server.

## Features

DogOS currently features the following:
- A working shell
- User accounts
- Text Editor
- Shell scripts
- File system
- SHA256 hashing

Check the repositories [Project's](https://github.com/DogOSdev/DogOS/projects) tab to check what needs to be done.

## Screenshots

### v0.0.1

![screenshot_1](https://raw.githubusercontent.com/DogOSdev/DogOSdev/main/screenshots/0.0.1/screenshot_1.png)
![screenshot_2](https://raw.githubusercontent.com/DogOSdev/DogOSdev/main/screenshots/0.0.1/screenshot_2.png)
![screenshot_3](https://raw.githubusercontent.com/DogOSdev/DogOSdev/main/screenshots/0.0.1/screenshot_3.png)
![screenshot_4](https://raw.githubusercontent.com/DogOSdev/DogOSdev/main/screenshots/0.0.1/screenshot_4.png)
![screenshot_5](https://raw.githubusercontent.com/DogOSdev/DogOSdev/main/screenshots/0.0.1/screenshot_5.png)
![screenshot_6](https://raw.githubusercontent.com/DogOSdev/DogOSdev/main/screenshots/0.0.1/screenshot_6.png)

## Building

You can build DogOS on your own, and help development if you want. There are multiple steps to building.

1. Install the COSMOS Dev Kit. The instructions are [here](https://www.gocosmos.org/docs/install/).
1. Once installed, clone the repository and open the project in Visual Studio 2019.
1. If needed, open the project's Properties and change the profile. If you are using VMware, make sure to change the edition to your edition. It is currently at Workstation.
1. Run the project, and it should open and boot!

## Running Release

To run a release, please follow these steps.

1. Download the ISO and the Filesystem.
1. Create a new virtual machine with the ISO mounted, but **make sure to use the virtual disk that was downloaded**. DogOS does not work right without it.
1. Run the virtual machine!

## Additional Info

DogOS has only been tested on VMware Workstation 16. If you test it on a different virtual machine, please [open a issue](https://github.com/DogOSdev/DogOS/issues/new). Use this prefix on the title: `[README]`. This helps me make sure that DogOS works on other platforms.

Thank you for checking out DogOS.
